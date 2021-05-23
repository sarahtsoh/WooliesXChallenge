The Webapi is written in .NetCore 3.1
In WooliesChallenge.sln, it has 2 projects: 
(1)WooliesChallenge
(2)WooliesChallengeUnitTest (Also include integrationt testing)

In WooliesController:
public Test GetUser() is for /api/Exercise/exercise1

public async Task<List<Product>> GetSortedProducts([FromQuery] string sortOption) is /api/Exercise/exercise2

public async Task<decimal> GetTrolleyTotal([FromBody] ShoppingCart shoppingCart) is /api/Exercise/exercise3


The solution is structure in the following way
1.The Program.cs indicated it is using console.log as log provider for logger
2.Startup.cs is using httpfactory to create typedclient to send/post request to WooliesApi in ApiResource
3.Startup.cs has extra feature to enforce cors policy (but for the sake of this challenge, it allow
any origin to request Webapi service)
4.Startup.cs uses globalError handler, custom middleware, ExceptionMiddleware,  in its pipe line to catch all exception for request/response,
in any non-prod environment

Workflow:
1.Request hit the controller
2.then the controller function will call the ApiService (call SortMethod factory determine sorting mechanism)
and ApiResourceService
3.ApiResourceService will be injected with httpclient to call Woolies Resource endpoint
4.After receiving the result data from Woolies resource endpoint, then it will forward the data back to the 
controller and message statuscode 200 if all sucessful

Unit testing:
1.I have used variety of framework to do unit testing
2.Generally Unit testing is done in NUnit framework, but need to used Moq to mock httpclient and ILogger<> to unit test ApiResourceService
3.Add integration testing by using Xunit, to create fakeWebhost and injected with ApiResource to test wooliesController URL

The service is deployed to Azure webapp
https://woolieschallenge20210523164712.azurewebsites.net/index.html   -- for swagger

https://woolieschallenge20210523164712.azurewebsites.net/api/user
https://woolieschallenge20210523164712.azurewebsites.net/api/sort?sortoption=low
https://woolieschallenge20210523164712.azurewebsites.net/api/trolleyTotal

please note for Woolies exercise3, the expert problem, I need more time to complete it
but the logic is around what combination of product is in the Trolley and it tries to match the special (combination
of product with name and price)
depends on quantity, the quantity from trolledy - special quantity = remaining quantity
special total + remaining quantity*price  = total
