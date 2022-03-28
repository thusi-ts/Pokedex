using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pokedex.Models
{
    public class PokedexRepository : IPokedexRepository
    {

        private readonly pokeDBContext appDBContext;

        public PokedexRepository(pokeDBContext appDBContext)
        {
            this.appDBContext = appDBContext;
        }

        public async Task<IEnumerable<Pokes>> GetPokedexs()
        {
            var pokeResult = await appDBContext.Pokedexs.Include(t => t.Translations).Include(b => b.Base).ToListAsync();
            List<Pokes> pokes = new List<Pokes>();

            foreach (var p in pokeResult)
            {
                Pokes poke = new Pokes
                { 
                    Id = p.Id,
                    Name = p.Name,
                    Type = p.Types.Split(","),
                    Translations = new Translations{ Engb = p.Translations.Engb, Frfr = p.Translations.Frfr, Jajp = p.Translations.Jajp, Zhcn = p.Translations.Zhcn },
                    Base = new Bases { Attack = p.Base.Attack, Defense = p.Base.Defense, HP = p.Base.HP, Spattack = p.Base.Spattack, Spdefense = p.Base.Spdefense, Speed = p.Base.Speed }

                };
                pokes.Add(poke);
            }

            return pokes;
        }

        public async Task<Pokes> GetPokedex(int id)
        {
            var pokeResult = await appDBContext.Pokedexs.Include(t => t.Translations).Include(b => b.Base).Where(p => p.Id == id).ToListAsync();

            foreach (var p in pokeResult)
            {
                Pokes poke = new Pokes
                {
                    Id = p.Id,
                    Name = p.Name,
                    Type = p.Types.Split(","),
                    Translations = new Translations { Engb = p.Translations.Engb, Frfr = p.Translations.Frfr, Jajp = p.Translations.Jajp, Zhcn = p.Translations.Zhcn },
                    Base = new Bases { Attack = p.Base.Attack, Defense = p.Base.Defense, HP = p.Base.HP, Spattack = p.Base.Spattack, Spdefense = p.Base.Spdefense, Speed = p.Base.Speed }

                };
                return poke;
            }

            return null;
        }

        public async Task<Pokes> AddPokedex(Poke pokedex)
        {
            Poke poked = new Poke();
            poked.Name = pokedex.Name;
            poked.Types = pokedex.Types;

            var result = await appDBContext.Pokedexs.AddAsync(poked);
            await appDBContext.SaveChangesAsync();

            Translation translation = new Translation();
            translation.PokeId = result.Entity.Id;
            translation.Engb = pokedex.Translations.Engb;
            translation.Jajp = pokedex.Translations.Jajp;
            translation.Zhcn = pokedex.Translations.Zhcn;
            translation.Frfr = pokedex.Translations.Frfr;
            var resultt = await appDBContext.Translations.AddAsync(translation);
            await appDBContext.SaveChangesAsync();

            Base bas = new Base();
            bas.PokeId = result.Entity.Id;
            bas.HP = pokedex.Base.HP;
            bas.Attack = pokedex.Base.Attack;
            bas.Defense = pokedex.Base.Defense;
            bas.Spattack = pokedex.Base.Spattack;
            bas.Spdefense = pokedex.Base.Spdefense;
            bas.Speed = pokedex.Base.Speed;
            var bresult = await appDBContext.Bases.AddAsync(bas);
            await appDBContext.SaveChangesAsync();

            Pokes poke = new Pokes
            {
                Id = poked.Id,
                Name = poked.Name,
                Type = poked.Types.Split(","),
                Translations = new Translations { Engb = translation.Engb, Frfr = translation.Frfr, Jajp = translation.Jajp, Zhcn = translation.Zhcn },
                Base = new Bases { Attack = bas.Attack, Defense = bas.Defense, HP = bas.HP, Spattack = bas.Spattack, Spdefense = bas.Spdefense, Speed = bas.Speed }
            };


            return poke;
        }

        public async Task<bool> DeletePokedex(int id)
        {

            var result = await appDBContext.Pokedexs.FirstOrDefaultAsync(s => s.Id == id);
            if (result != null)
            {
                try
                {
                    var pokes = await GetPokedex(id);

                    if(pokes != null)
                    {
                        var resultt = await appDBContext.Translations.FirstOrDefaultAsync(s => s.PokeId == id);
                        appDBContext.Translations.Remove(resultt);
                        await appDBContext.SaveChangesAsync();

                        var resultb = await appDBContext.Bases.FirstOrDefaultAsync(s => s.PokeId == id);
                        appDBContext.Bases.Remove(resultb);
                        await appDBContext.SaveChangesAsync();

                        appDBContext.Pokedexs.Remove(result);
                        await appDBContext.SaveChangesAsync();

                        return true;
                    }
                    return false;

                }
                catch (ObjectDisposedException e)
                {
                    Console.WriteLine("Poke: {0}", result.Id);
                    Console.WriteLine("Caught: {0}", e.Message);
                }

            }
            return false;
        }

        public async Task<Pokes> EditPokedex(Poke pokedex)
        {
            try
            {
                var resultt = await appDBContext.Translations.FirstOrDefaultAsync(s => s.PokeId == pokedex.Id);
                appDBContext.Translations.Remove(resultt);
                await appDBContext.SaveChangesAsync();

                var resultb = await appDBContext.Bases.FirstOrDefaultAsync(s => s.PokeId == pokedex.Id);
                appDBContext.Bases.Remove(resultb);
                await appDBContext.SaveChangesAsync();
                
                Poke poked = new Poke();
                poked.Name = pokedex.Name;
                poked.Types = pokedex.Types;
                poked.Id = pokedex.Id;

                var resultp = appDBContext.Update(poked);
                await appDBContext.SaveChangesAsync();
                    
                Translation translation = new Translation();
                translation.PokeId = resultp.Entity.Id;
                translation.Engb = pokedex.Translations.Engb;
                translation.Jajp = pokedex.Translations.Jajp;
                translation.Zhcn = pokedex.Translations.Zhcn;
                translation.Frfr = pokedex.Translations.Frfr;
                await appDBContext.Translations.AddAsync(translation);
                await appDBContext.SaveChangesAsync();

                Base bas = new Base();
                bas.PokeId = resultp.Entity.Id;
                bas.HP = pokedex.Base.HP;
                bas.Attack = pokedex.Base.Attack;
                bas.Defense = pokedex.Base.Defense;
                bas.Spattack = pokedex.Base.Spattack;
                bas.Spdefense = pokedex.Base.Spdefense;
                bas.Speed = pokedex.Base.Speed;
                await appDBContext.Bases.AddAsync(bas);
                await appDBContext.SaveChangesAsync();

                Pokes poke = new Pokes
                {
                    Id = poked.Id,
                    Name = poked.Name,
                    Type = poked.Types.Split(","),
                    Translations = new Translations { Engb = translation.Engb, Frfr = translation.Frfr, Jajp = translation.Jajp, Zhcn = translation.Zhcn },
                    Base = new Bases { Attack = bas.Attack, Defense = bas.Defense, HP = bas.HP, Spattack = bas.Spattack, Spdefense = bas.Spdefense, Speed = bas.Speed }
                };
                return poke;
            }
            catch (ObjectDisposedException e)
            {
                Console.WriteLine("Caught: {0}", e.Message);
            }
            return null;
        }
    }
}