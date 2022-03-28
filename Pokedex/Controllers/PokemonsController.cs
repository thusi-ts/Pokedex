using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pokedex.Models;
using Microsoft.AspNetCore.Http;

namespace Pokedex.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PokemonsController : ControllerBase
    {
        private readonly IPokedexRepository pokedexRepository;

        public PokemonsController(IPokedexRepository pokedexRepository)
        {
            this.pokedexRepository = pokedexRepository;
        }

        [HttpGet]
        public async Task<ActionResult> GetPokedexs()
        {
            try
            {
                return Ok(await pokedexRepository.GetPokedexs());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult> GetPokedex(int id)
        {
            try
            {
                var result = await pokedexRepository.GetPokedex(id);

                if (result == null)
                {
                    return NotFound();
                }

                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        [HttpPut]
        public async Task<ActionResult<Poke>> AddPokedex(Poke pokedex)
        {
            try
            {
                if (pokedex == null)
                    return BadRequest();

                var types = String.Join(",", pokedex.Type);
                pokedex.Types = types;

                var createded = await pokedexRepository.AddPokedex(pokedex);

                return CreatedAtAction(nameof(GetPokedex),
                    new { id = createded.Id }, createded);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creating new Pokedex record");
            }
        }

        [HttpPost("{id:int}")]
        public async Task<ActionResult<Pokes>> EditPokedex(int id, Poke pokedex)
        {
            try
            {
                if (id != pokedex.Id)
                    return BadRequest("Pokedex ID mismatch");

                var types = String.Join(",", pokedex.Type);
                pokedex.Types = types;

                return await pokedexRepository.EditPokedex(pokedex);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating pokedex record");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeletePokedex(int id)
        {
            try
            {
                await pokedexRepository.DeletePokedex(id);

                return Ok($"Pokedex with Id = {id} deleted");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting pokedex record");
            }
        }

        [HttpDelete("{ids}")]
        public async Task<ActionResult> DeletePokedexRange(int[] ids)
        {
            try
            {
                foreach (var id in ids)
                {
                    await pokedexRepository.DeletePokedex(id);
                }

                return Ok($"Pokedex with Id = {String.Join(",", ids)} deleted");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting pokedex record");
            }
        }
    }
}
