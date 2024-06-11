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
    /// Logique d'interaction pour PageProprietaire.xaml
    /// </summary>
    public partial class PageProprietaire : Page
    {
        private string id;

        public string Id
        {
            get { return id; }
            set { id = value; }
        }
        public PageProprietaire(string id)
        {
            InitializeComponent();
            this.id = id;
            Loaded += PageProprietaire_Loaded;
        }

        private void PageProprietaire_Loaded(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigate(new ProprietaireGererClients());
            statistiques.IsChecked = true;
        }

        private void statistiques_Checked(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigate(new ProprietaireGererClients());
        }

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            HomeClient homeClient = new HomeClient();
            this.NavigationService.Navigate(homeClient);
        }


        private void Profil_Click(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigate(new ProprietaireModifierProfil(this.Id));
            statistiques.IsChecked = false;
        }

        private void GererClients_Checked(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigate(new ProprietaireGererClients());
        }
    }
}
