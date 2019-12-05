using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Datalayer.Structures;

namespace Datalayer.Templates
{
	public interface IEmailRepository
	{
		Task<Email> CreateEmail(Email email);

		Task<Email> GetById(string id);

		Task<Email> UpdateEmail(Email email);

		Task DeleteEmail(Email email);

		Task ChangeOwner(Email email, User user);
	}
}
