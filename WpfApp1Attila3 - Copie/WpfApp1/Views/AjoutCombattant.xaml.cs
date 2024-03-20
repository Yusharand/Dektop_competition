using System;
using System.Collections.Generic;
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
    /// Logique d'interaction pour AjoutCombattant.xaml
    /// </summary>
    public partial class AjoutCombattant : Window
    {

        public AjoutCombattant()
        {

            InitializeComponent();
            GetAge();
        }


        public void GetAge()
        {
            int d = DateTime.Now.Year;
            if (InpDDN.Text != "")
            {
                string[] d1 = InpDDN.Text.Split('/');
                int age = int.Parse(InpAge.Text);
                age = d - int.Parse(d1[2]);
            }

        }
        private void BtnAnnuler_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        public void Vider()
        {
            InpNum.Clear();
            InpClub.Clear();
            InpNom.Clear();
            InpPrenom.Clear();
            InpGenre.Clear();
            InpAge.Clear();
            InpGrade.Clear();
            InpPoids.Clear();

        }

        public bool valide()
        {
            if ((InpNum.Text == "") || (InpClub.Text == "") || (InpNom.Text == "") || (InpPrenom.Text == "") || (InpGenre.Text == "") || (InpDDN.Text == "")
                    || (InpAge.Text == "") || (InpGrade.Text == "") || (InpPoids.Text == ""))
                return false;
            else return true;
        }

        private void BtnAjouter_Click(object sender, RoutedEventArgs e)
        {
            string[] d1 = InpDDN.Text.Split('/');
            string dateNaiss = d1[0].ToString() + "-" + d1[1].ToString() + "-" + d1[1].ToString();


            bool correct = valide();
            if (correct)
            {
                ConnexionBD connection = new ConnexionBD();
                string requete = "INSERT INTO Combattants(ID_Combattant,Club_Combattant,Nom_Combattant,Prenom_Combattant,Genre_Combattant,Date_Naiss,Age,Grade,Poids) VALUES ('" + int.Parse(InpNum.Text) + "','" + InpClub.Text + "','" + InpNom.Text + "','" + InpPrenom.Text + "','" + InpGenre.Text + "','" + dateNaiss + "','" + int.Parse(InpAge.Text) + "','" + InpGrade.Text + "','" + int.Parse(InpPoids.Text) + "');";                      //string requete = "INSERT INTO Patientss(nomPatient,prenomPatient,agePatient,sexePatient,adresse,email,telephone,idDocteur,maladie,medicament,traitement,urgence,hospitalise,batiment,chambre,etage,dateRdv,dateHospitalisation) VALUES ('nomPatient', 'prenomPatient', 12, 1, 'adresse', 'email', 'telephone', 1, 'maladie', 'medicament', 'traitement', 1, 1, 1, 1, 1, '2020-10-10', '2020-10-10')";
                connection.Insert(requete);
                connection.Close();
                Vider();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Véerifiez vos données");
                Vider();
            }
        }
    }
}





