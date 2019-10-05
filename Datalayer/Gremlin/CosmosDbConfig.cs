using System;
using System.Collections.Generic;
using System.Text;

namespace Datalayer.Gremlin
{
	public class CosmosDBConfig
	{
		public string HostName { get; set; }

		public int Port { get; set; }

		public string AuthorityKey { get; set; }

		public string DatabaseName { get; set; }

		public string CollectionName { get; set; }

		public string GenerateUserName()
		{
			return String.Format("/dbs/{0}/colls/{1}", DatabaseName, CollectionName);
		}
	}
}
