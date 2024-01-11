using System;
using System.Diagnostics;
using System.Linq;
using TpExam.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TpExam
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CommandDetailPage : ContentPage
    {
        public CommandDetailPage(Commande selectedCommande)
        {
            InitializeComponent();

            // Load products for the selected command
            LoadProducts(selectedCommande);

            // Load cart items (if needed)
            var cartItems = App.Database.GetCartItems();
        }

        private void LoadProducts(Commande selectedCommande)
        {
            if (selectedCommande != null && selectedCommande.LignesCommande != null)
            {
                foreach (var item in selectedCommande.LignesCommande)
                {
                    item.Produit = App.Database.GetProductById(item.IdProduit);

                    Debug.WriteLine($"LigneCommande Id: {item.Id}, IdProduit: {item.IdProduit}, Quantite: {item.Quantite}");

                    if (item.Produit != null)
                    {
                        Debug.WriteLine($"Produit Nom: {item.Produit.Nom}");

                        // Calculate and set the total price for each LigneCommande
                        item.TotalProduit = item.Quantite * item.Produit.Prix;
                        Debug.WriteLine($"TotalProduit: {item.TotalProduit:C}");
                    }
                    else
                    {
                        Debug.WriteLine("Produit is null");
                    }
                }
            }
            else
            {
                Debug.WriteLine("Selected Commande or LignesCommande is null");
            }

            BindingContext = selectedCommande;
        }

    }
}
