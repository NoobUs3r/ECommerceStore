using SharedService.Models;

namespace SharedService.Interfaces;

public interface IUserRepository
{
    Task<bool> UserExistsAsync(string username);
    Task AddUserAsync(User user);
    Task<User?> GetUserAsync(string username);
}