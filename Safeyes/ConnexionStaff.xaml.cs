using MySql.Data.MySqlClient;
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
using System.Windows.Shapes;

namespace Safeyes
{
    /// <summary>
    /// Logique d'interaction pour ConnexionStaff.xaml
    /// </summary>
    public partial class ConnexionStaff : Window
    {
        private bool authentification = false;

        private string id;

        private string role;

        public string Role
        {
            get { return role; }
        }

        public string Id
        {
            get { return id; }
        }

        public bool Authentification
        {
            get { return authentification; }
        }

        public ConnexionStaff()
        {
            InitializeComponent();
            FocusManager.SetFocusedElement(this, Identifiant);
        }

        private void Connexion_Click(object sender, RoutedEventArgs e)
        {
            string ID = Identifiant.Text;
            string mdp = MotDePasse.Password;
            Connexion connect = new Connexion();
            connect.DataBase.Open();
            if (ID == null || ID.Length <= 0 || ID.Length > 50)
            {
                MessageBox.Show("Identifiant invalide");
            }
            if (connect.ExistsBase(ID, "proprietaire"))
            {
                if (!connect.AuthentificationStaff(ID, mdp, "proprietaire"))
                {
                    MessageBox.Show("Mot de passe incorrect");
                    return;
                }
                else
                {
                    authentification = true;
                    role = "proprietaire";
                    id = ID;
                    this.DialogResult = true;
                }
            }
            else if (connect.ExistsBase(ID, "vendeur"))
            {
                if (!connect.AuthentificationStaff(ID, mdp, "vendeur"))
                {
                    MessageBox.Show("Mot de passe incorrect");
                }
                else
                {
                    authentification = true;
                    role = "vendeur";
                    id = ID;
                    this.DialogResult = true;
                }
            }
            else if (connect.ExistsBase(ID, "designer"))
            {
                if (!connect.AuthentificationStaff(ID, mdp, "designer"))
                {
                    MessageBox.Show("Mot de passe incorrect");
                }
                else
                {
                    authentification = true;
                    role = "designer";
                    id = ID;
                    this.DialogResult = true;
                }
            }
            else
            { MessageBox.Show("L'identifiant n'existe pas."); }
            connect.DataBase.Close();
        }
    }
}
