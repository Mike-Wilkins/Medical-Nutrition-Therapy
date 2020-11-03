using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class RecipeItemAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecipeItems_Recipes_RecipeId",
                table: "RecipeItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RecipeItems",
                table: "RecipeItems");

            migrationBuilder.RenameTable(
                name: "RecipeItems",
                newName: "Items");

            migrationBuilder.RenameIndex(
                name: "IX_RecipeItems_RecipeId",
                table: "Items",
                newName: "IX_Items_RecipeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Items",
                table: "Items",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Recipes_RecipeId",
                table: "Items",
                column: "RecipeId",
                principalTable: "Recipes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_Recipes_RecipeId",
                table: "Items");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Items",
                table: "Items");

            migrationBuilder.RenameTable(
                name: "Items",
                newName: "RecipeItems");

            migrationBuilder.RenameIndex(
                name: "IX_Items_RecipeId",
                table: "RecipeItems",
                newName: "IX_RecipeItems_RecipeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RecipeItems",
                table: "RecipeItems",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeItems_Recipes_RecipeId",
                table: "RecipeItems",
                column: "RecipeId",
                principalTable: "Recipes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
