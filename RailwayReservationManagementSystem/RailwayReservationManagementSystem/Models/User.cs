using System;
using System.Collections.Generic;

namespace RailwayReservationManagementSystem.Models;

public partial class User
{
    public int UserId { get; set; }

    public string Username { get; set; } = null!;

    public string? PasswordHash { get; set; }

    public string? Role { get; set; }
}
