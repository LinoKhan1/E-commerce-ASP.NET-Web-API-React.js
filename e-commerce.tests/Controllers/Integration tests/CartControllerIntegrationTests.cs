using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using e_commerce.Server.DTOs;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace e_commerce.IntegrationTests
{
    public class CartControllerIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public CartControllerIntegrationTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GetCartItems_ReturnsSuccessStatusCode()
        {
            // Arrange
            var client = _factory.CreateClient();
            // Simulate authentication if needed (generate a JWT token or set a test user)
            // Example assumes JWT token, adjust as per your actual authentication mechanism
            var token = "your_generated_jwt_token_here";

            // Set authorization header with the token
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            // Make the request
            var response = await client.GetAsync("/api/cart");

            // Assert
            response.EnsureSuccessStatusCode(); // This will throw if the response is not successful (2xx)

        }

        [Fact]
        public async Task AddCart_ReturnsSuccessStatusCode()
        {
            // Arrange
            var client = _factory.CreateClient();
            var addToCartDto = new AddToCartDTO { /* Initialize with test data */ };
            var content = new StringContent(JsonSerializer.Serialize(addToCartDto), System.Text.Encoding.UTF8, "application/json");

            // Act
            var response = await client.PostAsync("/api/cart", content);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
        }

        [Fact]
        public async Task UpdateCartItem_ReturnsSuccessStatusCode()
        {
            // Arrange
            var client = _factory.CreateClient();
            var id = 1; // Replace with existing cart item ID for update
            var quantity = 2; // Replace with new quantity

            // Act
            var response = await client.PutAsync($"/api/cart/{id}", new StringContent(quantity.ToString()));

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
        }

        [Fact]
        public async Task RemoveCartItem_ReturnsSuccessStatusCode()
        {
            // Arrange
            var client = _factory.CreateClient();
            var id = 1; // Replace with existing cart item ID for removal

            // Act
            var response = await client.DeleteAsync($"/api/cart/{id}");

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
        }
    }
}
