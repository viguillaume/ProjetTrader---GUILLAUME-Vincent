using MySql.Data.MySqlClient;
using System;
using MetierTrader;
using System.Collections.Generic;

namespace GestionnaireBDD
{
    public class GstBdd
    {
        private MySqlConnection cnx;
        private MySqlCommand cmd;
        private MySqlDataReader dr;

        // Constructeur
        public GstBdd()
        {
            string chaine = "Server=localhost;Database=bourse;Uid=root;Pwd=";
            cnx = new MySqlConnection(chaine);
            cnx.Open();
        }

        public List<Trader> getAllTraders()
        {
            List<Trader> lestraders = new List<Trader>();
            cmd = new MySqlCommand("select idTrader, nomTrader from trader;", cnx);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Trader unTrader = new Trader(Convert.ToInt16(dr[0].ToString()), dr[1].ToString());
                lestraders.Add(unTrader);
            }
            dr.Close();
            return lestraders;
        }
        public List<ActionPerso> getAllActionsByTrader(int numTrader)
        {
            List<ActionPerso> lesActions = new List<ActionPerso>();
            cmd = new MySqlCommand("SELECT idAction,nomAction, prixAchat, quantite, prixAchat*quantite FROM action INNER JOIN acheter ON action.idAction = acheter.numAction WHERE acheter.numTrader =" + numTrader, cnx);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                MetierTrader.ActionPerso uneAction = new ActionPerso(Convert.ToInt16(dr[0].ToString()), dr[1].ToString(), Convert.ToDouble(dr[2].ToString()), Convert.ToInt16(dr[3].ToString()), Convert.ToDouble(dr[4].ToString()));
                lesActions.Add(uneAction);
            }
            dr.Close();
            return lesActions;
        }

        public List<MetierTrader.Action> getAllActionsNonPossedees(int numTrader)
        {
            List<MetierTrader.Action> lesActions = new List<MetierTrader.Action>();
            cmd = new MySqlCommand("SELECT idAction,nomAction, FROM action INNER JOIN acheter ON action.idAction = acheter.numAction WHERE acheter.numTrader != (Select idTrader from Trader where idTrader =" + numTrader +")",cnx);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                MetierTrader.Action uneAction = new MetierTrader.Action(Convert.ToInt16(dr[0].ToString()), dr[1].ToString());
                lesActions.Add(uneAction);
            }
            dr.Close();
            return lesActions;
        }

        public void SupprimerActionAcheter(int numAction, int numTrader)
        {
            
        }

        public void UpdateQuantite(int numAction, int numTrader, int quantite)
        {
            
        }

        public double getCoursReel(int numAction)
        {
            return 0;
        }
        public void AcheterAction(int numAction, int numTrader, double prix, int quantite)
        {

        }
        public double getTotalPortefeuille(int numTrader)
        {
            double total;
            cmd = new MySqlCommand("SELECT SUM(prixAchat * quantite) FROM action INNER JOIN acheter ON action.idAction = acheter.numAction WHERE acheter.numTrader = " + numTrader, cnx);
            dr = cmd.ExecuteReader();
            dr.Read();
            total = Convert.ToDouble(dr[0].ToString());
            dr.Close();
            return total;
        }
    }
}
