using System;
using System.Collections.Generic;
using System.Text;

namespace Datalayer.Structures
{
	public class Email : IEntity, ISecurityStamp
	{
		public string Id { get; set; }

		public string EmailAddress { get; set; }

		public bool EmailConfirmed { get; set; }

		public string NormalizedEmailAddress { get; set; }

		public string SecurityStamp { get; set; }

		public DateTime CreatedDate { get; set; }
	}
}
