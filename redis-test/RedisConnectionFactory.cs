using System;
using System.Net;
using NUnit.Framework.Constraints;
using StackExchange.Redis;

namespace Redis.test
{
  public class RedisConnectionFactory : IDisposable
  {
    public IDatabase GetConnection()
    {
      if (ConnectionMultiplexer == null)
      {
        ConnectionMultiplexer = ConnectionMultiplexer.Connect(new ConfigurationOptions()
        {
          AbortOnConnectFail = false,
          DefaultDatabase = 2,
          ConnectRetry = 3,
          Ssl = false,
          EndPoints =
          {
            new IPEndPoint(IPAddress.Parse("172.17.0.2"), 6379)
          }
        });
      }

      if (!ConnectionMultiplexer.IsConnected)
        throw new Exception("Not connected");

      return ConnectionMultiplexer.GetDatabase(0);
      //return ConnectionMultiplexer.GetServer(new IPEndPoint(IPAddress.Parse("172.17.0.2"), 6379)).Multiplexer.GetDatabase(0);
    }

    private ConnectionMultiplexer ConnectionMultiplexer;

    public void Dispose()
    {
      ConnectionMultiplexer?.Dispose();
      ConnectionMultiplexer = null;
    }
  }
}