using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ReservationProject.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    LocationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Alkohols = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LegalProtection = table.Column<bool>(type: "bit", nullable: true),
                    Cuisine = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Decoration = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KindOfRoom = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.LocationID);
                });

            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    ServiceID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PositionWorker = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Competencies = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.ServiceID);
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    EventID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QuantityofPerson = table.Column<int>(type: "int", nullable: false),
                    Motive = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LocationID = table.Column<int>(type: "int", nullable: false),
                    ServiceID = table.Column<int>(type: "int", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ArtShow = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.EventID);
                    table.ForeignKey(
                        name: "FK_Events_Locations_LocationID",
                        column: x => x.LocationID,
                        principalTable: "Locations",
                        principalColumn: "LocationID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Events_Services_ServiceID",
                        column: x => x.ServiceID,
                        principalTable: "Services",
                        principalColumn: "ServiceID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "People",
                columns: table => new
                {
                    OsobaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumberOfPhone = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Discount = table.Column<int>(type: "int", nullable: true),
                    NameOfCompany = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Iban = table.Column<float>(type: "real", nullable: true),
                    Budget = table.Column<float>(type: "real", nullable: true),
                    IndividualNumberofAccount = table.Column<float>(type: "real", nullable: true),
                    ServiceID = table.Column<int>(type: "int", nullable: true),
                    NettoPerHour = table.Column<float>(type: "real", nullable: true),
                    WorkHours = table.Column<int>(type: "int", nullable: true),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_People", x => x.OsobaID);
                    table.ForeignKey(
                        name: "FK_People_Services_ServiceID",
                        column: x => x.ServiceID,
                        principalTable: "Services",
                        principalColumn: "ServiceID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    ReservationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StatusOfReservation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TheDateOfMakeReserve = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TheLastPaymentsDay = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReservationPeriodinDays = table.Column<int>(type: "int", nullable: false),
                    CostPerDay = table.Column<float>(type: "real", nullable: false),
                    EventId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.ReservationID);
                    table.ForeignKey(
                        name: "FK_Reservations_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "EventID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Events_LocationID",
                table: "Events",
                column: "LocationID");

            migrationBuilder.CreateIndex(
                name: "IX_Events_ServiceID",
                table: "Events",
                column: "ServiceID");

            migrationBuilder.CreateIndex(
                name: "IX_People_ServiceID",
                table: "People",
                column: "ServiceID");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_EventId",
                table: "Reservations",
                column: "EventId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "People");

            migrationBuilder.DropTable(
                name: "Reservations");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropTable(
                name: "Services");
        }
    }
}
