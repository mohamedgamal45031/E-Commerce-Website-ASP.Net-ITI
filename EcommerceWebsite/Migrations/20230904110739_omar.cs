using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcommerceWebsite.Migrations
{
    /// <inheritdoc />
    public partial class omar : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image2",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder) 
        {
            migrationBuilder.DropColumn(
                name: "Image2",
                table: "Products");
        }
    }
}
