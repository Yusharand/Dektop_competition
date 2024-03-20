using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
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
using WpfApp1.Views.UserControls;

namespace WpfApp1.Views
{
    /// <summary>
    /// Logique d'interaction pour CreerCategorie.xaml
    /// </summary>
    public partial class CreerCategorie : Window
    {
        public UCListeCategorie UCListeC;
        public ObservableCollection<Combattant> Listecombattant;
        public int Id;
        public CreerCategorie(int id, UCListeCategorie ucListeC)
        {
            InitializeComponent();
            this.Id = id;
            this.UCListeC = ucListeC;
            Charger();
        }

        public void Charger()
        {
            ConnexionBD connection = new ConnexionBD();
            DataSet dt = connection.SelectDataSet("SELECT * FROM Categories WHERE ID_Categorie= " + this.Id);

            // Vérification oe nisy données tafaverina ve
            if (dt.Tables.Count > 0 && dt.Tables[0].Rows.Count > 0)
            {
                // Accéder aux données du tableau et les afficher (exemple ici)
                foreach (DataRow row in dt.Tables[0].Rows)
                {
                    InpNom_Cat.Text = row["Nom_Categorie"].ToString();



                }
            }
            else
                Console.WriteLine("Aucun résultat trouvé.");



        }

        private void BtnAnnuler_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        public void Vider()
        {

            InpNom_Cat.Clear();
        }

        public bool valide()
        {
            if ((InpNom_Cat.Text == ""))
                return false;
            else return true;
        }
        private void BtnAjouter_Click(object sender, RoutedEventArgs e)
        {
            bool correct = valide();
            if (correct)
            {
                ConnexionBD connection = new ConnexionBD();
                string requete = "UPDATE Categories SET Nom_Categorie= '" + InpNom_Cat.Text + "' WHERE ID_Categorie = " + this.Id;                    //string requete = "INSERT INTO Patientss(nomPatient,prenomPatient,agePatient,sexePatient,adresse,email,telephone,idDocteur,maladie,medicament,traitement,urgence,hospitalise,batiment,chambre,etage,dateRdv,dateHospitalisation) VALUES ('nomPatient', 'prenomPatient', 12, 1, 'adresse', 'email', 'telephone', 1, 'maladie', 'medicament', 'traitement', 1, 1, 1, 1, 1, '2020-10-10', '2020-10-10')";
                connection.Insert(requete);
                connection.Close();
                Vider();
                this.Hide();
                this.UCListeC.Charger();
            }
            else
            {
                MessageBox.Show("Véerifiez vos données");
                Vider();
            }
        }
    }
}




