using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PocumanAPI.Dto;
using PocumanAPI.IRepository;
using PocumanAPI.Models;

namespace PocumanAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class OwnerController : Controller
    {
        private readonly IMapper _mapper;
        private IOwnerRepository _ownerRepository;
        public OwnerController(IOwnerRepository ownerRepository, IMapper mapper)
        {
            _ownerRepository = ownerRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ICollection<Owner>))]
        public ActionResult GetOwners()
        {
            var owners = _mapper.Map<List<OwnerDto>>(_ownerRepository.GetOwners());
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(owners);
        }

        [HttpGet("{ownerId}")]
        [ProducesResponseType(200, Type = typeof(ICollection<Owner>))]
        [ProducesResponseType(400)]
        public ActionResult GetOwner(int ownerId)
        {
            var exists = _ownerRepository.OwnerExists(ownerId);
            if(!exists)
            {
                return BadRequest();
            }

            var owner = _mapper.Map<OwnerDto>(_ownerRepository.GetOwner(ownerId));

            if(!ModelState.IsValid)
            {
                return BadRequest($"Owner {ownerId} is not valid");
            }

            return Ok(owner);
        }

        [HttpGet("{ownerId}/pokemon")]
        [ProducesResponseType(200, Type = typeof(ICollection<Owner>))]
        [ProducesResponseType(400)]
        public IActionResult GetOwnerByOwnerId(int ownerId)
        {
            var exists = _ownerRepository.OwnerExists(ownerId);

            if (!exists)
                return BadRequest();

            var owner = _mapper.Map<OwnerDto>(_ownerRepository.GetPokemonByOwner(ownerId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(owner); 
        }





    }
}
