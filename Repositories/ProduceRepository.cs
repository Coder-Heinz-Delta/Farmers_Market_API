using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Farmers_Market_API.Enums;
using Farmers_Market_API.Exceptions;
using Farmers_Market_API.Models;
using Farmers_Market_API.Interfaces;

namespace Farmers_Market_API.Repositories
{
    public class ProduceRepository : IRepository<ProduceListing>
    {
        private List<ProduceListing> ProduceListings =
        [
            new (1, 1, "Tomatoes", Category.Vegetables, 2.5, 100, false, DateTime.Now.AddDays(-10), DateTime.Now.AddDays(-5), "Freshly harvested tomatoes."),
            new (2, 2, "Apples", Category.Fruit, 3.0, 50, true, DateTime.Now.AddDays(-15), DateTime.Now.AddDays(-7), "Crisp and sweet apples."),
            new (3, 3, "Carrots", Category.Vegetables, 1.8, 200, true, DateTime.Now.AddDays(-20), DateTime.Now.AddDays(-10), "Organic carrots from our farm."),
            new (4, 1, "Lettuce", Category.Vegetables, 2.0, 75, true, DateTime.Now.AddDays(-8), DateTime.Now.AddDays(-3), "Crisp green lettuce leaves."),
            new (5, 2, "Oranges", Category.Fruit, 2.8, 60, false, DateTime.Now.AddDays(-12), DateTime.Now.AddDays(-6), "Juicy navel oranges."),
            new (6, 3, "Potatoes", Category.Vegetables, 1.5, 150, true, DateTime.Now.AddDays(-25), DateTime.Now.AddDays(-12), "Fresh russet potatoes."),
            new (7, 1, "Strawberries", Category.Fruit, 4.5, 30, false, DateTime.Now.AddDays(-5), DateTime.Now.AddDays(-2), "Sweet seasonal strawberries."),
            new (8, 2, "Broccoli", Category.Vegetables, 3.2, 40, false, DateTime.Now.AddDays(-7), DateTime.Now.AddDays(-4), "Nutritious broccoli florets."),
            new (9, 3, "Bananas", Category.Fruit, 1.2, 80, true, DateTime.Now.AddDays(-18), DateTime.Now.AddDays(-9), "Ripe yellow bananas."),
            new (10, 1, "Spinach", Category.Vegetables, 2.3, 55, true, DateTime.Now.AddDays(-6), DateTime.Now.AddDays(-3), "Fresh baby spinach leaves.")
        ];

        public List<ProduceListing> GetAll() => ProduceListings;

        public ProduceListing GetById(int id)
        {
            var listing = ProduceListings.FirstOrDefault(p => p.ListingId == id);
            if (listing == null)
            {
                throw new ListingNotFoundException(id);
            }
            return listing;
        }

        // Specific method for available produce
        public List<ProduceListing> GetAvailable() => ProduceListings.Where(p => p.IsAvailable).ToList();

        // Specific method for filtering by category
        public List<ProduceListing> GetByCategory(Category category) =>
            ProduceListings.Where(produce => produce.Category == category).ToList();

        public void Add(ProduceListing listing)
        {
            if (listing == null)
            {
                throw new InvalidProduceFormatException("Produce listing cannot be null.");
            }

            // Using the property names from your validation logic
            if (string.IsNullOrWhiteSpace(listing.ProduceName))
            {
                throw new InvalidProduceFormatException("Produce name cannot be empty.");
            }

            if (listing.PricePerKg <= 0)
            {
                throw new InvalidProduceFormatException("Price must be greater than zero.");
            }

            if (listing.QuantityKg < 0)
            {
                throw new InvalidProduceFormatException("Quantity cannot be negative.");
            }

            // Logic to auto-assign ID based on current max
            listing.ListingId = ProduceListings.Any() ? ProduceListings.Max(p => p.ListingId) + 1 : 1;
            ProduceListings.Add(listing);
        }

        public void Delete(int id)
        {
            var listing = GetById(id); // This will throw ListingNotFoundException if not found
            ProduceListings.Remove(listing);
        }
    }
}