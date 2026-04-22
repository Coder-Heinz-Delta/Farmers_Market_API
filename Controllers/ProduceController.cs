using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Farmers_Market_API.Models;
using Microsoft.AspNetCore.Mvc;
using Farmers_Market_API.Enums;
using Farmers_Market_API.Repositories;

namespace Farmers_Market_API.Controllers
{
    [ApiController]
[Route("api/[controller]")]
public class ProduceController : ControllerBase
{
    private readonly IProduceRepository _repository;

    public ProduceController(IProduceRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public IActionResult GetProduceListings()
    {
        return Ok(_repository.GetAll());
    }

    [HttpGet("{id}")]
    public IActionResult GetProduceListingById(int id)
    {
        var produce = _repository.GetById(id);
        return produce == null ? NotFound() : Ok(produce);
    }

    [HttpGet("{id}/summary")]
    public IActionResult GetProduceListingSummary(int id)
    {
        var produce = _repository.GetById(id);
        return produce == null ? NotFound() : Ok(produce.GetFormattedSummary());
    }

    [HttpPost]
    public IActionResult CreateProduceListing([FromBody] ProduceListing newListing)
    {
        _repository.Add(newListing);
        return CreatedAtAction(nameof(GetProduceListingById), new { id = newListing.ListingId }, newListing);
    }

    [HttpGet("available")]
    public IActionResult GetAvailableProduce()
    {
        return Ok(_repository.GetAvailable());
    }

    [HttpGet("category/{category}")]
    public IActionResult GetProduceByCategory([FromRoute] Category category)
    {
        return Ok(_repository.GetByCategory(category));
    }
}
}