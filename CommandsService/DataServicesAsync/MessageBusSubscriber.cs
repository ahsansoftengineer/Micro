
using System.Text;
using CommandsService.EventProcessing;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
// using RabbitMQ.Client.Events;

namespace CommandsService.DataServicesAsync
{
  public class MessageBusSubscriber : BackgroundService
  {
    private readonly IConfiguration _config;
    private readonly IEventProcessor _eventProcessor;
    private IConnection? _con;
    private IModel? _channel;
    private string? _queueName;

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
        Port = Convert.ToInt32(_config["RabbitMQPort"]),
      };
      try
      {
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

        // Or you can create Private Function
        _con.ConnectionShutdown += (sender, args) =>
        {
          Console.WriteLine("--> Connection Shutdown");
        };

      }
      catch (Exception ex)
      {
        Console.WriteLine($"--> Error Occured while Creating a Connection: {ex.Message} -- {ex.StackTrace}");
      }


    }
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
      try
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
      }
      catch (Exception ex)
      {
        Console.WriteLine($"Failed to Execute Async {ex.Message}");
      }

      return Task.CompletedTask;
    }
    public override void Dispose()
    {
      if (_channel != null && _con != null && _channel.IsOpen)
      {
        _channel.Close();
        _con.Close();
      }
      base.Dispose();
    }
  }
}