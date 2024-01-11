using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TpExam.Models;

namespace TpExam
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AjouteCategorie : ContentPage
    {
        public AjouteCategorie()
        {
            InitializeComponent();
        }

        private async void BtnAdd_Clicked(object sender, EventArgs e)
        {
            try
            {
                Models.Models categorie = new Models.Models
                {
                    Nom = Name.Text
                };

                App.Database.AjouterCategorie(categorie);

                // Optional: Log or display a message to verify the successful addition
                Console.WriteLine($"Category added: {categorie.Nom}");

                // Retrieve the list of categories
                List<Models.Models> categories = App.Database.ObtenirCategories();

                // Log the list of categories for verification
                Console.WriteLine("List of categories after addition:");
                foreach (Models.Models category in categories)
                {
                    Console.WriteLine($"Category ID: {category.Id}, Name: {category.Nom}");
                }

                // Display a success alert
                await DisplayAlert("Ajout", "Categorie Ajouté avec succès", "Okay");
            }
            catch (Exception ex)
            {
                // Log or display an error message
                Console.WriteLine($"Error adding category: {ex.Message}");
                await DisplayAlert("Erreur", "Une erreur s'est produite", "Okay");
            }
        }

        private async void ShowCategoriesButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CategoriesPage());
        }

    }
}