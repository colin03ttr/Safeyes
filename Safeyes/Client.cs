using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Safeyes
{
    class Client
    {
		private string mail;
		private string nom;
		private string prenom;
		private string adresse;
		private string telephone;
		private string motDePasse;
		private string carteCredit;
		private Connexion connect;

        #region Proprietes
        public Connexion Database
		{
			get { return connect; }
			set { connect = value; }
		}

		public string CarteCredit
		{
			get { return carteCredit; }
			set { carteCredit = value; }
		}


		public string MotDePasse
		{
			get { return motDePasse; }
			set { motDePasse = value; }
		}


		public string Telephone
		{
			get { return telephone; }
			set { telephone = value; }
		}


		public string Adresse
		{
			get { return adresse; }
			set { adresse = value; }
		}


		public string Prenom
		{
			get { return prenom; }
			set { prenom = value; }
		}


		public string Nom
		{
			get { return nom; }
			set { nom = value; }
		}


		public string Mail
		{
			get { return mail; }
			set { mail = value; }
		}

        #endregion

        #region Constructeurs
        public Client()
		{

		}

		public Client(string mail, string nom, string prenom, string adresse, string telephone, string motDePasse, string carteCredit)
		{
            this.mail = mail;
            this.nom = nom;
            this.prenom = prenom;
            this.adresse = adresse;
            this.telephone = telephone;
            this.motDePasse = motDePasse;
			this.carteCredit = carteCredit;
			this.connect= new Connexion();
        }
        #endregion
        public bool AjoutClient()
		{
			bool ajout;
            Connexion connect = new Connexion();
            connect.DataBase.Open();
            string insertInto = "INSERT INTO safeyes.clients " +
                "VALUES ('" + mail + "','" + nom + "','" + prenom + "','" + telephone + "','" + motDePasse + "','" + adresse + "','0000');";
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
				ajout= false;
            }
            command3.Dispose();
            connect.DataBase.Close();
            return ajout;
        }
		public bool UpdateClient()
		{
			bool update;
			connect.DataBase.Open();
			string requete = "UPDATE clients SET nom_c='" + nom + "', prenom_c='" + prenom + "', adresse='" + adresse + "', telephone='" + telephone + "', mdp='" + motDePasse + "', carte_credit='" + carteCredit + "' WHERE mail='" + mail + "';";
			MySqlCommand command3 = connect.DataBase.CreateCommand();
			command3.CommandText = requete;
			try
			{
				command3.ExecuteNonQuery();
				update = true;
			}
			catch (MySqlException e)
			{
				Console.WriteLine("\tErreur requete : " + e.Message);
				update = false;
			}
			command3.Dispose();
			return update;
		}
		public List<string> ListCommandesMois(Connexion connect)
		{
			MySqlCommand command = connect.DataBase.CreateCommand();
			command.CommandText = "SELECT commande_id FROM commande WHERE mail='" + mail + "' " +
				"AND MONTH(date_commande)=MONTH(CURRENT_DATE());";
			MySqlDataReader reader = command.ExecuteReader();
			List<string> list = new List<string>();
			while (reader.Read())
			{
				list.Add(reader.GetValue(0).ToString());
			}
			return list;
		}
    }
}
