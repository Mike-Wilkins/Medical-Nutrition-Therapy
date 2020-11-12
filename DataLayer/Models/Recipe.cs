using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.Models
{
    public class Recipe
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Calories { get; set; }
        [Required]
        public string Carbohydrates { get; set; }
        [Required]
        public string Fat { get; set; }
        [Required]
        public string Fiber { get; set; }
        [Required]
        public string Protein { get; set; }

        [Required]
        [DisplayName("Saturated Fat")]
        public string SaturatedFat { get; set; }
        [Required]
        public string Sodium { get; set; }
        [Required]
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
