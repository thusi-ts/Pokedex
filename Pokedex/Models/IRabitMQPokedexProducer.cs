using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pokedex.Models
{
    public interface IRabitMQPokedexProducer
    {
        public void SendProductMessage<T>(T message);
    }
}
