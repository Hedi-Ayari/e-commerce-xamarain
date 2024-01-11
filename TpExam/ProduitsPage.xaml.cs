using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TpExam.Models;

namespace TpExam
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProduitsPage : ContentPage
    {
        public ProduitsPage()
        {
            InitializeComponent();
            DisplayProducts();
        }

        private void AddProductButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AjouteProduit());
        }

        private void DisplayProducts()
        {
            List<Produit> products = App.Database.ObtenirTousProduits();
            ProductsListView.ItemsSource = products;

            foreach (var product in products)
            {
                Console.WriteLine($"Product: {product.Nom}, Prix: {product.Prix}");
            }
        }

        private async void UpdateButton_Clicked(object sender, EventArgs e)
        {
            var selectedProduct = (Produit)((Button)sender).CommandParameter;
            // Navigate to an update page (you can replace this with your update logic)
            await Navigation.PushAsync(new UpdateProduitPage(selectedProduct));
        }

        private void DeleteButton_Clicked(object sender, EventArgs e)
        {
            var selectedProduct = (Produit)((Button)sender).CommandParameter;
            // Implement your delete logic here
            App.Database.SupprimerProduit(selectedProduct.Id);
            DisplayProducts(); // Refresh the list after deletion
        }


    }
}
