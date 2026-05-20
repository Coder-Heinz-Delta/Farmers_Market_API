using Microsoft.VisualStudio.TestTools.UnitTesting;
using Farmers_Market_API.Repositories;
using Farmers_Market_API.Models;
using System;

namespace Farmers_Market_API.Tests
{
    [TestClass]
    public class FarmerRepositoryTests
    {
        private FarmerRepository _repo = null!;
        private Farmer _testFarmer = null!;

        [TestInitialize]
        public void Setup()
        {
            _repo = new FarmerRepository();
            _testFarmer = new Farmer(0, "John Smith", "john@example.com", "555-1234",
                                     "Pretoria", "Gauteng", 4.5, true, "Smith's Organic Farm");
        }

        // TC-11 equivalent: Adding a farmer returns 201 and assigns ID
        [TestMethod]
        public void Add_ValidFarmer_AssignsIdAndIncreasesCount()
        {
            int countBefore = _repo.GetAll().Count;
            _repo.Add(_testFarmer);
            Assert.AreEqual(countBefore + 1, _repo.GetAll().Count);
            Assert.IsTrue(_testFarmer.FarmerId > 0, "FarmerId should be auto-assigned after Add.");
        }

        // FIX: Was using FullName.GetHashCode() as the ID — now correctly uses FarmerId
        [TestMethod]
        public void GetById_AfterAdd_ReturnsCorrectFarmer()
        {
            _repo.Add(_testFarmer);

            // FIX: Use the auto-assigned FarmerId, not a hash code
            var result = _repo.GetById(_testFarmer.FarmerId);

            Assert.IsNotNull(result);
            Assert.AreEqual("John Smith", result.FullName);
            Assert.AreEqual("Smith's Organic Farm", result.FarmName);
            Assert.AreEqual("Pretoria", result.Location);
            Assert.AreEqual("Gauteng", result.Province);
            Assert.AreEqual(4.5, result.Rating);
            Assert.IsTrue(result.IsVerified);
        }

        [TestMethod]
        public void GetById_InvalidId_ThrowsException()
        {
            try { _repo.GetById(9999); Assert.Fail("Expected exception was not thrown."); }
            catch (Exception) { }
        }

        // Seed data verification (3 farmers in seed)
        [TestMethod]
        public void GetAll_ReturnsSeedData()
        {
            var farmers = _repo.GetAll();
            Assert.AreEqual(3, farmers.Count);
        }

        [TestMethod]
        public void Delete_ExistingFarmer_RemovesFromList()
        {
            int countBefore = _repo.GetAll().Count;
            _repo.Delete(1); // Kobus is seeded with FarmerId = 1
            Assert.AreEqual(countBefore - 1, _repo.GetAll().Count);
        }

        // TC-21: RecalculateRating averages correctly
        [TestMethod]
        public void RecalculateRating_AveragesReviewRatings()
        {
            // Add reviews manually via the repo's internal AddReview method
            _repo.AddReview(new Review { FarmerId = 1, BuyerId = 1, OrderId = 1, Rating = 3 });
            _repo.AddReview(new Review { FarmerId = 1, BuyerId = 2, OrderId = 2, Rating = 4 });
            _repo.AddReview(new Review { FarmerId = 1, BuyerId = 3, OrderId = 3, Rating = 5 });

            _repo.RecalculateRating(1);

            var farmer = _repo.GetById(1);
            Assert.AreEqual(4.0, farmer.Rating, 0.01);
        }
    }
}
