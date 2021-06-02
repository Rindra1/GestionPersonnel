using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.IO;

namespace TestTanyah
{
    public class Personne
    {
        public int id { get; set; }
        public string nom { get; set; }
        public string prenom { get; set; }
        public string adresse { get; set; }
        public string telephone { get; set; }
        public DateTime date_de_naissance { get; set; }

        private string connectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source="+Directory.GetCurrentDirectory()+@"\TestTanyah.mdb;Persist Security Info=False;";

        public Personne AjouterPersonne(Personne pers)
        {
            string requete = "insert into Personne([nom],[prenom],[adresse],[telephone],[date_de_naissance])";
            requete += "values(?,?,?,?,?)";
            using(OleDbConnection cnx=new OleDbConnection(connectionString))
            {
                cnx.Open();
                
                using (OleDbCommand cmd = new OleDbCommand())
                {
                    cmd.Connection = cnx;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = requete;
                    cmd.Parameters.AddWithValue("@nom",pers.nom);
                    cmd.Parameters.AddWithValue("@prenom", pers.prenom);
                    cmd.Parameters.AddWithValue("@adresse", pers.adresse);
                    cmd.Parameters.AddWithValue("@telephone", pers.telephone);
                    cmd.Parameters.AddWithValue("@date_de_naissance", pers.date_de_naissance);
                    pers.id = cmd.ExecuteNonQuery();
                    
                }
                cnx.Close();
            }
            return pers;
        }

        public void ModifierPersonne(Personne pers)
        {
            string requete = "update Personne set [nom]=?,[prenom]=?,[adresse]=?,[telephone]=?,[date_de_naissance]=? where [id]=?";
            using (OleDbConnection cnx = new OleDbConnection(connectionString))
            {
                cnx.Open();
                using (OleDbCommand cmd = new OleDbCommand(requete,cnx))
                {
                    
                    cmd.Connection = cnx;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = requete;
                    cmd.Parameters.AddWithValue("@nom", pers.nom);
                    cmd.Parameters.AddWithValue("@prenom", pers.prenom);
                    cmd.Parameters.AddWithValue("@adresse", pers.adresse);
                    cmd.Parameters.AddWithValue("@telephone", pers.telephone);
                    cmd.Parameters.AddWithValue("@date_de_naissance",pers.date_de_naissance);
                    cmd.Parameters.AddWithValue("@id", pers.id);
                    cmd.ExecuteNonQuery();
                }
                cnx.Close();
            }
        }

        public void SupprimerPersonne(Personne pers)
        {
            string requete = "delete from personne where id=@id";
            using (OleDbConnection cnx = new OleDbConnection(connectionString))
            {
                cnx.Open();
                using (OleDbCommand cmd = new OleDbCommand())
                {
                    cmd.Connection = cnx;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = requete;
                    cmd.Parameters.AddWithValue("@id", pers.id);
                    cmd.ExecuteNonQuery();
                }
                cnx.Close();
            }
        }

        public List<Personne> ListePersonne()
        {
            List<Personne> lstPers = new List<Personne>();
            string requete = "select * from personne";
            using(OleDbConnection cnx=new OleDbConnection(connectionString))
            {
                cnx.Open();
                using(OleDbCommand cmd=new OleDbCommand())
                {
                    cmd.Connection = cnx;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = requete;
                    var read = cmd.ExecuteReader();
                    if (read.HasRows)
                    {
                        while (read.Read())
                        {
                            lstPers.Add(new Personne
                            {
                                id = read["id"]==DBNull.Value?default(int):Convert.ToInt32(read["id"].ToString()),
                                nom = read["nom"] == DBNull.Value ? default(string) : read["nom"].ToString(),
                                prenom = read["prenom"] == DBNull.Value ? default(string) : read["prenom"].ToString(),
                                adresse = read["adresse"] == DBNull.Value ? default(string) : read["adresse"].ToString(),
                                telephone = read["telephone"] == DBNull.Value ? default(string) : read["telephone"].ToString(),
                                date_de_naissance = read["date_de_naissance"]==DBNull.Value?default(DateTime):Convert.ToDateTime(read["date_de_naissance"].ToString())
                            });
                        }
                    }
                }
                cnx.Close();
            }
            return lstPers;
        }
    }
}
