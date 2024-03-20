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
using System.Windows.Shapes;
using WpfApp1.Models;

namespace WpfApp1.Views
{
    /// <summary>
    /// Logique d'interaction pour CompetitionExist.xaml
    /// </summary>
    public partial class CompetitionExist : Window
    {
        private ObservableCollection<Competition> ListeCompetitions;
        public CompetitionExist()
        {
            InitializeComponent();
            Charger();
            /* ConnexionBD connection = new ConnexionBD();
             ListeCompetitions = new ObservableCollection<string>();
             SqlDataReader reader = connection.Select("SELECT * FROM Competitions");

             while (reader.Read())
             {
                 ListeCompetitions.Add(reader["Nom_Competition"].ToString());
             }
             cbxCompet.ItemsSource = ListeCompetitions;*/
        }

        public void Charger()
        {
            ConnexionBD connection = new ConnexionBD();
            ListeCompetitions = new ObservableCollection<Competition>();
            SqlDataReader reader = connection.Select("SELECT * FROM Competitions ");
            while (reader.Read())
            {
                string numero = reader["ID_Competition"].ToString();
                string nom = reader["Nom_Competition"].ToString();
                ListeCompetitions.Add(new Competition { ID_Competition = int.Parse(numero), Nom_Competition = nom });
            }

            ListesCompetitionDataGrid.ItemsSource = ListeCompetitions;
        }


        private void DeleteCat_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Competition selectedMember = (Competition)ListesCompetitionDataGrid.SelectedItem;
                int id = selectedMember.ID_Competition;

                ConnexionBD connection = new ConnexionBD();
                ConnexionBD connection1 = new ConnexionBD();
                ConnexionBD connection2 = new ConnexionBD();
                ConnexionBD connection3 = new ConnexionBD();
                ConnexionBD connection4 = new ConnexionBD();
                ConnexionBD connection5 = new ConnexionBD();

                connection.Delete("DELETE FROM Combats WHERE ID_Competition = " + id);
                connection1.Delete("DELETE FROM Poules WHERE ID_Competition = " + id);
                connection2.Delete("DELETE FROM Combattants WHERE ID_Competition = " + id);
                connection3.Delete("DELETE FROM Clubs WHERE ID_Competition = " + id);
                connection4.Delete("DELETE FROM Categories WHERE ID_Competition = " + id);
                connection5.Delete("DELETE FROM Competitions WHERE ID_Competition = " + id);
                connection.Close();
                connection1.Close();
                connection2.Close();
                connection3.Close();
                connection4.Close();
                connection5.Close();
                Charger();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void BtnRetour_Click(object sender, RoutedEventArgs e)
        {
            ChoixCompet choixCompet = new ChoixCompet();
            choixCompet.Show();
            this.Close();
        }

        private void BtnValider_Click(object sender, RoutedEventArgs e)
        {
            
            try
            {
                Competition selectedMember = (Competition)ListesCompetitionDataGrid.SelectedItem;
                int id = selectedMember.ID_Competition;
                DashboardCombattant dashboardCombattant = new DashboardCombattant(id);
                dashboardCombattant.Show();
                this.Close();
            }
            catch(Exception ex)
            {
                
                MessageBox.Show(ex.Message);
            }
        }
    }
}




