using System;
using System.Collections.Generic;

namespace PaymentMicroservice.Models;

public partial class Payment
{
    public int PaymentId { get; set; }

    public string? ReservationId { get; set; }

    public decimal Amount { get; set; }

    public string PaymentStatus { get; set; } = null!;

    public string? PaymentMethod { get; set; }

    public DateTime? PaymentDate { get; set; }

    public bool? IsSuccessful { get; set; }

    public string? ErrorMessage { get; set; }

    public virtual Reservation? Reservation { get; set; }
    //public string Id { get; internal set; }
}
