using System;
using Newtonsoft.Json;
using Datalayer.Gremlin;

namespace Datalayer.Structures
{
  public class Vertex<T> where T : IProperties
  {
    public Guid Id { get; set; }

    public string Label { get; set; }

    [JsonConverter(typeof(GremlinPropertiesConverter))]
    public T Properties { get; set; }
  }
}
