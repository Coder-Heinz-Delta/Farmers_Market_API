using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Farmers_Market_API.Models;
using Microsoft.AspNetCore.Mvc;
using Farmers_Market_API.Enums;

namespace Farmers_Market_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProduceController : ControllerBase
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

        [HttpGet]
        public IActionResult GetProduceListings()
        {
            return Ok(ProduceListings);
        }

        [HttpGet("{id}")]
        public IActionResult GetProduceListingById(int id)
        {
            var produce = ProduceListings.FirstOrDefault(p => p.ListingId == id);
            if (produce == null)
            {
                return NotFound();
            } else
            {
                return Ok(produce);
            }
        }

        [HttpGet("{id}/summary")]
        public IActionResult GetProduceListingSummary(int id)
        {
            var produce = ProduceListings.FirstOrDefault(p => p.ListingId == id);
            if(produce == null)
            {
                return NotFound();
            } else
            {
                return Ok(produce.GetFormattedSummary());
            }
        }

        [HttpPost]
        public IActionResult CreateProduceListing([FromBody] ProduceListing newListing)
        {
            ProduceListings.Add(newListing);
            return Created($"localhost:5142/api/Produce/{newListing.ListingId}", ProduceListings);
        }

        [HttpGet("available")]
        public IActionResult GetAvailableProduce(){
            List<ProduceListing> available = new List<ProduceListing>();
            for (int i = 0; i < ProduceListings.Count; i++){
                if(ProduceListings[i].IsAvailable){
                    available.Add(ProduceListings[i]);
                }
            }
            return Ok(available);
        }

        [HttpGet("category/{category}")]
        public IActionResult GetProduceByCategory([FromRoute] Category category)
        {
           return Ok(ProduceListings.Where(produce => produce.Category == category).ToList()); 
        }
        


    }
}