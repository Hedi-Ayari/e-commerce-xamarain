using System;
using Xamarin.Forms;

namespace TpExam
{
    public partial class AdministrationHomePage : ContentPage
    {
        public AdministrationHomePage()
        {
            InitializeComponent();
        }
        private async void VoirCommandesButton_Clicked(object sender, EventArgs e)
        {
            // Navigate to the page displaying all commands
            await Navigation.PushAsync(new CommandsPage());
        }

        private async void CategoriesButton_Clicked(object sender, EventArgs e)
        {
            // Navigate to the CategoriesPage
            await Navigation.PushAsync(new CategoriesPage());
        }

        private async void ProduitsButton_Clicked(object sender, EventArgs e)
        {
            // Navigate to the ProduitsPage (replace with your actual page)
            await Navigation.PushAsync(new ProduitsPage());
        }

      /*  private async void CommandesButton_Clicked(object sender, EventArgs e)
        {
            // Navigate to the CommandesPage (replace with your actual page)
            await Navigation.PushAsync(new CommandesPage());
        }*/
    }
}
