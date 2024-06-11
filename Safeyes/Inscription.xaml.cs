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
using MySql.Data.MySqlClient;

namespace Safeyes
{
    /// <summary>
    /// Logique d'interaction pour Inscription.xaml
    /// </summary>
    public partial class Inscription : Page
    {
        public Inscription()
        {
            InitializeComponent();
            FocusManager.SetFocusedElement(this, Nom);
        }

        private void Bienvenue_Click(object sender, RoutedEventArgs e)
        {
            string nom = Nom.Text;
            string prenom = Prenom.Text;
            string email = Email.Text;
            string password = MotDePasse.Password;
            string telephone = Telephone.Text;
            int phone = 0;
            string adresse = Adresse.Text;
            if(email==null || email.Length <= 0 || email.Length > 50 || !email.Contains('@') || !email.Contains('.') || email[0] == '.' || email[0] == '@' || email[email.Length - 1] == '.' || email[email.Length - 1] == '@')
            {
                MessageBox.Show("Email invalide (doit contenir . et @, pas aux extremités)");
                return;
            }
            else
            {
                Connexion connect = new Connexion();
                connect.DataBase.Open();
                MySqlCommand command = connect.DataBase.CreateCommand();
                command.CommandText = "SELECT mail FROM clients WHERE mail != 'null';";
                List<string> mails = Connexion.resultSELECT0(command);
                if (mails.Contains(email))
                {
                    MessageBox.Show("Email déjà utilisé !");
                    return;
                }
            }
            if (nom == null || nom.Length <= 0 || nom.Length > 20)
            {
                MessageBox.Show("Nom invalide (doit contenir entre 1 et 20 caractères)");
                return;
            }
            if (prenom == null || prenom.Length <= 0 || prenom.Length > 20)
            {
                MessageBox.Show("Prénom invalide (doit contenir entre 1 et 20 caractères)");
                return;
            }
            if(telephone==null || telephone.Length!=10 || !int.TryParse(telephone, out phone))
            {
                MessageBox.Show("Numéro de téléphone invalide (doit contenir 10 chiffres)");
                return;
            }
            if (adresse == null || adresse.Length <= 0 || adresse.Length > 100)
            {
                MessageBox.Show("Adresse invalide (doit contenir entre 1 et 100 caractères)");
                return;
            }
            if (password == null || password.Length <= 0 || password.Length > 20)
            {
                MessageBox.Show("Mot de passe invalide (doit contenir entre 1 et 20 caractères)");
                return;
            }
            else
            {
                Client client = new Client(email, nom, prenom, adresse, telephone, password,"");//fidelite à 0 default
                if(client.AjoutClient())
                {
                    MessageBox.Show("Inscription réussie");
                    var home = new HomeClient();
                    this.NavigationService.Navigate(home);
                }
                else
                {
                    MessageBox.Show("Erreur lors de l'inscription");
                }
            }
            

        }
        private void Home_Click(object sender, RoutedEventArgs e)
        {
            HomeClient homeClient = new HomeClient();
            this.NavigationService.Navigate(homeClient);
        }

    }
}
