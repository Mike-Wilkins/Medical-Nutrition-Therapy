using System.ComponentModel.DataAnnotations;

namespace DataLayer.Models
{
    public class DietType
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
