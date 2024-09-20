namespace SharedService.Interfaces;

public interface IPasswordHasher
{
    string HashPassword(string password);
}