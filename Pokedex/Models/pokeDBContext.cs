using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace Pokedex.Models
{
    public class pokeDBContext : DbContext
    {
        public pokeDBContext(DbContextOptions<pokeDBContext> options)
            : base(options)
        {
        }

        public DbSet<Poke> Pokedexs { get; set; }

        public DbSet<Translation> Translations { get; set; }
        
        public DbSet<Base> Bases { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }
    }
}

