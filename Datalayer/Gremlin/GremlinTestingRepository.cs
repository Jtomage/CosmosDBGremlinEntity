using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Gremlin.Net.Driver;
using Datalayer.Structures;

namespace Datalayer.Gremlin
{
	public class GremlinTestingRepository : RepositoryBase
	{
		public Task<List<User>> MultipleResultsTest()
		{
			StringBuilder sb = new StringBuilder("g");
			sb.Append(".V().hasLabel('User')");
			string query = sb.ToString();

			var results = SubmitAsync<User>(query);
			return results;
		}

		public async Task<ResultSet<dynamic>> GremlinTest()
		{
			string id = "fc9440f5-7a3e-4f30-b6df-3ba0f27e312a";
			string query = "g.V().has('User', 'id', id)";
			Dictionary<string, object> arguments = new Dictionary<string, object>
			{
				{ "id", id }
			};
			var result = await ExecuteTest(query, arguments);
			return result;
		}

		private async Task<ResultSet<dynamic>> ExecuteTest(string query, Dictionary<string, object> parameters = null)
		{
			using (GremlinClient client = GremlinClientFactory.GetClient())
			{
				try
				{
					var result = await client.SubmitAsync<dynamic>(query, parameters);
					return result;
				}
				catch(Exception)
				{
					throw;
				}
			}
		}
	}
}
