using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Pokedex.Models
{
    public class Bases
    {
        [JsonPropertyName("HP")]
        public int HP { get; set; }

        [JsonPropertyName("Attack")]
        public int Attack { get; set; }

        [JsonPropertyName("Defense")]
        public int Defense { get; set; }

        [JsonPropertyName("Sp. Attack")]
        public int Spattack { get; set; }

        [JsonPropertyName("Sp. Defense")]
        public int Spdefense { get; set; }

        [JsonPropertyName("Speed")]
        public int Speed { get; set; }
    }
}
