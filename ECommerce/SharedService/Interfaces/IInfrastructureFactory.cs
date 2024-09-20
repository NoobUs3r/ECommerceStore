namespace SharedService.Interfaces;

public interface IInfrastructureFactory
{
    IProductRepository CreateProductRepository();
    IUserRepository CreateUserRepository();
    IAuthService CreateAuth(string jwtKey, string jwtIssuer, string jwtAudience);
}