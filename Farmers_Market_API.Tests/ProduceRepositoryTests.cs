using Microsoft.VisualStudio.TestTools.UnitTesting;
using Farmers_Market_API.Repositories;
using Farmers_Market_API.Models;
using Farmers_Market_API.Exceptions;
using Farmers_Market_API.Enums;
using System;

namespace Farmers_Market_API.Tests
{
    [TestClass]
    public class ProduceRepositoryTests
    {
        private ProduceRepository _repo = null!;

        [TestInitialize]
        public void Setup()
        {
            _repo = new ProduceRepository();
        }

        // TC-03 equivalent: Seed data returns correct count
        [TestMethod]
        public void GetAll_ReturnsInitialSeedData()
        {
            var result = _repo.GetAll();
            Assert.AreEqual(10, result.Count);
        }

        // TC-06: Valid ID returns correct listing
        [TestMethod]
        public void GetById_ValidId_ReturnsCorrectListing()
        {
            var result = _repo.GetById(2);
            Assert.IsNotNull(result);
            Assert.AreEqual("Apples", result.ProduceName);
        }

        // TC-07: Invalid ID throws ListingNotFoundException
        [TestMethod]
        public void GetById_InvalidId_ThrowsListingNotFoundException()
        {
            try { _repo.GetById(999); Assert.Fail("Expected exception was not thrown."); }
            catch (ListingNotFoundException) { }
        }

        // TC-03: Add valid listing increases count and assigns new ID
        [TestMethod]
        public void Add_ValidListing_AssignsNewIdAndIncreasesCount()
        {
            var initialCount = _repo.GetAll().Count;
            var newListing = new ProduceListing
            {
                ProduceName = "Cucumbers",
                Category = Category.Vegetables,
                PricePerKg = 2.0,
                QuantityKg = 50
            };

            _repo.Add(newListing);

            Assert.AreEqual(initialCount + 1, _repo.GetAll().Count);
            Assert.AreEqual(11, newListing.ListingId);
        }

        // TC-08: POST with PricePerKg <= 0 throws InvalidProduceFormatException
        [TestMethod]
        public void Add_InvalidPrice_ThrowsInvalidProduceFormatException()
        {
            var invalidListing = new ProduceListing { ProduceName = "Bad Price", PricePerKg = -1.0, QuantityKg = 10 };
            try { _repo.Add(invalidListing); Assert.Fail("Expected exception was not thrown."); }
            catch (InvalidProduceFormatException) { }
        }

        // TC-09: POST with empty ProductName throws InvalidProduceFormatException
        [TestMethod]
        public void Add_EmptyProductName_ThrowsInvalidProduceFormatException()
        {
            var invalidListing = new ProduceListing { ProduceName = "", PricePerKg = 5.0, QuantityKg = 10 };
            try { _repo.Add(invalidListing); Assert.Fail("Expected exception was not thrown."); }
            catch (InvalidProduceFormatException) { }
        }

        // TC-04: GetAvailable returns only IsAvailable=true listings
        [TestMethod]
        public void GetAvailable_ReturnsOnlyAvailableListings()
        {
            var available = _repo.GetAvailable();
            Assert.IsTrue(available.All(p => p.IsAvailable),
                "GetAvailable should only return listings where IsAvailable is true.");
        }

        // TC-05: GetByCategory returns only listings of the correct category
        [TestMethod]
        public void GetByCategory_Fruit_ReturnsOnlyFruitListings()
        {
            var fruits = _repo.GetByCategory(Category.Fruit);
            Assert.IsTrue(fruits.Count > 0);
            Assert.IsTrue(fruits.All(p => p.Category == Category.Fruit));
        }

        // TC-17: Update changes price and records price history
        [TestMethod]
        public void Update_ChangedPrice_RecordsPriceHistory()
        {
            var original = _repo.GetById(1);
            double oldPrice = original.PricePerKg;

            var updated = new ProduceListing
            {
                ListingId = 1,
                ProduceName = original.ProduceName,
                PricePerKg = 99.99,
                QuantityKg = original.QuantityKg,
                HarvestDate = original.HarvestDate,
                Description = original.Description
            };

            _repo.Update(updated);

            var history = _repo.GetPriceHistory(1);
            Assert.AreEqual(1, history.Count);
            Assert.AreEqual(oldPrice, history[0].OldPrice);
            Assert.AreEqual(99.99, history[0].NewPrice);
        }

        // TC-18: SoftDelete sets IsAvailable=false, listing still exists
        [TestMethod]
        public void SoftDelete_SetsIsAvailableToFalse_DoesNotRemoveListing()
        {
            _repo.SoftDelete(2);
            var listing = _repo.GetById(2);
            Assert.IsNotNull(listing);
            Assert.IsFalse(listing.IsAvailable,
                "SoftDelete should set IsAvailable=false, not remove the listing.");
        }

        // TC-19: Price range filter returns only matching listings
        [TestMethod]
        public void GetByPriceRange_ReturnsListingsWithinRange()
        {
            var results = _repo.GetByPriceRange(2.0, 3.0);
            Assert.IsTrue(results.Count > 0);
            Assert.IsTrue(results.All(p => p.PricePerKg >= 2.0 && p.PricePerKg <= 3.0));
        }
    }
}
