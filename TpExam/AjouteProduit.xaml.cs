using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TpExam.Models;
using System.Linq;

namespace TpExam
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AjouteProduit : ContentPage
    {
        private int selectedCategoryId; // Store the selected category ID

        public AjouteProduit()
        {
            InitializeComponent();

            // Populate the Picker with existing categories
            List<Models.Models> categories = App.Database.ObtenirCategories();
            foreach (Models.Models category in categories)
            {
                CategoryPicker.Items.Add(category.Nom);
            }

            // Subscribe to the event when the selected item in the Picker changes
            CategoryPicker.SelectedIndexChanged += (sender, args) =>
            {
                // Retrieve the selected category from the Picker
                string selectedCategoryName = CategoryPicker.SelectedItem?.ToString();

                // Find the selected category from the list of existing categories
                Models.Models selectedCategory = App.Database.ObtenirCategories().FirstOrDefault(c => c.Nom == selectedCategoryName);

                // Set the selected category ID
                selectedCategoryId = selectedCategory?.Id ?? 0;
            };
        }

        private async void BtnAdd_Clicked(object sender, EventArgs e)
        {
            try
            {
                // Check if a category is selected
                if (selectedCategoryId == 0)
                {
                    await DisplayAlert("Erreur", "Veuillez sélectionner une catégorie", "Okay");
                    return;
                }

                // Validate and parse the Price.Text input
                if (!float.TryParse(Price.Text, out float price))
                {
                    await DisplayAlert("Erreur", "Veuillez entrer un prix valide", "Okay");
                    return;
                }

                // Now, proceed with adding the product
                Produit produit = new Produit
                {
                    Nom = Name.Text,
                    Description = Description.Text,
                    Prix = (int)price,
                    UrlImage = ImageUrl.Text,
                    IdCategorie = selectedCategoryId // Use the selected category ID
                };

                App.Database.AjouterProduit(produit);

                // Optional: Log or display a message to verify the successful addition
                Console.WriteLine("Product added:");
                Console.WriteLine($"Name: {produit.Nom}");
                Console.WriteLine($"Description: {produit.Description}");
                Console.WriteLine($"Price: {produit.Prix}");

                // Retrieve the list of products for verification
                List<Produit> products = App.Database.ObtenirProduits(selectedCategoryId);

                // Log the list of products for verification
                Console.WriteLine("List of products after addition:");
                foreach (Produit product in products)
                {
                    Console.WriteLine($"Product ID: {product.Id}, Name: {product.Nom}, Price :{product.Prix}");
                }

                // Display a success alert
                await DisplayAlert("Ajout", "Produit Ajouté avec succès", "Okay");
            }
            catch (Exception ex)
            {
                // Log or display an error message
                Console.WriteLine($"Error adding product: {ex.Message}");
                await DisplayAlert("Erreur", "Une erreur s'est produite", "Okay");
            }
        }


        private async void ShowProductsButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ProduitsPage());
        }
    }
}
