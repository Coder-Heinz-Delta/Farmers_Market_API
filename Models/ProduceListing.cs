using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Farmers_Market_API.Enums;

namespace Farmers_Market_API.Models
{
    public class ProduceListing
    {
        public int ListingId { get; set; }
        public int FarmerId { get; set; }
        public string ProduceName { get; set; } = string.Empty;
        public Category Category { get; set; } = Category.Other;
        public double PricePerKg { get; set; } = 0.0;
        public double QuantityKg { get; set; } = 0;
        public bool IsAvailable { get; set; } = true;
        public DateTime HarvestDate { get; set; } = DateTime.Now;
        public DateTime DateListed { get; set; } = DateTime.Now;
        public string? Description { get; set; }

        public ProduceListing(){}
        
        public ProduceListing(int listingId, int farmerId, string produceName, Category category, double pricePerKg, double quantityKg, bool isAvailable, DateTime harvestDate, DateTime dateListed, string? description)
        {
            ListingId = listingId;
            FarmerId = farmerId;
            ProduceName = produceName;
            Category = category;
            PricePerKg = pricePerKg;
            QuantityKg = quantityKg;
            IsAvailable = isAvailable;
            HarvestDate = harvestDate;
            DateListed = dateListed;
            Description = description;
        }
    
        public string GetFormattedSummary()
        {
            return $"""
            Produce: {ProduceName} 
            Price: ${PricePerKg:F2} 
            Quantity: {QuantityKg} kg 
            Category: {Category}
            Description: {Description ?? "None Provided"}
            """;
        }

        public double CalculateRevenue()
        {
            return PricePerKg * QuantityKg;
        }
    }
}