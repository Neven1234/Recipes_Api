using DomainLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using RepositoryLayer.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer
{
    public class RecipeDbContext: IdentityDbContext
    {
        public RecipeDbContext(DbContextOptions options):base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            SeedRoles(modelBuilder);
            modelBuilder.Entity<RateAndReview>()
                .HasOne<recipe>()
                .WithMany()
                .HasForeignKey(r => r.RecipeId);
           
        }
        private void SeedRoles(ModelBuilder builder)
        {
            builder.Entity<IdentityRole>().HasData
                (
                    new IdentityRole() { Name="User",ConcurrencyStamp="1",NormalizedName="User"}
                );
        }
        public DbSet<recipe> recipes { get; set; }
        public DbSet<IngredientsList> ingredients { get; set; }
        public DbSet<RateAndReview> reviews { get; set; }
        public DbSet<CategoryList> categories { get; set; }
        public DbSet<Favorites> favorites { get; set; }
        public DbSet<Planner> planner { get; set; }
        public DbSet<ShoppingList> shoppingLists { get; set; }

        public DbSet<Comment> comments { get; set; }
        public DbSet<Follow> follows { get; set; }
        public DbSet<User> users { get; set; }

    }
}
