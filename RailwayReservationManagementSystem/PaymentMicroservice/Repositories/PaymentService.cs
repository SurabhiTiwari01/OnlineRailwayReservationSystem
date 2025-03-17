using Microsoft.EntityFrameworkCore;
using PaymentMicroservice.Models;
using Stripe;

namespace PaymentMicroservice.Repositories
{
    public class PaymentService
    {
        private readonly IConfiguration _configuration;
        private readonly RailwayReservationManagementSystemContext _context;

        public PaymentService(IConfiguration configuration, RailwayReservationManagementSystemContext context)
        {
            _configuration = configuration;
            _context = context;

            // Initialize Stripe with the secret key
            StripeConfiguration.ApiKey = _configuration["Stripe:SecretKey"];
        }

        public async Task<Payment> ProcessPaymentAsync(Payment payment, string stripeToken)
        {
            // Create a charge using the provided Stripe token
            try
            {
                var options = new ChargeCreateOptions
                {
                    Amount = (long)(payment.Amount+30), // Stripe expects the amount in cents
                    Currency = "Indian", // You can change this depending on your currency
                    Description = "Payment for Order",
                    Source = stripeToken, // The token created by the client-side
                };
                var service = new ChargeService();
                Charge charge = await service.CreateAsync(options);

                // Save payment details to database
                payment.IsSuccessful = charge.Status == "succeeded";
                payment.ReservationId = charge.Id;
                payment.PaymentDate = DateTime.UtcNow;
                payment.ErrorMessage = charge.Status == "succeeded" ? null : "Payment failed.";
                _context.Payments.Add(payment);
                await _context.SaveChangesAsync();

                return payment;
            }
            catch (StripeException ex)
            {
                payment.IsSuccessful = false;
                payment.ErrorMessage = ex.Message;
                _context.Payments.Add(payment);
                await _context.SaveChangesAsync();
                throw new Exception($"Stripe Error: {ex.Message}");
            }
        }

        public async Task<Payment> GetPaymentByIdAsync(int id)
        {
            return await _context.Payments.FindAsync(id);
        }
    }
}
