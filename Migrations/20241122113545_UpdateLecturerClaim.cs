using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContractPoe.Migrations
{
    /// <inheritdoc />
    public partial class UpdateLecturerClaim : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "LecturerClaims",
                newName: "ClaimId");

            migrationBuilder.AlterColumn<string>(
                name: "DocumentPath",
                table: "LecturerClaims",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "AdditionalNotes",
                table: "LecturerClaims",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ClaimId",
                table: "LecturerClaims",
                newName: "Id");

            migrationBuilder.AlterColumn<string>(
                name: "DocumentPath",
                table: "LecturerClaims",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AdditionalNotes",
                table: "LecturerClaims",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
