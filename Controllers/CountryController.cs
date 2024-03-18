
using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using PocumanAPI.Dto;
using PocumanAPI.IRepository;
using PocumanAPI.Models;

namespace PocumanAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CountryController : Controller
    {
        private readonly ICountryRepository _countryRepository;
        private readonly IMapper _mapper;
        public CountryController(ICountryRepository countryRepository, IMapper mapper) {
           this._countryRepository = countryRepository;
           this._mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ICollection<Country>))]
        public IActionResult GetCountries()
        {
            var countries = _countryRepository.GetCountries();
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }
            return Ok(countries);
        }

        [HttpGet("{countryId}")]
        [ProducesResponseType(200, Type=typeof(ICollection<Country>))]
        [ProducesResponseType(400)]
        public  IActionResult GetCountry(int countryId)
        {
            var exists  =  _countryRepository.CountryExists(countryId);
            if(!exists)
            {
                return BadRequest();
            }
            var country = _mapper.Map<CountryDto>(_countryRepository.GetCountry(countryId));
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            return Ok(country);
        }

        [HttpGet("owners/ownerId")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult GetCountryOfAOwner(int ownerId)
        {
            var country = _mapper.Map<CountryDto>(_countryRepository.GetOwnersFromACountry(ownerId));

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            return Ok(country);
        }
    }
}