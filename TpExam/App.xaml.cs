using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TpExam.Models;
using System.IO;
using System.Collections.Generic;

namespace TpExam
{
    public partial class App : Application
    {
        public static Commande CurrentOrder { get; set; }

        static BoutiqueDataBase database;
        public static BoutiqueDataBase Database
        {
            get
            {
                if (database == null)
                {
                    database = new BoutiqueDataBase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Boutique.db3"));
                }
                return database;
            }
        }

        public App()
        {
            InitializeComponent();

            // Log the database path
            Console.WriteLine($"Database path: {Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Boutique.db3")}");
            CurrentOrder = new Commande
            {
                NomClient = "John Doe",
                LignesCommande = new List<LigneCommande>()
            };

            MainPage = new MainPage(); // Assuming MainPage is a MasterDetailPage
        }

        protected override void OnStart()
        {
            App.Database.ClearCart();
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
