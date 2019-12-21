# CosmosDBGremlinEntity
Prototyping Azure features

## Introduction
Prototyping a few of Azure features 
1. Cosmos DB graph capabilties the Gremlin API. 
2. Azure Storage, Blobs and possibly BlockBlobs

## Synopsis 
Currently there are 2 issues 

1. Cosmos DB does not implement the Gremlin Fluent API

There is nothing that can be done about this until Microsoft makes a change. All Gremlin queries are done as strings

2. Database Results do not have a mapper to an entity / model / class

Since not all the gremlin api has been implemented the automapping shown in some of the Gremlin API documents does not work. All results are returned as basically JSON. A custom JSONConvertor was created for mapping the query results to each entity. It uses reflection but it might be better to use an attribute version. Test will need to be done or wait until Cosmos DB makes the updates on their end. it should be noted that a custom mapper maybe needed for more complex queries.

Currently using a console app used to call the data layer. Still working on getting the Test projectup and running

# Getting started
Create an appsettings.json file with 2 JSON objects CosmosDBConfig and StorageAccountConfig with the same property names as the CosmosDBConfig and StorageAccountConfig class. 

## Updates

Made the GremlinClient a static object wrapped in the GremlinClientFactory class to use just one GremlinClient object and to reuse that across your application because the client uses a connection pool so the same connection can be used again for different requests instead of having to create and tear down one connection for each request. 

After finding the 2mb size limit for the Cosmos DB, switched over to Azure Storage Blobs. There is a 128 mb size limit for the blob. Based on the documentation it might be possible to upload more but no clue on how it possible. When trying upload a 250 mb file, it DID NOT throw a size limit exception but a Task was Cancelled, with no other information. Best guess it was due to timeout. If a block blob storage type is used it might be possible to upload TB worth, will need to test. 

## Future updates
- [X] Add Edge Mapping
- [X] Gremlin Response Wrapper. For the other variables sent back like status code. Not sure if needed
- [X] Picture uploading / management with Azure storage blob
- [X] Adding label property for entities
- [X] Adding type property for entities
- [ ] Get Testing project running


## Possible updates
- [ ] Test attribute model for performance
- [ ] Adding Versioning to Entities
	- To account for furture changes
- [ ] Add a web api 
