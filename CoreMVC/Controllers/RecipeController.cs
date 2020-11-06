using DataLayer.Models;
using DataLayer.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoreMVC.Controllers
{
    public class RecipeController : Controller
    {
        private IRecipeRepository _db;
        public RecipeController(IRecipeRepository db)
        {
            _db = db;
        }
        public async Task<IActionResult> Index(int id)
        {
            ViewBag.DietId = id;
            var dietType = _db.GetDietType(id);
            ViewBag.DietType = dietType.Name;
            var recipeList = await _db.GetAllRecipes(id);

            return View(recipeList);
        }
        //GET: Recipe/Create
        public IActionResult Create(int id)
        {
            ViewBag.DietId = id;
            var model = new Recipe();
            model.Items.Add(new RecipeItem());
            var dietType = _db.GetDietType(id);
            ViewBag.DietType = dietType.Name;
            model.DietType = dietType.Name;
            model.DietId = id;

            return View(model);
        }

        //POST: Recipe/Create
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Name, Items, Calories, Carbohydrates, Fat, Fiber, Protein, SaturatedFat, Sodium, Sugar, DietId, DietType")] Recipe recipe, int id)
        {
            ViewBag.DietType = recipe.DietType;
            recipe.DietId = id;
            ViewBag.DietId = id;
            await _db.Add(recipe);
            var recipeList = await _db.GetAllRecipes(recipe.DietId);

            return View("Index", recipeList);
        }

        [HttpPost]
        public ActionResult AddRecipeItem([Bind("Items")] Recipe recipe)
        {
            recipe.Items.Add(new RecipeItem());
            return PartialView("RecipeItems", recipe);
        }

        //GET: Recipe/Delete
        public async Task<IActionResult> Delete(int id)
        {
            
            var recipe = await _db.GetRecipe(id);
            ViewBag.DietId = recipe.DietId;
            return View(recipe);
        }
        //POST: Recipe/Delete
        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteRecipe(int id)
        {
            var recipe = await _db.GetRecipe(id);
            ViewBag.DietId = recipe.DietId;
            await _db.Delete(id);
            var recipeList = await _db.GetAllRecipes(recipe.DietId);
            return View("Index", recipeList);
        }

        //GET: Recipe/Edit
        public async Task<IActionResult> Edit(int id)
        {
            var recipe = await _db.GetRecipe(id);
            ViewBag.DietId = recipe.DietId;
            return View(recipe);
        }
        //POST: Recipe/Edit
        [HttpPost]
        public async Task<IActionResult> Edit(Recipe recipe)
        {
            ViewBag.DietId = recipe.DietId;
            _db.Update(recipe);
            var recipeList = await _db.GetAllRecipes(recipe.DietId);
            return View("Index", recipeList);
        }
    }
}
