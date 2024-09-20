using BuyerService.Models;
using MediatR;
using SharedService.Interfaces;

namespace BuyerService.Commands;

public record BuyProductCommand(long ProductId) : IRequest<BuyProductResponse>;

public class BuyProductCommandHandler(IProductRepository productRepository) : IRequestHandler<BuyProductCommand, BuyProductResponse>
{
    public async Task<BuyProductResponse> Handle(BuyProductCommand request, CancellationToken cancellationToken)
    {
        // Payment to be here
        await productRepository.DeleteProductAsync(request.ProductId).ConfigureAwait(false);
        return await Task.FromResult(new BuyProductResponse(true, request.ProductId));
    }
}