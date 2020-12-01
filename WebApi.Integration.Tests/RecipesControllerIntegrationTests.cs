using WebAPI;
using DataLayer.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Extensions.Ordering;

[assembly: TestCaseOrderer("Xunit.Extensions.Ordering.TestCaseOrderer", "Xunit.Extensions.Ordering")]

namespace WebApi.Integration.Tests
{
    public class RecipesControllerIntegrationTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;
        public RecipesControllerIntegrationTests(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }


        [Fact, Order(1)]
        public async Task CanGetRecipes()
        {
            var httpResponse = await _client.GetAsync("/recipe/all");
            httpResponse.EnsureSuccessStatusCode();

            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var recipes = JsonConvert.DeserializeObject<IEnumerable<Recipe>>(stringResponse);

            Xunit.Assert.Equal(3, recipes.Count());

            Xunit.Assert.Contains(recipes, m => m.Name == "Lemon Chicken");
            Xunit.Assert.Contains(recipes, m => m.Calories == "350 calories");

            Xunit.Assert.Contains(recipes, m => m.Name == "Easy Potato Soup");
            Xunit.Assert.Contains(recipes, m => m.Calories == "91 calories");
        }
        [Fact, Order(2)]
        public async Task CanGetRecipeById()
        {
            var httpResponse = await _client.GetAsync("recipe/1");
            httpResponse.EnsureSuccessStatusCode();

            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var recipes = JsonConvert.DeserializeObject<Recipe>(stringResponse);

            Xunit.Assert.Equal(1, recipes.Id);
            Xunit.Assert.Equal("Lemon Chicken", recipes.Name);
        }
        [Fact, Order(3)]
        public async Task CanCreateRecipe()
        {
            var recipe = new Recipe
            {
                Name = "Lentil Soup",
                Calories = "315 calories",
                Carbohydrates = "53g",
                Fat = "9g",
                Fiber = "12g",
                Protein = "14g",
                SaturatedFat = "1g",
                Sodium = "2mg",
                Sugar = "5g"
            };
            var json = JsonConvert.SerializeObject(recipe);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            await _client.PostAsync("/recipe/create", content);

            var httpResponse = await _client.GetAsync("/recipe/all");
            httpResponse.EnsureSuccessStatusCode();

            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var recipes = JsonConvert.DeserializeObject<IEnumerable<Recipe>>(stringResponse);

            Xunit.Assert.Equal(4, recipes.Count());
            Xunit.Assert.Contains(recipes, m => m.Name == "Lentil Soup");
        }
        [Fact, Order(4)]
        public async Task CanUpdateById()
        {
            var recipe = new Recipe
            {
                Id = 1,
                Name = "Lemon Chicken UPDATE",
                Calories = "350 calories",
                Carbohydrates = "2mg",
                Fat = "13g",
                Fiber = "0g",
                Protein = "53g",
                SaturatedFat = "2g",
                Sodium = "2mg",
                Sugar = "3g"
            };
            var json = JsonConvert.SerializeObject(recipe);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            await _client.PutAsync("/recipe/update/1", content);

            var httpResponse = await _client.GetAsync("recipe/1");
            httpResponse.EnsureSuccessStatusCode();

            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var recipes = JsonConvert.DeserializeObject<Recipe>(stringResponse);

            Xunit.Assert.Equal(1, recipes.Id);
            Xunit.Assert.Equal("Lemon Chicken UPDATE", recipes.Name);
            Xunit.Assert.Equal("13g", recipes.Fat);

        }
        [Fact, Order(5)]
        public async Task CanDeleteRecipeById()
        {
            await _client.DeleteAsync("recipe/delete/1");
            var httpResponse = await _client.GetAsync("/recipe/all");
            httpResponse.EnsureSuccessStatusCode();

            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var artists = JsonConvert.DeserializeObject<IEnumerable<Recipe>>(stringResponse);

            Xunit.Assert.Equal(3, artists.Count());
        }
       
    }
}
