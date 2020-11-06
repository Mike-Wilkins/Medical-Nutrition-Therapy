using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.Models
{
    public class Recipe
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }


        public string Calories { get; set; }
        public string Carbohydrates { get; set; }
        public string Fat { get; set; }
        public string Fiber { get; set; }
        public string Protein { get; set; }

        [DisplayName("Saturated Fat")]
        public string SaturatedFat { get; set; }
        public string Sodium { get; set; }
        public string Sugar { get; set; }

        public int DietId { get; set; }
        public string DietType { get; set; }



        public List<RecipeItem> Items { get; set; }

        public Recipe()
        {
            Items = new List<RecipeItem>();
        }
    }
}
