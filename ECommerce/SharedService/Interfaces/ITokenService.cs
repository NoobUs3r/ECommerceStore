namespace SharedService.Interfaces;

public interface ITokenService
{
    string GenerateJwtToken(string username);
}