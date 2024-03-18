using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PocumanAPI.Dto;
using PocumanAPI.IRepository;
using PocumanAPI.Models;

namespace PocumanAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ReviewController : Controller
    {
        private readonly IReviewRepository _ireviewRepository;
        private readonly IMapper _mapper;
        private readonly IPokemanRepository _pokemanRepository;
        private readonly IReviewerRepository _reviewerRepository;
        public ReviewController(IReviewRepository reviewRepository, IMapper mapper, IPokemanRepository pokemanRepository, IReviewerRepository reviewerRepository)
        {
            _ireviewRepository = reviewRepository;
            _mapper = mapper;
            _pokemanRepository = pokemanRepository;
            _reviewerRepository = reviewerRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ICollection<Review>))]
        public IActionResult GetReviews()
        {
            var revies = _mapper.Map<List<ReviewDto>>(_ireviewRepository.GetReviews());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(revies);
        }

        [HttpGet("{reviewId}")]
        [ProducesResponseType(200, Type = typeof(ICollection<Review>))]
        [ProducesResponseType(400)]
        public IActionResult GetReview(int reviewId)
        {
            var exists = _ireviewRepository.ReviewExists(reviewId);

            if (!exists)
            {
                return BadRequest();
            }

            var review = _mapper.Map<ReviewDto>(_ireviewRepository.GetReview(reviewId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(review);
        }

        [HttpGet("pokemon/{pokemonId}")]
        [ProducesResponseType(200, Type = typeof(ICollection<Review>))]
        [ProducesResponseType(400)]
        public IActionResult GetReviewsForAPokemon(int pokeId)
        {
            var reviews = _mapper.Map<List<ReviewDto>>(_ireviewRepository.GetReviewsOfAPokemon(pokeId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(reviews);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateReview([FromQuery] int reviewerId, [FromQuery] int pokeId, [FromBody] ReviewDto review)
        {
            if (review == null)
                return BadRequest(ModelState);

            var reviews = _ireviewRepository.GetReviews()
                .Where(r => r.Title.Trim().ToUpper() == review.Title.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (reviews != null)
            {
                ModelState.AddModelError("", "Review Already exists!");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var reviewMap = _mapper.Map<Review>(review);

            reviewMap.Pokemon = _pokemanRepository.GetPokemon(pokeId);
            reviewMap.Reviewer = _reviewerRepository.GetReviewer(pokeId);


            if (!_ireviewRepository.CreateReview(reviewMap))
            {
                ModelState.AddModelError("", "Something went wrong while creating the review");
                return StatusCode(500, ModelState);
            }

            return Ok("Review created successfully");
        }
    }
}
