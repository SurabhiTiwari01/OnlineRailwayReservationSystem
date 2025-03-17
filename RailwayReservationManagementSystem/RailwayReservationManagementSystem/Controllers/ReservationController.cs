using Microsoft.AspNetCore.Mvc;
using RailwayReservationManagementSystem.Interfaces;
using RailwayReservationManagementSystem.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RailwayReservationManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationRepo _reservationRepository;

        public ReservationController(IReservationRepo reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }

        // GET: api/Reservation/{pnr}
        [HttpGet("{pnr}")]
        public async Task<ActionResult<Reservation>> GetReservationByPnr(string pnr)
        {
            var reservation = await _reservationRepository.GetReservationByPNRAsync(pnr);
            if (reservation == null)
            {
                return NotFound();
            }
            return Ok(reservation);
        }

        // POST: api/Reservation
        [HttpPost]
        public async Task<ActionResult<Reservation>> CreateReservation(Reservation reservation)
        {
            await _reservationRepository.AddReservationAsync(reservation);
            return CreatedAtAction(nameof(GetReservationByPnr), new { pnr = reservation.Pnrnumber }, reservation);
        }

        // PUT: api/Reservation/{pnr}
        [HttpPut("{pnr}")]
        public async Task<IActionResult> UpdateReservation(string pnr, Reservation reservation)
        {
            if (pnr != reservation.Pnrnumber)
            {
                return BadRequest();
            }

            await _reservationRepository.UpdateReservationAsync(reservation);
            return NoContent();
        }

        // DELETE: api/Reservation/{pnr}
        [HttpDelete("{pnr}")]
        public async Task<IActionResult> CancelReservation(string pnr)
        {
            await _reservationRepository.CancelReservationAsync(pnr);
            return NoContent();
        }

    }
}
