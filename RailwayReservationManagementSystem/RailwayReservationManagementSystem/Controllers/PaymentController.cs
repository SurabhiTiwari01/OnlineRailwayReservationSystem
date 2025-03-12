using Microsoft.AspNetCore.Mvc;
using RailwayReservationManagementSystem.Interfaces;
using RailwayReservationManagementSystem.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RailwayReservationManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentRepo _paymentRepository;

        public PaymentController(IPaymentRepo paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        // GET: api/Payment/{reservationId}
        [HttpGet("{reservationId}")]
        public async Task<ActionResult<Payment>> GetPaymentByReservationId(int reservationId)
        {
            var payment = await _paymentRepository.GetPaymentByReservationIdAsync(reservationId);
            if (payment == null)
            {
                return NotFound();
            }
            return Ok(payment);
        }

        // POST: api/Payment
        [HttpPost]
        public async Task<ActionResult<Payment>> CreatePayment(Payment payment)
        {
            await _paymentRepository.AddPaymentAsync(payment);
            return CreatedAtAction(nameof(GetPaymentByReservationId), new { reservationId = payment.ReservationId }, payment);
        }

    }
}
