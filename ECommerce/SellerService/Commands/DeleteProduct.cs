using MediatR;
using SellerService.Models;
using SharedService.Interfaces;

namespace SellerService.Commands;

public record DeleteProductCommand(long ProductId) : IRequest<DeleteProductResponse>;

public class DeleteProductHandler(IProductRepository productRepository) : IRequestHandler<DeleteProductCommand, DeleteProductResponse>
{
    public async Task<DeleteProductResponse> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        await productRepository.DeleteProductAsync(request.ProductId).ConfigureAwait(false);
        return new DeleteProductResponse(true);
    }
}