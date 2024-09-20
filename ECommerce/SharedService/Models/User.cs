using SharedService.Interfaces;

namespace SharedService.Models;

public record User(long Id, string Username, string PasswordHash) : IDatabaseEntity;