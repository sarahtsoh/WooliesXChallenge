
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using WooliesChallenge.Models;
using WooliesChallenge.Models.Configuration;

namespace WooliesChallenge.Service.ApiResources
{
    public interface IApiResource
    {
        Task<List<Product>> GetProducts();
        Task<List<ShopperHistory>> GetProductHistory();
        Task<decimal> PostShoppingCartAsync(string payload);
    }

    public class ApiResource : IApiResource
    {
        private readonly HttpClient client;
        private readonly ILogger<ApiResource> logger;
        private readonly string token;
        private readonly ResourceOptions resourceOptions;

        public ApiResource(HttpClient client, IOptions<ResourceOptions> resourceOption, ILogger<ApiResource> logger)
        {
            this.logger = logger;
            this.client = client;
            this.resourceOptions = resourceOption.Value;
            token = this.resourceOptions.token;
        }
        
        public async Task<List<Product>> GetProducts()
        {
            var response = await client.GetAsync(@$"api/resource/products?token={token}");
            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<Product>>(responseBody);
                return result;
            }

            logger.LogError("GetProducts status code" + response.StatusCode);
            
            throw new HttpRequestException("GetProducts Api is failed to getproducts");
                

        }
        
        public async Task<List<ShopperHistory>> GetProductHistory()
        {
            var response = await client.GetAsync(@$"api/resource/shopperHistory?token={token}");
            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<ShopperHistory>>(responseBody);
                return result;
            }

            logger.LogError("GetProductHistory status code" + response.StatusCode);
            throw new HttpRequestException("GetProductHistory is failed to get product history");
            
        }
        
          public async Task<decimal> PostShoppingCartAsync(string payload) 
          {
          
                var content = new StringContent(payload, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(@$"api/resource/trolleyCalculator?token={token}", content);

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<decimal>(responseBody);
                    return result;
                }
                
                logger.LogError("PostShoppingCartAsync status code" + response.StatusCode);
               
                throw new HttpRequestException("PostShoppingCartAsync failed to post ShoppingCart payload");

        }

        
      
    }
}