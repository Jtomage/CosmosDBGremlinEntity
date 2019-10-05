using System;
using System.Collections.Generic;
using System.Text;
using Datalayer.Templates;
using Gremlin.Net.Driver;
using Gremlin.Net.Structure.IO.GraphSON;

namespace Datalayer.Gremlin
{
	public class GremlinClientFactory : IClientFactory<GremlinClient>
	{
		protected GremlinServer server;

		public GremlinClientFactory(CosmosDBConfig config)
		{
			server = new GremlinServer(config.HostName, config.Port,
																 enableSsl: true,
																 username: config.GenerateUserName(),
																 password: config.AuthorityKey);
		}

		public GremlinClient GetClient()
		{
			
			return new GremlinClient(server, new GraphSON2Reader(), new GraphSON2Writer(), GremlinClient.GraphSON2MimeType);
		}
	}
}
