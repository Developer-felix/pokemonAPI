using PocumanAPI.Data;
using PocumanAPI.IRepository;
using PocumanAPI.Models;

namespace PocumanAPI.Repository
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly ApplicationDBContext _db;
        public ReviewRepository(ApplicationDBContext db)
        {
            _db = db;
        }

        public bool CreateReview(Review review)
        {
            _db.Add(review);
            return Save();
        }

        public Review GetReview(int reviewId)
        {
            return _db.Reviews.Where(r => r.Id == reviewId).FirstOrDefault();
        }

        public ICollection<Review> GetReviews()
        {
            return _db.Reviews.OrderBy(r=> r.Id).ToList();
        }

        public ICollection<Review> GetReviewsOfAPokemon(int pokeId)
        {
            return _db.Reviews.Where(r=>r.Pokemon.Id == pokeId).ToList();
        }

        public bool ReviewExists(int reviewId)
        {
            return _db.Reviews.Any(r => r.Id == reviewId);
        }

        public bool Save()
        {
            var saved = _db.SaveChanges();
            return saved > 0 ? true : false;
        }

        
    }
}
