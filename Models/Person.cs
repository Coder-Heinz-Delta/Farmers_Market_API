using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Farmers_Market_API.Models
{
    public class Person
    {
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;

        public virtual string GetContactInfo()
        {
            return $"Name: {FullName}, Email: {Email}, Phone: {PhoneNumber}";
        }
    }
}