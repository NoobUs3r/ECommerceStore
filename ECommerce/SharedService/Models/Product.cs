using SharedService.Interfaces;

namespace SharedService.Models;

public record Product(long Id, string Name, string Description, decimal Price, decimal Weight, string Category) : IDatabaseEntity;