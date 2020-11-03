using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer;
using DataLayer.Models;
using DataLayer.Repositories;
using DataLayer.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CoreMVC.Controllers
{
    public class RecipeController : Controller
    {
        private IRecipeRepository _db;
        public RecipeController(IRecipeRepository db)
        {
            _db = db;
        }
        public async Task<IActionResult> Index()
        {
            var recipeList = await _db.GetAllRecipes();
           
            return View(recipeList);
        }
        //GET: Recipe/Create
        public IActionResult Create()
        {
            var model = new Recipe();
            model.Items.Add(new RecipeItem());

            return View(model);
        }

        //POST: Recipe/Create
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Name, Items, Calories, Carbohydrates, Fat, Fiber, Protein, SaturatedFat, Sodium, Sugar, DietId, DietType")] Recipe recipe)
        {
            await _db.Add(recipe);
            var recipeList = await _db.GetAllRecipes();
            return View("Index", recipeList);
        }

        [HttpPost]
        public ActionResult AddRecipeItem([Bind("Items")] Recipe recipe)
        {
            recipe.Items.Add(new RecipeItem());
            return PartialView("RecipeItems", recipe);
        }
    }
}
