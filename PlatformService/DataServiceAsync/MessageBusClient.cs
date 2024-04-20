using System.Text;
using System.Text.Json;
using PlatformService.DTO;
using RabbitMQ.Client;

namespace PlatformService.DataServiceAsync
{
  public class MessageBusClient : IMessageBusClient
  {
    private readonly IConfiguration _config;
    private readonly IConnection _conn;
    private readonly IModel _channel;

    public MessageBusClient(IConfiguration config)
    {
      _config = config;
      var factory = new ConnectionFactory()
      {
        HostName = _config["RabbitMQHost"],
        Port = Convert.ToInt32(_config["RabbitMQPort"]),
      };
      try
      {
        _conn = factory.CreateConnection();
        _channel = _conn.CreateModel();

        _channel.ExchangeDeclare(
          exchange: "trigger",
          type: ExchangeType.Fanout
        );

        _conn.ConnectionShutdown += (sender, e) =>
        {
          Console.WriteLine($"-- RabbitMQ Connection Shutdown");
        };
        Console.WriteLine("--> Connected to MessageBus");

      }
      catch (Exception ex)
      {
        Console.WriteLine($"--> PlatformService Could not connect to Message Bus:  {ex.Message}");
      }

    }
    public void PublishNewPlatform(PlatformPublishedDto dto)
    {
      var message = JsonSerializer.Serialize(dto);

      if (_conn.IsOpen)
      {
        Console.WriteLine("--> RabbitMQ Connection Open, Sending Message");
        SendMessage(message);
      }
      else
      {
        Console.WriteLine("--> RabbitMQ Connections Closed, not Sending");
      }
    }
    private void SendMessage(string message)
    {
      var body = Encoding.UTF8.GetBytes(message);
      _channel.BasicPublish(
        exchange: "trigger",
        routingKey: "",
        basicProperties: null,
        body: body);

      Console.WriteLine($"--> We have send {message}");
    }
    public void Dispose()
    {
      Console.WriteLine("MessageBus Disposed");
      if (_channel.IsOpen)
      {
        _channel.Close();
        _conn.Close();
      }
    }
  }
}