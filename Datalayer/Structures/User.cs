using System;
using System.Collections.Generic;
using System.Text;

namespace Datalayer.Structures

{
	public class User : IEntity, ISecurityStamp
	{
		public string Id { get; set; }

		public string FirstName { get; set; }

		public string LastName { get; set; }

		public string SecurityStamp { get; set; }

		public DateTime CreatedDate { get; set; }
	}
}
