using DataLayer.Models;
using DataLayer.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repositories
{
    public class SQLRecipeRepository : IRecipeRepository
    {
        private IApplicationDbContext _context;
        public SQLRecipeRepository(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Recipe> Add(Recipe recipe)
        {
            _context.Recipes.Add(recipe);
            await _context.SaveChangesAsync();
            return recipe;
        }

        public async Task<Recipe> Delete(int id)
        {
            var recipe = await _context.Recipes.Where(m => m.Id == id).FirstOrDefaultAsync();
            _context.Recipes.Remove(recipe);
            await _context.SaveChangesAsync();
            return recipe;
        }

        public async Task<IEnumerable<Recipe>> GetAllRecipes(int id)
        {
            var recipeList = await _context.Recipes.Include(m => m.Items).Where(m => m.DietId == id).ToListAsync();
            return recipeList;
        }

        public DietType GetDietType(int id)
        {
            var dietType = _context.DietTypes.Where(m => m.Id == id).FirstOrDefault();
            return dietType;
        }

        public async Task<Recipe> GetRecipe(int id)
        {
            var recipe = await _context.Recipes.Where(m => m.Id == id).FirstOrDefaultAsync();
            return recipe;
        }

        public Recipe Update(Recipe recipe)
        {
           var recipeUpdate = _context.Recipes.Attach(recipe);
            recipeUpdate.State = EntityState.Modified;
            _context.SaveChanges();
            return recipe;
        }

    }
}
