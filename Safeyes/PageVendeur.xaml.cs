using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Safeyes
{
    /// <summary>
    /// Logique d'interaction pour PageVendeur.xaml
    /// </summary>
    public partial class PageVendeur : Page
    {
        private string id;

        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        public PageVendeur(string id)
        {
            this.id = id;
            InitializeComponent();

            AjouterNomPrenom();
            Loaded += PageVendeur_Loaded;
        }

        private void PageVendeur_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void AjouterNomPrenom()
        {
            Connexion connect = new Connexion();
            Vendeur vendeur = new Vendeur();
            vendeur.Database = connect;
            connect.DataBase.Open();
            vendeur = connect.RecupererVendeur(id);
            this.Nom.Text = vendeur.Prenom + " " + vendeur.Nom;
            connect.DataBase.Close();

        }

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            HomeClient homeClient = new HomeClient();
            this.NavigationService.Navigate(homeClient);
        }
    }
}