using Microsoft.AspNetCore.Mvc;
using RailwayReservationManagementSystem.Interfaces;
using RailwayReservationManagementSystem.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RailwayReservationManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PassengerController : ControllerBase
    {
        private readonly IPassengerRepo _passengerRepository;

        public PassengerController(IPassengerRepo passengerRepository)
        {
            _passengerRepository = passengerRepository;
        }
        // GET: api/Passenger
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Passenger>>> GetAllPassengers()
        {
            var passengers = await _passengerRepository.GetAllPassengersAsync();
            return Ok(passengers);
        }

        // GET: api/Passenger/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Passenger>> GetPassengerById(int id)
        {
            var passenger = await _passengerRepository.GetPassengerByIdAsync(id);
            if (passenger == null)
            {
                return NotFound();
            }
            return Ok(passenger);
        }

        // POST: api/Passenger
        [HttpPost]
        public async Task<ActionResult<Passenger>> CreatePassenger(Passenger passenger)
        {
            await _passengerRepository.AddPassengerAsync(passenger);
            return CreatedAtAction(nameof(GetPassengerById), new { id = passenger.PassengerId }, passenger);
        }

        // PUT: api/Passenger/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePassenger(int id, Passenger passenger)
        {
            if (id != passenger.PassengerId)
            {
                return BadRequest();
            }

            await _passengerRepository.UpdatePassengerAsync(passenger);
            return NoContent();
        }

        // DELETE: api/Passenger/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePassenger(int id)
        {
            await _passengerRepository.DeletePassengerAsync(id);
            return NoContent();
        }
    }

}

