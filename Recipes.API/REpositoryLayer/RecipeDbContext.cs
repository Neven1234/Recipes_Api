using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer
{
    public class RecipeDbContext:DbContext
    {
        public RecipeDbContext(DbContextOptions options):base(options) { }

        public DbSet<recipe> recipes { get; set; }
        public DbSet<IngredientsList> ingredients { get; set; }
        public DbSet<User> users { get; set; }

    }
}
