using ECommerce.API.Seller.V1.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SellerService.Models;
using SharedService.Models;

namespace ECommerce.API.Seller.V1.Controllers;

[ApiController]
[Authorize]
[Route("api/v1/seller/products")]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
[ProducesResponseType(StatusCodes.Status500InternalServerError)]
public class ProductController(SellerService.SellerService sellerService) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(AddProductResponseDto), StatusCodes.Status200OK)]
    public async Task<AddProductResponseDto> AddProduct(AddProductRequest request)
    {
        var addProductResponse = await sellerService.AddProduct(
            new Product(request.Product.Id,
                        request.Product.Name,
                        request.Product.Description,
                        request.Product.Price,
                        request.Product.Weight,
                        request.Product.Category)).ConfigureAwait(false);
        return new AddProductResponseDto(addProductResponse.IsAddedSuccessfully, addProductResponse.ProductId);
    }

    [HttpGet("{productId}")]
    [ProducesResponseType(typeof(GetProductResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<GetProductResponseDto> GetProduct(long productId)
    {
        var getProductResponse = await sellerService.GetProduct(productId).ConfigureAwait(false);
        return new GetProductResponseDto(new ProductDto(getProductResponse.Product.Id,
                                                        getProductResponse.Product.Name,
                                                        getProductResponse.Product.Description,
                                                        getProductResponse.Product.Price,
                                                        getProductResponse.Product.Weight,
                                                        getProductResponse.Product.Category));
    }

    [HttpPut]
    [ProducesResponseType(typeof(UpdateProductResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<UpdateProductResponseDto> UpdateProduct(ProductDto product)
    {
        var updateProductResponse = await sellerService.UpdateProduct(new Product(product.Id,
                                                                                  product.Name,
                                                                                  product.Description,
                                                                                  product.Price,
                                                                                  product.Weight,
                                                                                  product.Category)).ConfigureAwait(false);
        return new UpdateProductResponseDto(updateProductResponse.IsUpdatedSuccessfully, new ProductDto(updateProductResponse.Product.Id,
                                                                                                        updateProductResponse.Product.Name,
                                                                                                        updateProductResponse.Product.Description,
                                                                                                        updateProductResponse.Product.Price,
                                                                                                        updateProductResponse.Product.Weight,
                                                                                                        updateProductResponse.Product.Category));
    }

    [HttpDelete("productId")]
    [ProducesResponseType(typeof(DeleteProductResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<DeleteProductResponseDto> DeleteProduct(long productId)
    {
        var deleteProductResponse = await sellerService.DeleteProduct(productId).ConfigureAwait(false);
        return new DeleteProductResponseDto(deleteProductResponse.IsProductDeleted);
    }
}