
using System.Text;
using CommandsService.EventProcessing;
using Microsoft.AspNetCore.Connections;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

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
      stoppingToken.ThrowIfCancellationRequested();
      var consumer = new EventingBasicConsumer(_channel);
      consumer.Received += (sender, args) =>
      {
        Console.WriteLine($"--> Event Received!");

        var body = args.Body;
        var notifyMsg = Encoding.UTF8.GetString(body.ToArray());
        _eventProcessor.ProcessEvent(notifyMsg);


      };
      _channel.BasicConsume(
          queue: _queueName,
          autoAck: true,
          consumer: consumer
        );
      return Task.CompletedTask;
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