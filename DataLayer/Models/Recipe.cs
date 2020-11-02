using DataLayer.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DataLayer
{
    public class Recipe
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }


        public int Calories { get; set; }
        public int Carbohydrates { get; set; }
        public int Fat { get; set; }
        public int Fiber { get; set; }
        public int Protein { get; set; }

        [DisplayName("Saturated Fat")]
        public int SaturatedFat { get; set; }
        public int Sodium { get; set; }
        public int Sugar { get; set; }

        public int DietId { get; set; }
        public string DietType { get; set; }



        public List<RecipeItem> Items { get; set; }

        public Recipe()
        {
            Items = new List<RecipeItem>();
        }
    }
}
