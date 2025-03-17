using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PaymentMicroservice.Models;

public partial class RailwayReservationManagementSystemContext : DbContext
{
    public RailwayReservationManagementSystemContext()
    {
    }

    public RailwayReservationManagementSystemContext(DbContextOptions<RailwayReservationManagementSystemContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Administrator> Administrators { get; set; }

    public virtual DbSet<Passenger> Passengers { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Reservation> Reservations { get; set; }

    public virtual DbSet<Train> Trains { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("server=(localdb)\\MsSqlLocalDb;Trusted_Connection=True;Database=RailwayReservationManagementSystem;TrustServerCertificate=yes");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Administrator>(entity =>
        {
            entity.HasKey(e => e.AdminId).HasName("PK__Administ__719FE4E88105E8D7");

            entity.ToTable("Administrator");

            entity.Property(e => e.AdminId).HasColumnName("AdminID");
            entity.Property(e => e.PasswordHash).HasMaxLength(255);
            entity.Property(e => e.Username).HasMaxLength(50);
        });

        modelBuilder.Entity<Passenger>(entity =>
        {
            entity.HasKey(e => e.PassengerId).HasName("PK__Passenge__88915F9013B7F521");

            entity.ToTable("Passenger");

            entity.Property(e => e.PassengerId).HasColumnName("PassengerID");
            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("PK__Payment__9B556A58DA11BA79");

            entity.ToTable("Payment");

            entity.Property(e => e.PaymentId).HasColumnName("PaymentID");
            entity.Property(e => e.Amount).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.ErrorMessage)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.PaymentDate).HasColumnType("datetime");
            entity.Property(e => e.PaymentMethod)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.PaymentStatus).HasMaxLength(50);
            entity.Property(e => e.ReservationId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("ReservationID");

            entity.HasOne(d => d.Reservation).WithMany(p => p.Payments)
                .HasForeignKey(d => d.ReservationId)
                .HasConstraintName("FK__Payment");
        });

        modelBuilder.Entity<Reservation>(entity =>
        {
            entity.ToTable("Reservation");

            entity.Property(e => e.ReservationId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("ReservationID");
            entity.Property(e => e.PassengerId).HasColumnName("PassengerID");
            entity.Property(e => e.Pnrnumber)
                .HasMaxLength(50)
                .HasColumnName("PNRNumber");
            entity.Property(e => e.ReservationDate).HasColumnType("datetime");
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.TicketFare).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.TrainId).HasColumnName("TrainID");

            entity.HasOne(d => d.Passenger).WithMany(p => p.Reservations)
                .HasForeignKey(d => d.PassengerId)
                .HasConstraintName("FK__Reservati__Passe__32E0915F");

            entity.HasOne(d => d.Train).WithMany(p => p.Reservations)
                .HasForeignKey(d => d.TrainId)
                .HasConstraintName("FK__Reservati__Train__33D4B598");
        });

        modelBuilder.Entity<Train>(entity =>
        {
            entity.HasKey(e => e.TrainId).HasName("PK__Train__8ED2725ADC27AEF5");

            entity.ToTable("Train");

            entity.Property(e => e.TrainId).HasColumnName("TrainID");
            entity.Property(e => e.ArrivalTime).HasColumnType("datetime");
            entity.Property(e => e.DepartureTime).HasColumnType("datetime");
            entity.Property(e => e.DestinationStation).HasMaxLength(100);
            entity.Property(e => e.SourceStation).HasMaxLength(100);
            entity.Property(e => e.TicketFare).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.TrainName).HasMaxLength(100);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CC4C28367BF1");

            entity.Property(e => e.UserId).ValueGeneratedNever();
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(8)
                .IsUnicode(false);
            entity.Property(e => e.Role)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Username)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
