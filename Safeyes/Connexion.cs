using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Relational;
using System.Windows.Controls;
//using System.Runtime.Remoting.Contexts;

namespace Safeyes
{
    class Connexion
    {
        #region Propriétés
        private MySqlConnection _connexion;

        public MySqlConnection DataBase
        {
            get { return _connexion; }
            set { _connexion = value; }
        }

        #endregion

        #region Constructeur
        public Connexion()
        {
            MySqlConnection connection = null;
            try
            {
                string connectionString = "SERVER=localhost;PORT=3306;" +
                                      "DATABASE=Safeyes;" +
                                      "UID=root;PASSWORD=root";
                connection = new MySqlConnection(connectionString);
                Console.WriteLine("Connexion à la base de données Safeyes réussie");
            }
            catch (MySqlException e)
            {
                Console.WriteLine("\tErreur connexion : " + e.Message);
                return;
            }

            _connexion = connection;

        }

        #endregion

        #region Authentification
        public bool AuthentificationClient(string email, string password)
        {
            string requete = "SELECT mdp FROM clients WHERE mail = '" + email + "';";
            MySqlCommand commande = _connexion.CreateCommand();
            commande.CommandText = requete;
            string mdpRes = resultSELECT0(commande)[0];
            if (mdpRes == password)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool AuthentificationStaff(string email, string password, string role)
        {
            string requete="";
            if (role=="proprietaire")
                requete = "SELECT mdp_p FROM proprietaire WHERE proprietaire_id = '" + email + "';";
            else if (role=="vendeur")
                requete = "SELECT mdp_v FROM vendeur WHERE vendeur_id = '" + email + "';";
            MySqlCommand commande = _connexion.CreateCommand();
            commande.CommandText = requete;
            string mdpRes = resultSELECT0(commande)[0];
            if (mdpRes == password)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Client RecupererClient(string email)
        {
            string nom;
            string prenom;
            string telephone;
            string motDePasse;
            string adresse;
            string carteCredit;
            if (email != "Aucun")
            {
                string requete = "CALL RecupererClient('" + email + "');";
                MySqlCommand commande = _connexion.CreateCommand();
                commande.CommandText = requete;
                MySqlDataReader reader = commande.ExecuteReader();
                reader.Read();
                string mail = reader.GetValue(0).ToString();
                nom = reader.GetValue(1).ToString();
                prenom = reader.GetValue(2).ToString();
                telephone = reader.GetValue(3).ToString();
                motDePasse = reader.GetValue(4).ToString();
                adresse = reader.GetValue(5).ToString();
                carteCredit = reader.GetValue(6).ToString();
                reader.Close();
            }
            else
            {
                nom="";
                prenom = "";
                telephone = "";
                motDePasse = "";
                adresse = "";
                carteCredit = "";
            }
                Client client = new Client(email, nom, prenom, adresse, telephone, motDePasse, carteCredit);
            return client;
        }

        public Vendeur RecupererVendeur(string id)
        {
            string vendeur_id;
            string nom;
            string prenom;
            string magasin;
            string motDePasse;
            if (id != "Aucun")
            {
                string requete = "CALL RecupererVendeur('" + id + "');";
                MySqlCommand commande = _connexion.CreateCommand();
                commande.CommandText = requete;
                MySqlDataReader reader = commande.ExecuteReader();
                reader.Read();
                vendeur_id = reader.GetValue(0).ToString();
                nom = reader.GetValue(1).ToString();
                prenom = reader.GetValue(2).ToString();
                magasin = reader.GetValue(3).ToString();
                motDePasse = reader.GetValue(4).ToString();
                reader.Close();
            }
            else
            {
                vendeur_id = "";
                nom = "";
                prenom = "";
                magasin = "";
                motDePasse = "";
            }
            Vendeur vendeur = new Vendeur(vendeur_id, nom, prenom, magasin, motDePasse);
            return vendeur;
        }

        public Proprietaire RecupererProprietaire(string id)
        {
            string requete = "CALL RecupererProprietaire('" + id + "');";
            MySqlCommand commande = _connexion.CreateCommand();
            commande.CommandText = requete;
            MySqlDataReader reader = commande.ExecuteReader();
            reader.Read();
            string proprietaire_id = reader.GetValue(0).ToString();
            string motDePasse = reader.GetValue(1).ToString();
            reader.Close();
            Proprietaire proprietaire = new Proprietaire(id, motDePasse);
            return proprietaire;
        }
        #endregion

        /// <summary>
        /// Vérifie si un client existe dans la base de données
        /// </summary>
        /// <param name="ID"></identifiant>
        /// <returns></true s'il existe, false s'il n'existe pas>
        public bool ExistsBase(string ID, string role="clients")
        {
            string requete = "";
            if (role=="clients")
                requete = "SELECT mail " +
                             "FROM clients;";
            else if (role=="proprietaire")
                requete = "SELECT proprietaire_id " +
                             "FROM proprietaire;";
            else if (role=="vendeur")
                requete = "SELECT vendeur_id " +
                             "FROM vendeur;";
            MySqlCommand command = _connexion.CreateCommand();
            command.CommandText = requete;
            command.Dispose();
            List<string> listclients = resultSELECT0(command);
            bool exists;
            if (listclients.Contains(ID)) { exists = true; }
            else { exists = false; }
            return exists;
        }
        
        /// <summary>
        /// Retourne les tuples d'une requête SELECT
        /// </summary>
        /// <param name="command"></requête>
        /// <returns></liste des tuples>
        public static List<string> resultSELECT0(MySqlCommand command)
        {
            MySqlDataReader reader = command.ExecuteReader();
            List<string> tuples = new List<string>();
            while (reader.Read())
            {
                tuples.Add(reader.GetValue(0).ToString());
            }
            reader.Close();
            return tuples;
        }

        public static List<string[]> resultSELECT(MySqlCommand command)
        {
            MySqlDataReader reader = command.ExecuteReader();
            List<string[]> tuples = new List<string[]>();
            while (reader.Read())
            {
                string[] tuple = new string[reader.FieldCount];
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    tuple[i] = reader.GetValue(i).ToString();
                }
                tuples.Add(tuple);
            }
            reader.Close();
            return tuples;
        }
    }
}
