using System;
using Xamarin.Forms;
using TpExam.Models;

namespace TpExam
{
    public partial class AddToCartPage : ContentPage
    {
        private Produit selectedProduct;

        public AddToCartPage(Produit product)
        {
            InitializeComponent();
            selectedProduct = product;

            // Display product details
            ProductNameLabel.Text = selectedProduct.Nom;
            DescriptionLabel.Text = selectedProduct.Description;
            PriceLabel.Text = $"Price: {selectedProduct.Prix:C}";
        }

        private async void AddToCartButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                // Validate and parse quantity
                if (!int.TryParse(QuantityEntry.Text, out int quantity) || quantity <= 0)
                {
                    await DisplayAlert("Error", "Please enter a valid quantity", "OK");
                    return;
                }

                // TODO: Add the selected product to the cart with the specified quantity
                // You might want to use your existing models and database logic here

                // Display a success message
                await DisplayAlert("Success", $"Added {quantity} {selectedProduct.Nom}(s) to the cart", "OK");

                // Navigate back to the previous page
                await Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                // Handle errors, display an error message
                await DisplayAlert("Error", $"Error adding to cart: {ex.Message}", "OK");
            }
        }
    }
}
