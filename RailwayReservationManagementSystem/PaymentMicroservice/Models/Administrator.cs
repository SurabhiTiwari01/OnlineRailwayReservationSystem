using System;
using System.Collections.Generic;

namespace PaymentMicroservice.Models;

public partial class Administrator
{
    public int AdminId { get; set; }

    public string Username { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;
}
