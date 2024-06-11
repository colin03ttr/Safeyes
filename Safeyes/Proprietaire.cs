using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Safeyes
{
    internal class Proprietaire
    {
        private string id;
        private string mdp;
        private Connexion connect;

        #region Propriétés
        
        public Connexion DataBase
        {
            get { return connect; }
            set { connect = value; }
        }
        public string MotDePasse
        {
            get { return mdp; }
            set { mdp = value; }
        }
        public string Id
        {
            get { return id; }
            set { id = value; }
        }
        #endregion
        #region Constructeur
        public Proprietaire(string id, string mdp)
        {
            this.id = id;
            this.mdp = mdp;
            this.connect= new Connexion();
        }
        #endregion

        public bool UpdateProprietaire()
        {
            bool update;
            connect.DataBase.Open();
            string requete = "UPDATE proprietaire SET mdp_p='" + mdp + "' WHERE proprietaire_id='" + id + "';";
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
            connect.DataBase.Close();
            return update;
        }
    }
}
