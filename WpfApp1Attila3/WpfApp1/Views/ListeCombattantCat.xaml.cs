using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using WpfApp1.Views.UserControls;

namespace WpfApp1.Views
{
    /// <summary>
    /// Logique d'interaction pour ListeCombattantCat.xaml
    /// </summary>
    public partial class ListeCombattantCat : Window
    {
        public UCListeCombattant UCListeC;
        public ObservableCollection<Combattant> ListeCombattantsCat;
        public int Id;
        private Competition_JJBEntities context = new Competition_JJBEntities();

        public ListeCombattantCat(int id)
        {
            InitializeComponent();
            this.Id = id;

            Charger();
        }

        public void Charger()
        {
            ConnexionBD connection = new ConnexionBD();
            ListeCombattantsCat = new ObservableCollection<Combattant>();
            SqlDataReader reader = connection.Select("SELECT * FROM Combattants WHERE ID_Categorie= " + this.Id);
            while (reader.Read())
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


                if (ListeCombattantsCat == null)
                {
                    Console.WriteLine("Pas de liste");
                }
                else
                {
                    ListeCombattantsCat.Add(new Combattant { ID_Combattant = int.Parse(numero), Club_Combattant = club, Nom_Combattant = nom, Prenom_Combattant = prenom, Genre_Combattant = genre, Date_Naiss = DateTime.Parse(date), Age = int.Parse(age), Grade = grade, Poids = double.Parse(poids) });

                }



            }
            connection.Close();

            ListeCombattantsCatDataGrid.ItemsSource = ListeCombattantsCat;

        }

        private void DeleteCatCombattant_Click(object sender, RoutedEventArgs e)
        {
            var combattantsSelectionnes = ListeCombattantsCatDataGrid.SelectedItems.Cast<Combattant>().ToList();
            foreach (var combattant in combattantsSelectionnes)
            {
                //nouvelleCategorie.Combattants.Add(combattant);
                combattant.ID_Categorie = null;
                context.Combattants.Attach(combattant);
                context.Entry(combattant).State = EntityState.Modified;
                ListeCombattantsCat.Remove(combattant);

            }



            // Ajouter la catégorie à la base de données via Entity Framework


            context.SaveChanges();

        }

        private void CreerPoule_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var combattantsSelectionnes = ListeCombattantsCatDataGrid.SelectedItems.Cast<Combattant>().ToList();
                // Afficher une boîte de dialogue pour saisir le nom de la catégorie
                string nomPoule = PromptUserForCategoryName();

                if (!string.IsNullOrEmpty(nomPoule))
                {
                    // Créer une nouvelle catégorie
                    Poule nouvellePoule = new Poule
                    {
                        Nom_poule = nomPoule,
                        ID_Categorie = this.Id
                    };
                    context.Poules.Add(nouvellePoule);

                    // Ajouter les combattants sélectionnés à la nouvelle catégorie
                    foreach (var combattant in combattantsSelectionnes)
                    {
                        //nouvelleCategorie.Combattants.Add(combattant);
                        combattant.ID_Poule = nouvellePoule.ID_Poule;
                        context.Combattants.Attach(combattant);
                        context.Entry(combattant).State = EntityState.Modified;
                        ListeCombattantsCat.Remove(combattant);

                    }



                    // Ajouter la catégorie à la base de données via Entity Framework


                    context.SaveChanges();

                    MessageBox.Show("Poule créée avec succès!");
                }
                else
                {
                    MessageBox.Show("Entrer le nom de catégorie");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur" + ex.Message);
            }
        }

        private string PromptUserForCategoryName()
        {
            throw new NotImplementedException();
        }

        private void AjoutCombattant_Click(object sender, RoutedEventArgs e)
        {
            AjoutCombattantCat ajoutCombattantCat = new AjoutCombattantCat(this.Id);
            ajoutCombattantCat.Show();

        }

        private void Move_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}




