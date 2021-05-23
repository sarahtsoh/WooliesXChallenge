using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Win32.SafeHandles;
using Newtonsoft.Json;
using Shouldly;
using WooliesChallenge.Models;
using Xunit;


namespace WooliesChallengeUnitTest.IntegrationTests
{
    public class WooliesControllerTest:IClassFixture<CustomWebApplicationFactory<WooliesChallenge.Startup>>
    {
        private readonly CustomWebApplicationFactory<WooliesChallenge.Startup> factory;

        public WooliesControllerTest(CustomWebApplicationFactory<WooliesChallenge.Startup> factory)
        {
            this.factory = factory;
        }

        [Fact]
        public async Task GetUser_ReturnsSpecifiedUser()
        {
            // Arrange
            var url = "/api/user";
            var client = factory.CreateClient();

            // Act
            var response = await client.GetAsync(url);
            var responseBody = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Test>(responseBody);
          
            // Assert
            response.EnsureSuccessStatusCode();
            result.name.ShouldBe("Sarah Tsoh");
            
        }
        
        [Theory]
        [InlineData("/api/sort?sortOption=low")]
        [InlineData("/api/sort?sortOption=High")]
        [InlineData("/api/sort?sortOption=Ascending")]
        [InlineData("/api/sort?sortOption=Descending")]
        public async Task Test(string url)
        {
            // Arrange
            
            var client = factory.CreateClient();

            // Act
            var response = await client.GetAsync(url);
            var responseBody = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<Product>>(responseBody);

            // Assert
            response.EnsureSuccessStatusCode();
            result.Count.ShouldBe(3);
        }
        
        [Fact]
        public async Task TrolleyTotal()
        {
            var client = factory.CreateClient();
            var url = "/api/trolleyTotal";

            var shoppingCart = new ShoppingCart()
            {
                products = new List<RegularPriceProduct>(),
                quantities = new List<ProductQuantity>(),
                specials = new List<SpecialPriceProductDeal>()
            };

            var jsonContent = JsonConvert.SerializeObject(shoppingCart);
              
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            // Act
            var response = await client.PostAsync(url, content);
            var responseBody = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<decimal>(responseBody);

            // Assert
            response.EnsureSuccessStatusCode();
        
            result.ShouldBe(5m);
        }


        
    }
}