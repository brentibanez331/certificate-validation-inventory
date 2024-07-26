using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class RemovedIssuedBy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Certificate_Organization_IssuedById",
                table: "Certificate");

            migrationBuilder.DropIndex(
                name: "IX_Certificate_IssuedById",
                table: "Certificate");

            migrationBuilder.DropColumn(
                name: "IssuedById",
                table: "Certificate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IssuedById",
                table: "Certificate",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Certificate_IssuedById",
                table: "Certificate",
                column: "IssuedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Certificate_Organization_IssuedById",
                table: "Certificate",
                column: "IssuedById",
                principalTable: "Organization",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
