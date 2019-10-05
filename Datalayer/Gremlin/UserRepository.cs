using System;
using System.Collections.Generic;
using System.Text;
using Datalayer.Templates;
using Datalayer.Structures;
using System.Threading.Tasks;
using Gremlin.Net.Driver;
using Gremlin.Net.Driver.Exceptions;
using Gremlin.Net.Structure.IO.GraphSON;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Datalayer.Gremlin
{
	public class UserRepository : RespositoryBase, IUserRepository
	{
		public UserRepository(GremlinClientFactory factory)
			: base(factory)
		{
		}

		public Task<UserVertex> CreateUser(UserVertex user)
		{
			//Build the query since cosmos db does NOT support the fluent api https://github.com/Azure/azure-cosmos-dotnet-v2/issues/439
			StringBuilder sb = new StringBuilder("g");
			sb.Append(".addV('User')");
			sb.Append(".property('FirstName', firstName)");
			sb.Append(".property('LastName', lastName)");
			sb.Append(".property('SecurityStamp', securityStamp)");
			sb.Append(".property('CreatedDate', createdDate)");
			string query = sb.ToString();

			//Add all the values / arguments to avoid 
			//injection attacks https://www.azurefromthetrenches.com/avoiding-gremlin-injection-attacks-with-azure-cosmos-db/
			Dictionary<string, object> arguments = new Dictionary<string, object>
			{
				{ "firstName", user.FirstName },
				{ "lastName", user.LastName },
				{ "securityStamp", user.SecurityStamp },
				{ "createdDate", DateTime.UtcNow }
			};

			using (GremlinClient gremlinClient = factory.GetClient())
			{
				user = SubmitAsync<UserVertex>(gremlinClient, query, arguments).Result;
			}

			return Task.FromResult(user);
		} 

		public Task DeleteUser(UserVertex user)
		{
			if (String.IsNullOrWhiteSpace(user.Id)|| user.Id == Guid.Empty.ToString())
				throw new ArgumentNullException("user.id");

			string query = "g.V().has('User', 'id', id).drop()";

			Dictionary<string, object> args = new Dictionary<string, object>
			{
				{ "id", user.Id }
			};
			using (GremlinClient client = factory.GetClient())
			{
				//var resultSet = ExecuteQuery(client, query, values);
				SubmitAsync<UserVertex>(client, query, args);
			}
			return Task.CompletedTask;
		}

		public Task<UserVertex> GetById(string id)
		{
			if (String.IsNullOrWhiteSpace(id) || id == Guid.Empty.ToString())
				throw new ArgumentNullException("id");

			string query = "g.V().has('User', 'id', id)";
			Dictionary<string, object> arguments = new Dictionary<string, object>
			{
				{ "id", id }
			};
			using (GremlinClient client = factory.GetClient())
			{
				var resultSet = ExecuteQuery(client, query, arguments);
			}
			throw new NotImplementedException();
		}

		public Task<UserVertex> UpdateUser(UserVertex user)
		{
			if (user == null || user.Id == Guid.Empty.ToString())
				throw new ArgumentNullException("user");
			StringBuilder sb = new StringBuilder("g.V()");
			sb.Append(".has('User', 'id', id");
			sb.Append(".property('FirstName', firstName)");
			sb.Append(".property('LastName', lastName)");
			sb.Append(".property('SecurityStamp', securityStamp)");
			sb.Append(".property('CreatedDate', createdDate)");

			string query = sb.ToString();

			Dictionary<string, object> arguments = new Dictionary<string, object>
			{
				{"id", user.Id },
				{ "firstName", user.FirstName },
				{ "lastName", user.LastName },
				{ "securityStamp", user.SecurityStamp },
				{ "createdDate", DateTime.UtcNow }
			};

			using (GremlinClient client = factory.GetClient())
			{
				var resultSet = ExecuteQuery(client, query, arguments);
			}

			throw new NotImplementedException();
		}

	}
}
