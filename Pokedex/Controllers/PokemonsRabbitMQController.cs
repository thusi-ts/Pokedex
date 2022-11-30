using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pokedex.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pokedex.Controllers
{
    [Route("PokemonsRabbitMQ")]
    [ApiController]
    public class PokemonsRabbitMQController : ControllerBase
    {
        private readonly IPokedexRepository _pokedexRepository;
        private readonly IRabitMQPokedexProducer _rabitMQPokedexProducer;

        public PokemonsRabbitMQController(IPokedexRepository pokedexRepository, IRabitMQPokedexProducer rabitMQPokedexProducer)
        {
            this._pokedexRepository = pokedexRepository;
            this._rabitMQPokedexProducer = rabitMQPokedexProducer;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult> SendProductToConsumers(int id)
        {
            try
            {
                IRabitMQPokedexBody<Pokes> body = new IRabitMQPokedexBody<Pokes>();
                var result = await _pokedexRepository.GetPokedex(id);

                body.Operation = "Save";
                body.Body = (Pokes)result;

                _rabitMQPokedexProducer.SendProductMessage(body);

                return Ok(result);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error found in producer");
            }
        }
    }
}
