using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
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
using WpfApp1.Views;


namespace WpfApp1.Views
{
    /// <summary>
    /// Logique d'interaction pour AjoutCombattantCat.xaml
    /// </summary>
    public partial class AjoutCombattantCat : Window
    {
        int Id;
        public ObservableCollection<Combattant> items;
        private ObservableCollection<Combattant> listecombattant;
        private ObservableCollection<Combattant> ListeCombattants;
        private ObservableCollection<Combattant> combattantsNonSelectionnes = new ObservableCollection<Combattant>();
        //private ObservableCollection<Combattant> combattantsSelectionnes = new ObservableCollection<Combattant>();
        private Combattant combattant;
        private Competition_JJBEntities context = new Competition_JJBEntities();

        public AjoutCombattantCat(int id)
        {
            InitializeComponent();
            this.Id = id;
            Charger();

        }

        public void Charger()
        {
            try
            {
                ConnexionBD connection = new ConnexionBD();
                ConnexionBD connection1 = new ConnexionBD();
                string categorie;
                ListeCombattants = new ObservableCollection<Combattant>();
                SqlDataReader reader = connection.Select("SELECT * FROM Combattants");

                while (reader.Read())
                {
                    if (reader["ID_Categorie"].ToString() == "")
                    {
                        string numero = reader["ID_Combattant"].ToString();
                        string club = reader["Club_Combattant"].ToString();
                        string nom = reader["Nom_Combattant"].ToString();
                        string prenom = reader["Prenom_Combattant"].ToString();
                        string genre = reader["Genre_Combattant"].ToString();
                        string date = reader["Date_Naiss"].ToString();
                        string age = reader["Age"].ToString();
                        string grade = reader["Grade"].ToString();
                        string poids = reader["Poids"].ToString();

                        ListeCombattants.Add(new Combattant { ID_Combattant = int.Parse(numero), Club_Combattant = club, Nom_Combattant = nom, Prenom_Combattant = prenom, Genre_Combattant = genre, Date_Naiss = DateTime.Parse(date), Age = int.Parse(age), Grade = grade, Poids = double.Parse(poids) });
                    }




                    if (ListeCombattants == null)
                    {
                        Console.WriteLine("Pas de liste");
                    }




                }
                connection.Close();

                ListeCombattantsDataGrid.ItemsSource = ListeCombattants;
                listecombattant = ListeCombattants;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur:" + ex.Message);
            }


        }

        private void BtnSupprimer_Click(object sender, RoutedEventArgs e)
        {
            Combattant selectedMember = (Combattant)ListeCombattantsDataGrid.SelectedItem;
            int id = selectedMember.ID_Combattant;

            ConnexionBD connection = new ConnexionBD();
            connection.Delete("DELETE FROM Combattants WHERE ID_Combattant = " + id);
            connection.Close();
            Charger();
        }

        private void Filter_Click(object sender, RoutedEventArgs e)
        {
            var collectionView = CollectionViewSource.GetDefaultView(ListeCombattantsDataGrid.ItemsSource) as ICollectionView;

            if (collectionView != null)
            {
                collectionView.Filter = item =>
                {
                    var combattant = item as Combattant;
                    if (combattant != null)
                    {
                        bool include = true;

                        if (chkGrade.IsChecked == true && cmbGrade.SelectedItem != null)
                        {
                            // Filtrez en fonction de la grade sélectionnée
                            var selectedGrade = (cmbGrade.SelectedItem as ComboBoxItem).Content.ToString();
                            if (combattant.Grade != selectedGrade)
                                include = false;
                        }
                        if (chkAge.IsChecked == true && cmbAge.SelectedItem != null)
                        {
                            // Filtrez en fonction de l'intervalle d'âge sélectionné
                            int age = combattant.Age;
                            var selectedAgeRange = cmbAge.SelectedItem as AgeRange; // Définissez votre classe pour les intervalles d'âge
                            if (selectedAgeRange != null && (age < selectedAgeRange.Min || age > selectedAgeRange.Max))
                                include = false;
                        }
                        if (chkPoids.IsChecked == true && cmbPoids.SelectedItem != null)
                        {
                            // Filtrez en fonction de l'intervalle de poids sélectionné
                            double poids = combattant.Poids;
                            var selectedWeightRange = cmbPoids.SelectedItem as WeightRange; // Définissez votre classe pour les intervalles de poids
                            if (selectedWeightRange != null && (poids < selectedWeightRange.Min || poids > selectedWeightRange.Max))
                                include = false;
                        }
                        // Répétez pour les autres critères de filtrage

                        return include;
                    }
                    return false;
                };
            }
        }

        private void Ajout_Click(object sender, RoutedEventArgs e)
        {
            //try
            //{
            var combattantsSelectionnes = ListeCombattantsDataGrid.SelectedItems.Cast<Combattant>().ToList();
            foreach (var combattant in combattantsSelectionnes)
            {
                //nouvelleCategorie.Combattants.Add(combattant);
                combattant.ID_Categorie = this.Id;
                context.Combattants.Attach(combattant);
                context.Entry(combattant).State = EntityState.Modified;
                ListeCombattants.Remove(combattant);

            }



            // Ajouter la catégorie à la base de données via Entity Framework


            context.SaveChanges();

            MessageBox.Show("Ajout avec succès");
            /*}

            catch(Exception ex)
            {
                MessageBox.Show("Erreur: " + ex.Message);
            }*/

        }
    }
}



