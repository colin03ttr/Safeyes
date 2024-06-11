using System;
using System.Collections;
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
    /// Logique d'interaction pour ClientModifierProfil.xaml
    /// </summary>
    public partial class ClientModifierProfil : Page
    {
        public ClientModifierProfil(string email)
        {
            InitializeComponent();
            AfficheProfil(email);
        }
        public void AfficheProfil(string email)
        {
            Connexion connect = new Connexion();
            connect.DataBase.Open();
            Client client = connect.RecupererClient(email);
            Email.Text = client.Mail;
            Nom.Text = client.Nom;
            Prenom.Text = client.Prenom;
            MotDePasse.Password = client.MotDePasse;
            Telephone.Text = client.Telephone.ToString();
            Adresse.Text = client.Adresse;
            CarteCredit.Text = client.CarteCredit;
            connect.DataBase.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string email = Email.Text;
            string nom = Nom.Text;
            string prenom = Prenom.Text;
            string password = MotDePasse.Password;
            string telephone = Telephone.Text;
            string adresse = Adresse.Text;
            string carteCredit = CarteCredit.Text;
            if (email == null || email.Length <= 0 || email.Length > 50 || !email.Contains('@') || !email.Contains('.') || email[0] == '.' || email[0] == '@' || email[email.Length - 1] == '.' || email[email.Length - 1] == '@')
            {
                MessageBox.Show("Email invalide (doit contenir . et @, pas aux extremités)");
                return;
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
            if (telephone == null || telephone.Length != 10 || !int.TryParse(telephone, out int phone))
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
                Client client = new Client(email, nom, prenom, adresse, telephone, password, carteCredit);
                if (client.UpdateClient())
                {
                    MessageBox.Show("Modification enregistrée");
                }
                else
                {
                    MessageBox.Show("Erreur lors de la modification");
                }
            }
        }
    }
}
