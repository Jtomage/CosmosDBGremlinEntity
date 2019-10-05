using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Datalayer.Structures;

namespace Datalayer.Templates
{
	public interface IEmailRepository
	{
		Task<EmailVertex> CreateEmail(EmailVertex email);

		Task<EmailVertex> GetById(string id);

		Task<EmailVertex> UpdateEmail(EmailVertex email);

		Task<bool> DeleteUser(EmailVertex email);
	}
}
