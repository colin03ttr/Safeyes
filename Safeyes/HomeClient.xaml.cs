using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
//using System.Runtime.Remoting.Contexts;
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
    /// Logique d'interaction pour HomeClient.xaml
    /// </summary>
    public partial class HomeClient : Page
    {
        public HomeClient()
        {
            InitializeComponent();
            FocusManager.SetFocusedElement(this, Identifiant);
        }

        private void Inscription_Click(object sender, RoutedEventArgs e)
        {
            var inscription = new Inscription();
            this.NavigationService.Navigate(inscription);
        }

        private void Propriétaire_Click(object sender, RoutedEventArgs e)
        {
            var staff = new ConnexionStaff();
            if (staff.ShowDialog() == true)
            {
                if (staff.Authentification)
                {
                    if (staff.Role == "proprietaire")
                    {
                        var proprietaire = new PageProprietaire(staff.Id);
                        this.NavigationService.Navigate(proprietaire);
                    }
                    if (staff.Role == "vendeur")
                    {
                        var vendeur = new PageVendeur(staff.Id);
                        this.NavigationService.Navigate(vendeur);
                    }

                }
            }


        }

        private void Connexion_Click(object sender, RoutedEventArgs e)
        {
            string email = Identifiant.Text;
            string password = MotDePasse.Password;
            Connexion connect = new Connexion();
            connect.DataBase.Open();
            if (email == null || email.Length <= 0 || email.Length > 50 || !email.Contains('@') || !email.Contains('.') || email[0] == '.' || email[0] == '@' || email[email.Length - 1] == '.' || email[email.Length - 1] == '@')
            {
                MessageBox.Show("Email invalide (doit contenir . et @, pas aux extremités)");
                connect.DataBase.Close();
                return;
            }
            bool ExistEmail = connect.ExistsBase(email);
            if (!ExistEmail)
            {
                MessageBox.Show("Vous n'êtes pas enregistré comme client, veuillez vous inscrire");
                connect.DataBase.Close();
                return;
            }

            if (!connect.AuthentificationClient(email, password))
            {
                MessageBox.Show("Erreur : email ou mot de passe incorrect");
                connect.DataBase.Close();
                return;
            }
            else
            {
                var connecte = new PageClient(email);
                this.NavigationService.Navigate(connecte);
                connect.DataBase.Close();
            }
        }
    }
}
