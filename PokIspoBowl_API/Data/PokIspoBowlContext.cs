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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Pas obligatoire dans ce cas car on hérite de DbCOntext directement donc OnModelCreating est vide
            // Dans le cas où on hérite de Identity ca pourrait être utile.
            base.OnModelCreating(modelBuilder);

            // Facultatif
            // Si je souhaite stocker les types d'ingrédient de l'enum sous leur nom dans la DB
            // Par exemple : Protein
            // Par défaut, c'est la valeur numérique qui sera stockée...
            // ce qui est plus rapide mais moins lisible dans la db.
            modelBuilder.Entity<Ingredient>()
                .Property(i => i.Type)
                .HasConversion<string>();

            // Permet de gérer l'héritage dans la DB, ici on aura une seule table Products
            // avec une colonne qui permettra de savoir si c'est un Bowl ou un dessert.
            // Inheritance TPH (par défaut). Discriminateur optionnel :
            modelBuilder.Entity<Product>()
                .HasDiscriminator<string>("ProductType")
                .HasValue<Bowl>("Bowl")
                .HasValue<Dessert>("Dessert");


            // Relations OrderLine -> Product / Order (Required)
            //Empêche de supprimer un produit qui est dans des lignes de commande
            modelBuilder.Entity<OrderLine>()
                .HasOne(ol => ol.Product)
                .WithMany(p => p.OrderLines)
                .HasForeignKey(ol => ol.ProductId)
                .OnDelete(DeleteBehavior.Restrict);


            // Relation OrderLine → Order avec suppression en cascade
            modelBuilder.Entity<OrderLine>()
                .HasOne(ol => ol.Order)           // chaque OrderLine a un Order
                .WithMany(o => o.Lines)           // chaque Order peut avoir plusieurs OrderLines
                .HasForeignKey(ol => ol.OrderId)  // colonne FK dans OrderLine
                .OnDelete(DeleteBehavior.Cascade); // si l’Order est supprimé, toutes ses lignes le sont aussi


        }
    }
}
