using Microsoft.AspNetCore.Mvc;
using Farmers_Market_API.Models;
using Farmers_Market_API.Interfaces;


namespace Farmers_Market_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IRepository<Order> _orderRepo;
        private readonly IRepository<ProduceListing> _produceRepo;
        private readonly INotifiable _notifier; // Polymorphic notifier

        public OrderController(IRepository<Order> orderRepo, IRepository<ProduceListing> produceRepo)
        {
            _orderRepo = orderRepo;
            _produceRepo = produceRepo;
            _notifier = new EmailNotifier(); // Stubbed for now
        }

        // Place an order
        [HttpPost]
        public IActionResult PlaceOrder([FromBody] Order order)
        {
            var listing = _produceRepo.GetById(order.ListingId);
            
            if (listing.QuantityKg < order.QuantityOrdered)
                return BadRequest("Not enough stock available.");

            order.Status = "Pending";
            _orderRepo.Add(order);
            
            return CreatedAtAction(nameof(PlaceOrder), new { id = order.OrderId }, order);
        }

        //  Confirm order
        [HttpPatch("{id}/confirm")]
        public IActionResult ConfirmOrder(int id)
        {
            var order = _orderRepo.GetById(id);
            order.Status = "Confirmed";
            
            _notifier.Send($"Your order #{id} has been confirmed!", "buyer@example.com");
            
            return Ok(order);
        }

        [HttpPatch("{id}/collect")]
        public IActionResult CollectOrder(int id)
        {
            var order = _orderRepo.GetById(id);
            if (order.Status != "Confirmed")
                return BadRequest("Order must be confirmed before collection.");

            var listing = _produceRepo.GetById(order.ListingId);
            
            // State Machine Logic & Quantity Update
            listing.QuantityKg -= order.QuantityOrdered; 
            order.Status = "Collected";

            _notifier.Send($"Order #{id} collected. Thank you!", "buyer@example.com");

            return Ok(new { Message = "Collected", RemainingStock = listing.QuantityKg, Order = order });
        }
    }
}