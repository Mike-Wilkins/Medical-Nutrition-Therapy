using DataLayer.Models;
using DataLayer.Repositories;
using DataLayer.Services;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace DataLayer.Tests
{
    public class SQLRecipeTests
    {
        List<Recipe> RecipeInMemoryDb()
        {
            List<Recipe> recipeList = new List<Recipe>()
            {
                new Recipe
                {
                    Id = 1,
                    Name = "Chicken Tamale Pie",
                    Calories = "227 calories",
                    Carbohydrates = "15g",
                    Fat = "4g",
                    Fiber = "1g",
                    Protein = "9g",
                    SaturatedFat = "2g",
                    Sodium = "303mg",
                    Sugar = "2g",
                    DietId = 3
                },
                new Recipe
                {
                    Id =2,
                    Name = "Lemon Chicken",
                    Calories = "350 calories",
                    Carbohydrates = "2g",
                    Fat = "13g",
                    Fiber = "0g",
                    Protein = "53g",
                    SaturatedFat = "2g",
                    Sodium = "360mg",
                    Sugar = "1g",
                    DietId = 1
                },
                new Recipe
                {
                    Id =3,
                    Name = "Easy Potato Soup",
                    Calories = "91 calories",
                    Carbohydrates = "13g",
                    Fat = "0g",
                    Fiber = "1g",
                    Protein = "10g",
                    SaturatedFat = "0g",
                    Sodium = "83mg",
                    Sugar = "1g",
                    DietId = 1
                }
            };
            return recipeList;
        }

        [Fact]
        public async void SQLRecipeRepository_SHould_Add_Recipe()
        {
            IRecipeRepository sut = GetInMemoryRecipeRepository();
            List<Recipe> recipe = RecipeInMemoryDb();

            Recipe savedRecipe1 = await sut.Add(recipe[0]);
            Recipe savedRecipe2 = await sut.Add(recipe[1]);
            Recipe savedRecipe3 = await sut.Add(recipe[2]);

            var getAllTest1 = await sut.GetAllRecipes(recipe[0].DietId);
            var getAllTest2 = await sut.GetAllRecipes(recipe[1].DietId);

            Assert.Single(getAllTest1);
            Assert.Equal(2, getAllTest2.Count());
            Assert.Equal("Chicken Tamale Pie", savedRecipe1.Name);
            Assert.Equal("Lemon Chicken", savedRecipe2.Name);
            Assert.Equal("Easy Potato Soup", savedRecipe3.Name);
        }

        [Fact]
        public async void SQLRecipeRepository_Should_Delete_Recipe_By_Id()
        {
            IRecipeRepository sut = GetInMemoryRecipeRepository();
            List<Recipe> recipe = RecipeInMemoryDb();

            Recipe savedRecipe1 = await sut.Add(recipe[1]);
            await sut.Add(recipe[2]);

            await sut.Delete(savedRecipe1.Id);
            Assert.Single(await sut.GetAllRecipes(recipe[1].DietId));
        }

        [Fact]
        public async void SQLRecipeRepository_Should_Get_Recipe_By_Id()
        {
            IRecipeRepository sut = GetInMemoryRecipeRepository();
            List<Recipe> recipe = RecipeInMemoryDb();

            Recipe savedRecipe1 = await sut.Add(recipe[0]);
            await sut.Add(recipe[1]);

            var getRecipe = await sut.GetRecipe(savedRecipe1.Id);

            Assert.Single(await sut.GetAllRecipes(recipe[0].DietId));
            Assert.Equal(1, getRecipe.Id);
            Assert.Equal("Chicken Tamale Pie", getRecipe.Name);
        }

        [Fact]
        public async void SQLRecipeRepository_Should_Update_By_Id()
        {
            IRecipeRepository sut = GetInMemoryRecipeRepository();
            List<Recipe> recipe = RecipeInMemoryDb();

            Recipe savedRecipe1 = await sut.Add(recipe[0]);

            Assert.Equal(1, savedRecipe1.Id);
            Assert.Equal("Chicken Tamale Pie", savedRecipe1.Name);

            savedRecipe1.Name = "Chicken tamale Pie EDITED";

            var updateRecipe = sut.Update(savedRecipe1);
            var getUpdateRecipe = await sut.GetRecipe(updateRecipe.Id);

            Assert.Equal(1, getUpdateRecipe.Id);
            Assert.Equal("Chicken tamale Pie EDITED", getUpdateRecipe.Name);
        }

        private IRecipeRepository GetInMemoryRecipeRepository()
        {
            DbContextOptions<ApplicationDbContext> options;
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            builder.UseInMemoryDatabase(databaseName: "RecipeDatabase");

            options = builder.Options;
            ApplicationDbContext recipeDataContext = new ApplicationDbContext(options);
            recipeDataContext.Database.EnsureDeleted();
            recipeDataContext.Database.EnsureCreated();

            return new SQLRecipeRepository(recipeDataContext);
        }
    }

}
