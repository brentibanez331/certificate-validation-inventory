using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class AddedOrganizerUserToEvent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrganizerUserId",
                table: "Event",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Event_OrganizerUserId",
                table: "Event",
                column: "OrganizerUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Event_User_OrganizerUserId",
                table: "Event",
                column: "OrganizerUserId",
                principalTable: "User",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Event_User_OrganizerUserId",
                table: "Event");

            migrationBuilder.DropIndex(
                name: "IX_Event_OrganizerUserId",
                table: "Event");

            migrationBuilder.DropColumn(
                name: "OrganizerUserId",
                table: "Event");
        }
    }
}
