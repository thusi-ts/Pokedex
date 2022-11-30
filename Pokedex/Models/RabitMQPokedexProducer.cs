using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace Pokedex.Models
{
    public class RabitMQPokedexProducer : IRabitMQPokedexProducer
    {

        public RabitMQPokedexProducer()
        {
        }
        public void SendProductMessage<T>(T message)
        {
            var factory = new ConnectionFactory
            {
                HostName = "localhost"
            };

            using var connection = factory.CreateConnection(); 
            using var channel = connection.CreateModel(); //using uses to clean up

            channel.ExchangeDeclare(exchange: "pubsub", type: ExchangeType.Fanout);

            var json = JsonConvert.SerializeObject(message);
            var body = Encoding.UTF8.GetBytes(json);

            channel.BasicPublish(exchange: "pubsub", "", null, body: body);
        }
    }
}
