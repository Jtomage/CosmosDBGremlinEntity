using System;
using System.Collections.Generic;
using System.Text;

namespace Datalayer.Structures
{
	public class Photo : IEntity
	{
		public string Id { get; set; }

		public string Type { get; set; }

		public byte[] Image { get; set; }

		public DateTime CreatedDate { get; set; }
	}
}
