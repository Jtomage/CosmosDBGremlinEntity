using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Datalayer.Structures;
using Gremlin.Net.Driver;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Datalayer.Gremlin
{
	public class GremlinResults<T> 
	{
		public long StatusCode { get; }

		public IReadOnlyCollection<T> Results { get; }

		public GremlinResults(ResultSet<JToken> resultSet)
		{
			if (resultSet == null)
				throw new ArgumentNullException(nameof(resultSet));

			StatusCode = (long)resultSet.StatusAttributes["x-ms-status-code"];

			var output = JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(resultSet));
			//Results = resultSet.Select(token => token.ToObject<T>()).ToList();
		}
	}
}
