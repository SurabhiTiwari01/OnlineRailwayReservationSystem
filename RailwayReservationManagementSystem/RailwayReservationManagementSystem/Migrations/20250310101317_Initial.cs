using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RailwayReservationManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Administrator",
                columns: table => new
                {
                    AdminID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Administ__719FE4E88105E8D7", x => x.AdminID);
                });

            migrationBuilder.CreateTable(
                name: "Passenger",
                columns: table => new
                {
                    PassengerID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Passenge__88915F9013B7F521", x => x.PassengerID);
                });

            migrationBuilder.CreateTable(
                name: "Train",
                columns: table => new
                {
                    TrainID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrainName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ArrivalTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    DepartureTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    SourceStation = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DestinationStation = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TicketFare = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Train__8ED2725ADC27AEF5", x => x.TrainID);
                });

            migrationBuilder.CreateTable(
                name: "Reservation",
                columns: table => new
                {
                    ReservationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PassengerID = table.Column<int>(type: "int", nullable: true),
                    TrainID = table.Column<int>(type: "int", nullable: true),
                    ReservationDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    TicketFare = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    PNRNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Reservat__B7EE5F04912FB8DF", x => x.ReservationID);
                    table.ForeignKey(
                        name: "FK__Reservati__Passe__32E0915F",
                        column: x => x.PassengerID,
                        principalTable: "Passenger",
                        principalColumn: "PassengerID");
                    table.ForeignKey(
                        name: "FK__Reservati__Train__33D4B598",
                        column: x => x.TrainID,
                        principalTable: "Train",
                        principalColumn: "TrainID");
                });

            migrationBuilder.CreateTable(
                name: "Payment",
                columns: table => new
                {
                    PaymentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReservationID = table.Column<int>(type: "int", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    PaymentStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Payment__9B556A58DA11BA79", x => x.PaymentID);
                    table.ForeignKey(
                        name: "FK__Payment__Reserva__36B12243",
                        column: x => x.ReservationID,
                        principalTable: "Reservation",
                        principalColumn: "ReservationID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Payment_ReservationID",
                table: "Payment",
                column: "ReservationID");

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_PassengerID",
                table: "Reservation",
                column: "PassengerID");

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_TrainID",
                table: "Reservation",
                column: "TrainID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Administrator");

            migrationBuilder.DropTable(
                name: "Payment");

            migrationBuilder.DropTable(
                name: "Reservation");

            migrationBuilder.DropTable(
                name: "Passenger");

            migrationBuilder.DropTable(
                name: "Train");
        }
    }
}
