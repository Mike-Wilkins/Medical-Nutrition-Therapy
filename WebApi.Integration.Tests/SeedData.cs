using DataLayer.Models;
using DataLayer.Services;

namespace WebApi.Integration.Tests
{
    public static class SeedData
    {
        public static void PopulateTestData(ApplicationDbContext context)
        {
            context.Recipes.Add(new Recipe { Name = "Lemon Chicken", Calories = "350 calories", Carbohydrates = "2mg", Fat = "13g", Fiber = "0g", Protein = "53g", SaturatedFat = "2g", Sodium = "2mg", Sugar = "3g" });
            context.Recipes.Add(new Recipe { Name = "Easy Potato Soup", Calories = "91 calories", Carbohydrates = "13mg", Fat = "0g", Fiber = "1g", Protein = "10g", SaturatedFat = "0g", Sodium = "3mg", Sugar = "6g" });
            context.Recipes.Add(new Recipe { Name = "Summer Pasta Salad", Calories = "320 calories", Carbohydrates = "19mg", Fat = "9g", Fiber = "5g", Protein = "16g", SaturatedFat = "1g", Sodium = "4mg", Sugar = "7g" });

            context.SaveChanges();
        }
    }
}
