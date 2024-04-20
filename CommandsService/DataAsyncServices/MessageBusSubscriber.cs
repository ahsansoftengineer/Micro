
using CommandsService.EventProcessing;
using Microsoft.AspNetCore.Connections;
using RabbitMQ.Client;

namespace CommandsService.AsyncDataServices
{
  public class MessageBusSubscriber : BackgroundService
  {
    private readonly IConfiguration _config;
    private readonly IEventProcessor _eventProcessor;
    private IConnection _con;
    private IModel _channel;
    private string _queueName;

    public MessageBusSubscriber(
IConfiguration config,
IEventProcessor eventProcessor
)
    {
      _config = config;
      _eventProcessor = eventProcessor;
      InitializeRabbitMQ();
    }
    private void InitializeRabbitMQ()
    {
      var factory = new ConnectionFactory()
      {
        HostName = _config["RabbitMQHost"],
        Port = int.Parse(_config["RabbitMQPort"]),
      };
      _con = factory.CreateConnection();
      _channel = _con.CreateModel();
      _channel.ExchangeDeclare(
        exchange: "trigger",
        type: ExchangeType.Fanout
      );
      _queueName = _channel.QueueDeclare().QueueName;
      _channel.QueueBind(
        queue: _queueName,
        exchange: "trigger",
        routingKey: ""
      );
      Console.WriteLine("--> Listening on the Message Bus...");

      // Or you can create Private Msg
      _con.ConnectionShutdown += (sender, args) =>
      {
        Console.WriteLine("--> Connection Shutdown");
      };

    }
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
      throw new NotImplementedException();
    }
    public override void Dispose()
    {
      if (_channel.IsOpen)
      {
        _channel.Close();
        _con.Close();
      }
      base.Dispose();
    }
  }
}