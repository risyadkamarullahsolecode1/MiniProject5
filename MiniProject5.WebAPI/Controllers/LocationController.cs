using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniProject5.Domain.Entities;
using MiniProject5.Domain.Interfaces;
using MiniProject5.Infrastucture.Data;

namespace MiniProject5.WebAPI.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly ILocationRepository _locationRepository;

        public LocationController(ILocationRepository locationRepository) 
        {

            _locationRepository = locationRepository;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Location>>> GetAllLocations()
        {
            var Location = await _locationRepository.GetAllLocations();
            return Ok(Location);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Location>>> GetlLocationById(int id)
        {
            var Location = await _locationRepository.GetLocationById(id);
            return Ok(Location);
        }
        [HttpPost]
        public async Task<ActionResult<Location>> AddLocation(Location location)
        {
            var createdLocation = await _locationRepository.AddLocation(location);
            return Ok(createdLocation);
        }
        [HttpPut]
        public async Task<ActionResult<Location>> UpdateLocation(Location location)
        {
            var updatedLocation = await _locationRepository.UpdateLocation(location);
            return Ok(updatedLocation);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteLocation(Location location)
        {
            _locationRepository.DeleteLocation(location);
            return Ok("location has been deleted");
        }
    }
}
