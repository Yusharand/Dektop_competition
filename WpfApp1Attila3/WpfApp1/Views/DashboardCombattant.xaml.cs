using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using ExcelDataReader;
using Microsoft.Win32;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfApp1.Models;
using System.Data;
using Excel = Microsoft.Office.Interop.Excel;
using OfficeOpenXml;
using WpfApp1.Views;
using WpfApp1.Views.UserControls;
using System.Linq;
using WpfApp1.Viewx.UserControls;

namespace WpfApp1.Views
{
    /// <summary>
    /// Logique d'interaction pour DashboardCombattant.xaml
    /// </summary>
    public partial class DashboardCombattant : System.Windows.Window
    {

        public int Id;

        public OpenFileDialog openFD;
        public object sender = new object();
        public MouseButtonEventArgs e;
        public DashboardCombattant(int id)
        {

            InitializeComponent();
            this.Id = id;
            Information_Click(sender, e);
            Charger_Information();  
            
        }

        public void Charger_Information()
        {
            ConnexionBD connexionBD = new ConnexionBD();

            SqlDataReader reader = connexionBD.Select("SELECT * FROM Competitions WHERE ID_Competition = " + this.Id);

            while (reader.Read())
            {
                string numero = reader["ID_Competition"].ToString();
                string nom = reader["Nom_Competition"].ToString();
                string lieu = reader["Lieu_Competition"].ToString();
                string logo = reader["Logo_Competition"].ToString();

                
                /*
                if (logo != null)
                {
                    BitmapImage bitmapImage = new BitmapImage();
                    bitmapImage.BeginInit();
                    bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                    bitmapImage.StreamSource = new MemoryStream(logo);
                    bitmapImage.EndInit();

                    Image img = new Image();
                    img.Source = bitmapImage;
                    imageLogoDash.Fill = new ImageBrush(img.Source);
                }*/

                 BitmapImage imglogo = new BitmapImage(new Uri(logo.ToString(), UriKind.RelativeOrAbsolute));
                 imageLogoDash.Source = imglogo;


                nomCompet.Text = nom;
                lieuCompet.Text = lieu;

            }
        }

        

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }
        private bool IsMaximized = false;


        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                if (IsMaximized)
                {
                    this.WindowState = WindowState.Normal;
                    this.Width = 1080;
                    this.Height = 720;

                    IsMaximized = false;
                }
                else
                {
                    this.WindowState = WindowState.Maximized;

                    IsMaximized = true;
                }
            }
        }

        private void Retour_Click(object sender, RoutedEventArgs e)
        {
            ChoixCompet choixCompet = new ChoixCompet();
            choixCompet.Show();
            this.Close();
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
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            try
            {
                string filepath = @"E:\connexion.txt";
                using (var package = new ExcelPackage(new FileInfo(excelFilePath)))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                    int rowCount = worksheet.Dimension.Rows;

                    string connectionString = File.ReadAllText(filepath);
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        SqlCommand cmd = new SqlCommand("INSERT INTO Combattants (ID_Combattant, Club_Combattant, Nom_Combattant, Prenom_Combattant, Genre_Combattant, Date_Naiss, Age, Grade, Poids) VALUES (@Colonne1, @Colonne2, @Colonne3, @Colonne4, @Colonne5,@Colonne6, @Colonne7, @Colonne8, @Colonne9)", connection);

                        for (int row = 2; row <= rowCount; row++)
                        {
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@Colonne1", worksheet.Cells[row, 1].Value);
                            cmd.Parameters.AddWithValue("@Colonne2", worksheet.Cells[row, 2].Value);
                            cmd.Parameters.AddWithValue("@Colonne3", worksheet.Cells[row, 3].Value);
                            cmd.Parameters.AddWithValue("@Colonne4", worksheet.Cells[row, 4].Value);
                            cmd.Parameters.AddWithValue("@Colonne5", worksheet.Cells[row, 5].Value);
                            cmd.Parameters.AddWithValue("@Colonne6", worksheet.Cells[row, 6].Value);
                            cmd.Parameters.AddWithValue("@Colonne7", worksheet.Cells[row, 7].Value);
                            cmd.Parameters.AddWithValue("@Colonne8", worksheet.Cells[row, 8].Value);
                            cmd.Parameters.AddWithValue("@Colonne9", worksheet.Cells[row, 9].Value);


                            cmd.ExecuteNonQuery();
                        }

                        connection.Close();
                    }

                    MessageBox.Show("Données Excel importées avec succès dans la base de données.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur: " + ex.Message);
            }
        }


        private void Information_Click(object sender, RoutedEventArgs e)
        {
            #region Animate
            /*btnListeC.IsEnabled = true;
            Button clickedButton = sender as Button;
            if (previousButton != null && previousButton != clickedButton)
            {
                previousButton.IsEnabled = false;
            }
            previousButton = clickedButton;*/
            #endregion
            #region programme
            UCInforamtion UcInforamtion = new UCInforamtion(this.Id); //User controle de l'information de la compétition


            //int cpt = UcListeCombattant.compteurData(0, this.Id);
            Info info = new Info();
            this.DataContext = info;
            combattantsDataGrid.Children.Clear();
            combattantsDataGrid.Children.Add(UcInforamtion);

            #endregion
        }


        private void ListeCombattant_Click(object sender, RoutedEventArgs e)
        {
            #region Animate
            /*btnListeC.IsEnabled = true;
            Button clickedButton = sender as Button;
            if (previousButton != null && previousButton != clickedButton)
            {
                previousButton.IsEnabled = false;
            }
            previousButton = clickedButton;*/
            #endregion
            #region programme
            UCListeCombattant UcListeCombattant = new UCListeCombattant(this.Id); //User controle de Liste des Combattants


            //int cpt = UcListeCombattant.compteurData(0, this.Id);
            ListeC ListeCombattant = new ListeC(); //get list of patient
            this.DataContext = ListeCombattant;
            combattantsDataGrid.Children.Clear();
            combattantsDataGrid.Children.Add(UcListeCombattant);

            #endregion
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
        /*private void FormCombat_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Formulaire formulaire = new Formulaire(this.Id);
                Application.Current.MainWindow = formulaire;

                formulaire.Show();

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur:" + ex.Message);
            }
        }*/

        private void AjoutCombattant_Click(object sender, RoutedEventArgs e)
        {
            AjoutCombattant ajoutCombattant = new AjoutCombattant();
            Application.Current.MainWindow = ajoutCombattant;
            ajoutCombattant.Show();
        }




        private void TSupprimer_Click(object sender, RoutedEventArgs e)
        {
            ConnexionBD connection = new ConnexionBD();
            ConnexionBD connection1 = new ConnexionBD();
            ConnexionBD connection2 = new ConnexionBD();
            ConnexionBD connection3 = new ConnexionBD();
            ConnexionBD connection4 = new ConnexionBD();

            connection.Delete("DELETE FROM Combats WHERE ID_Competition = " + this.Id);
            connection1.Delete("DELETE FROM Combattants WHERE ID_Competition = " + this.Id);
            connection2.Delete("DELETE FROM Poules WHERE ID_Competition = " + this.Id);
            connection3.Delete("DELETE FROM Clubs WHERE ID_Competition = " + this.Id);
            connection4.Delete("DELETE FROM Categories WHERE ID_Competition = " + this.Id);
            connection.Close();
            connection1.Close();
            connection2.Close();
            connection3.Close();
            connection4.Close();
            ListeCombattant_Click(sender, e);
        }

        private void LogOut_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Login login = new Login();
                login.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur: " + ex.Message);
            }
        }

        private void BtnListeCombats_Click(object sender, RoutedEventArgs e)
        {
            UCListeCombat ucListeCombat = new UCListeCombat(this.Id);
            ListeCombat listeCombat = new ListeCombat();
            this.DataContext = listeCombat;
            combattantsDataGrid.Children.Clear();
            combattantsDataGrid.Children.Add(ucListeCombat);
        }

        private void BtnListeClub_Click(object sender, RoutedEventArgs e)
        {
            UCListeClubs uCListeClubs = new UCListeClubs(this.Id);
            ListeClubs listeClubs = new ListeClubs();
            this.DataContext = listeClubs;
            combattantsDataGrid.Children.Clear();
            combattantsDataGrid.Children.Add(uCListeClubs);
        }

        private void Results_Click(object sender, RoutedEventArgs e)
        {
            UCResults uCResults = new UCResults(this.Id);
            Results results = new Results();
            this.DataContext = results;
            combattantsDataGrid.Children.Clear();
            combattantsDataGrid.Children.Add(uCResults);
        }

        private void ListeCategorie_Click_1(object sender, RoutedEventArgs e)
        {
            UCListeCategorie UcListeCategorie = new UCListeCategorie(this.Id); //User controle de Liste des Catégories


            //int cpt = UcListeCombattant.compteurData(0, this.Id);
            ListeCat ListeCategorie = new ListeCat(); //get list of patient
            this.DataContext = ListeCategorie;
            combattantsDataGrid.Children.Clear();
            combattantsDataGrid.Children.Add(UcListeCategorie);
        }
    }
}







