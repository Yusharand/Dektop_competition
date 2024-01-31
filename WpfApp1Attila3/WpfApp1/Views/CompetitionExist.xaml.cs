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
            Competition selectedMember = (Competition)ListesCompetitionDataGrid.SelectedItem;
            int id = selectedMember.ID_Competition;

            ConnexionBD connection = new ConnexionBD();
            connection.Delete("DELETE FROM Competitions WHERE ID_Competition = " + id);
            connection.Close();
            Charger();

        }
        private void BtnRetour_Click(object sender, RoutedEventArgs e)
        {
            ChoixCompet choixCompet = new ChoixCompet();
            choixCompet.Show();
            this.Close();
        }

        private void BtnValider_Click(object sender, RoutedEventArgs e)
        {
            Competition selectedMember = (Competition)ListesCompetitionDataGrid.SelectedItem;
            int id = selectedMember.ID_Competition;
            DashboardCombattant dashboardCombattant = new DashboardCombattant(id);
            dashboardCombattant.Show();
            this.Close();
        }
    }
}




