https://woolieschallenge20210523164712.azurewebsites.net/index.html

The Webapi is written in .NetCore 3.1
In WooliesChallenge.sln, it has 2 projects: 
	(1)WooliesChallenge
	(2)WooliesChallengeUnitTest

In WooliesController:
public Test GetUser() is for /api/Exercise/exercise1

public async Task<List<Product>> GetSortedProducts([FromQuery] string sortOption) is /api/Exercise/exercise2

public async Task<decimal> GetTrolleyTotal([FromBody] ShoppingCart shoppingCart) is /api/Exercise/exercise3


The solution is structure in the following way
1.Request hit the controller
2.then the controller function will call the service, ResourceServiceApi
3.ResourceServiceApi call the httpclient with clienthandler to call Woolies Resource endpoint
4.After receiving the result data from Woolies resource endpoint, then it will forward the data back to the 
controller and message statuscode 200 if all sucessful
5.The httpclient is wrapped in the ResourceHttpClient class to allow mocking the httpclient function,
so that ResourceServiceApi logic can be unit tested
6.Unit testing is done in NUnit framework, it is to assert if the logic in the ResourceServiceApi
of processing the data returned from Woolies is done correctly

Add integration testing

The service is deployed to Azure webapp
https://woolieschallenge20210308001519.azurewebsites.net/api/user
https://woolieschallenge20210308001519.azurewebsites.net/api/sort?sortoption=low
https://woolieschallenge20210308001519.azurewebsites.net/api/trolleyTotal

please note for Woolies exercise3, the expert problem, I need more time to complete it
but the logic is around what combination of product is in the Trolley and it tries to match the special (combination
of product with name and price)
depends on quantity, the quantity from trolledy - special quantity = remaining quantity
special total + remaining quantity*price  = total
