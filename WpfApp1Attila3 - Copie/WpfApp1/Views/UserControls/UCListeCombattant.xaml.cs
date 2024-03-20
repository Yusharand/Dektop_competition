using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
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
using Excel = Microsoft.Office.Interop.Excel;
using OfficeOpenXml;
using Microsoft.Win32;
using Microsoft.Office.Interop.Excel;
using Application = Microsoft.Office.Interop.Excel.Application;
using Range = Microsoft.Office.Interop.Excel.Range;
using System.Data;
using System.IO;
using System.Linq;
using System.Data.Entity;
using System.ComponentModel;

namespace WpfApp1.Views.UserControls
{
    /// <summary>
    /// Logique d'interaction pour UCListeCombattant.xaml
    /// </summary>
    public partial class UCListeCombattant : UserControl
    {
        public int Id;
        public ObservableCollection<Combattant> items;
        private ObservableCollection<Combattant> listecombattant;
        private ObservableCollection<Combattant> ListeCombattants;
        private ObservableCollection<Combattant> combattantsNonSelectionnes = new ObservableCollection<Combattant>();
        //private ObservableCollection<Combattant> combattantsSelectionnes = new ObservableCollection<Combattant>();
        private Combattant combattant;
        private Competition_JJBEntities context = new Competition_JJBEntities();

        public UCListeCombattant(int id)
        {

            InitializeComponent();
            this.Id = id;
            Charger();
            cmbAge.Items.Add(new AgeRange(0, 10));
            cmbAge.Items.Add(new AgeRange(11, 14));
            cmbAge.Items.Add(new AgeRange(15, 18));
            cmbAge.Items.Add(new AgeRange(18, 35));
            cmbAge.Items.Add(new AgeRange(35, 70));

            // Créez et ajoutez des intervalles de poids aux ComboBox
            cmbPoids.Items.Add(new WeightRange(40, 50));
            cmbPoids.Items.Add(new WeightRange(51, 60));
            cmbPoids.Items.Add(new WeightRange(61, 70));
            cmbPoids.Items.Add(new WeightRange(71, 80));
            cmbPoids.Items.Add(new WeightRange(81, 90));
            cmbPoids.Items.Add(new WeightRange(91, 100));
            cmbPoids.Items.Add(new WeightRange(101, 120));
        }

        /*public int compteurData(int cpt, int id)
        {
            ConnexionBD connection = new ConnexionBD();
            SqlDataReader reader;
            if (id != 0)
            {
                reader = connection.Select("SELECT * FROM Combattant WHERE Combattant.ID_Combattant= " + this.Id);
                while (reader.Read()) cpt++;
            }
            else if (id == 0)
            {
                Console.WriteLine("");
                reader = connection.Select("SELECT * FROM Combattant");
                while (reader.Read()) cpt++;
            }

            return cpt;
        }
        */

        public void Charger()
        {
            try
            {
                ConnexionBD connection = new ConnexionBD();
                ConnexionBD connection1 = new ConnexionBD();
                string categorie;
                ListeCombattants = new ObservableCollection<Combattant>();
                SqlDataReader reader = connection.Select("SELECT c.ID_Combattant, c.ID_Categorie, c.ID_Club, c.Nom_Combattant, c.Prenom_Combattant, c.Genre_Combattant, c.Date_Naiss, c.Age, c.Grade, c.Poids, cl.Nom_Club " +
                                         "FROM Combattants c " +
                                         "JOIN Clubs cl ON c.ID_Club = cl.ID_Club " +
                                         "WHERE c.ID_Competition = " + this.Id);

                while (reader.Read())
                {
                    if (reader["ID_Categorie"].ToString() == "")
                    {
                        string numero = reader["ID_Combattant"].ToString();
                        string Id_Club = reader["ID_Club"].ToString();
                        string club = reader["Nom_Club"].ToString();
                        string nom = reader["Nom_Combattant"].ToString();
                        string prenom = reader["Prenom_Combattant"].ToString();
                        string genre = reader["Genre_Combattant"].ToString();
                        string date = reader["Date_Naiss"].ToString();
                        string age = reader["Age"].ToString();
                        string grade = reader["Grade"].ToString();
                        string poids = reader["Poids"].ToString();
                        

                        ListeCombattants.Add(new Combattant { ID_Combattant = int.Parse(numero), ID_Club = int.Parse(Id_Club), Nom_Club = club, Nom_Combattant = nom, Prenom_Combattant = prenom, Genre_Combattant = genre, Date_Naiss = DateTime.Parse(date), Age = int.Parse(age), Grade = grade, Poids = double.Parse(poids) });
                    }




                    if (ListeCombattants == null)
                    {
                        Console.WriteLine("Pas de liste");
                    }




                }
                connection.Close();

                ListeCombattantsDataGrid.ItemsSource = ListeCombattants;
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur:" + ex.Message);
            }


        }

        private void Import_Click(object sender, RoutedEventArgs e)
        {
            // Utilisez OpenFileDialog pour permettre à l'utilisateur de sélectionner le fichier Excel.

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Fichiers Excel (*.xlsx)|*.xlsx";
            if (openFileDialog.ShowDialog() == true)
            {
                string excelFilePath = openFileDialog.FileName;
                ImportExcelDataToSqlServer(excelFilePath);
            }
        }

        private void ImportExcelDataToSqlServer(string excelFilePath)
        {
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            /*try
            {*/
                string filepath = @"E:\connexion.txt";
                using (var package = new ExcelPackage(new FileInfo(excelFilePath)))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                    int rowCount = worksheet.Dimension.Rows;

                    string connectionString = File.ReadAllText(filepath);
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        string club_actuel = "";
                        
                        SqlCommand cmd1 = new SqlCommand("INSERT INTO Clubs (Nom_Club, ID_Competition) VALUES (@Colonne2, @Compet)", connection);
                        for (int row = 2; row <= rowCount; row++)
                        {
                            if(club_actuel != worksheet.Cells[row, 2].Value.ToString())
                            {
                                cmd1.Parameters.Clear();
                                cmd1.Parameters.AddWithValue("@Colonne2", worksheet.Cells[row, 2].Value);
                                cmd1.Parameters.AddWithValue("@Compet", this.Id);

                                cmd1.ExecuteNonQuery();
                                club_actuel = worksheet.Cells[row, 2].Value.ToString();
                            }
                            row++;    
                        }



                    SqlCommand cmd = new SqlCommand("INSERT INTO Combattants (ID_Club, Nom_Combattant, Prenom_Combattant, Genre_Combattant, Date_Naiss, Age, Grade, Poids, ID_Competition) " +
                                                         "VALUES (@IdClub, @Colonne3, @Colonne4, @Colonne5, @Colonne6, @Colonne7, @Colonne8, @Colonne9, @Compet)", connection);
                    for (int row = 2; row <= rowCount; row++)
                        {
                            string nomClub = worksheet.Cells[row, 2].Value.ToString();

                            // Requête pour obtenir l'id_club correspondant au nom du club
                            SqlCommand getIdClubCmd = new SqlCommand("SELECT ID_Club FROM Clubs WHERE Nom_Club = @NomClub", connection);
                            getIdClubCmd.Parameters.AddWithValue("@NomClub", nomClub);


                            // Exécuter la commande pour obtenir l'id_club
                            int idClub = Convert.ToInt32(getIdClubCmd.ExecuteScalar());

                            // Maintenant que vous avez l'id_club, vous pouvez l'utiliser dans votre commande d'insertion
                            

                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@IdClub", idClub);
                            cmd.Parameters.AddWithValue("@Colonne3", worksheet.Cells[row, 3].Value);
                            cmd.Parameters.AddWithValue("@Colonne4", worksheet.Cells[row, 4].Value);
                            cmd.Parameters.AddWithValue("@Colonne5", worksheet.Cells[row, 5].Value);
                            cmd.Parameters.AddWithValue("@Colonne6", worksheet.Cells[row, 6].Value);
                            cmd.Parameters.AddWithValue("@Colonne7", worksheet.Cells[row, 7].Value);
                            cmd.Parameters.AddWithValue("@Colonne8", worksheet.Cells[row, 8].Value);
                            cmd.Parameters.AddWithValue("@Colonne9", worksheet.Cells[row, 9].Value);
                            cmd.Parameters.AddWithValue("@Compet", this.Id);

                            cmd.ExecuteNonQuery();
                        }



                        connection.Close();
                    }

                    MessageBox.Show("Données Excel importées avec succès dans la base de données.");
                }
            /*}
            catch (Exception ex)
            {
                MessageBox.Show("Erreur: " + ex.Message);
            }*/
        }

        private void ListeCategorie_Click(object sender, RoutedEventArgs e)
        {
            UCListeCategorie UcListeCategorie = new UCListeCategorie(this.Id); //User controle de Liste des Catégories


            //int cpt = UcListeCombattant.compteurData(0, this.Id);
            ListeCat ListeCategorie = new ListeCat(); //get list of patient
            this.DataContext = ListeCategorie;
            combattantsDataGrid.Children.Clear();
            combattantsDataGrid.Children.Add(UcListeCategorie);
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

        private void BtnModifier_Click(object sender, RoutedEventArgs e)
        {
            Combattant selectedMember = (Combattant)ListeCombattantsDataGrid.SelectedItem;
            int id = selectedMember.ID_Combattant;

            ModifierCombattant modifierCombattant = new ModifierCombattant(id, this);
            modifierCombattant.Show();
        }

        private void CreerCat_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var combattantsSelectionnes = ListeCombattantsDataGrid.SelectedItems.Cast<Combattant>().ToList();
                // Afficher une boîte de dialogue pour saisir le nom de la catégorie
                string nomCategorie = PromptUserForCategoryName();

                if (!string.IsNullOrEmpty(nomCategorie))
                {
                    // Créer une nouvelle catégorie
                    Category nouvelleCategorie = new Category
                    {
                        Nom_Categorie = nomCategorie,
                        ID_Competition = this.Id,
                    };
                    context.Categories.Add(nouvelleCategorie);

                    // Ajouter les combattants sélectionnés à la nouvelle catégorie
                    foreach (var combattant in combattantsSelectionnes)
                    {
                        //nouvelleCategorie.Combattants.Add(combattant);
                        combattant.ID_Categorie = nouvelleCategorie.ID_Categorie;
                        context.Combattants.Attach(combattant);
                        context.Entry(combattant).State = EntityState.Modified;
                        

                    }



                    // Ajouter la catégorie à la base de données via Entity Framework


                    context.SaveChanges();
                    Charger();
                    MessageBox.Show("Catégorie créée avec succès!");
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
            string categoryName = string.Empty;

            // Créez une boîte de dialogue InputBox personnalisée pour saisir le nom de la catégorie
            InputBox inputBox = new InputBox("Entrez le nom de la catégorie :", "Créer une nouvelle catégorie");

            if (inputBox.ShowDialog() == true)
            {
                categoryName = inputBox.InputText;
            }

            return categoryName;
        }
        /*private int InsererNouvelleCategorie()
        {
            int nouvelleCategorieID = 0;

            using (SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-0K417JF;Initial Catalog=Competition_JJB;Integrated Security=True"))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand("INSERT INTO Categories (Nom_Categorie) VALUES (@Nom); SELECT SCOPE_IDENTITY();", connection))
                {
                    cmd.Parameters.AddWithValue("@Nom", "Nouvelle Catégorie"); // Remplacez par le nom de la catégorie souhaité

                    // Exécutez la commande SQL et récupérez l'ID de la nouvelle catégorie
                    nouvelleCategorieID = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }

            return nouvelleCategorieID;
        }

        private void AssocierCombattantsACategorie(int categorieID)
        {
            // Code pour mettre à jour la colonne ID_Categorie des combattants sélectionnés
            using (SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-0K417JF;Initial Catalog=Competition_JJB;Integrated Security=True"))
            {
                connection.Open();
                foreach (var combattant in combattantsSelectionnes)
                {
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO Combattants(ID_Categorie) VALUES(@CategorieID) WHERE ID_Combattant = @CombattantID", connection))
                    {
                        cmd.Parameters.AddWithValue("@CategorieID", categorieID);
                        cmd.Parameters.AddWithValue("@CombattantID", combattant.ID_Combattant); // Remplacez "ID" par le nom de la colonne ID de votre table Combattants

                        // Exécutez la commande SQL pour mettre à jour la catégorie du combattant
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }*/
        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {


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



        /* private void Search_Click(object sender, RoutedEventArgs e)
         {

             string columnName = cmbColumns.SelectedItem?.ToString();
             string searchTerm = txtSearch.Text;

             ObservableCollection<Combattant> filteredPeople = new ObservableCollection<Combattant>(
             items.Where(combattant => columnName == null || combattant.IsMatch(columnName, searchTerm))
         );
             ListeCombattantsDataGrid.ItemsSource = filteredPeople;



         }*/
    }
}




