using System;
using System.IO;
using Newtonsoft.Json;

namespace Redis.test.Serializer
{
  public class AggregateSerializer
  {
    private static JsonSerializer _serializer = new JsonSerializer();

    public string Serialize<T>(T obj)
    {
              using (var sw = new StringWriter())
              {
                _serializer.Serialize(sw, obj);
                Console.WriteLine(sw.ToString());
                return sw.ToString();
              }
    }

    public T Deserialize<T>(string value)
    {
      using (StringReader stringReader = new StringReader(value))
        using (JsonReader jsonReader = new JsonTextReader(stringReader))
      		return _serializer.Deserialize<T>(jsonReader);
    }
  }
}