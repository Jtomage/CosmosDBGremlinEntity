using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Datalayer.Templates;
using Datalayer.Structures;
using Gremlin.Net.Driver;


namespace Datalayer.Gremlin
{
	public class EmailRepository : RepositoryBase, IEmailRepository
	{

		public Task ChangeOwner(Email email, User user)
		{
			if (email == null)
				throw new ArgumentNullException(nameof(email));
			if (user == null)
				throw new ArgumentNullException(nameof(user));

			StringBuilder sb = new StringBuilder("g.V().has('Email', 'id', emailId).as('e')");
			sb.Append(".V().has('User', 'id', userid)");
			sb.Append(".coalesce(_.inE('Owner').where(outV().as('e'))");
			sb.Append(", addE('Owner').from('v').property('CreatedDate', createdDate))");
			string query = sb.ToString();

			Dictionary<string, object> parameters = new Dictionary<string, object>()
			{
				{"emailId", email.Id},
				{"userid", user.Id },
				{"createdDate", DateTime.UtcNow }
			};

			var result = SubmitNoResponseAsync(query, parameters);
			return result;
		}

		public Task<Email> CreateEmail(Email email)
		{
			StringBuilder sb = new StringBuilder("g");
			sb.Append(".addV('Email'");
			sb.Append(".property('EmailAddress', emailAddress)");
			sb.Append(".property('EmailConfirmed', emailConfirmed");
			sb.Append(".property('NormalizedEmailAddress', normalizedEmailAddress");
			sb.Append(".property('SecurityStamp', securityStamp)");
			sb.Append(".property('CreatedDate', createdDate)");
			string query = sb.ToString();

			Dictionary<string, object> parameters = new Dictionary<string, object>
			{
				{"emailAddress", email.EmailAddress},
				{"EmailConfirmed", email.EmailConfirmed},
				{"normalizedEmailAddress", email.EmailAddress.ToUpper()},
				{"securityStamp", email.SecurityStamp},
				{"createdDate", email.CreatedDate}
			};

			var result = SubmitSingleAsync<Email>(query, parameters);
			return result;
		}

		public Task DeleteEmail(Email email)
		{
			if (email == null)
				throw new ArgumentNullException(nameof(email));

			string query = "g.V().has('Email', 'id', id).drop()";
			Dictionary<string, object> parameters = new Dictionary<string, object>
			{
				{"id", email.Id }
			};

			return SubmitNoResponseAsync(query, parameters);
		}

		public Task<Email> GetById(string id)
		{
			if (string.IsNullOrWhiteSpace(id))
				throw new ArgumentNullException(nameof(id));

			string query = "g.V().has('Email', 'id', id)";
			Dictionary<string, object> parameters = new Dictionary<string, object>
			{
				{"id", id }
			};

			var result = SubmitSingleAsync<Email>(query, parameters);
			return result;
		}

		public Task<Email> UpdateEmail(Email email)
		{
			if (email == null || email.Id == Guid.Empty.ToString())
				throw new ArgumentNullException(nameof(email));

			StringBuilder sb = new StringBuilder("g.V()");
			sb.Append(".has('Email', 'id', id)");
			sb.Append(".property('EmailAddress', emailAddress)");
			sb.Append(".property('EmailConfirmed', emailConfirmed");
			sb.Append(".property('NormalizedEmailAddress', normalizedEmailAddress");
			sb.Append(".property('SecurityStamp', securityStamp)");
			sb.Append(".property('CreatedDate', createdDate)");
			string query = sb.ToString();

			Dictionary<string, object> parameters = new Dictionary<string, object>
			{
				{"emailAddress", email.EmailAddress},
				{"EmailConfirmed", email.EmailConfirmed},
				{"normalizedEmailAddress", email.EmailAddress.ToUpper()},
				{"securityStamp", email.SecurityStamp},
				{"createdDate", email.CreatedDate}
			};

			var result = SubmitSingleAsync<Email>(query, parameters);
			return result;
		}
	}
}
