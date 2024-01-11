using System;
using Xamarin.Forms;

namespace TpExam
{
    public partial class Administration : ContentPage
    {
        public Administration()
        {
            InitializeComponent();
        }

        private void OnLoginButtonClicked(object sender, EventArgs e)
        {
            // Get entered username and password
            string username = UsernameEntry.Text;
            string password = PasswordEntry.Text;

            // Log the entered username and password
            Console.WriteLine($"Username: {username}");
            Console.WriteLine($"Password: {password}");

            // Perform authentication logic (replace with your actual authentication logic)
            bool isAuthenticated = AuthenticateUser(username, password);

            if (isAuthenticated)
            {
                // Navigate to the next page (replace with the desired navigation logic)
                Navigation.PushAsync(new AdministrationHomePage());
            }
            else
            {
                // Show an error message or handle unsuccessful login
                DisplayAlert("Login Failed", "Invalid username or password", "OK");
            }
        }

        private bool AuthenticateUser(string username, string password)
        {
            // Replace the following line with your actual authentication logic
            return username == "admin" && password == "adminpassword";
        }
    }
}
