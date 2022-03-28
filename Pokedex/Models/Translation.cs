using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Pokedex.Models
{
    public class Translation
    {
        [Key]
        [JsonPropertyName("pokeId")]
        public long PokeId { get; set; }

        [JsonPropertyName("en-gb")]
        public string Engb { get; set; }

        [JsonPropertyName("ja-jp")]
        public string Jajp { get; set; }

        [JsonPropertyName("zh-cn")]
        public string Zhcn { get; set; }

        [JsonPropertyName("fr-fr")]
        public string Frfr { get; set; }

        public virtual Poke Poke { get; set; }
    }
}
