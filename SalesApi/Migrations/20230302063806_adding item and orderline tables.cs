using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SalesApi.Migrations
{
    public partial class addingitemandorderlinetables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orderlines_Orders_OrderId",
                table: "Orderlines");

            migrationBuilder.AlterColumn<int>(
                name: "OrderId",
                table: "Orderlines",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ItemId",
                table: "Orderlines",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Orderlines",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(9,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orderlines_ItemId",
                table: "Orderlines",
                column: "ItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orderlines_Items_ItemId",
                table: "Orderlines",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orderlines_Orders_OrderId",
                table: "Orderlines",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orderlines_Items_ItemId",
                table: "Orderlines");

            migrationBuilder.DropForeignKey(
                name: "FK_Orderlines_Orders_OrderId",
                table: "Orderlines");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Orderlines_ItemId",
                table: "Orderlines");

            migrationBuilder.DropColumn(
                name: "ItemId",
                table: "Orderlines");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Orderlines");

            migrationBuilder.AlterColumn<int>(
                name: "OrderId",
                table: "Orderlines",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Orderlines_Orders_OrderId",
                table: "Orderlines",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id");
        }
    }
}
