using DataLayer.Models;
using DataLayer.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("recipe")]
    public class RecipeController : ControllerBase
    {
        private IApplicationDbContext _context;
        public RecipeController(IApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("all")]
        public ActionResult<IEnumerable<Recipe>> GetAllNutrition()
        {
            var recipeList = _context.Recipes.Include(m => m.Items).ToList();
            return recipeList;
        }

        [HttpGet("{id}")]
        public ActionResult<Recipe> GetRecipe(int id)
        {
            var recipe = _context.Recipes.Where(m => m.Id == id).Include(m => m.Items).FirstOrDefault();
            return recipe;
        }

        [HttpPost("create")]
        public ActionResult<Recipe> CreateRecipe(Recipe recipe)
        {
            _context.Recipes.Add(recipe);
            _context.SaveChanges();
            return recipe;
        }

        [HttpPost("delete/{id}")]
        public ActionResult<Recipe> DeleteRecipe(int id)
        {
            var recipe = _context.Recipes.Where(m => m.Id == id).FirstOrDefault();
            _context.Recipes.Remove(recipe);
            _context.SaveChanges();
            return recipe;
        }

        [HttpPut("update/{id}")]
        public ActionResult<Recipe> UpdateRecipe(int id, Recipe recipeUpdate)
        {
            if (id != recipeUpdate.Id)
            {
                return BadRequest();
            }

            var recipe = _context.Recipes.Attach(recipeUpdate);
            recipe.State = EntityState.Modified;
            _context.SaveChanges();
            return recipeUpdate;
        }

        [HttpGet("ingredients")]
        public ActionResult<IEnumerable<string>> GetAllIngredients()
        {
            var recipeList = _context.Recipes.Include(m => m.Items).ToList();
            var id = recipeList[0].Id;
            var name = recipeList[0].Name;
            var result = recipeList[0].Items;
            var resultCount = result.Count;

            var recipe = new List<string>();
            recipe.Add(name);

            foreach (var item in result)
            {
                recipe.Add(item.Ingredient);
            }

            var num = recipeList.Count;

            return recipe;
        }
    }
}
