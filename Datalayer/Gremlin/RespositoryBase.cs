using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Gremlin.Net.Driver;
using Gremlin.Net.Driver.Exceptions;
using Newtonsoft.Json;

namespace Datalayer.Gremlin
{
	public abstract class RespositoryBase
	{
		protected GremlinClientFactory factory;

		public RespositoryBase(GremlinClientFactory gcf)
		{
			factory = gcf;
		}

		protected Task<T> SubmitAsync<T>(GremlinClient gremlinClient, string query, Dictionary<string, object> arguments = null) 
		{
			try
			{
				var result = gremlinClient.SubmitAsync<dynamic>(query, arguments).Result;
				var entity = JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(result), new CosmosConverter());
				return Task.FromResult(entity);
			} 
			catch(ResponseException)
			{
				throw;
			}
		}

		protected Task<T> SubmitSingleAsync<T>(GremlinClient gremlinClient, string query, Dictionary<string, object> arguments = null)
		{
			try
			{
				var result = gremlinClient.SubmitWithSingleResultAsync<dynamic>(query, arguments).Result;
				var entity = JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(result), new CosmosConverter());
				return Task.FromResult(entity);
			}
			catch (ResponseException)
			{
				throw;
			}
		}

		protected Task<ResultSet<dynamic>> ExecuteQuery(GremlinClient gremlinClient, string query, Dictionary<string, object> arguments = null)
		{
			try
			{
				return gremlinClient.SubmitAsync<dynamic>(query, arguments);
			}
			catch (ResponseException e)
			{
				Console.WriteLine("\tRequest Error!");

				// Print the Gremlin status code.
				Console.WriteLine($"\tStatusCode: {e.StatusCode}");

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
	}
}
