using MediatR;
using SellerService.Commands;
using SellerService.Models;
using SellerService.Queries;
using SharedService.Models;

namespace SellerService;

public class SellerService(IMediator mediator)
{
    public async Task<AddProductResponse> AddProduct(Product product)
    {
        return await mediator.Send(new AddProductCommand(product)).ConfigureAwait(false);
    }

    public async Task<GetProductResponse> GetProduct(long productId)
    {
        return await mediator.Send(new GetProductQuery(productId)).ConfigureAwait(false);
    }

    public async Task<UpdateProductResponse> UpdateProduct(Product product)
    {
        return await mediator.Send(new UpdateProductCommand(product)).ConfigureAwait(false);
    }

    public async Task<DeleteProductResponse> DeleteProduct(long productId)
    {
        return await mediator.Send(new DeleteProductCommand(productId)).ConfigureAwait(false);
    }
}