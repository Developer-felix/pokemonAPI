﻿using PocumanAPI.Models;

namespace PocumanAPI.IRepository
{
    public interface IReviewRepository
    {
        ICollection<Review> GetReviews();

        Review GetReview(int reviewId);

        ICollection<Review> GetReviewsOfAPokemon(int pokeId);

        bool ReviewExists(int reviewId);    

        bool CreateReview(Review review);

        bool Save();

    }
}
