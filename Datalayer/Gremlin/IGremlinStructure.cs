using System;
using System.Collections.Generic;
using System.Text;

namespace Datalayer.Gremlin
{
	/// <summary>
	/// For creating Graph Database Specific structures. 
	/// To see if can use attributes and reflection to build queries
	/// </summary>
	public interface IGremlinStructure
	{
		string Type { get; }

		string Label { get; }
	}
}
