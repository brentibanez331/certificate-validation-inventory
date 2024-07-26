using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class AddedCertificateRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Certificate_Event_EventId",
                table: "Certificate");

            migrationBuilder.DropForeignKey(
                name: "FK_Certificate_Organization_IssuedById",
                table: "Certificate");

            migrationBuilder.AddForeignKey(
                name: "FK_Certificate_Event_EventId",
                table: "Certificate",
                column: "EventId",
                principalTable: "Event",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Certificate_Organization_IssuedById",
                table: "Certificate",
                column: "IssuedById",
                principalTable: "Organization",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Certificate_Event_EventId",
                table: "Certificate");

            migrationBuilder.DropForeignKey(
                name: "FK_Certificate_Organization_IssuedById",
                table: "Certificate");

            migrationBuilder.AddForeignKey(
                name: "FK_Certificate_Event_EventId",
                table: "Certificate",
                column: "EventId",
                principalTable: "Event",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Certificate_Organization_IssuedById",
                table: "Certificate",
                column: "IssuedById",
                principalTable: "Organization",
                principalColumn: "Id");
        }
    }
}
