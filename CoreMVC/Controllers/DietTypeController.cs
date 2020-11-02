using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.Models;
using DataLayer.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CoreMVC.Models
{
    public class DietTypeController : Controller
    {
        private IDietRepository _db;
        public DietTypeController(IDietRepository db)
        {
            _db = db;
        }
        public async Task<IActionResult> Index()
        {
            var dietList = await _db.GetAllDiets();
            return View(dietList);
        }

        //GET: Diet/Create
        public IActionResult Create()
        {
            return View();
        }
        //POST: Diet/Create
        [HttpPost]
        public async Task<IActionResult> Create(DietType diet)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            await _db.Add(diet);
            var dietList = await _db.GetAllDiets();
            return View("Index",dietList);
        }
        //GET: Diet/Delete
        public async Task<IActionResult> Delete(int id)
        {
            var diet = await _db.GetDiet(id);
            return View(diet);
        }

        //POST: Diet/Delete
        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteDiet(int id)
        {
            await _db.Delete(id);
            var dietList = await _db.GetAllDiets();
            return View("Index", dietList);

        }

        //GET: Diet/Edit
        public async Task<IActionResult> Edit(int id)
        {
            var updateDiet = await _db.GetDiet(id);
            return View(updateDiet);
        }
        //POST: Diet/Edit
        [HttpPost]
        public async Task<IActionResult> Edit(DietType diet)
        {
            _db.Update(diet);
            var dietList = await _db.GetAllDiets();
            return View("Index", dietList);
        }
        
        
    }
}
