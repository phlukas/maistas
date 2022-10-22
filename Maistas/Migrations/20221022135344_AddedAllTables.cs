using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Maistas.Migrations
{
    public partial class AddedAllTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dishes_Category_CategoryId",
                table: "Dishes");

            migrationBuilder.DropForeignKey(
                name: "FK_DishIngredient_Dishes_DishId",
                table: "DishIngredient");

            migrationBuilder.DropForeignKey(
                name: "FK_DishIngredient_Ingredient_IngredientId",
                table: "DishIngredient");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ingredient",
                table: "Ingredient");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DishIngredient",
                table: "DishIngredient");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Category",
                table: "Category");

            migrationBuilder.RenameTable(
                name: "Ingredient",
                newName: "Ingredients");

            migrationBuilder.RenameTable(
                name: "DishIngredient",
                newName: "DishIngredients");

            migrationBuilder.RenameTable(
                name: "Category",
                newName: "Categories");

            migrationBuilder.RenameIndex(
                name: "IX_DishIngredient_IngredientId",
                table: "DishIngredients",
                newName: "IX_DishIngredients_IngredientId");

            migrationBuilder.RenameIndex(
                name: "IX_DishIngredient_DishId",
                table: "DishIngredients",
                newName: "IX_DishIngredients_DishId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ingredients",
                table: "Ingredients",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DishIngredients",
                table: "DishIngredients",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categories",
                table: "Categories",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Dishes_Categories_CategoryId",
                table: "Dishes",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DishIngredients_Dishes_DishId",
                table: "DishIngredients",
                column: "DishId",
                principalTable: "Dishes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DishIngredients_Ingredients_IngredientId",
                table: "DishIngredients",
                column: "IngredientId",
                principalTable: "Ingredients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dishes_Categories_CategoryId",
                table: "Dishes");

            migrationBuilder.DropForeignKey(
                name: "FK_DishIngredients_Dishes_DishId",
                table: "DishIngredients");

            migrationBuilder.DropForeignKey(
                name: "FK_DishIngredients_Ingredients_IngredientId",
                table: "DishIngredients");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ingredients",
                table: "Ingredients");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DishIngredients",
                table: "DishIngredients");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Categories",
                table: "Categories");

            migrationBuilder.RenameTable(
                name: "Ingredients",
                newName: "Ingredient");

            migrationBuilder.RenameTable(
                name: "DishIngredients",
                newName: "DishIngredient");

            migrationBuilder.RenameTable(
                name: "Categories",
                newName: "Category");

            migrationBuilder.RenameIndex(
                name: "IX_DishIngredients_IngredientId",
                table: "DishIngredient",
                newName: "IX_DishIngredient_IngredientId");

            migrationBuilder.RenameIndex(
                name: "IX_DishIngredients_DishId",
                table: "DishIngredient",
                newName: "IX_DishIngredient_DishId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ingredient",
                table: "Ingredient",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DishIngredient",
                table: "DishIngredient",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Category",
                table: "Category",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Dishes_Category_CategoryId",
                table: "Dishes",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DishIngredient_Dishes_DishId",
                table: "DishIngredient",
                column: "DishId",
                principalTable: "Dishes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DishIngredient_Ingredient_IngredientId",
                table: "DishIngredient",
                column: "IngredientId",
                principalTable: "Ingredient",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
