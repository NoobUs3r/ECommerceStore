using SharedService.Models;

namespace SellerService.Models;

public record UpdateProductResponse(bool IsUpdatedSuccessfully, Product Product);