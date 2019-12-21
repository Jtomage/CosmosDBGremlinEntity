using System;
using System.Collections.Generic;
using System.Text;

namespace Datalayer.Gremlin
{
	public class StorageAccountConfig
	{

		public string ConnectionString { get; set; }

		public string ContainerName { get; set; }

		public string UploadPath { get; set; }
	}
}
