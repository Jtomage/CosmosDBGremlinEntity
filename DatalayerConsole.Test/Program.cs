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

			//Load the gremlin config
			CosmosDBConfig config = new CosmosDBConfig();
			configuration.GetSection("CosmosDBConfig").Bind(config);
			new GremlinClientFactory(config);

			StorageAccountConfig saConfig = new StorageAccountConfig();
			configuration.GetSection("StorageAccountConfig").Bind(saConfig);

			//RunGremlinTestingRepository();
			//TestUserRepository();
			RunStorageAccountUploadTest(saConfig);
			//RunTestOfContainerCreation(saConfig);
		}

		private static void TestUserRepository()
		{
			User user = new User()
			{
				FirstName = "Aaron",
				LastName = "Aardvark",
				SecurityStamp = "Stamp",
				CreatedDate = DateTime.UtcNow
			};

			UserRepository ur = new UserRepository();
			user = ur.CreateUser(user).Result;
			ur.DeleteUser(user);
		}

		private static void RunGremlinTestingRepository()
		{
			GremlinTestingRepository repo = new GremlinTestingRepository();
			//repo.MultipleResultsTest();
			var result = repo.GremlinTest().Result;
		}

		private static void RunStorageAccountUploadTest(StorageAccountConfig config)
		{
			PhotoRepository pr = new PhotoRepository(config.ConnectionString);
			var result = pr.UploadPhoto(config.ContainerName, config.UploadPath).Result;
		}

		private static void RunTestOfContainerCreation(StorageAccountConfig config)
		{
			PhotoRepository pr = new PhotoRepository(config.ConnectionString);
			pr.CreateContainer(config.ContainerName, config.UploadPath).Wait();
		}

	}
}
