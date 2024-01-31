using Microsoft.Win32;
using System;
using System.Collections.Generic;
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
    /// Logique d'interaction pour NouvelleCompetition.xaml
    /// </summary>
    public partial class NouvelleCompetition : Window
    {
        public NouvelleCompetition()
        {
            InitializeComponent();
        }

        public void Vider()
        {
            InpNomCompet.Clear();
            InpLieuCompet.Clear();
            InpLieuPese.Clear();
            InpCondition.Clear();
            InpDroit.Clear();

        }

        public bool valide()
        {
            if ((InpNomCompet.Text == "") || (InpLieuCompet.Text == "") || (InpLieuPese.Text == "") || (InpCondition.Text == "") || (InpDroit.Text == ""))
                return false;
            else return true;
        }

        private void ChoiceImageFond_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Fichiers image (*.png;*.jpg;*.jpeg)|*.png;*.jpg;*.jpeg|Tous les fichiers|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                // Charger l'image dans le contrôle Image
                Uri fileUri = new Uri(openFileDialog.FileName);
                BitmapImage bitmap = new BitmapImage(fileUri);
                imageFondSB.Source = bitmap;
            }
        }

        private void ChoiceImageLogo_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Fichiers image (*.png;*.jpg;*.jpeg)|*.png;*.jpg;*.jpeg|Tous les fichiers|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                // Charger l'image dans le contrôle Image
                Uri fileUri = new Uri(openFileDialog.FileName);
                BitmapImage bitmap = new BitmapImage(fileUri);
                imagelogo.Source = bitmap;
            }
        }

        private void BtnRetour_Click(object sender, RoutedEventArgs e)
        {
            ChoixCompet choixCompet = new ChoixCompet();
            choixCompet.Show();
            this.Close();
        }

        private void BtnEnregistrer_Click(object sender, RoutedEventArgs e)
        {
            //try
            //{
            string[] d1 = DateCompet.Text.Split('/');
            string dateCompet = d1[0].ToString() + "-" + d1[1].ToString() + "-" + d1[1].ToString();

            string[] d2 = Datepese.Text.Split('/');
            string datepese = d2[0].ToString() + "-" + d2[1].ToString() + "-" + d2[1].ToString();

            bool correct = valide();
            if (correct)
            {
                ConnexionBD connection = new ConnexionBD();
                string requete = "INSERT INTO Competitions(Nom_Competition,Lieu_Competition,Date_Competition,FondScoreboard_Competition,Logo_Competition,LieuPese_Competition, Date_Pese, Condition_Pese,DroitCombattant) VALUES ('" + InpNomCompet.Text + "','" + InpLieuCompet.Text + "','" + dateCompet + "','" + imageFondSB.Source + "','" + imagelogo.Source + "','" + InpLieuPese.Text + "','" + datepese + "','" + InpCondition.Text + "','" + int.Parse(InpDroit.Text) + "');";                      //string requete = "INSERT INTO Patientss(nomPatient,prenomPatient,agePatient,sexePatient,adresse,email,telephone,idDocteur,maladie,medicament,traitement,urgence,hospitalise,batiment,chambre,etage,dateRdv,dateHospitalisation) VALUES ('nomPatient', 'prenomPatient', 12, 1, 'adresse', 'email', 'telephone', 1, 'maladie', 'medicament', 'traitement', 1, 1, 1, 1, 1, '2020-10-10', '2020-10-10')";
                connection.Insert(requete);
                connection.Close();
                MessageBox.Show("Compétition créée avec succès!");
                Vider();

            }
            //}

            /*catch(Exception ex)
            {
                MessageBox.Show("Erreur: " + ex);
                Vider();
            }*/
        }

        private void BtnContinuer_Click(object sender, RoutedEventArgs e)
        {
            CompetitionExist competitionExist = new CompetitionExist();
            competitionExist.Show();
            this.Close();
        }
    }

}




