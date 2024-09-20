namespace ECommerce.API.Seller.V1.Models;

public record ProductDto(long Id, string Name, string Description, decimal Price, decimal Weight, string Category);