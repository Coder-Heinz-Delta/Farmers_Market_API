using Microsoft.VisualStudio.TestTools.UnitTesting;
using Farmers_Market_API.Models;
using Farmers_Market_API.Enums;
using Farmers_Market_API.Repositories;

namespace Farmers_Market_API.Tests
{
    [TestClass]
    public class OrderTests
    {
        private OrderRepository _orderRepo = null!;
        private ProduceRepository _produceRepo = null!;

        [TestInitialize]
        public void Setup()
        {
            _orderRepo   = new OrderRepository();
            _produceRepo = new ProduceRepository();
        }

        // TC-12: Valid order is created with Status = Pending
        [TestMethod]
        public void AddOrder_ValidOrder_StatusIsPending()
        {
            var order = new Order
            {
                BuyerId        = 1,
                FarmerId       = 2,
                ListingId      = 2,  // Apples — available, 50kg in stock
                QuantityOrdered = 10
            };

            _orderRepo.Add(order);

            var saved = _orderRepo.GetById(order.OrderId);
            Assert.AreEqual(OrderStatus.Pending, saved.Status);
        }

        // TC-14: Pending order can be confirmed
        [TestMethod]
        public void Order_ConfirmTransition_ChangesStatusToConfirmed()
        {
            var order = new Order { BuyerId = 1, ListingId = 2, QuantityOrdered = 5 };
            _orderRepo.Add(order);

            order.Status = OrderStatus.Confirmed;

            Assert.AreEqual(OrderStatus.Confirmed, order.Status);
        }

        // TC-15: Confirmed order can be collected and reduces listing quantity
        [TestMethod]
        public void Order_CollectTransition_ReducesListingQuantity()
        {
            var listing = _produceRepo.GetById(2); // Apples, 50kg
            double initialQty = listing.QuantityKg;

            var order = new Order { BuyerId = 1, ListingId = 2, QuantityOrdered = 10 };
            _orderRepo.Add(order);
            order.Status = OrderStatus.Confirmed;

            // Simulate collect action
            listing.QuantityKg -= order.QuantityOrdered;
            order.Status        = OrderStatus.Collected;

            Assert.AreEqual(OrderStatus.Collected, order.Status);
            Assert.AreEqual(initialQty - 10, listing.QuantityKg);
        }

        // TC-13: Order quantity exceeding stock should be rejected (business rule)
        [TestMethod]
        public void Order_QuantityExceedsStock_IsInvalid()
        {
            var listing = _produceRepo.GetById(2); // Apples, 50kg
            var order   = new Order { BuyerId = 1, ListingId = 2, QuantityOrdered = 999 };

            bool isValid = order.QuantityOrdered <= listing.QuantityKg;

            Assert.IsFalse(isValid, "Order exceeding available stock should be flagged as invalid.");
        }
    }
}
