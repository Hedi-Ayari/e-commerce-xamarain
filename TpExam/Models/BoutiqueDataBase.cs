using System.Collections.Generic;
using System.Diagnostics;
using SQLite;
using SQLiteNetExtensions.Extensions;
using System.Linq;

using TpExam.Models;  // Add this if your Categorie and Produit classes are in the TpExam.Models namespace

namespace TpExam.Models
{
    public class BoutiqueDataBase
    {
        private readonly SQLiteConnection _baseDeDonnees;

        public BoutiqueDataBase(string cheminBaseDeDonnees)
        {
            _baseDeDonnees = new SQLiteConnection(cheminBaseDeDonnees);
            _baseDeDonnees.CreateTable<Models>();
            _baseDeDonnees.CreateTable<Produit>();
            _baseDeDonnees.CreateTable<LigneCommande>();
            _baseDeDonnees.CreateTable<Commande>();
        }

        public List<Models> ObtenirCategories()
        {
            // Use the extension method to get categories with related products
            return _baseDeDonnees.GetAllWithChildren<Models>();
        }

        public void AjouterCategorie(Models categorie)
        {
            _baseDeDonnees.InsertWithChildren(categorie);
        }

        public void ModifierCategorie(Models categorie)
        {
            _baseDeDonnees.UpdateWithChildren(categorie);
        }

        public void SupprimerCategorie(int idCategorie)
        {
            // Use Delete method without specifying recursive parameter
            _baseDeDonnees.Delete<Models>(idCategorie);
        }

        public List<Produit> ObtenirTousProduits()
        {
            return _baseDeDonnees.Table<Produit>().ToList();
        }

        public List<Produit> ObtenirProduits(int idCategorie)
        {
            return _baseDeDonnees.Table<Produit>().Where(p => p.IdCategorie == idCategorie).ToList();
        }

        public void AjouterProduit(Produit produit)
        {
            _baseDeDonnees.Insert(produit);
        }

        public void ModifierProduit(Produit produit)
        {
            _baseDeDonnees.Update(produit);
        }

        public void SupprimerProduit(int idProduit)
        {
            _baseDeDonnees.Delete<Produit>(idProduit);
        }

        public List<LigneCommande> GetCartItems()
        {
            List<LigneCommande> cartItems = _baseDeDonnees.GetAllWithChildren<LigneCommande>(recursive: true);

            Debug.WriteLine($"Cart items: {string.Join(", ", cartItems.Select(item => $"{item.IdProduit}({item.Quantite})"))}");
            return cartItems;
        }


        public void AddToCart(Produit product)
        {
            LigneCommande existingItem = _baseDeDonnees.Table<LigneCommande>().FirstOrDefault(item => item.IdProduit == product.Id);

            if (existingItem != null)
            {
                // If the product is already in the cart, update the quantity
                existingItem.Quantite++;
                _baseDeDonnees.Update(existingItem);
            }
            else
            {
                // If the product is not in the cart, add a new item
                _baseDeDonnees.Insert(new LigneCommande { IdProduit = product.Id, Quantite = 1 });
            }
        }

        public void RemoveFromCart(Produit product)
        {
            LigneCommande existingItem = _baseDeDonnees.Table<LigneCommande>().FirstOrDefault(item => item.IdProduit == product.Id);

            if (existingItem != null)
            {
                if (existingItem.Quantite > 1)
                {
                    // If the quantity is greater than 1, decrement the quantity
                    existingItem.Quantite--;
                    _baseDeDonnees.Update(existingItem);
                }
                else
                {
                    // If the quantity is 1, remove the item from the cart
                    _baseDeDonnees.Delete(existingItem);
                }
            }
        }

        public void ClearCart()
        {
            // Remove all items from the cart
            _baseDeDonnees.Execute("DELETE FROM LigneCommande");
        }
        public void EnregistrerCommande(Commande commande)
        {
            _baseDeDonnees.InsertWithChildren(commande);
        }
        public Produit GetProductById(int productId)
        {
            return _baseDeDonnees.Find<Produit>(productId);
        }
        public void UpdateCartItem(LigneCommande ligneCommande)
        {
            _baseDeDonnees.Update(ligneCommande);
        }

        public void RemoveCartItem(Produit product)
        {
            _baseDeDonnees.Table<LigneCommande>().Delete(item => item.IdProduit == product.Id);
        }
        public List<Commande> GetAllCommandsWithDetails()
        {
            List<Commande> commands = _baseDeDonnees.GetAllWithChildren<Commande>(recursive: true);
            return commands;
        }



    }

}
