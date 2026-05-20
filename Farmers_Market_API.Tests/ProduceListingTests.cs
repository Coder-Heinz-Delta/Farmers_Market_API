using Microsoft.VisualStudio.TestTools.UnitTesting;
using Farmers_Market_API.Models;
using Farmers_Market_API.Enums;

namespace Farmers_Market_API.Tests
{
    [TestClass]
    public class ProduceListingTests
    {
        // TC-02: CalculateRevenue returns PricePerKg × QuantityKg
        [TestMethod]
        public void CalculateRevenue_ValidPriceAndQuantity_ReturnsCorrectTotal()
        {
            var listing = new ProduceListing { PricePerKg = 15.50, QuantityKg = 10 };
            double revenue = listing.CalculateRevenue();
            Assert.AreEqual(155.00, revenue);
        }

        [TestMethod]
        public void CalculateRevenue_ZeroQuantity_ReturnsZero()
        {
            var listing = new ProduceListing { PricePerKg = 10.0, QuantityKg = 0 };
            double revenue = listing.CalculateRevenue();
            Assert.AreEqual(0.0, revenue);
        }

        // TC-01 equivalent: GetFormattedSummary contains all required fields
        [TestMethod]
        public void GetFormattedSummary_ContainsEssentialInfo()
        {
            var listing = new ProduceListing
            {
                ProduceName = "Organic Spinach",
                PricePerKg  = 5.0,
                QuantityKg  = 2,
                Category    = Category.Vegetables
            };

            string summary = listing.GetFormattedSummary();

            Assert.IsTrue(summary.Contains("Organic Spinach"), "Summary should contain produce name.");
            Assert.IsTrue(summary.Contains("5.00"),            "Summary should contain formatted price.");
            Assert.IsTrue(summary.Contains("Vegetables"),      "Summary should contain category.");
        }

        [TestMethod]
        public void GetFormattedSummary_NullDescription_ShowsNoneProvided()
        {
            var listing = new ProduceListing
            {
                ProduceName = "Mango",
                PricePerKg  = 8.0,
                QuantityKg  = 20,
                Description = null
            };

            string summary = listing.GetFormattedSummary();
            Assert.IsTrue(summary.Contains("None Provided"));
        }
    }
}
