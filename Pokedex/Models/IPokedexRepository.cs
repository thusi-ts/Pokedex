using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pokedex.Models
{
    public interface IPokedexRepository
    {
        Task<IEnumerable<Pokes>> GetPokedexs();

        Task<Pokes> GetPokedex(int id);

        Task<Pokes> AddPokedex(Poke store);

        Task<Pokes> EditPokedex(Poke store);

        Task<bool> DeletePokedex(int id);
    }
}
