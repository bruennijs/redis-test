using System;
using System.IO;
using log4net;
using Newtonsoft.Json;
using Redis.test.Serializer;
using StackExchange.Redis;

namespace Redis.test
{
  public class AggregateRepository
  {
    private static string ObjectHashField = "object";

    private static ILog Log = LogManager.GetLogger(typeof(AggregateRepository));

    public void Insert(Smgw gateway)
    {
      using (var factory = new RedisConnectionFactory())
      {
        IDatabase connection = factory.GetConnection();

        string key = CreateKey(gateway);

        //Console.WriteLine(connection.Multiplexer.IsConnected);
        string objSerialized = new AggregateSerializer().Serialize(gateway);

        //Console.WriteLine($"Inserting '{key}->{objSerialized}'");
        Log.Info($"Inserting '{key}->{objSerialized}'");

        connection.HashSet(key, ObjectHashField, objSerialized, When.Always, CommandFlags.FireAndForget);
      }
    }

    public Smgw GetById(string id)
    {
    	using (var factory = new RedisConnectionFactory())
        {
          IDatabase connection = factory.GetConnection();

              //Console.WriteLine(connection.Multiplexer.IsConnected);


          string key = CreateKey(id);
          RedisValue value = connection.HashGet(key, ObjectHashField);

          if (!value.HasValue)
            throw new ArgumentException($"could not get hash bey key '{key}'");

          return new AggregateSerializer().Deserialize<Smgw>(value);
        }
    }

    private string CreateKey(Smgw gateway)
    {
      return CreateKey(gateway.Id);
    }

        private string CreateKey(string id)
        {
          return string.Format("gws:{0}", id);
        }
  }
}