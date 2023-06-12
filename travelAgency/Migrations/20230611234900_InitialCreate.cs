using System;
using Microsoft.EntityFrameworkCore.Migrations;
using travelAgency.model;

#nullable disable

namespace travelAgency.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Places",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    Location = table.Column<string>(type: "TEXT", nullable: false),
                    lat = table.Column<double>(type: "REAL", nullable: false),
                    lng = table.Column<double>(type: "REAL", nullable: false),
                    Discriminator = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Places", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Trips",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    Price = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trips", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: false),
                    Surname = table.Column<string>(type: "TEXT", nullable: false),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: true),
                    Auth = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Amenities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    amenity = table.Column<int>(type: "INTEGER", nullable: false),
                    StayId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Amenities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Amenities_Places_StayId",
                        column: x => x.StayId,
                        principalTable: "Places",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ImgLocation = table.Column<string>(type: "TEXT", nullable: false),
                    PlaceId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Images_Places_PlaceId",
                        column: x => x.PlaceId,
                        principalTable: "Places",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TripSchedules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TripId = table.Column<int>(type: "INTEGER", nullable: false),
                    PlaceId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TripSchedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TripSchedules_Places_PlaceId",
                        column: x => x.PlaceId,
                        principalTable: "Places",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TripSchedules_Trips_TripId",
                        column: x => x.TripId,
                        principalTable: "Trips",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Arrangements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NumberOfPersons = table.Column<int>(type: "INTEGER", nullable: false),
                    DateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Price = table.Column<double>(type: "REAL", nullable: false),
                    TripId = table.Column<int>(type: "INTEGER", nullable: false),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    IsReservation = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Arrangements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Arrangements_Trips_TripId",
                        column: x => x.TripId,
                        principalTable: "Trips",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Arrangements_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Amenities_StayId",
                table: "Amenities",
                column: "StayId");

            migrationBuilder.CreateIndex(
                name: "IX_Arrangements_TripId",
                table: "Arrangements",
                column: "TripId");

            migrationBuilder.CreateIndex(
                name: "IX_Arrangements_UserId",
                table: "Arrangements",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Images_PlaceId",
                table: "Images",
                column: "PlaceId");

            migrationBuilder.CreateIndex(
                name: "IX_TripSchedules_PlaceId",
                table: "TripSchedules",
                column: "PlaceId");

            migrationBuilder.CreateIndex(
                name: "IX_TripSchedules_TripId",
                table: "TripSchedules",
                column: "TripId");
            UserType userType = UserType.Agent;
            int authValue = (int)userType;
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Name", "Surname", "Email", "Password", "Auth" },
                values: new object[] { "Nikola", "Savic", "ns@gmail.com", "sifra", authValue });
            migrationBuilder.InsertData(
                table: "Places",
                columns: new[] { "Name", "Description", "Location", "lat", "lng", "Discriminator" },
                values: new object[] { "Sabac", "Najlepsi grad", "Macva", 44.75, 19.60, "Attraction" });

            migrationBuilder.InsertData(
                table: "Places",
                columns: new[] { "Name", "Description", "Location", "lat", "lng", "Discriminator" },
                values: new object[] { "Novi Sad", "Srpska Atina", "Vojvodina", 45.75, 19.60, "Attraction" });

            migrationBuilder.InsertData(
                table: "Trips",
                columns: new[] { "Name", "Description", "Price" },
                values: new object[] { "Zapadna Srbija", "Tupatu", 2000.0 });

            migrationBuilder.InsertData(
                table: "TripSchedules",
                columns: new[] { "DateTime", "TripId", "PlaceId" },
                values: new object[] { new DateTime(2023, 6, 10, 12, 0, 0), 1, 1 });

            migrationBuilder.InsertData(
                table: "TripSchedules",
                columns: new[] { "DateTime", "TripId", "PlaceId" },
                values: new object[] { new DateTime(2023, 6, 10, 14, 0, 0), 1, 2 });
            migrationBuilder.InsertData(
                table: "Arrangements",
                columns: new[] { "DateTime", "TripId", "NumberOfPersons", "Price", "UserId" , "IsReservation"},
                values: new object[] { new DateTime(2023, 6, 10, 13, 0, 0), 1, 3, 23000, 1, true }
                );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Amenities");

            migrationBuilder.DropTable(
                name: "Arrangements");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "TripSchedules");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Places");

            migrationBuilder.DropTable(
                name: "Trips");
        }
    }
}
