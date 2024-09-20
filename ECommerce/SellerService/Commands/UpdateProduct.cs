using MediatR;
using SellerService.Models;
using SharedService.Interfaces;
using SharedService.Models;

namespace SellerService.Commands;

public record UpdateProductCommand(Product Product) : IRequest<UpdateProductResponse>;

public class UpdateProductHandler(IProductRepository productRepository) : IRequestHandler<UpdateProductCommand, UpdateProductResponse>
{
    public async Task<UpdateProductResponse> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        await productRepository.UpdateProductAsync(request.Product).ConfigureAwait(false);
        return new UpdateProductResponse(true, request.Product);
    }
}