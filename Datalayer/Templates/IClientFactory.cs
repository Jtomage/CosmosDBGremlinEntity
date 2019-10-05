using System;
using System.Collections.Generic;
using System.Text;

namespace Datalayer.Templates
{
	public interface IClientFactory<T>
	{
		T GetClient();
	}
}
