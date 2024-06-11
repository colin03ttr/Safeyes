using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Safeyes
{
    class Vendeur
    {
        private string id;
        private string nom;
        private string prenom;
        private string magasin;
        private Connexion connect;
        private string motDePasse;

        public string MotDePasse
        {
            get { return motDePasse; }
            set { motDePasse = value; }
        }


        public Connexion Database
        {
            get { return connect; }
            set { connect = value; }
        }

        public string Magasin
        {
            get { return magasin; }
            set { magasin = value; }
        }

        public string Id
        {
            get { return id; }
            set { id = value; }
        }
        public string Nom
        {
            get { return nom; }
            set { nom = value; }
        }
        public string Prenom
        {
            get { return prenom; }
            set { prenom = value; }
        }
        public Vendeur()
        {
        }

        public Vendeur(string id, string nom, string prenom, string magasin, string mdp)
        {
            this.id = id;
            this.nom = nom;
            this.prenom = prenom;
            this.magasin = magasin;
            this.connect = new Connexion();
            this.motDePasse = mdp;
        }

        public bool AjoutVendeur()
        {
            connect.DataBase.Open();
            bool ajout;
            string insertInto = "INSERT INTO safeyes.vendeur " +
                "VALUES ('" + id + "','" + nom + "','" + prenom + "','" + magasin + "','" + motDePasse + "');";
            MySqlCommand command3 = connect.DataBase.CreateCommand();
            command3.CommandText = insertInto;
            try
            {
                command3.ExecuteNonQuery();
                ajout = true;
            }
            catch (MySqlException e)
            {
                Console.WriteLine("\tErreur requete : " + e.Message);
                ajout = false;
            }
            command3.Dispose();
            connect.DataBase.Close();
            return ajout;
        }
    }
}
