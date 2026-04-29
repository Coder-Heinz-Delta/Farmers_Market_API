using System;

namespace Farmers_Market_API.Models
{
    // Inheritance: Buyer inherits shared properties from Person
    public class Buyer : Person
    {
        public int BuyerId { get; set; }
        public string BuyerType { get; set; } = string.Empty; // e.g., Individual, Wholesale, Restaurant
        public string Location { get; set; } = string.Empty;

        public Buyer(int buyerId, string fullName, string email, string phoneNumber, string buyerType, string location)
        {
            BuyerId = buyerId;
            FullName = fullName; // Inherited from Person
            Email = email;       // Inherited from Person
            PhoneNumber = phoneNumber; // Inherited from Person
            BuyerType = buyerType;
            Location = location;
        }

        public Buyer() { }

        // Polymorphism: Custom output for Buyer contact info
        public override string GetContactInfo()
        {
            return $"Buyer [{BuyerType}]: {FullName}. Preferred Delivery: {Location}. Email: {Email}";
        }
    }
}