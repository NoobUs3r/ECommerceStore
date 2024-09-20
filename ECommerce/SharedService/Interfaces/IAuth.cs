using SharedService.Models;

namespace SharedService.Interfaces;

public interface IAuthService
{
    Task<string> RegisterUser(string username, string password);
    Task<AuthenticateUserResponse> AuthenticateUser(string username, string password);
}