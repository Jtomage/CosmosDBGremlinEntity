using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Datalayer.Gremlin;
using Datalayer.Structures;

namespace Datalayer.Test.Gremlin
{
	[TestClass]
	public class UserRepoClientTest
	{

		private readonly UserRepository userRepo;

		public UserRepoClientTest()
		{
			//userRepo = new UserRepository(new GremlinClientFactory(config));
		}

		[TestMethod]
		public async Task UserRepoCreateTest()
		{

			User user = new User()
			{
				FirstName = "Aaron",
				LastName = "Aardvark",
				SecurityStamp = "Stamp",
				CreatedDate = DateTime.UtcNow
			};
			try
			{
				var x = await userRepo.CreateUser(user);
				Assert.IsTrue(true);
			}
			catch(Exception)
			{
				throw;
			}
		}
	}
}
