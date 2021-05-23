using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using Shouldly;
using FluentAssertions;
using WooliesChallenge.Service;
using WooliesChallenge.Service.ApiResources;

namespace WooliesChallengeUnitTest
{
    public class SortMethodUnitTest
    {
        private IApiResource apiResource;
        
        [SetUp]
        public void Setup()
        { 
            apiResource = Substitute.For<IApiResource>();

        }
        
        [Test]
        public async Task AscendingSort_Sort_ShouldReturnProductNameInAscendingOrder()
        {
            //arrange
            apiResource.GetProducts().Returns(TestData.GetProduct());
            var target = new AscendingSort(apiResource);
            //act
            var result = await target.Sort();

            //assert
            result.Should().BeInAscendingOrder(c => c.name);
        }
        
        [Test]
        public async Task DecendingSort_Sort_ShouldReturnProductNameInDecendingOrder()
        {
            //arrange
            apiResource.GetProducts().Returns(TestData.GetProduct());
            var target = new DecendingSort(apiResource);
            //act
            var result = await target.Sort();

            //assert
            result.Should().BeInDescendingOrder(c => c.name);
        }
        
        [Test]
        public async Task LowToHighSort_Sort_ShouldReturnProductPriceInAscendingOrder()
        {
            //arrange
            apiResource.GetProducts().Returns(TestData.GetProduct());
            var target = new LowToHighSort(apiResource);
            //act
            var result = await target.Sort();

            //assert
            result.Should().BeInAscendingOrder(c => c.price);
        }
        
        [Test]
        public async Task HighToLowSort_Sort_ShouldReturnProductPriceInDecendingOrder()
        {
            //arrange
            apiResource.GetProducts().Returns(TestData.GetProduct());
            var target = new HighToLowSort(apiResource);
            //act
            var result = await target.Sort();

            //assert
            result.Should().BeInDescendingOrder(c => c.price);
        }
        
        [Test]
        public async Task RecommendedSort_Sort_ShouldReturnAllProductQuantityInDescendingOrder()
        {
            //arrange
            apiResource.GetProducts().Returns(TestData.GetProduct());
            apiResource.GetProductHistory().Returns(TestData.GetCustomerHistory());
            var target = new RecommendedSort(apiResource);
          
            //act
            var result = await target.Sort();

            //assert
            result.Should().ShouldSatisfyAllConditions(
                () => result.Count.ShouldBe(3),
                () => result.Should().BeInDescendingOrder(c => c.quantity),
                () => result.Single(c => c.name == "Test Product A").quantity.ShouldBe(12),
                () => result.Single(c => c.name == "Test Product B").quantity.ShouldBe(16),
                () => result.Single(c => c.name == "Test Product C").quantity.ShouldBe(0)
            );
        }
        
    }
}