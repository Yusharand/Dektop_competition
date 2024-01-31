using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
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
    /// Logique d'interaction pour UCListeCategorie.xaml
    /// </summary>
    public partial class UCListeCategorie : UserControl
    {
        public UCListeCategorie()
        {
            InitializeComponent();
            Charger();
        }

        public void Charger()
        {
            ConnexionBD connection = new ConnexionBD();
            ObservableCollection<Category> ListeCategories = new ObservableCollection<Category>();
            SqlDataReader reader = connection.Select("SELECT * FROM Categories ");
            while (reader.Read())
            {
                string id = reader["ID_Categorie"].ToString();
                string nom = reader["Nom_Categorie"].ToString();

                if (ListeCategories == null)
                {
                    Console.WriteLine("Pas de liste");
                }
                else
                {
                    ListeCategories.Add(new Category { ID_Categorie = int.Parse(id), Nom_Categorie = nom });
                }
            }
            connection.Close();
            ListeCategoriesDataGrid.ItemsSource = ListeCategories;
        }


        private void DeleteCat_Click(object sender, RoutedEventArgs e)
        {
            Category selectedMember = (Category)ListeCategoriesDataGrid.SelectedItem;
            int id = selectedMember.ID_Categorie;
            ConnexionBD connection = new ConnexionBD();
            ConnexionBD connection2 = new ConnexionBD(); ;
            connection2.Update("UPDATE Combattants SET ID_Categorie = NULL WHERE ID_Categorie = " + id);
            connection2.Close();
            connection.Delete("DELETE FROM Categories WHERE ID_Categorie = " + id);
            connection.Close();

            Charger();
        }

        private void ModifCat_Click(object sender, RoutedEventArgs e)
        {
            Category selectedMember = (Category)ListeCategoriesDataGrid.SelectedItem;
            int id = selectedMember.ID_Categorie;

            CreerCategorie creerCategorie = new CreerCategorie(id, this);
            creerCategorie.Show();
        }

        private void ShowList_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void Bracket_Click(object sender, RoutedEventArgs e)
        {
            Category selectedMember = (Category)ListeCategoriesDataGrid.SelectedItem;
            

            Brackets brackets = new Brackets(selectedMember);
            brackets.Show();
        }

        private void ListeCategoriesDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Category selectedMember = (Category)ListeCategoriesDataGrid.SelectedItem;
            int id = selectedMember.ID_Categorie;

            ListeCombattantCat listeCombattantCat = new ListeCombattantCat(id);
            listeCombattantCat.Show();
        }

        private void Poule_Click(object sender, RoutedEventArgs e)
        {
            Category categorieSelectionnee = (Category)ListeCategoriesDataGrid.SelectedItem;
            

            ListePoule listePoule = new ListePoule(categorieSelectionnee.Poules);
            listePoule.Show();
        }
    }
}







