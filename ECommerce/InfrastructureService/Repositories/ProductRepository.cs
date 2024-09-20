using SharedService.Interfaces;
using SharedService.Models;

namespace InfrastructureService.Repositories;

public class ProductRepository(IDatabase<Product> productDatabase) : IProductRepository
{
    public async Task<Product?> GetProductByIdAsync(long id)
    {
        return await productDatabase.Read(id).ConfigureAwait(false);
    }

    public async Task<long> AddProductAsync(Product product)
    {
        return await productDatabase.Create(product).ConfigureAwait(false);
    }

    public async Task<IEnumerable<Product>> GetAllProductsAsync()
    {
        return await productDatabase.GetAll().ConfigureAwait(false);
    }
    
    public async Task<bool> DeleteProductAsync(long id)
    {
        await productDatabase.Delete(id).ConfigureAwait(false);
        return true;
    }
    
    public async Task<Product> UpdateProductAsync(Product product)
    {
        await productDatabase.Update(product).ConfigureAwait(false);
        return product;
    }
}