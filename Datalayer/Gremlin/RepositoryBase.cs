using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Gremlin.Net.Driver;
using Gremlin.Net.Driver.Exceptions;
using Newtonsoft.Json;

namespace Datalayer.Gremlin
{
	public abstract class RepositoryBase
	{

		protected Task<List<T>> SubmitAsync<T>(string query, Dictionary<string, object> parameters = null) 
		{
			try
			{
				using (GremlinClient gremlinClient = GremlinClientFactory.GetClient())
				{
					var response = gremlinClient.SubmitAsync<dynamic>(query, parameters).Result;
					List<T> results = response.Select<dynamic, T>(token => JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(token), new CosmosConverter())).ToList();
					return Task.FromResult(results);
				}
			} 
			catch(ResponseException)
			{
				throw;
			}
		}

		protected Task<T> SubmitSingleAsync<T>(string query, Dictionary<string, object> parameters = null)
		{
			try
			{
				using (GremlinClient gremlinClient = GremlinClientFactory.GetClient())
				{
					var response = gremlinClient.SubmitWithSingleResultAsync<dynamic>(query, parameters).Result;
					T result = JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(response), new CosmosConverter());
					return Task.FromResult(result);
				}
			}
			catch (ResponseException)
			{
				throw;
			}
		}

		protected Task SubmitNoResponseAsync(string query, Dictionary<string, object> parameters = null)
		{
			try
			{
				using (GremlinClient gremlinClient = GremlinClientFactory.GetClient())
				{
					var result = gremlinClient.SubmitAsync<dynamic>(query, parameters).Result;
					return Task.CompletedTask;
				}
			}
			catch(ResponseException)
			{
				throw;
			}
		}

		protected Task<ResultSet<dynamic>> ExecuteQuery(string query, Dictionary<string, object> arguments = null)
		{
			try
			{
				using (GremlinClient gremlinClient = GremlinClientFactory.GetClient())
				{
					return gremlinClient.SubmitAsync<dynamic>(query, arguments);
				}
			}
			catch (ResponseException)
			{
				//Console.WriteLine("\tRequest Error!");

				// Print the Gremlin status code.
				//Console.WriteLine($"\tStatusCode: {e.StatusCode}");

				// On error, ResponseException.StatusAttributes will include the common StatusAttributes for successful requests, as well as
				// additional attributes for retry handling and diagnostics.
				// These include:
				//  x-ms-retry-after-ms         : The number of milliseconds to wait to retry the operation after an initial operation was throttled. This will be populated when
				//                              : attribute 'x-ms-status-code' returns 429.
				//  x-ms-activity-id            : Represents a unique identifier for the operation. Commonly used for troubleshooting purposes.
				//PrintStatusAttributes(e.StatusAttributes);
				//Console.WriteLine($"\t[\"x-ms-retry-after-ms\"] : { GetValueAsString(e.StatusAttributes, "x-ms-retry-after-ms")}");
				//Console.WriteLine($"\t[\"x-ms-activity-id\"] : { GetValueAsString(e.StatusAttributes, "x-ms-activity-id")}");

				throw;
			}
		}

		protected async Task<string> ExecuteQueryAndSerialize(string query, Dictionary<string, object> arguments = null)
		{
			try
			{
				using (GremlinClient gremlinClient = GremlinClientFactory.GetClient())
				{
					var resultset = await gremlinClient.SubmitAsync<dynamic>(query, arguments);
					if (resultset.Count > 0)
					{
						string temp = JsonConvert.SerializeObject(resultset);
						return temp;
					}
					else
						return null;
				}
			}
			catch (ResponseException)
			{
				throw;
			}
		}
	}
}
