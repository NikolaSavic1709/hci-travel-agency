using Microsoft.EntityFrameworkCore.Migrations;

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
                name: "Journeys",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Journeys", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Places",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Places", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "JourneyPlace",
                columns: table => new
                {
                    JourneyId = table.Column<int>(type: "INTEGER", nullable: false),
                    PlaceId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JourneyPlace", x => new { x.JourneyId, x.PlaceId });
                    table.ForeignKey(
                        name: "FK_JourneyPlace_Journeys_JourneyId",
                        column: x => x.JourneyId,
                        principalTable: "Journeys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JourneyPlace_Places_PlaceId",
                        column: x => x.PlaceId,
                        principalTable: "Places",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JourneyPlace_PlaceId",
                table: "JourneyPlace",
                column: "PlaceId");
            migrationBuilder.InsertData(
                table: "Places",
                columns: new[] { "Name", "Description" },
                values: new object[] { "Sabac", "opis sapca" });
            migrationBuilder.InsertData(
                table: "Places",
                columns: new[] { "Name", "Description" },
                values: new object[] { "Novi Sad", "opis ns" });
            migrationBuilder.InsertData(
                table: "Journeys",
                columns: new[] { "Name", "Description" },
                values: new object[] { "Tura gore dole", "turatura" });
            migrationBuilder.InsertData(
                table: "JourneyPlace",
                columns: new[] { "JourneyId", "PlaceId" },
                values: new object[] { "1", "1" });
            migrationBuilder.InsertData(
                table: "JourneyPlace",
                columns: new[] { "JourneyId", "PlaceId" },
                values: new object[] { "1", "2" });

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JourneyPlace");

            migrationBuilder.DropTable(
                name: "Journeys");

            migrationBuilder.DropTable(
                name: "Places");
        }
    }
}
