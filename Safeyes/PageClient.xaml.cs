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
    /// Logique d'interaction pour PageClient.xaml
    /// </summary>
    public partial class PageClient : Page
    {
        private string email;
        
        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        public PageClient(string email)
        {
            InitializeComponent();
            this.email = email;
            AfficherNomPrenom();
            Loaded += PageClient_Loaded;
        }

        private void PageClient_Loaded(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigate(new ClientModifierProfil(email));
        }
        public void AfficherNomPrenom()
        {
            Connexion connect = new Connexion();
            connect.DataBase.Open();
            Client client = new Client();
            client.Database = connect;
            client = connect.RecupererClient(email);
            this.Nom.Text = client.Prenom + " " + client.Nom;
            connect.DataBase.Close();
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigate(new ClientModifierProfil(email));
        }
        private void Home_Click(object sender, RoutedEventArgs e)
        {
            HomeClient homeClient = new HomeClient();
            this.NavigationService.Navigate(homeClient);
        }
    }
}
