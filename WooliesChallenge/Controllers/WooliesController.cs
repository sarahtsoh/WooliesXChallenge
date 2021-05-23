using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WooliesChallenge.Models;
using WooliesChallenge.Models.Configuration;
using WooliesChallenge.Service;

namespace WooliesChallenge.Controllers
{
    [EnableCors("CorsPolicy")]
    [ApiController]
    [Route("/api/")]
    public class WooliesController : ControllerBase
    {
        private readonly ILogger<WooliesController> logger;
        private readonly IApiService apiService;
        private readonly UserOptions userOption;
        private readonly ResourceOptions resourceOption;
        public WooliesController(ILogger<WooliesController> logger,  IApiService apiService, IOptions<UserOptions> userOption, IOptions<ResourceOptions> resourceOption)
        {
            this.logger = logger;
            this.apiService = apiService;
            this.userOption = userOption.Value;
            this.resourceOption = resourceOption.Value;
            
        }

        [HttpGet]
        [Route("user")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public Test GetUser()
        {
            return new Test
            {
                name = userOption.name,
                token = resourceOption.token
            };
        }

        [HttpGet]
        [Route("sort")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<Product>>> GetSortedProducts([FromQuery] string sortOption)
        {
            SortOption.Option option;
            if (Enum.TryParse(sortOption, true, out option))
                return await apiService.GetSortedProductsAsync(option);

            return BadRequest();
            
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("trolleyTotal")]
        public async Task<ActionResult<decimal>> GetTrolleyTotal([FromBody] ShoppingCart shoppingCart)
        {
            return await apiService.GetTrolleyTotal(shoppingCart) ;

        }
        
    }
}
