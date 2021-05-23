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
    public interface ISortMethod
    {
        Task<List<Product>> Sort();

    }

    public class AscendingSort : ISortMethod
    {
        private IApiResource apiResource;
        public AscendingSort(IApiResource apiResource)
        {
            this.apiResource = apiResource;
        }
        public async Task<List<Product>> Sort()
        {
            var products = await apiResource.GetProducts();
            return  products.OrderBy(c => c.name).ToList();
        }
    }
    
    public class DecendingSort : ISortMethod
    {
        private IApiResource apiResource;
        public DecendingSort(IApiResource apiResource)
        {
            this.apiResource = apiResource;
        }
        public async Task<List<Product>> Sort()
        {
            var products = await apiResource.GetProducts();
            return products.OrderByDescending(c => c.name).ToList();
        }
    }
    
    public class LowToHighSort : ISortMethod
    {
        private IApiResource apiResource;
        public LowToHighSort(IApiResource apiResource)
        {
            this.apiResource = apiResource;
        }
        public async Task<List<Product>> Sort()
        {
            var products = await apiResource.GetProducts();
            return products.OrderBy(c => c.price).ToList();
        }
    }
    
    public class HighToLowSort : ISortMethod
    {
        private IApiResource apiResource;
        public HighToLowSort(IApiResource apiResource)
        {
            this.apiResource = apiResource;
        }
        public async Task<List<Product>> Sort()
        {
            var products = await apiResource.GetProducts();
            return products.OrderByDescending(c => c.price).ToList();
        }
    }

    public class RecommendedSort : ISortMethod
    {
        private IApiResource apiResource;

        public RecommendedSort(IApiResource apiResource)
        {
            this.apiResource = apiResource;
        }

        public async Task<List<Product>> Sort()
        {
            var products = await apiResource.GetProducts();
            var shoppingHistories = await apiResource.GetProductHistory();
            if (shoppingHistories.Count > 0)
            {
                var popularity = shoppingHistories.SelectMany(c => c.products).GroupBy(c => new {c.name, c.price})
                    .Select(p => new Product
                    {
                        name = p.Key.name,
                        price = p.Key.price,
                        quantity = p.Sum(c => c.quantity)
                    }).OrderByDescending(c => c.quantity).ToList();


                var productNameWithoutHistory = products.Select(c => c.name).Except(popularity.Select(c => c.name));
                var productWithoutHistory = products.Where(c => productNameWithoutHistory.Contains(c.name));
                popularity.AddRange(productWithoutHistory);
                return popularity;
            }

            return products;
        }
    }
}