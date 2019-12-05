using System;
using System.Collections.Generic;
using System.Text;
using Datalayer.Structures;
using System.Threading.Tasks;

namespace Datalayer.Templates
{
	public interface IUserRepository
	{
		Task<User> CreateUser(User user);

		Task<User> GetById(string id);

		Task<User> UpdateUser(User user);

		Task DeleteUser(User user);

	}
}
