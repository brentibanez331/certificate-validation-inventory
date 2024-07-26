using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class CombinedEventDetailToEvent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Event_EventDetail_EventDetailId",
                table: "Event");

            migrationBuilder.DropTable(
                name: "EventDetail");

            migrationBuilder.DropIndex(
                name: "IX_Event_EventDetailId",
                table: "Event");

            migrationBuilder.DropColumn(
                name: "EventDetailId",
                table: "Event");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDateTime",
                table: "Event",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "EventDescription",
                table: "Event",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDateTime",
                table: "Event",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Venue",
                table: "Event",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndDateTime",
                table: "Event");

            migrationBuilder.DropColumn(
                name: "EventDescription",
                table: "Event");

            migrationBuilder.DropColumn(
                name: "StartDateTime",
                table: "Event");

            migrationBuilder.DropColumn(
                name: "Venue",
                table: "Event");

            migrationBuilder.AddColumn<int>(
                name: "EventDetailId",
                table: "Event",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "EventDetail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EndDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    venue = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventDetail", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Event_EventDetailId",
                table: "Event",
                column: "EventDetailId");

            migrationBuilder.AddForeignKey(
                name: "FK_Event_EventDetail_EventDetailId",
                table: "Event",
                column: "EventDetailId",
                principalTable: "EventDetail",
                principalColumn: "Id");
        }
    }
}
