using MediatR;
using SellerService.Models;
using SharedService.Interfaces;
using SharedService.Models;

namespace SellerService.Queries;

public record GetProductQuery(long ProductId) : IRequest<GetProductResponse>;

public class GetProductHandler(IProductRepository productRepository) : IRequestHandler<GetProductQuery, GetProductResponse>
{
    public async Task<GetProductResponse> Handle(GetProductQuery request, CancellationToken cancellationToken)
    {
        var productFromDb = await productRepository.GetProductByIdAsync(request.ProductId).ConfigureAwait(false);
        if (productFromDb is Product product)
        {
            return new GetProductResponse(product);
        }

        throw new Exception("Database entity couldn't be mapped to Product.");
    }
}