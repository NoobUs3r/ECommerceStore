using MediatR;
using SellerService.Models;
using SharedService.Interfaces;
using SharedService.Models;

namespace SellerService.Commands;

public record AddProductCommand(Product Product) : IRequest<AddProductResponse>;

public class AddProductCommandHandler(IProductRepository productRepository) : IRequestHandler<AddProductCommand, AddProductResponse>
{
    public async Task<AddProductResponse> Handle(AddProductCommand request, CancellationToken cancellationToken)
    {
        var createdProductId = await productRepository.AddProductAsync(request.Product).ConfigureAwait(false);
        return await Task.FromResult(new AddProductResponse(true, createdProductId));
    }
}