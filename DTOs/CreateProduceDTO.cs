using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Farmers_Market_API.DTOs
{
    public readonly struct CreateProduceDTO
    {
        public int FarmerId { get; init; }
        public string ProduceName { get; init; }
        public string Category { get; init; }
        public double PricePerUnit { get; init; }
        public int QuantityKg { get; init; }
        public bool IsAvailable { get; init; }
        public DateTime HarvestDate { get; init; }
        public DateTime ExpirationDate { get; init; }
        public string Description { get; init; }

        public CreateProduceDTO(int farmerId, string produceName, string category, double pricePerUnit, int quantityKg, bool isAvailable, DateTime harvestDate, DateTime expirationDate, string description)
        {
            FarmerId = farmerId;
            ProduceName = produceName;
            Category = category;
            PricePerUnit = pricePerUnit;
            QuantityKg = quantityKg;
            IsAvailable = isAvailable;
            HarvestDate = harvestDate;
            ExpirationDate = expirationDate;
            Description = description;
        }
    }
}