using System;
using Newtonsoft.Json;

namespace Datalayer.Structures
{
  [JsonObject]
  public class UserProperties : IProperties
  {
    [JsonProperty(".Version[0].value")]
    public string Version { get; set; }

    [JsonProperty(".FirstName[0].value")]
    public string FirstName { get; set; }

    [JsonProperty(".LastName[0].value")]
    public string LastName { get; set; }

    [JsonProperty(".SecurityStamp[0].value")]
    public string SecurityStamp { get; set; }

    [JsonProperty(".CreatedDate[0].value")]
    public DateTime CreatedDate { get; set; }

  }
}
