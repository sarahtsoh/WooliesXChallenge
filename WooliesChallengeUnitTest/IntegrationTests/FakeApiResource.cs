using System.Collections.Generic;
using System.Threading.Tasks;
using WooliesChallenge.Models;
using WooliesChallenge.Service.ApiResources;

namespace WooliesChallengeUnitTest.IntegrationTests
{
    public class FakeApiResource:IApiResource
    {
        public async Task<List<Product>> GetProducts()
        {
            return await Task.FromResult(TestData.GetProduct());
            
        }

        public async Task<List<ShopperHistory>> GetProductHistory()
        {
            return await Task.FromResult(TestData.GetCustomerHistory());
        }

        public async  Task<decimal> PostShoppingCartAsync(string payload)
        {
            return await Task.FromResult(5m);
        }
    }
}