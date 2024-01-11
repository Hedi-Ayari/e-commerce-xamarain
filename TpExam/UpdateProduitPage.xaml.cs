using System;
using Xamarin.Forms;
using TpExam.Models;

namespace TpExam
{
    public partial class UpdateProduitPage : ContentPage
    {
        private Produit _product;

        public UpdateProduitPage(Produit product)
        {
            InitializeComponent();
            _product = product;
        }

        private async void UpdateButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                // Update the product information and save changes
                _product.Nom = UpdatedProductName.Text;
                _product.Description = UpdatedProductDescription.Text;

                // Convert the string input to int for Prix
                if (int.TryParse(UpdatedProductPrice.Text, out int updatedPrice))
                {
                    // Assign the string representation of updatedPrice to _product.Prix
                    _product.Prix = Convert.ToInt32(updatedPrice);


                    // Update the image URL
                    _product.UrlImage = UpdatedProductImageUrl.Text;

                    // Call the database method to update the product
                    App.Database.ModifierProduit(_product);

                    // Display a success message
                    await DisplayAlert("Success", "Product updated successfully", "OK");

                    // Navigate back to the previous page
                    await Navigation.PopAsync();
                }
                else
                {
                    // Display an error message for invalid price input
                    await DisplayAlert("Error", "Invalid price input", "OK");
                }
            }
            catch (Exception ex)
            {
                // Handle errors, display an error message
                await DisplayAlert("Error", $"Error updating product: {ex.Message}", "OK");
            }
        }
    }
}
