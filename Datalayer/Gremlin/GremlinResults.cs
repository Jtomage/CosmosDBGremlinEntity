using System;
using System.Collections.Generic;
using System.Linq;
using Gremlin.Net.Driver;
using Newtonsoft.Json;

namespace Datalayer.Gremlin
{
	public class GremlinResults<T> 
	{
		public long StatusCode { get; }

		public List<T> Results { get; }

		public GremlinResults(ResultSet<dynamic> resultSet)
		{
			if (resultSet == null)
				throw new ArgumentNullException(nameof(resultSet));

			StatusCode = (long)resultSet.StatusAttributes["x-ms-status-code"];

			if (resultSet.Count > 1)
			 Results = resultSet.Select<dynamic, T>(token => JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(token), new CosmosConverter())).ToList();
		}
	}
}
