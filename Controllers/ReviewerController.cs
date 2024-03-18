using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PocumanAPI.Dto;
using PocumanAPI.IRepository;
using PocumanAPI.Models;
using PocumanAPI.Repository;

namespace PocumanAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ReviewerController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IReviewerRepository _reviewerRepository;
        public ReviewerController(IReviewerRepository reviewerRepository, IMapper mapper)
        {
            this._mapper = mapper;
            this._reviewerRepository = reviewerRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ICollection<Reviewer>))]
        public IActionResult GetReviewers()
        {
            var reviewers = _mapper.Map<List<ReviewerDto>>(_reviewerRepository.GetReviewers());

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            return View(reviewers);
        }

        [HttpGet("{reviewerId}")]
        [ProducesResponseType(200, Type = typeof(ICollection<Reviewer>))]
        [ProducesResponseType(400)]
        public IActionResult GetReviewer(int reviewerId)
        {
            var exists = _reviewerRepository.ReviewerExists(reviewerId);

            if (!exists)
            {
                return BadRequest();
            }

            var reviewer = _mapper.Map<List<ReviewDto>>(_reviewerRepository.GetReviewer(reviewerId)); 

            return Ok(reviewer);

        }

        [HttpGet("{reviewerId}/reviews")]
        [ProducesResponseType(200, Type = typeof(ICollection<Reviewer>))]
        [ProducesResponseType(400)]
        public IActionResult GetReviewsByReviewer(int reviewerId)
        {
            var exists = _reviewerRepository.ReviewerExists(reviewerId);

            if(!exists)
                return BadRequest();

            var reviews = _mapper.Map<List<ReviewDto>>(_reviewerRepository.GetReviewsByReviewer(reviewerId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(reviews);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateReviewer( [FromBody] ReviewerDto reviewer)
        {
            if (reviewer == null)
                return BadRequest(ModelState);

            var reviewers = _reviewerRepository.GetReviewers()
                .Where(r => r.FirstName.Trim().ToUpper() == reviewer.FirstName.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (reviewers != null)
            {
                ModelState.AddModelError("", "Reviewers Already exists!");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var reviewerMap = _mapper.Map<Reviewer>(reviewer);
              
            if (!_reviewerRepository.CreateReviewer(reviewerMap))
            {
                ModelState.AddModelError("", "Something went wrong while creating the reviewer");
                return StatusCode(500, ModelState);
            }

            return Ok("Reviewer created successfully");
        }
    }
}
