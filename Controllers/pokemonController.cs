
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PocumanAPI.Dto;
using PocumanAPI.IRepository;
using PocumanAPI.Models;

namespace PocumanAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class pokemonController : Controller
    {
        private readonly IPokemanRepository _pokemonRepository;
        private readonly IMapper _mapper;
        public pokemonController(IPokemanRepository pokemanRepository, IMapper mapper) {
            this._pokemonRepository = pokemanRepository;
            this._mapper = mapper;
        }

        [HttpGet] 
        [ProducesResponseType(200, Type = typeof(IEnumerable<Pokemon>))]
        public IActionResult GetPokemons()
        {
            var pokemons = _mapper.Map<List<PokemonDto>>(_pokemonRepository.GetPokemons());

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(pokemons); 
        }

        [HttpGet("{pokeId}")]
        [ProducesResponseType(200, Type=typeof(ICollection<Pokemon>))]
        [ProducesResponseType(400)]
        public IActionResult GetPokemon(int pokeId) { 

            var exist = _pokemonRepository.PokemonExists(pokeId);

            if(!exist)
            {
                return BadRequest();
            }

            var pokemon = _mapper.Map<PokemonDto>(_pokemonRepository.GetPokemon(pokeId));

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            return Ok(pokemon);
        }

        [HttpGet("rating/{pokeId}")]
        [ProducesResponseType(200, Type= typeof(ICollection<Pokemon>))]
        [ProducesResponseType(400)]
        public IActionResult GetPokemanRating(int pokeId)
        {
            var exists = _pokemonRepository.PokemonExists(pokeId);

            if(!exists)
            {
                return BadRequest();
            }

            var pokemon_rating = _pokemonRepository.GetPokemonRating(pokeId);

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            return Ok(pokemon_rating);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreatePokemon([FromQuery] int ownerId, [FromQuery] int catId, [FromBody] PokemonDto pokemonCreate)
        {
            if (pokemonCreate == null)
            {
                return BadRequest();
            }

            var pokemons = _pokemonRepository.GetPokemons()
                .Where(c => c.Name.Trim() == pokemonCreate.Name.TrimEnd().ToUpper())
                .FirstOrDefault();
            if (pokemons != null)
            {
                ModelState.AddModelError("", "Owner already exists");
                return StatusCode(422, ModelState);
            }

            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            var pokemonMap = _mapper.Map<Pokemon>(pokemonCreate);

            if(!_pokemonRepository.CreatePokemon(ownerId, catId, pokemonMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving pokemon! ");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }
    }
}
