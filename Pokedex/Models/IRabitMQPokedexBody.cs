using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pokedex.Models
{
    public class IRabitMQPokedexBody<T>
    {
        public string Operation { get; set; }

        public T Body { get; set; }
    }
}
