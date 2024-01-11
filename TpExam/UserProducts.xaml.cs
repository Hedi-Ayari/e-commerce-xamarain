using System;
using System.Collections.Generic;
using Xamarin.Forms;
using TpExam.Models;

namespace TpExam
{
    public partial class UserProducts : ContentPage 
    {
        private int selectedCategoryId;

        public UserProducts(int categoryId)
        {
            InitializeComponent();

            selectedCategoryId = categoryId;
            LoadProducts();

        }

        private void LoadProducts()
        {
            // Load and display products related to the selected category
            List<Produit> products = App.Database.ObtenirProduits(selectedCategoryId);
            ProductsListView.ItemsSource = products;
        }

        private async void AddToCartButton_Clicked(object sender, EventArgs e)
        {
            if (sender is Image tappedImage && tappedImage.BindingContext is Produit selectedProduct)
            {
                // Add the selected product to the cart
                App.Database.AddToCart(selectedProduct);

                // Optionally, show a message or perform other actions
                await DisplayAlert("Success", $"{selectedProduct.Nom} added to cart", "OK");
            }
        }
        private async void OpenSideBar(object sender, EventArgs e)
        {
            await DisplayAlert("Success", "aaaaa", "Ok");

            if (Parent is FlyoutPage flyoutPage)
            {
                await DisplayAlert("Success","aaaaa","Ok");

                flyoutPage.IsPresented = !flyoutPage.IsPresented;
            }
        }



        private async void OnBasketClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Panier());
        }



    }
}
