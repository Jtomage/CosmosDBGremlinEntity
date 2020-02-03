using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Gremlin.Net.Driver;
using Datalayer.Structures;
using Newtonsoft.Json;

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
        catch (Exception)
        {
          throw;
        }
      }
    }

    public void ParseTest()
    {
      string result = "[\r\n  {\r\n    \"id\": \"5dcf700e-3b92-4914-ad12-3dc9dd605e74\",\r\n" +
                      "    \"label\": \"User\",\r\n    \"type\": \"vertex\",\r\n    \"properties\": {\r\n      " +
                      "\"Version\": [\r\n        {\r\n          \"id\": \"2d55f549-bea2-40a3-8b1e-afd2b233024d\",\r\n          " +
                      "\"value\": \"1\"\r\n        }\r\n      ],\r\n      \"FirstName\": [\r\n        {\r\n          " +
                      "\"id\": \"63b19a8f-ddc1-4675-b0f2-bb6a96b928c2\",\r\n          \"value\": \"Baron\"\r\n        }\r\n      " +
                      "],\r\n      \"LastName\": [\r\n        {\r\n          \"id\": \"30d9389e-cd33-4c5c-b558-faedea7fe198\",\r\n          " +
                      "\"value\": \"Bolt\"\r\n        }\r\n      ],\r\n      \"SecurityStamp\": [\r\n        {\r\n         " +
                      " \"id\": \"2e5d4db5-7df4-49f5-931d-bbf8bcf469ab\",\r\n          \"value\": \"Stamp\"\r\n        }\r\n      ],\r\n      " +
                      "\"CreatedDate\": [\r\n        {\r\n          \"id\": \"21041b66-a0a2-4e4f-93f5-1d25ae9d9d7c\",\r\n          " +
                      "\"value\": \"2020-02-02T10:40:40.1851216Z\"\r\n        }\r\n      ]\r\n    }\r\n  }\r\n]";

      var userReturn = JsonConvert.DeserializeObject<List<Vertex<UserProperties>>>(result);
    }
  }
}