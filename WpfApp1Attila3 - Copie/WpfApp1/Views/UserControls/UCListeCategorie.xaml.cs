﻿using System;
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
        private int Id;
        public UCListeCategorie(int id)
        {
            InitializeComponent();
            this.Id = id;
            Charger();
        }

        public void Charger()
        {
            ConnexionBD connection = new ConnexionBD();
            ObservableCollection<Category> ListeCategories = new ObservableCollection<Category>();
            SqlDataReader reader = connection.Select("SELECT * FROM Categories WHERE ID_Competition = " +this.Id);
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
            ConnexionBD connection2 = new ConnexionBD();
            ConnexionBD connection3 = new ConnexionBD();
            ConnexionBD connection4 = new ConnexionBD();
            ConnexionBD connection5 = new ConnexionBD();
            connection2.Update("UPDATE Combattants SET ID_Categorie = NULL WHERE ID_Categorie = " + id);
            connection2.Close();
            connection3.Update("UPDATE Combattants SET ID_Poule = NULL WHERE ID_Categorie = " + id);
            connection3.Close();
            connection4.Update("UPDATE Poules SET ID_Categorie = NULL WHERE ID_Categorie = " + id);
            connection4.Close();
            connection5.Delete("DELETE FROM Combats WHERE ID_Categorie = " + id);
            connection5.Close();
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
            int id = selectedMember.ID_Categorie;
            Brackets brackets = new Brackets(this.Id,id);
            brackets.Show();
        }

        private void ListeCategoriesDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Category selectedMember = (Category)ListeCategoriesDataGrid.SelectedItem;
                int id = selectedMember.ID_Categorie;

                ListeCombattantCat listeCombattantCat = new ListeCombattantCat(this.Id, id);
                listeCombattantCat.Show();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void Poule_Click(object sender, RoutedEventArgs e)
        {
            Category categorieSelectionnee = (Category)ListeCategoriesDataGrid.SelectedItem;
            int id = categorieSelectionnee.ID_Categorie;

            ListePoule listePoule = new ListePoule(id);
            listePoule.Show();
        }
    }
}







