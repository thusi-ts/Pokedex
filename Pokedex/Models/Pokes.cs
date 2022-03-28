using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pokedex.Models
{
    public class Pokes
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string[] Type { get; set; }

        public Translations Translations { get; set; }

        public Bases Base { get; set; }
    }
}
