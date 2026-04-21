using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Farmers_Market_API.Models
{
    public class Farmer
    {
        static int _idCounter = 1;

        public int FarmerId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public string Province { get; set; } = string.Empty;
        public double Rating { get; set; } = 0.0;
        public bool IsVerified { get; set; } = false;

        public Farmer(int farmerId, string fullName, string email, string phoneNumber, string location, string province, double rating, bool isVerified)
        {
            FarmerId = farmerId;
            FullName = fullName;
            Email = email;
            PhoneNumber = phoneNumber;
            Location = location;
            Province = province;
            Rating = rating;
            IsVerified = isVerified;
        }

        public Farmer(){}

        public int GetFarmerId()
        {
            return FarmerId;
        }
    }
}