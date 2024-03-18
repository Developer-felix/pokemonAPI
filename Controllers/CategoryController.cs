
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PocumanAPI.IRepository;
using PocumanAPI.Models;
using PocumanAPI.Dto;

namespace PocumanAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        public CategoryController(ICategoryRepository categoryRepository,IMapper mapper)
        {
            this._categoryRepository = categoryRepository;
            this._mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ICollection<Category>))]
        public IActionResult GetCategories() {
           var categories = _mapper.Map<List<CategoryDto>>(_categoryRepository.GetCategories());

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(categories);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type=typeof(ICollection<Category>))]
        [ProducesResponseType(400)]
        public IActionResult GetCategory(int id)
        {
            var exists = _categoryRepository.CategoryExists(id);
            if(!exists)
            {
                return BadRequest();
            }

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var category = _categoryRepository.GetCategory(id);

            return Ok(category);
        }

        [HttpGet("{categoryId}/pokemon")]
        [ProducesResponseType(200, Type = typeof(ICollection<Pokemon>))]
        [ProducesResponseType(400)]
        public IActionResult GetPokemonByCategory(int categoryId)
        {
            var exists = _categoryRepository.CategoryExists(categoryId);

            if(!exists)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var pokemons = _mapper.Map<List<PokemonDto>>(_categoryRepository.GetPokemonByCategory(categoryId));

            return Ok(pokemons);
        }
    }
}
