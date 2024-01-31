using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
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

namespace WpfApp1.Views
{
    /// <summary>
    /// Logique d'interaction pour Formulaire.xaml
    /// </summary>
    public partial class Formulaire : Window
    {
        public static Formulaire instance = null;

        public int Id;
        public string imagePath1;
        public string textPath1;
        public string imagePath2;
        public string textPath2;
        public string imagePath3;
        public string textPath3;
        public string imagePath4;
        public string textPath4;

        public string Couleur2;

        public string selectedImagePath;
        private string selectedImagePath1;

        public string imagePath5; //variable scoreboardpublic
        public string textPath5;
        public string imagePath6;
        public string textPath6;
        public string imagePath7;
        public string textPath7;
        public string imagePath8;
        public string textPath8;

        public string Couleur3;

        public string selectedImagePath2;

        public Competition_JJBEntities db;

        private Category categorieSelectionnee;

        public Formulaire()
        {
            InitializeComponent();
            if (instance == null)
            {
                instance = this;
            }


            using (var contexte = new Competition_JJBEntities()) // Remplacez VotreContexte par le nom de votre contexte EF
            {
                // Récupérez les données de la table de la base de données
                var categories = contexte.Categories.ToList();
                comboboxcategorie.ItemsSource = categories;
                var nomcombattant = contexte.Combattants.Select(c => c.Nom_Combattant).ToList();
                textBoxNom1.ItemsSource = nomcombattant;
                textBoxNom2.ItemsSource = nomcombattant;
                var prenomcombattant = contexte.Combattants.Select(c => c.Prenom_Combattant).ToList();
                textBoxPrenom1.ItemsSource = prenomcombattant;
                textBoxPrenom2.ItemsSource = prenomcombattant;

                // Parcourez les catégories et ajoutez-les en tant qu'éléments ComboBoxItem
                /*foreach (var categorie in categories)
                {
                    var comboBoxItem = new ComboBoxItem
                    {
                        Content = categorie.Nom_Categorie,
                        Tag = categorie // Vous pouvez utiliser Tag pour stocker la catégorie associée
                    };

                    comboboxcategorie.Items.Add(comboBoxItem);
                }*/
                /*foreach (var nom1 in nomcombattant1)
                {
                    var comboBoxItem = new ComboBoxItem
                    {
                        Content = nom1.Nom_Combattant,
                        Tag = nom1 // Vous pouvez utiliser Tag pour stocker la catégorie associée
                    };

                    textBoxNom1.Items.Add(comboBoxItem);
                    
                }*/
                /*foreach (var prenom1 in prenomcombattant1)
                {
                    var comboBoxItem = new ComboBoxItem
                    {
                        Content = prenom1.Prenom_Combattant,
                        Tag = prenom1 // Vous pouvez utiliser Tag pour stocker la catégorie associée
                    };

                    textBoxPrenom1.Items.Add(comboBoxItem);
                    
                }
                /*foreach (var nom2 in nomcombattant2)
                {
                    var comboBoxItem = new ComboBoxItem
                    {
                        Content = nom2.Nom_Combattant,
                        Tag = nom2 // Vous pouvez utiliser Tag pour stocker la catégorie associée
                    };

                    textBoxNom2.Items.Add(comboBoxItem);

                }
                foreach (var prenom2 in prenomcombattant2)
                {
                    var comboBoxItem = new ComboBoxItem
                    {
                        Content = prenom2.Prenom_Combattant,
                        Tag = prenom2 // Vous pouvez utiliser Tag pour stocker la catégorie associée
                    };

                    textBoxPrenom2.Items.Add(comboBoxItem);

                }*/
            }


        }

        private void Windows_Keydown_2(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Button_Click_Scoreboard(sender, e);
            }
            if (e.Key == Key.Escape)
            {
                RetourDashboard_Click(sender, e);
            }
            e.Handled = true;
        }



        private void Button_Click_Scoreboard(object sender, RoutedEventArgs e)
        {
            try
            {
                /*if (((ComboBoxItem)textBoxPrenom1.SelectedItem).Content == null || ((ComboBoxItem)textBoxPrenom2.SelectedItem).Content == null)
                {
                    MessageBox.Show("Veuillez remplir les prénoms!");
                }
                .
                else
                {*/
                ComboBoxItem club1 = textBoxClub1.SelectedItem as ComboBoxItem;

                ComboBoxItem club2 = textBoxClub2.SelectedItem as ComboBoxItem;

                ComboBoxItem pays1 = textBoxPays1.SelectedItem as ComboBoxItem;

                ComboBoxItem pays2 = textBoxPays2.SelectedItem as ComboBoxItem;
                if (club1 != null && club2 != null && pays1 != null && pays2 != null)
                {
                    StackPanel stackPanel1 = club1.Content as StackPanel;
                    StackPanel stackPanel2 = club2.Content as StackPanel;
                    StackPanel stackPanel3 = pays1.Content as StackPanel;
                    StackPanel stackPanel4 = pays2.Content as StackPanel;
                    if ((stackPanel1 != null && stackPanel2 != null && stackPanel3 != null && stackPanel4 != null) && (stackPanel1.Children.Count > 0 && stackPanel2.Children.Count > 0 && stackPanel3.Children.Count > 0 && stackPanel4.Children.Count > 0))
                    {
                        TextBlock textBlock1 = stackPanel1.Children[0] as TextBlock;
                        TextBlock textBlock2 = stackPanel2.Children[0] as TextBlock;
                        TextBlock textBlock3 = stackPanel3.Children[0] as TextBlock;
                        TextBlock textBlock4 = stackPanel4.Children[0] as TextBlock;
                        if (textBlock1 != null && textBlock2 != null && textBlock3 != null && textBlock4 != null)
                        {
                            selectedTextBlock1.Text = textBlock1.Text;
                            selectedTextBlock2.Text = textBlock2.Text;
                            selectedTextBlock3.Text = textBlock3.Text;
                            selectedTextBlock4.Text = textBlock4.Text;

                        }

                        Image image1 = stackPanel1.Children[1] as Image;
                        Image image2 = stackPanel2.Children[1] as Image;
                        Image image3 = stackPanel3.Children[1] as Image;
                        Image image4 = stackPanel4.Children[1] as Image;

                        if (image1 != null && image2 != null && image3 != null && image4 != null)
                        {
                            selectedImage1.Source = image1.Source;
                            selectedImage2.Source = image2.Source;
                            selectedImage3.Source = image3.Source;
                            selectedImage4.Source = image4.Source;
                        }
                    }


                    //image de fond
                    ComboBoxItem selectedImgfnd = cbx2.SelectedItem as ComboBoxItem;

                    if (selectedImgfnd != null)
                    {
                        StackPanel stackPanel5 = selectedImgfnd.Content as StackPanel;
                        if (stackPanel5 != null && stackPanel5.Children.Count > 0)
                        {
                            Image imagefnd = stackPanel5.Children[1] as Image;
                            if (imagefnd != null)
                            {
                                selectedImage5.Source = imagefnd.Source;

                            }
                        }

                    }

                    //drapeau pays






                    string imagePath1 = selectedImage1.Source.ToString();
                    string textPath1 = selectedTextBlock1.Text;

                    string imagePath2 = selectedImage2.Source.ToString();
                    string textPath2 = selectedTextBlock2.Text;

                    string selectedImagePath = selectedImage5.Source.ToString();

                    string imagePath3 = selectedImage3.Source.ToString();
                    string textPath3 = selectedTextBlock3.Text;

                    string imagePath4 = selectedImage4.Source.ToString();
                    string textPath4 = selectedTextBlock4.Text;



                    string couleur1 = colorpicker1.SelectedColorText;
                    string couleur2 = colorpicker2.SelectedColorText;

                    string minuteC = ((ComboBoxItem)comboboxminutes.SelectedItem).Content.ToString();
                    string phaseC = ((ComboBoxItem)comboboxphase.SelectedItem).Content.ToString();
                    string categorieC = categorieSelectionnee.Nom_Categorie;
                    string nom1 = textBoxNom1.SelectedItem.ToString();
                    string prenom1 = textBoxPrenom1.SelectedItem.ToString();
                    string nom2 = textBoxNom2.SelectedItem.ToString();
                    string prenom2 = textBoxPrenom2.SelectedItem.ToString();
                    MainWindow mainwindow = new MainWindow();
                    Application.Current.MainWindow = mainwindow;
                    mainwindow.SetImage(textPath1, imagePath1, textPath2, imagePath2, selectedImagePath, textPath3, imagePath3, textPath4, imagePath4, nom1, prenom1, nom2, prenom2, couleur1, couleur2, minuteC, phaseC, categorieC);
                    mainwindow.Show();

                    ScoreboardPublic scoreboardPublic = new ScoreboardPublic();
                    Application.Current.MainWindow = scoreboardPublic;
                    scoreboardPublic.SetImage(textPath1, imagePath1, textPath2, imagePath2, selectedImagePath, textPath3, imagePath3, textPath4, imagePath4, nom1, prenom1, nom2, prenom2, couleur1, couleur2, minuteC, phaseC, categorieC);
                    scoreboardPublic.Show();

                    this.Close();

                }

            }

            catch (Exception ex)
            {
                MessageBox.Show("Erreur:" + ex.Message);
            }



        }

        private void Comboboxcategorie_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                categorieSelectionnee = comboboxcategorie.SelectedItem as Category;

                //if (categorieSelectionnee != null)
                //{
                using (var contexte = new Competition_JJBEntities()) // Remplacez VotreContexte par le nom de votre contexte EF
                {
                    // Récupérez les combattants associés à la catégorie sélectionnée
                    var prenomcombattants = contexte.Combattants.Where(c => c.ID_Categorie == categorieSelectionnee.ID_Categorie).Select(c => c.Prenom_Combattant).ToList();
                    var nomcombattants = contexte.Combattants.Where(c => c.ID_Categorie == categorieSelectionnee.ID_Categorie).Select(c => c.Nom_Combattant).ToList();





                    textBoxPrenom1.ItemsSource = prenomcombattants;
                    textBoxPrenom2.ItemsSource = prenomcombattants;
                    textBoxNom1.ItemsSource = nomcombattants;
                    textBoxNom2.ItemsSource = nomcombattants;


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }



        private void TextBoxPrenom1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            var prenomSelectionnee = textBoxPrenom1.SelectedItem as string;

            if (!string.IsNullOrEmpty(prenomSelectionnee))
            {
                using (var contexte = new Competition_JJBEntities()) // Remplacez VotreContexte par le nom de votre contexte EF
                {
                    // Récupérez les noms des combattants correspondant au prénom sélectionné
                    var nomsDesCombattants = contexte.Combattants.Where(c => c.Prenom_Combattant == prenomSelectionnee).Select(c => c.Nom_Combattant).FirstOrDefault();

                    textBoxNom1.SelectedItem = nomsDesCombattants;
                }
            }
        }

        private void TextBoxPrenom2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var prenomSelectionnee = textBoxPrenom2.SelectedItem as string;

            if (!string.IsNullOrEmpty(prenomSelectionnee))
            {
                using (var contexte = new Competition_JJBEntities()) // Remplacez VotreContexte par le nom de votre contexte EF
                {
                    // Récupérez les noms des combattants correspondant au prénom sélectionné
                    var nomsDesCombattants = contexte.Combattants.Where(c => c.Prenom_Combattant == prenomSelectionnee).Select(c => c.Nom_Combattant).FirstOrDefault();

                    textBoxNom2.SelectedItem = nomsDesCombattants;
                }
            }
        }

        private void RetourDashboard_Click(object sender, RoutedEventArgs e)
        {
            DashboardCombattant dashboardCombattant = new DashboardCombattant(Id);
            dashboardCombattant.Show();
            this.Close();
        }
    }
}





