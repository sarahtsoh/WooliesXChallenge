using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using Shouldly;
using FluentAssertions;
using Microsoft.Extensions.Logging.Abstractions;
using Newtonsoft.Json;
using WooliesChallenge.Models;
using WooliesChallenge.Service;
using WooliesChallenge.Service.ApiResources;


namespace WooliesChallengeUnitTest
{
    public class ApiServiceTest
    {
        private ISortMethodFactory sortMethodFactory;
        private IApiResource apiResource;
        private ApiService apiService;
        private ISortMethod sortMethod;

        [SetUp]
        public void Setup()
        { 
            apiResource = Substitute.For<IApiResource>();
            sortMethodFactory =  Substitute.For<ISortMethodFactory>();
            sortMethod = Substitute.For<ISortMethod>();
            apiService = new ApiService(apiResource, sortMethodFactory, new NullLogger<ApiService>() );
        }
        
        [Test]
        public async Task GetSortedProducts_ShouldReturn_Products()
        {
            //arrange
            var option = SortOption.Option.Ascending;
            sortMethodFactory.CreateSortMethod(Arg.Any<SortOption.Option>()).Returns(sortMethod);
            sortMethod.Sort().Returns(Task.FromResult(TestData.GetProduct()));
            //act
            var result = await apiService.GetSortedProductsAsync(option);
            //assert
            result.ShouldBeOfType(typeof(List<Product>));
            
        }
        
        [Test]
        public async Task  GetTrolleyTestTotal_ShouldReturn_TotalAmount()
        {
            //arrange
            var shoppingCart = new ShoppingCart();
            apiResource.PostShoppingCartAsync(JsonConvert.SerializeObject(shoppingCart)).Returns(10);
            //act
            var result = await apiService.GetTrolleyTotal(shoppingCart);
            //assert
            result.ShouldBeOfType(typeof(decimal));

        }
    }
}