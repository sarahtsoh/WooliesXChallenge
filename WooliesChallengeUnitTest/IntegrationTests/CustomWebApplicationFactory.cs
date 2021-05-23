using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using WooliesChallenge.Service.ApiResources;

namespace WooliesChallengeUnitTest.IntegrationTests
{
    public class CustomWebApplicationFactory<TStartup>
        : WebApplicationFactory<TStartup> where TStartup : class
    {
        //example base on https://docs.microsoft.com/en-us/aspnet/core/test/integration-tests?view=aspnetcore-5.0
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                         typeof(IApiResource));

                services.Remove(descriptor);

                services.AddScoped<IApiResource, FakeApiResource>();
            });
        }
    }

}