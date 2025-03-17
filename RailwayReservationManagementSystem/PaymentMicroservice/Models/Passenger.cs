using System;
using System.Collections.Generic;

namespace PaymentMicroservice.Models;

public partial class Passenger
{
    public int PassengerId { get; set; }

    public string Name { get; set; } = null!;

    public int Age { get; set; }

    public string Address { get; set; } = null!;

    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
}
