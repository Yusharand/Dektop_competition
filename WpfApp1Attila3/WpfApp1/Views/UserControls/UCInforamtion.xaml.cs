using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
using WpfApp1.Models;

namespace WpfApp1.Viewx.UserControls
{
    /// <summary>
    /// Logique d'interaction pour UCInforamtion.xaml
    /// </summary>
    public partial class UCInforamtion : UserControl
    {
        public int Id;
        public UCInforamtion(int id)
        {
            InitializeComponent();
            this.Id = id;

            ConnexionBD connexionBD = new ConnexionBD();

            SqlDataReader reader = connexionBD.Select("SELECT * FROM Competitions WHERE ID_Competition = " + this.Id);
            while (reader.Read())
            {
                string numero = reader["ID_Competition"].ToString();
                string nom = reader["Nom_Competition"].ToString();
                string lieuC = reader["Lieu_Competition"].ToString();
                string dateC = reader["Date_Competition"].ToString();
                string lieuP = reader["LieuPese_Competition"].ToString();
                string dateP = reader["Date_Pese"].ToString();
                string condition = reader["Condition_Pese"].ToString();
                string droit = reader["DroitCombattant"].ToString();
                string remboursement = reader["Remboursement"].ToString();

                txtNomCompet.Text = nom;
                txtLieuCompet.Text = lieuC;
                txtDateCompet.Text = dateC;
                txtLieuPese.Text = lieuP;
                txtDatePese.Text = dateP;
                txtCondiPese.Text = condition;
                txtDroitCombattant.Text = droit;
                txtRemboursement.Text = remboursement;
            }
        }
    }
}




