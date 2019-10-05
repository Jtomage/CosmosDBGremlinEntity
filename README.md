# CosmosDBGremlinEntity
Prototyping the Gremlin API with Cosmos DB. Trying a Entity Framework POCO Repository style pattern

## Introduction
Prototyping how to use Cosmos DB graph capabilties the Gremlin API. As of right now the Cosmos DB does not implement the Gremlin Fluent API or have result to entity mapping. Therefore all gremlinqueries are done as strings and a custom JSONConvertor was created for mapping the query results to each entity.

Currently using a console app used to call the data layer. Still working on getting the Test projectup and running

# Getting started
Create an appsettings.json file with an JSON object CosmosDBConfig with the same property names as the CosmosDBConfig class. 

## Issues
* The Gremlin Client must be generated and passed in as an argument before executing the query
I m not sure why this needs to be done. It might be that I running through a console and unit test
* Unit test is not currently configure to run

## Future updates
- [ ] Add Email Entity / Vertex
- [ ] Add User-Email Edge aka Ownership Edges
- [ ] Uploading photos as Vertices 
- [ ] Get Testing project running
- [ ] Add Gremlin Query Builder
	- [ ] Add Search Repository
- [ ] Add a web api 

## POssible updates
- [ ] Gremlin Response Wrapper. For the other variables sent back like status code
- [ ] Adding Versioning to Entities
	- Possible problems when needing to modify entities in the future
- [ ] Adding label property for entities