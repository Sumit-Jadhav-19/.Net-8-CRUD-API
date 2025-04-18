using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DotNetCore_CRUD_API.Migrations
{
    /// <inheritdoc />
    public partial class changeInProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "products",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "products");
        }
    }
}
