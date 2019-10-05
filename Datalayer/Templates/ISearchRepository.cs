using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Datalayer.Templates
{
	public interface ISearchRepository
	{
		Task BuildSearch<T>(T searchCriteria);

		Task<TResult> ExecuteSearch<TResult>();

	}
}