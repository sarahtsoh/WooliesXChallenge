using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using WooliesChallenge.Models;
using WooliesChallenge.Models.Configuration;
using WooliesChallenge.Service.ApiResources;
using System.Linq;
namespace WooliesChallenge.Service
{
    public interface IApiService
    {
        Task<List<Product>> GetSortedProductsAsync(SortOption.Option sortOption);
        Task<decimal> GetTrolleyTotal(ShoppingCart shoppingCart);
    }

    public class ApiService : IApiService
    {
        private ILogger<ApiService> logger;
        private IApiResource apiResource;
        private ISortMethodFactory sortMethodFactory;

        public ApiService(IApiResource apiResource,  ISortMethodFactory sortMethodFactory, ILogger<ApiService> logger)
        {
            this.apiResource = apiResource;
            this.sortMethodFactory = sortMethodFactory;
            this.logger = logger;
        }


        public async Task<List<Product>> GetSortedProductsAsync(SortOption.Option sortOption)
        {
            var sortMethod = sortMethodFactory.CreateSortMethod(sortOption);

            var result = await sortMethod.Sort();

            return result;

        }
        
        public async Task<decimal> GetTrolleyTotal(ShoppingCart shoppingCart)
        {
            var payload = JsonConvert.SerializeObject(shoppingCart);
            var total = await apiResource.PostShoppingCartAsync(payload);
            return total;
            
        } 

    }

  

}
