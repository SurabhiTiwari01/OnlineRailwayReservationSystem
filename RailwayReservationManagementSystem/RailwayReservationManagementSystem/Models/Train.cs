using System;
using System.Collections.Generic;

namespace RailwayReservationManagementSystem.Models;

public partial class Train
{
    public int TrainId { get; set; }

    public string TrainName { get; set; } = null!;

    public DateTime ArrivalTime { get; set; }

    public DateTime DepartureTime { get; set; }

    public string SourceStation { get; set; } = null!;

    public string DestinationStation { get; set; } = null!;

    public decimal TicketFare { get; set; }

    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
}
