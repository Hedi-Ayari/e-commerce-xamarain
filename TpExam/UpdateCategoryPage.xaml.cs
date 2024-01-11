using System;
using Xamarin.Forms;
using TpExam.Models;

namespace TpExam
{
    public partial class UpdateCategoryPage : ContentPage
    {
        private Models.Models _category;

        public UpdateCategoryPage(Models.Models category)
        {
            InitializeComponent();
            _category = category;
        }

        private async void UpdateButton_Clicked(object sender, EventArgs e)
        {
            // Update the category name and save changes
            _category.Nom = UpdatedCategoryName.Text;
            App.Database.ModifierCategorie(_category);

            // Navigate back to the CategoriesPage
            await Navigation.PopAsync();
        }
    }
}
