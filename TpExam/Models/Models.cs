using System.Collections.Generic;
using System.Linq;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace TpExam.Models
{
    public class Models
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [NotNull]
        public string Nom { get; set; }
    }

    public class Produit
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [NotNull]
        public string Nom { get; set; }

        public string Description { get; set; }

        [NotNull]
        public decimal Prix { get; set; }

        public string UrlImage { get; set; }

        [ForeignKey(typeof(Models))]
        public int IdCategorie { get; set; }
    }

    public class LigneCommande
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [ForeignKey(typeof(Produit))]
        public int IdProduit { get; set; }

        public int Quantite { get; set; }
        [Ignore]
        public dynamic Produit { get; set; }
        public decimal TotalProduit { get; set; }
        [ForeignKey(typeof(Commande))]
        public int IdCommande { get; set; }
    }

    public class Commande
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [NotNull]
        public string NomClient { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<LigneCommande> LignesCommande { get; set; }
    }

    public class ArticlePanier
    {
        public int IdProduit { get; set; }
        public string NomProduit { get; set; }
        public decimal PrixUnitaire { get; set; }
        public int Quantite { get; set; }
    }
    
    public class Panier
    {
        public List<ArticlePanier> Articles { get; set; }

        public Panier()
        {
            Articles = new List<ArticlePanier>();
        }

        public void AjouterArticle(int idProduit, string nomProduit, decimal prixUnitaire, int quantite)
        {
            // Vérifier si l'article est déjà dans le panier
            var articleExist = Articles.FirstOrDefault(a => a.IdProduit == idProduit);

            if (articleExist != null)
            {
                // L'article existe déjà, mettre à jour la quantité
                articleExist.Quantite += quantite;
            }
            else
            {
                // Ajouter un nouvel article au panier
                Articles.Add(new ArticlePanier
                {
                    IdProduit = idProduit,
                    NomProduit = nomProduit,
                    PrixUnitaire = prixUnitaire,
                    Quantite = quantite
                });
            }
        }

        public void RetirerArticle(int idProduit)
        {
            // Retirer l'article du panier
            var article = Articles.FirstOrDefault(a => a.IdProduit == idProduit);

            if (article != null)
            {
                Articles.Remove(article);
            }
        }

        public decimal CalculerTotal()
        {
            // Calculer le total du panier
            return Articles.Sum(article => article.PrixUnitaire * article.Quantite);
        }

        public void ViderPanier()
        {
            // Vider le panier
            Articles.Clear();
        }

    }
}
