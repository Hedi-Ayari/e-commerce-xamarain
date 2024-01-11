using System;
using System.Collections.Generic;
using Xamarin.Forms;
using TpExam.Models;

namespace TpExam
{
    public partial class UserCategory : ContentPage
    {
        public UserCategory()
        {
            InitializeComponent();

            // Populate the CollectionView with existing categories
            List<Models.Models> categories = App.Database.ObtenirCategories();
            CategoriesCollectionView.ItemsSource = categories;
        }
        private async void OpenSideBar(object sender, EventArgs e)
        {
            
        }

        private async void OnCategorySelected(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection == null || e.CurrentSelection.Count == 0)
                return;

            // Get the selected category
            Models.Models selectedCategory = e.CurrentSelection[0] as Models.Models;

            // Navigate to the ProductsPage and pass the selected category ID
            await Navigation.PushAsync(new UserProducts(selectedCategory.Id));

            // Deselect the item
            CategoriesCollectionView.SelectedItem = null;
        }
        private async void OnFrameTapped(object sender, EventArgs e)
        {
            // Get the selected category from the binding context of the Frame
            Models.Models selectedCategory = (sender as Frame)?.BindingContext as Models.Models;

            if (selectedCategory != null)
            {
                // Navigate to the ProductsPage and pass the selected category ID
                await Navigation.PushAsync(new UserProducts(selectedCategory.Id));

                // Deselect the item
                CategoriesCollectionView.SelectedItem = null;
            }
        }
        private async void OnBasketClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Panier());
        }


    }
}
