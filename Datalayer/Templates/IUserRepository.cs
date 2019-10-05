using System;
using System.Collections.Generic;
using System.Text;
using Datalayer.Structures;
using System.Threading.Tasks;

namespace Datalayer.Templates
{
	public interface IUserRepository
	{
		Task<UserVertex> CreateUser(UserVertex user);

		Task<UserVertex> GetById(string id);

		Task<UserVertex> UpdateUser(UserVertex user);

		Task DeleteUser(UserVertex user);

	}
}
