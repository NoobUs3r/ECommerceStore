using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using SharedService.Interfaces;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(SellerService.SellerService).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(BuyerService.BuyerService).Assembly);
});

builder.Services.AddControllers();
builder.Services.AddHealthChecks();

// Read JWT settings from appsettings.json
var jwtSettings = builder.Configuration.GetSection("Jwt");
var jwtKey = jwtSettings["Key"];
var jwtIssuer = jwtSettings["Issuer"];
var jwtAudience = jwtSettings["Audience"];

builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtIssuer,
            ValidAudience = jwtAudience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
        };
    });

builder.Services.AddAuthorization();

builder.Services.AddSingleton<IInfrastructureFactory, InfrastructureFactory.InfrastructureFactory>();
builder.Services.AddScoped<IUserRepository>(serviceProvider =>
{
    var factory = serviceProvider.GetService<IInfrastructureFactory>();
    return factory.CreateUserRepository();
});

builder.Services.AddScoped<IProductRepository>(serviceProvider =>
{
    var factory = serviceProvider.GetService<IInfrastructureFactory>();
    return factory.CreateProductRepository();
});

builder.Services.AddScoped<IAuthService>(serviceProvider => 
{
    var factory = serviceProvider.GetService<IInfrastructureFactory>();

    // Pass the repository into the CreateAuth method
    return factory.CreateAuth(jwtKey, jwtIssuer, jwtAudience);
});

builder.Services.AddScoped<SellerService.SellerService>();
builder.Services.AddScoped<BuyerService.BuyerService>();

var app = builder.Build();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.MapHealthChecks("/health");

app.Run();
