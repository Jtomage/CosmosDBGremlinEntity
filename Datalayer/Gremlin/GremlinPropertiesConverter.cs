using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Datalayer.Gremlin
{
  public class GremlinPropertiesConverter : JsonConverter
  {

    public override bool CanWrite
    {
      get { return false; }
    }

    public override bool CanConvert(Type objectType)
    {
      return true;
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
      if (reader.TokenType == JsonToken.Null)
        return default;

      var jObject = JObject.Load(reader);                     //load the json as a json object
      var targetObj = Activator.CreateInstance(objectType);   // create the type

      //get the properties
      IEnumerable<PropertyInfo> propertyInfos = targetObj.GetType().GetProperties().Where(p => p.CanWrite && p.CanRead);

      foreach(PropertyInfo pi in propertyInfos)
      {
        //Use PropertyName to get the value using JSONPath
        //This is the current way Gremlin stores the values
        var token = jObject.SelectToken($"{pi.Name}[0].value");
        if (token != null && token.Type != JTokenType.Null)
        {
          var value = token.ToObject(pi.PropertyType, serializer);  //get the value
          pi.SetValue(targetObj, value);                            //set values to object
        }
      }

      return targetObj;
    }

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
      throw new NotImplementedException();
    }
  }
}
