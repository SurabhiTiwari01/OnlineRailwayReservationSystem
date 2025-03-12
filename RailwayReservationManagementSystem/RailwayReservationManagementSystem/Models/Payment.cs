using System;
using System.Collections.Generic;

namespace RailwayReservationManagementSystem.Models;

public partial class Payment
{
    public int PaymentId { get; set; }

    public int? ReservationId { get; set; }

    public decimal Amount { get; set; }

    public string PaymentStatus { get; set; } = null!;

    public virtual Reservation? Reservation { get; set; }
}
