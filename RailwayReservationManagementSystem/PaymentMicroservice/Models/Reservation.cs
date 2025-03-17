using System;
using System.Collections.Generic;

namespace PaymentMicroservice.Models;

public partial class Reservation
{
    public int? PassengerId { get; set; }

    public int? TrainId { get; set; }

    public DateTime ReservationDate { get; set; }

    public decimal TicketFare { get; set; }

    public string Pnrnumber { get; set; } = null!;

    public string Status { get; set; } = null!;

    public string ReservationId { get; set; } = null!;

    public virtual Passenger? Passenger { get; set; }

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual Train? Train { get; set; }
}
