using System;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using SciPaperService.Models.Dtos;

namespace SciPaperService.MessageBus
{
  public class MessageBusClient : IMessageBusClient
  {
    private readonly IConfiguration _configuration;
    private readonly IConnection _connection;
    private readonly IModel _channel;

    public MessageBusClient(IConfiguration conf)
    {
      _configuration = conf;
      var factory = new ConnectionFactory() { HostName = _configuration["RabbitMQHost"] };
      try
      {
        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();

        _channel.ExchangeDeclare(exchange: "trigger", type: ExchangeType.Fanout);

        _connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;

        Console.WriteLine("Connection to MessageBus success!");
      }
      catch (Exception ex)
      {
        Console.WriteLine("Connection to MessageBus failed.");
        Console.WriteLine(ex.Message);
      }
    }

    public void PublishNewPaper(PublishedPaperDTO dto)
    {
      var message = JsonSerializer.Serialize(dto);

      if (_connection.IsOpen)
      {
        Console.WriteLine("Sending message!");

        SendMessage(message);
      }
      else 
      {
        Console.WriteLine("Connection closed, not sending message!");
      }
    }

    public void Dispose()
    {
      Console.WriteLine("MessageBus dipsosed");
      if (_channel.IsOpen)
      {
        _channel.Close();
        _connection.Close();
      }
    }

    private void SendMessage(string message)
    {
      var body = Encoding.UTF8.GetBytes(message);

      _channel.BasicPublish(exchange: "trigger", routingKey: "", basicProperties: null, body: body);

      Console.WriteLine($"Sent message to message bus. Message: {message}");
    }

    public void RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs args)
    {
      Console.WriteLine("Connection to MessageBus is closed.");
    }

  }
}