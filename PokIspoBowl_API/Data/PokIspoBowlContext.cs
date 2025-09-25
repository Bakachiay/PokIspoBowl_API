using Microsoft.EntityFrameworkCore;
using PokIspoBowl_API.Model;

namespace PokIspoBowl_API.Data
{
    //Hérite de DbContext : Hérite des fonctions qui permettent...
    //... de manipuler la db avec EntityFrameworkCore
    public class PokIspoBowlContext : DbContext
    {
        //Je reçois les options de connexion (comme les mdp, users,... de la db) et je les envoie à dbcontext
        public PokIspoBowlContext(DbContextOptions<PokIspoBowlContext> options) : base(options)
        {
            
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Bowl> Bowls { get; set; }
        public DbSet<Dessert> Desserts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderLine> OrderLines { get; set; }
        public DbSet<Taste> Tastes { get; set; }
        public DbSet<SelectedIngredient> SelectedIngredients { get; set; }


    }
}
