using Microsoft.VisualStudio.TestTools.UnitTesting;
using Farmers_Market_API.Models;
using Farmers_Market_API.Repositories;
using System;

namespace Farmers_Market_API.Tests
{
    [TestClass]
    public class ReviewTests
    {
        private ReviewRepository _reviewRepo = null!;

        [TestInitialize]
        public void Setup()
        {
            _reviewRepo = new ReviewRepository();
        }

        // TC-20: Valid review (rating 1–5) is created successfully
        [TestMethod]
        public void AddReview_ValidRating_CreatesReview()
        {
            var review = new Review
            {
                BuyerId = 1,
                FarmerId = 1,
                OrderId = 1,
                Rating = 4,
                Comment = "Great produce!"
            };

            _reviewRepo.Add(review);

            Assert.AreEqual(1, _reviewRepo.GetAll().Count);
            Assert.AreEqual(4, _reviewRepo.GetById(review.ReviewId).Rating);
        }

        // TC-20: Review with out-of-range rating is rejected
        [TestMethod]
        public void AddReview_InvalidRating_ThrowsArgumentException()
        {
            var review = new Review { BuyerId = 1, FarmerId = 1, OrderId = 1, Rating = 6 };
            try { _reviewRepo.Add(review); Assert.Fail("Expected exception was not thrown."); }
            catch (ArgumentException) { }
        }

        // TC-21: Average rating calculation is correct
        [TestMethod]
        public void GetByFarmer_AverageRating_IsCorrect()
        {
            _reviewRepo.Add(new Review { BuyerId = 1, FarmerId = 1, OrderId = 1, Rating = 3 });
            _reviewRepo.Add(new Review { BuyerId = 2, FarmerId = 1, OrderId = 2, Rating = 4 });
            _reviewRepo.Add(new Review { BuyerId = 3, FarmerId = 1, OrderId = 3, Rating = 5 });

            var reviews = _reviewRepo.GetByFarmer(1);
            double average = 0;
            foreach (var r in reviews) average += r.Rating;
            average /= reviews.Count;

            Assert.AreEqual(4.0, average, 0.01);
        }
    }
}
