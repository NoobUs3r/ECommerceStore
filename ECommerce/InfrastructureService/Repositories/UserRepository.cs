using SharedService.Interfaces;
using SharedService.Models;

namespace InfrastructureService.Repositories;

public class UserRepository(IDatabase<User> userDatabase) : IUserRepository
{
    public async Task<bool> UserExistsAsync(string username)
    {
        var users = await userDatabase.GetAll();
        return users.Any(u => u.Username == username);
    }

    public async Task AddUserAsync(User user)
    {
        await userDatabase.Create(user);
    }

    public async Task<User?> GetUserAsync(string username)
    {
        var users = await userDatabase.GetAll();
        return users.FirstOrDefault(u => u.Username == username);
    }
}