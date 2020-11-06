using DataLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Services
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<RecipeItem> Items { get; set; }
        public DbSet<DietType> DietTypes { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
    }
}
