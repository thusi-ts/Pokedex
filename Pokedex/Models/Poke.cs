using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;


namespace Pokedex.Models
{
    public class Poke
    {
        [Key]
        public long Id { get; set; }
        
        public string Name { get; set; }

        public string Types { get; set; }

        public Translation Translations { get; set; }

        public Base Base { get; set; }

        [NotMapped]
        public string[] Type { get; set; }
    }
}
