using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Civica.Api.Migrations
{
    /// <inheritdoc />
    public partial class RemoveApprovedStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Update any existing issues with Status='Approved' to 'Active'
            migrationBuilder.Sql(
                "UPDATE \"Issues\" SET \"Status\" = 'Active' WHERE \"Status\" = 'Approved'");

            // Drop existing filtered indexes
            migrationBuilder.DropIndex(
                name: "IX_Issues_Status_PublicVisibility",
                table: "Issues");

            migrationBuilder.DropIndex(
                name: "IX_Issues_Status_PublicVisibility_CreatedAt",
                table: "Issues");

            // Recreate indexes with 'Active' filter
            migrationBuilder.CreateIndex(
                name: "IX_Issues_Status_PublicVisibility",
                table: "Issues",
                columns: new[] { "Status", "PublicVisibility" },
                filter: "\"Status\" = 'Active' AND \"PublicVisibility\" = true");

            migrationBuilder.CreateIndex(
                name: "IX_Issues_Status_PublicVisibility_CreatedAt",
                table: "Issues",
                columns: new[] { "Status", "PublicVisibility", "CreatedAt" },
                filter: "\"Status\" = 'Active' AND \"PublicVisibility\" = true");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Revert indexes back to 'Approved' filter
            migrationBuilder.DropIndex(
                name: "IX_Issues_Status_PublicVisibility",
                table: "Issues");

            migrationBuilder.DropIndex(
                name: "IX_Issues_Status_PublicVisibility_CreatedAt",
                table: "Issues");

            migrationBuilder.CreateIndex(
                name: "IX_Issues_Status_PublicVisibility",
                table: "Issues",
                columns: new[] { "Status", "PublicVisibility" },
                filter: "\"Status\" = 'Approved' AND \"PublicVisibility\" = true");

            migrationBuilder.CreateIndex(
                name: "IX_Issues_Status_PublicVisibility_CreatedAt",
                table: "Issues",
                columns: new[] { "Status", "PublicVisibility", "CreatedAt" },
                filter: "\"Status\" = 'Approved' AND \"PublicVisibility\" = true");

            // Note: Data migration (Active -> Approved) is not automatically reverted
            // as it could affect issues that were originally Active
        }
    }
}
