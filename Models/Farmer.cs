using System;

namespace Farmers_Market_API.Models
{
    // Inheritance: Farmer inherits from Person
    public class Farmer : Person
    {
        public int FarmerId { get; set; }
        public string Location { get; set; } = string.Empty;
        public string Province { get; set; } = string.Empty;
        public double Rating { get; set; } = 0.0;
        public bool IsVerified { get; set; } = false;
        public string FarmName { get; set; } = string.Empty;

        // Constructor using 'base' to pass shared data to the Person class
        public Farmer(int farmerId, string fullName, string email, string phoneNumber, 
                      string location, string province, double rating, bool isVerified, string farmName)
        {
            FarmerId = farmerId;
            FullName = fullName; // Property from Person
            Email = email;       // Property from Person
            PhoneNumber = phoneNumber; // Property from Person
            Location = location;
            Province = province;
            Rating = rating;
            IsVerified = isVerified;
            FarmName = farmName;
        }

        // Parameterless constructor for flexibility/serialization
        public Farmer() { }

        // Polymorphism: Providing a unique implementation for Farmer contact info
        public override string GetContactInfo()
        {
            string verifiedStatus = IsVerified ? " [Verified Seller]" : "";
            return $"Farmer: {FullName} ({FarmName}){verifiedStatus} - Phone: {PhoneNumber} - Location: {Location}, {Province}";
        }

        public int GetFarmerId()
        {
            return FarmerId;
        }
    }
}