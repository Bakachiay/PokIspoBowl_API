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

        //TODO : finir les tables ici
        public DbSet<Bowl> Bowls => Set<Bowl>();
        public DbSet<Category> Categories => Set<Category>();
    }
}
