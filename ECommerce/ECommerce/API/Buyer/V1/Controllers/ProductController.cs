using ECommerce.API.Buyer.V1.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Buyer.V1.Controllers;

[ApiController]
[Authorize]
[Route("api/v1/buyer/products")]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
[ProducesResponseType(StatusCodes.Status500InternalServerError)]
public class ProductController(BuyerService.BuyerService buyerService) : ControllerBase
{
    [HttpPost]
    public async Task<BuyProductResponseDTO> BuyProduct(long productId)
    {
        var buyProductResponse = await buyerService.BuyProduct(productId).ConfigureAwait(false);
        return new BuyProductResponseDTO(buyProductResponse.IsBoughtSuccessfully, buyProductResponse.ProductId);
    }
}