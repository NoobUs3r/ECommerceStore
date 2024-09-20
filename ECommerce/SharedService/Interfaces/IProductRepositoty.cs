using SharedService.Models;

namespace SharedService.Interfaces;

public interface IProductRepository
{
    Task<Product?> GetProductByIdAsync(long id);
    Task<long> AddProductAsync(Product product);
    Task<IEnumerable<Product>> GetAllProductsAsync();
    Task<bool> DeleteProductAsync(long id);
    Task<Product> UpdateProductAsync(Product product);
}