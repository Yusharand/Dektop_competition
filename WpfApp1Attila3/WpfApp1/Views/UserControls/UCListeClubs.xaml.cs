using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace WpfApp1.Views.UserControls
{
    /// <summary>
    /// Logique d'interaction pour UCListeClubs.xaml
    /// </summary>
    public partial class UCListeClubs : UserControl
    {
        private int Id;
        public UCListeClubs(int id)
        {
            InitializeComponent();
            this.Id = id;
            Charger();
        }

        public void Charger()
        {
            ConnexionBD connection = new ConnexionBD();
            ObservableCollection<Club> ListeClubs = new ObservableCollection<Club>();
            SqlDataReader reader = connection.Select("SELECT * FROM Clubs WHERE ID_Competition = " + this.Id);
            while (reader.Read())
            {
                string nom = reader["Nom_Club"].ToString();
                //byte[] logo = (byte[])reader["Logo_Club"];

                if (ListeClubs == null)
                {
                    Console.WriteLine("Pas de liste");
                }
                else
                {
                    ListeClubs.Add(new Club { Nom_Club = nom });
                }
            }
            connection.Close();
            ListeClubsDataGrid.ItemsSource = ListeClubs;
        }
    }
}
