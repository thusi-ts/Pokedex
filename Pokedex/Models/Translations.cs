using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Pokedex.Models
{
    public class Translations
    {
        [JsonPropertyName("en-gb")]
        public string Engb { get; set; }

        [JsonPropertyName("ja-jp")]
        public string Jajp { get; set; }

        [JsonPropertyName("zh-cn")]
        public string Zhcn { get; set; }

        [JsonPropertyName("fr-fr")]
        public string Frfr { get; set; }
    }
}
