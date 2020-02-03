using System;
using System.Collections.Generic;
using System.Text;

namespace Datalayer.Structures
{
  /// <summary>
  /// Represents the properties in the CosmosDB query
  /// </summary>
  public interface IProperties
  {

    /// <summary>
    /// In case the entity get updated 
    /// </summary>
    string Version { get; set; }
  }
}
