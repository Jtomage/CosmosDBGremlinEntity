# CosmosDBGremlinEntity
Prototyping the Gremlin API with Cosmos DB.

## Introduction
Prototyping how to use Cosmos DB graph capabilties the Gremlin API. As of right now the Cosmos DB does not implement the Gremlin Fluent API or have result to entity mapping. Therefore all gremlinqueries are done as strings and a custom JSONConvertor was created for mapping the query results to each entity to save on writing multiple similar redundant code. May do test to see if an attribute version has better performance.
May need to build specific parsers for the results for complex queries.

Currently using a console app used to call the data layer. Still working on getting the Test projectup and running

# Getting started
Create an appsettings.json file with an JSON object CosmosDBConfig with the same property names as the CosmosDBConfig class. 

## Changes

Made the GremlinClient a static object wrapped in the GremlinClientFactory class to use just one GremlinClient object and to reuse that across your application because the client uses a connection pool so the same connection can be used again for different requests instead of having to create and tear down one connection for each request. 

## Future updates
- [X] Add Edge Mapping
- [ ] ~~Uploading photos as Vertices~~
	- There is a 2mb size limit. Compression would need to be added making this more troublesome as a tractional database. Instead will try Azure Storage
- [ ] Picture uploading / management with Azure storage blob
	- started
- [ ] Get Testing project running


## Possible updates
- [X] Gremlin Response Wrapper. For the other variables sent back like status code
	- After looking at more complex queries mapping in the results in the repositories would be better.
- [ ] Test attribute model for performance
- [ ] Adding Versioning to Entities
	- Possible problems when needing to modify entities in the future
- [X] Adding label property for entities
- [X] Adding type property for entities
- [ ] Add a web api 
