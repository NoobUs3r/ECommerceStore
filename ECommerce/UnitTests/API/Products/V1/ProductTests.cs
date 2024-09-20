using System.Text;
using System.Text.Json;
using UnitTests.Shared;

namespace UnitTests.API.Products.V1;

[TestClass]
public class ProductTests
{
    private static HttpClient _client;

    [ClassInitialize]
    public static void Setup(TestContext context)
    {
        _client = new HttpClient
        {
            BaseAddress = new System.Uri("https://localhost:5001/")
        };
        
        _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJNaXNoYSIsImp0aSI6IjE0MTk1MGJhLTgyNmYtNDdhNC04ZWQ2LWQzNDQ1NjhiNTJhYSIsImV4cCI6MTcyNjY5MTk4NCwiaXNzIjoiTWlzaGEiLCJhdWQiOiJFdmVyeW9uZSJ9.-62p8s2ExrZBIqTD6HOYqDBtl2GXhV4xiriU5-0k5QA");
    }

    [TestMethod]
    public async Task Should_Add_Product_Using_Anonymous_Type()
    {
        var requestUri = "api/v1/seller/products";
        var jsonPayload = FileReader.ReadEmbededJson("UnitTests.API.Products.V1.Payloads.AddProduct.json");
        var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

        var response = await _client.PostAsync(requestUri, content);
        Assert.IsTrue(response.IsSuccessStatusCode, "API POST request was not successful.");

        var responseBody = await response.Content.ReadAsStringAsync();
        Assert.IsNotNull(responseBody, "Response content is null.");

        var anonymousType = new { IsAddedSuccessfully = false, ProductId = 0L };
        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            PropertyNameCaseInsensitive = true
        };

        var addProductResponse = JsonSerializer.Deserialize(responseBody, anonymousType.GetType(), options);

        var isAddedSuccessfully = (bool)addProductResponse?.GetType().GetProperty("IsAddedSuccessfully").GetValue(addProductResponse);
        var productId = (long)addProductResponse?.GetType().GetProperty("ProductId").GetValue(addProductResponse);

        Assert.IsTrue(isAddedSuccessfully, "The product was not added.");
        Assert.IsTrue(productId == 0, "Invalid ProductId.");
    }

    [ClassCleanup]
    public static void Cleanup()
    {
        _client.Dispose();
    }
}