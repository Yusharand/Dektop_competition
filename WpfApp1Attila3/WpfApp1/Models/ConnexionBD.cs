using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Windows;

namespace WpfApp1.Models
{
    class ConnexionBD
    {
        public SqlConnection Con;
        public SqlCommand Cmd;
        string filepath = @"E:\connexion.txt";
        public ConnexionBD()
        {
            string connectionString = File.ReadAllText(filepath);
            Con = new SqlConnection(connectionString);
            //Cmd = new SqlCommand();
            //Cmd.Connection = Con;
            Console.WriteLine("Mande ");
        }

        public SqlDataReader Select(string requete)
        {
            Con.Open();
            Cmd = new SqlCommand(requete, Con);
            SqlDataReader reader = Cmd.ExecuteReader();
            return reader;
        }

        public DataSet SelectDataSet(string requete)
        {
            SqlCommand command = new SqlCommand(requete, Con);
            SqlDataAdapter sda = new SqlDataAdapter(command);
            DataSet dt = new DataSet();
            sda.Fill(dt);
            return dt;
        }

        public void Insert(string requete)
        {
            Con.Open();
            SqlCommand command = new SqlCommand(requete, Con);
            command.ExecuteNonQuery();
            //Con.Close();
        }

        public void Delete(string requete)
        {
            try
            {
                Con.Open();
                SqlCommand command = new SqlCommand(requete, Con);
                command.ExecuteNonQuery();
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
            
            //Con.Close();
        }

        public void Update(string requete)
        {
            Con.Open();

            SqlCommand command = new SqlCommand(requete, Con);

            command.ExecuteNonQuery();
            //Con.Close();
        }

        /*MBOLA TSY METY
         * public DataSet RecupererId(string requete, string id)
        {

            Con.Open();
            SqlCommand command = new SqlCommand(requete, Con);
            SqlDataAdapter sda = new SqlDataAdapter(command);
            DataSet dt = new DataSet();
            sda.Fill(dt);
            return dt;
            //return int.Parse(dt.Tables[0].Rows[0][id].ToString());
        }*/

        public void Close()
        {
            Con.Close();
        }
    }
}
