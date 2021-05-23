using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using NUnit.Framework;
using FluentAssertions;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using WooliesChallenge.Models;
using WooliesChallenge.Models.Configuration;
using WooliesChallenge.Service.ApiResources;
using System;

namespace WooliesChallengeUnitTest
{
    public class ApiResourceUnitTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void GetProduct_ShouldReturnProductList_ForSuccessfulResponse()
        {
            // Arrange
            var productList = new List<Product>();
            var mockFactory = new Mock<IHttpClientFactory>();
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(JsonConvert.SerializeObject(productList), System.Text.Encoding.UTF8, "text/xml")
                });

            var client = new HttpClient(mockHttpMessageHandler.Object);
            client.BaseAddress = new System.Uri("http://localhost:8090");
            mockFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);
            var logger = new Mock<ILogger<ApiResource>>();
            var resourceOption = new Mock<IOptions<ResourceOptions>>();
            resourceOption.Setup(c => c.Value).Returns(new ResourceOptions {baseUri="http://fake", token="faketoken" });
            var apiResource = new ApiResource(client , resourceOption.Object, logger.Object);

            // Act
            var result = apiResource.GetProducts().Result;

            // Assert
            result.ShouldBeEquivalentTo(productList);
         
        }

        [Test]
        public void GetProduct_ShouldReturnExcepion_ForUnSuccessfulResponse()
        {
            // Arrange
            var productList = new List<Product>();
            var mockFactory = new Mock<IHttpClientFactory>();
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.BadGateway,
                    Content = new StringContent(JsonConvert.SerializeObject(productList), System.Text.Encoding.UTF8, "text/xml")
                });

            var client = new HttpClient(mockHttpMessageHandler.Object);
            client.BaseAddress = new System.Uri("http://localhost:8090");
            mockFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);
            var logger = new Mock<ILogger<ApiResource>>();
            var _ = logger.Object;
            var resourceOption = new Mock<IOptions<ResourceOptions>>();
            resourceOption.Setup(c => c.Value).Returns(new ResourceOptions { baseUri = "http://fake", token = "faketoken" });
            var apiResource = new ApiResource(client, resourceOption.Object, logger.Object);

            // Act
            Func<Task> act = async () => { await apiResource.GetProducts(); };

            // Assert
           act.ShouldThrow<HttpRequestException>();

        }


        //public void PostShoppingCartAsync_ShouldReturnAmount_ForSuccessfulResponse()
        //for postasync
    }
}