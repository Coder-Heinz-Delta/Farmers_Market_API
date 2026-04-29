using System;

namespace Farmers_Market_API.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public int BuyerId { get; set; }
        public int FarmerId { get; set; }
        public int ListingId { get; set; }
        
        // Transaction Details
        public double QuantityOrdered { get; set; } // e.g., 5.5kg
        public double TotalPrice { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public string Status { get; set; } = "Pending"; // e.g., Pending, Completed, Cancelled

        public Order(int orderId, int buyerId, int farmerId, int listingId, double quantity, double price)
        {
            OrderId = orderId;
            BuyerId = buyerId;
            FarmerId = farmerId;
            ListingId = listingId;
            QuantityOrdered = quantity;
            TotalPrice = price;
        }

        public Order() { }
    }
}