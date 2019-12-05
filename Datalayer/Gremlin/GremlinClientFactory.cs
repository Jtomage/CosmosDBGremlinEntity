using System;
using Gremlin.Net.Driver;
using Gremlin.Net.Structure.IO.GraphSON;

namespace Datalayer.Gremlin
{
	public sealed class GremlinClientFactory
	{
		/// <summary>
		/// https://stackoverflow.com/questions/54600832/gremlinclient-management
		/// Per documentation should only have 1 client
		/// </summary>
		private static GremlinClient client;

		/// <summary>
		/// Should only be called once
		/// </summary>
		/// <param name="config"></param>
		public GremlinClientFactory(CosmosDBConfig config)
		{

			GremlinServer server = new GremlinServer(config.HostName,
																 config.Port,
																 enableSsl: true,
																 username: config.GenerateUserName(),
																 password: config.AuthorityKey);

			client = new GremlinClient(server, new GraphSON2Reader(), new GraphSON2Writer(), GremlinClient.GraphSON2MimeType);
		}

		internal static GremlinClient GetClient()
		{
			if (client == null)
				throw new InvalidOperationException("Singleton not created");
			return client;
		}
	}
}
