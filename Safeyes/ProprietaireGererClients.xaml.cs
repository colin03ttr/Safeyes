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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Safeyes
{
    /// <summary>
    /// Logique d'interaction pour ProprietaireGererClients.xaml
    /// </summary>
    public partial class ProprietaireGererClients : Page
    {
        public ProprietaireGererClients()
        {
            InitializeComponent();

            Button suppr = new Button();
            suppr.Content = "Supprimer";
            suppr.Width = 60;
            suppr.Height = 30;
            suppr.Margin = new Thickness(10, 0, 0, 0);
            suppr.Background = Brushes.Red;
            suppr.Click += new RoutedEventHandler(SupprClient);

            Button annul = new Button();
            annul.Content = "X";
            annul.Width = 30;
            annul.Height = 30;
            annul.Margin = new Thickness(10, 0, 0, 0);
            annul.Background = Brushes.LightGreen;
            annul.Click += new RoutedEventHandler(AnnulSupprClient);

            boutonsdroite.Children.Add(annul); 
            boutonsdroite.Children.Add(suppr);

            AfficherClients();
        }
        public void AfficherClients()
        {
            Connexion connect = new Connexion();
            connect.DataBase.Open();
            MySqlCommand command = connect.DataBase.CreateCommand();
            command.CommandText = "SELECT mail,nom_c,prenom_c,telephone,adresse " +
                "FROM clients WHERE mail != 'null';";
            List<string[]> clients = Connexion.resultSELECT(command);

            boutonsdroite.Background = Brushes.Transparent;
            boutonsdroite.Children[0].Visibility = Visibility.Hidden;
            boutonsdroite.Children[1].Visibility = Visibility.Hidden;

            scrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;

            stackPanel.Orientation = Orientation.Vertical;
            stackPanel.VerticalAlignment = VerticalAlignment.Top;
            stackPanel.HorizontalAlignment = HorizontalAlignment.Left;
            stackPanel.Width = 1028;

            foreach (string[] client in clients)
            {
                Image image = new Image();
                image.Source = new BitmapImage(new Uri("Images/bellefleur.png", UriKind.Relative));
                image.Width = 40;
                image.Height = 40;
                image.Margin = new Thickness(10, 0, 0, 0);

                Button button = new Button();
                button.Width = 1028;
                button.Height = 50;
                button.VerticalContentAlignment = VerticalAlignment.Center;
                button.HorizontalContentAlignment = HorizontalAlignment.Stretch;
                button.Background = Brushes.Transparent;
                button.BorderBrush = Brushes.Transparent;
                button.Click += new RoutedEventHandler(Click_Client);

                StackPanel stack = new StackPanel();
                stack.Orientation = Orientation.Horizontal;
                stack.HorizontalAlignment = HorizontalAlignment.Stretch;
                stack.VerticalAlignment = VerticalAlignment.Top;

                TextBlock text0 = new TextBlock();//email
                text0.Text = client[0];
                text0.FontSize = 15;
                text0.Width = 200;
                text0.Margin = new Thickness(10, 0, 0, 0);
                text0.Background = Brushes.Transparent;
                text0.HorizontalAlignment = HorizontalAlignment.Stretch;
                text0.VerticalAlignment = VerticalAlignment.Center;
                text0.HorizontalAlignment = HorizontalAlignment.Center;

                TextBlock text1 = new TextBlock();//prenom NOM
                text1.Text = client[1].ToUpper() + " " + client[2];
                text1.FontSize = 15;
                text1.Width = 200;
                text1.Margin = new Thickness(10, 0, 0, 0);
                text1.Background = Brushes.Transparent;
                text1.HorizontalAlignment = HorizontalAlignment.Stretch;
                text1.VerticalAlignment = VerticalAlignment.Center;
                text1.HorizontalAlignment = HorizontalAlignment.Center;

                TextBlock text2 = new TextBlock();//telephone
                text2.Text = client[3];
                text2.FontSize = 15;
                text2.Width = 100;
                text2.Margin = new Thickness(10, 0, 0, 0);
                text2.Background = Brushes.Transparent;
                text2.HorizontalAlignment = HorizontalAlignment.Stretch;
                text2.VerticalAlignment = VerticalAlignment.Center;
                text2.HorizontalAlignment = HorizontalAlignment.Center;

                TextBlock text3 = new TextBlock();//adresse
                text3.Text = client[4];
                text3.FontSize = 15;
                text3.Width = 200;
                text3.Margin = new Thickness(10, 0, 0, 0);
                text3.Background = Brushes.Transparent;
                text3.HorizontalAlignment = HorizontalAlignment.Stretch;
                text3.VerticalAlignment = VerticalAlignment.Center;
                text3.HorizontalAlignment = HorizontalAlignment.Center;

                stack.Children.Add(image);
                stack.Children.Add(text0);
                stack.Children.Add(text1);
                stack.Children.Add(text2);
                stack.Children.Add(text3);
                button.Content = stack;
                stackPanel.Children.Add(button);
            }
        }
        private void Click_Client(object sender, RoutedEventArgs e)
        {
            ((StackPanel)((Button)sender).Content).Background= Brushes.LightGray;
            boutonsdroite.Background = Brushes.LightGray;
            boutonsdroite.Children[0].Visibility = Visibility.Visible;
            boutonsdroite.Children[1].Visibility = Visibility.Visible;
        }
        private void AnnulSupprClient(object sender, RoutedEventArgs e)
        {
            stackPanel.Children.Clear();
            AfficherClients();
        }
        private void SupprClient(object sender, RoutedEventArgs e)
        {
            Connexion connect = new Connexion();
            connect.DataBase.Open();
            for(int i=0;i<stackPanel.Children.Count;i++)
            {
                if (((TextBlock)((StackPanel)((Button)stackPanel.Children[i]).Content).Children[1]).Text != "" && ((StackPanel)((Button)stackPanel.Children[i]).Content).Background==Brushes.LightGray)
                {
                    MySqlCommand command = connect.DataBase.CreateCommand();
                    command.CommandText = "DELETE FROM clients WHERE mail = '" + ((TextBlock)((StackPanel)((Button)stackPanel.Children[i]).Content).Children[1]).Text + "';";
                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (MySqlException exc)
                    {
                        Console.WriteLine(exc.Message);
                    }
                }
            }
            connect.DataBase.Close();
            stackPanel.Children.Clear();
            AfficherClients();
        }
    }
}
