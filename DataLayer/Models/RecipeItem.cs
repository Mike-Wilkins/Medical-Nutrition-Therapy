using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.Models
{
    public class RecipeItem
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Ingredient { get; set; }
    }
}
