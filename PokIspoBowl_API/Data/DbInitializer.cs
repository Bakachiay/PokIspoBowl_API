using Microsoft.EntityFrameworkCore;
using PokIspoBowl_API.Model;
using System.Text.Json;

namespace PokIspoBowl_API.Data
{
    //Cette classe va nous permette d'initialiser la db
    //Elle va exécuter les migrations et les insert de données pour nous
    public static class DbInitializer
    {
        public static void Initialize(PokIspoBowlContext context, ILogger logger)
        {
            //Même chose que quand on fait dotnet ef update etc...
            context.Database.Migrate();

            //Vérifier si il n'y a pas déjà des clients dans la DB
            if(context.Clients.Any())
            {
                logger.LogInformation("La base de donnée contient déjà des clients...");
                return;
            }

            //On va chercher le chemin vers le fichier JSON
            //Path.Combine() permet de combiner 3 strings en un seul string/chemin
            //AppContext.BaseDirectory permet de retrouver le répertoire de base de l'application
            string jsonFilePath = Path.Combine(AppContext.BaseDirectory, "Data", "clients.json");

            if(!File.Exists(jsonFilePath))
            {
                logger.LogError("Le fichier clients.json est introuvable...");
                return;
            }

            try
            {
                //On va lire le fichier telquel
                string jsonData = File.ReadAllText(jsonFilePath);

                List<Client>? clients = JsonSerializer.Deserialize<List<Client>>(jsonData, new JsonSerializerOptions { PropertyNameCaseInsensitive = true});
            
                if(clients != null && clients.Count > 0) // pourquoi pas clients.any() ??
                {
                    context.Clients.AddRange(clients);
                    context.SaveChanges();
                    logger.LogInformation($"{clients.Count()} clients ont été ajouté dans la table Clients");
                }
                else
                {
                    logger.LogWarning("Pas de clients detectés dans le fichier clients.json");
                }
                
            }
            catch (Exception ex)
            {
                logger.LogError("Erreur lors de la lecture du fichier JSON... "+ ex.Message);
            }


        }
    }
}
