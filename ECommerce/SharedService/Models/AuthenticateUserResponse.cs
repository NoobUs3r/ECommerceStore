namespace SharedService.Models;

public record AuthenticateUserResponse(string Jwt, bool IsAuthenticated);