using BuyerService.Commands;
using BuyerService.Models;
using MediatR;

namespace BuyerService;

public class BuyerService(IMediator mediator)
{
    public async Task<BuyProductResponse> BuyProduct(long productId)
    {
        return await mediator.Send(new BuyProductCommand(productId)).ConfigureAwait(false);
    }
}