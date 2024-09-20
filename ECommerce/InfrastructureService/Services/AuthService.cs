using SharedService.Interfaces;
using SharedService.Models;

namespace InfrastructureService.Services;

public class AuthService(IUserRepository userRepository, IPasswordHasher passwordHasher, ITokenService tokenService) : IAuthService
{
    public async Task<string> RegisterUser(string username, string password)
    {
        if (await userRepository.UserExistsAsync(username))
        {
            return "Username already exists.";
        }

        var hashedPassword = passwordHasher.HashPassword(password);
        var user = new User
        (
            -1, // TODO: ???
            username,
            hashedPassword
        );

        await userRepository.AddUserAsync(user);
        return "User registered successfully.";
    }

    public async Task<AuthenticateUserResponse> AuthenticateUser(string username, string password)
    {
        var user = await userRepository.GetUserAsync(username);
        if (user == null || !VerifyPassword(password, user.PasswordHash))
        {
            return new AuthenticateUserResponse(string.Empty, false);
        }

        var jwt = tokenService.GenerateJwtToken(username);
        return new AuthenticateUserResponse(jwt, true);
    }

    private bool VerifyPassword(string password, string storedHash)
    {
        var hashedPassword = passwordHasher.HashPassword(password);
        return hashedPassword == storedHash;
    }
}