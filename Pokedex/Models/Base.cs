using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Pokedex.Models
{
    public class Base
    {
        [Key]
        [JsonPropertyName("pokeId")]
        public long PokeId { get; set; }

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

        public virtual Poke Poke { get; set; }
    }
}
