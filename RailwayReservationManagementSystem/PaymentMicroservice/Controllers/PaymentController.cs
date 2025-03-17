using Microsoft.AspNetCore.Mvc;
using PaymentMicroservice.Models;
using PaymentMicroservice.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PaymentMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly PaymentService _paymentService;

        public PaymentController(PaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        // POST: api/payment/process
        [HttpPost("process")]
        public async Task<IActionResult> ProcessPayment([FromBody] Payment payment, [FromQuery] string stripeToken)
        {
            if (payment == null || string.IsNullOrEmpty(stripeToken))
            {
                return BadRequest("Invalid payment details or missing token.");
            }

            // Call the payment service to process payment
            var processedPayment = await _paymentService.ProcessPaymentAsync(payment, stripeToken);

            if ((bool)processedPayment.IsSuccessful)
            {
                return Ok(new
                {
                    Message = "Payment successful",
                    TransactionId = processedPayment.ReservationId,
                    Amount = processedPayment.Amount,
                    PaymentDate = processedPayment.PaymentDate
                });
            }

            return BadRequest(new
            {
                Message = "Payment failed",
                ErrorMessage = processedPayment.ErrorMessage
            });
        }

        //GET: api/payment/status/{id}
        [HttpGet("status/{id}")]
        public async Task<IActionResult> GetPaymentStatus(int id)
        {
            var payment = await _paymentService.GetPaymentByIdAsync(id);
            if (payment == null)
            {
                return NotFound("Payment not found.");
            }

            return Ok(new
            {
                PaymentId = payment.PaymentId,
                Amount = payment.Amount,
                PaymentMethod = payment.PaymentMethod,
                ReservationId = payment.ReservationId,
                PaymentDate = payment.PaymentDate,
                IsSuccessful = payment.IsSuccessful,
                ErrorMessage = payment.ErrorMessage
            });
        }
    }
}
