using System;
using System.IO;
using Datalayer.Structures;
using Datalayer.Gremlin;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace DatalayerConsole.Test
{
	/// <summary>
	/// Sample program that shows how to get started with the Graph (Gremlin) APIs for Azure Cosmos DB using the open-source connector Gremlin.Net
	/// </summary>
	class Program
	{
		static void Main(string[] args)
		{
			//Add build Configuration
			var configuration = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json", false)
				.Build();

			IConfigurationSection cosmosDbconfig = configuration.GetSection("CosmosDBConfig");

			CosmosDBConfig config = new CosmosDBConfig()
			{
				HostName = cosmosDbconfig["HostName"],
				Port = Convert.ToInt32(cosmosDbconfig["Port"]),
				AuthorityKey = cosmosDbconfig["AuthorityKey"],
				DatabaseName = cosmosDbconfig["DatabaseName"],
				CollectionName = cosmosDbconfig["CollectionName"]
			};
			GremlinClientFactory factory = new GremlinClientFactory(config);

			UserVertex user = new UserVertex()
			{
				FirstName = "Aaron",
				LastName = "Aardvark",
				SecurityStamp = "Stamp",
				CreatedDate = DateTime.UtcNow
			};
		
			UserRepository ur = new UserRepository(factory);
			user = ur.CreateUser(user).Result;
			ur.DeleteUser(user);
		}
	}
}
