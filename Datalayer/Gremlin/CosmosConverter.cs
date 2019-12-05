using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.ComponentModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Datalayer.Gremlin
{
	class CosmosConverter : JsonConverter
	{
		public CosmosConverter()
		{
		}

		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			throw new NotImplementedException();
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType == JsonToken.Null)
				return default;

			//create object
			existingValue = Activator.CreateInstance(objectType);

			//parse json
			while (reader.Read())
			{
				if (reader.TokenType == JsonToken.PropertyName)
				{
					if (reader.Value.ToString() == "id")   //get and set id
					{
						reader.Read();        //move to id value
						PropertyInfo pi = objectType.GetProperty("Id");
						pi.SetValue(existingValue, TypeDescriptor.GetConverter(pi.PropertyType).ConvertFrom(reader.Value));
					}
					else if (reader.Value.ToString() == "properties") // read the properties aka the entities values
					{
						existingValue = ReadObject(reader, existingValue);
					}
				}
			}
			return existingValue;			
		}

		private object ReadValue(JsonReader reader, object targetObject)
		{
			switch (reader.TokenType)
			{
				case JsonToken.StartObject:
					return ReadObject(reader, targetObject);
				case JsonToken.StartArray:
					return ReadArray(reader, targetObject);
				case JsonToken.Integer:
				case JsonToken.Float:
				case JsonToken.String:
				case JsonToken.Boolean:
				case JsonToken.Undefined:
				case JsonToken.Null:
				case JsonToken.Date:
				case JsonToken.Bytes:
					return reader.Value;
				default:
					throw new JsonSerializationException("Invalid token while converting");
			}
		}
		private object ReadArray(JsonReader reader, object targetObject)
		{
			List<object> list = new List<object>();
			while (reader.Read())
			{
				switch (reader.TokenType)
				{
					case JsonToken.EndArray:
						if (list.Count == 1)
							return list[0];
						else
							return list;
					case JsonToken.Comment:
						break;
					default:
						list.Add(ReadValue(reader, targetObject));
						break;
				}
			}
			throw new JsonSerializationException("Error Reading Array");
		}

		private object ReadObject(JsonReader reader, object targetObject)
		{
			while (reader.Read())
			{
				switch (reader.TokenType)
				{
					case JsonToken.PropertyName:
						if (reader.Value.ToString() == "id")       //skip id properties
						{
							reader.Read();
							continue;
						}
						if (reader.Value.ToString() == "value")
						{
							reader.Read();
							targetObject = ReadValue(reader, null);
						}
						else
						{
							string propertyName = reader.Value.ToString();

							if (!reader.Read())
								throw new JsonSerializationException("Unexpected End of Json");

							object propObj = null;
							//Get the property
							PropertyInfo pi = targetObject.GetType().GetProperty(propertyName);
							if (pi.PropertyType.IsValueType)
								propObj = Activator.CreateInstance(pi.PropertyType);

							object value = ReadValue(reader, propObj);    //read the value
							
							//set the property
							if (value.GetType() == pi.PropertyType)
								pi.SetValue(targetObject, value);
							else
								pi.SetValue(targetObject, TypeDescriptor.GetConverter(pi.PropertyType).ConvertFrom(value));							
						}
						break;
					case JsonToken.Comment:
						break;
					case JsonToken.EndObject:
						return targetObject;
				}
			}

			throw new JsonSerializationException("Unexpected end of json object");
		}

		public override bool CanConvert(Type objectType)
		{
			return true;
		}
	}
}
