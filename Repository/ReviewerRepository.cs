using Microsoft.EntityFrameworkCore;
using PocumanAPI.Data;
using PocumanAPI.IRepository;
using PocumanAPI.Models;

namespace PocumanAPI.Repository
{
    public class ReviewerRepository : IReviewerRepository
    {
        private readonly ApplicationDBContext _db;

        public ReviewerRepository(ApplicationDBContext db)
        {
            _db = db;
        }

        public bool CreateReviewer(Reviewer reviewer)
        {
            _db.Add(reviewer);
            return Save();
        }

        public Reviewer GetReviewer(int reviewerId)
        {
            return _db.Reviewers.Where(r => r.Id == reviewerId).Include(e => e.Reviews).FirstOrDefault();
        }

        public ICollection<Reviewer> GetReviewers()
        {
            return _db.Reviewers.OrderBy(r => r.Id).ToList();
        }

        public ICollection<Review> GetReviewsByReviewer(int reviewerId)
        {
            return _db.Reviews.Where(r => r.Reviewer.Id == reviewerId).ToList();
        }

        public bool ReviewerExists(int reviewerId)
        {
            return _db.Reviewers.Any(r => r.Id == reviewerId);
        }

        public bool Save()
        {
            var saved = _db.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
