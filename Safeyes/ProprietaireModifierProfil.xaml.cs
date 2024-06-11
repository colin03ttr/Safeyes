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
    /// Logique d'interaction pour ProprietaireModifierProfil.xaml
    /// </summary>
    public partial class ProprietaireModifierProfil : Page
    {
        private Proprietaire proprietaire;
        public ProprietaireModifierProfil(string identifiant)
        {
            InitializeComponent();
            Connexion connect = new Connexion();
            connect.DataBase.Open();
            this.proprietaire = connect.RecupererProprietaire(identifiant);
            connect.DataBase.Close();
            AfficheProfil();
        }
        public void AfficheProfil()
        {
            Identifiant.Text = this.proprietaire.Id;
            MotDePasse.Password = this.proprietaire.MotDePasse;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string id = Identifiant.Text;
            string password = MotDePasse.Password;
            if (id.Length<=0)
            {
                MessageBox.Show("Identifiant invalide");
                return;
            }
            if (password == null || password.Length <= 0 || password.Length > 20)
            {
                MessageBox.Show("Mot de passe invalide (doit contenir entre 1 et 20 caractères)");
                return;
            }
            else
            {
                Proprietaire proprio = new Proprietaire(id, password);
                if (proprio.UpdateProprietaire())
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
