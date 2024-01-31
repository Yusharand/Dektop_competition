using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
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
    /// Logique d'interaction pour ModifierCombattant.xaml
    /// </summary>
    public partial class ModifierCombattant : Window
    {
        public UCListeCombattant UCListeC;
        public ObservableCollection<Combattant> Listecombattant;
        public int Id;


        public ModifierCombattant(int id, UCListeCombattant ucListeC)
        {

            this.Id = id;
            this.UCListeC = ucListeC;

            InitializeComponent();
            Charger();
        }

        public void Charger()
        {
            ConnexionBD connection = new ConnexionBD();
            DataSet dt = connection.SelectDataSet("SELECT * FROM Combattants WHERE ID_Combattant= " + this.Id);

            // Vérification oe nisy données tafaverina ve
            if (dt.Tables.Count > 0 && dt.Tables[0].Rows.Count > 0)
            {
                // Accéder aux données du tableau et les afficher (exemple ici)
                foreach (DataRow row in dt.Tables[0].Rows)
                {
                    InpNum1.Text = row["ID_Combattant"].ToString();
                    InpClub1.Text = (string)row["Club_Combattant"];
                    InpNom1.Text = (string)row["Nom_Combattant"];
                    InpPrenom1.Text = (string)row["Prenom_Combattant"];
                    InpGenre1.Text = (string)row["Genre_Combattant"];
                    InpDDN1.Text = row["Date_Naiss"].ToString();
                    InpAge1.Text = row["Age"].ToString(); ;
                    InpGrade1.Text = (string)row["Grade"];
                    InpPoids1.Text = row["Poids"].ToString();
                    if (row["ID_Categorie"].ToString() != "")
                    {
                        cbxCat.SelectedItem = row["Nom_Categorie"].ToString();
                    }

                    //InpDateSortie = (string)row["nomPatient"];
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
            InpNum1.Clear();
            InpClub1.Clear();
            InpNom1.Clear();
            InpPrenom1.Clear();
            InpGenre1.Clear();
            InpAge1.Clear();
            InpGrade1.Clear();
            InpPoids1.Clear();
        }

        public bool valide()
        {
            if ((InpNum1.Text == "") || (InpClub1.Text == "") || (InpNom1.Text == "") || (InpPrenom1.Text == "") || (InpGenre1.Text == "") || (InpDDN1.Text == "")
                    || (InpAge1.Text == "") || (InpGrade1.Text == "") || (InpPoids1.Text == ""))
                return false;
            else return true;
        }
        public bool valide_cbx()
        {
            if (cbxCat.SelectedValue == null)
                return false;
            else return true;
        }

        private void BtnModif_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string[] d1 = InpDDN1.Text.Split('/');
                string dateNaiss = d1[0].ToString() + "-" + d1[1].ToString() + "-" + d1[1].ToString();

                bool correct = valide();
                bool allow = valide_cbx();
                if (correct)
                {
                    ConnexionBD connection = new ConnexionBD();

                    string requete1 = "UPDATE Combattants SET ID_Combattant=  '" + int.Parse(InpNum1.Text) + "',Club_Combattant= '" + InpClub1.Text + "',Nom_Combattant= '" + InpNom1.Text + "',Prenom_Combattant= '" + InpPrenom1.Text + "',Genre_Combattant='" + InpGenre1.Text + "',Date_Naiss='" + dateNaiss + "',Age='" + int.Parse(InpAge1.Text) + "', Grade='" + InpGrade1.Text + "',Poids= '" + InpPoids1.Text + "' WHERE ID_Combattant = " + this.Id;
                    /*if (allow)
                    {
                        string id;
                        string requete2;
                        int intValue;
                        ConnexionBD connection1 = new ConnexionBD();
                        DataSet dt = connection1.SelectDataSet("SELECT * FROM Categorie ");
                        if (dt.Tables.Count > 0 && dt.Tables[0].Rows.Count > 0)
                        {
                            // Accéder aux données du tableau et les afficher (exemple ici)

                            foreach (DataRow row in dt.Tables[0].Rows)
                            {
                                object columnValue = row["ID_Categorie"];
                                if(columnValue is IConvertible)
                                {
                                    IConvertible convertibleValue = (IConvertible)columnValue;
                                    intValue = convertibleValue.ToInt32(null);
                                }
                                if ( Convert.ToInt32(cbxCat.SelectedValue))
                                {
                                    id = row["ID_Categorie"].ToString();
                                    requete2 = "INSERT INTO Combattant(ID_Categorie) VALUES('" + id + "') ;";
                                    connection1.Update(requete2);
                                    connection1.Close();
                                    break;
                                }


                            }

                        }
                    }*/
                    connection.Update(requete1);
                    connection.Close();
                    Vider();
                    this.Close();
                    this.UCListeC.Charger();
                }
                else
                {
                    Vider();
                    MessageBox.Show("Véerifiez vos données");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur:" + ex.Message);
            }


        }
    }
}





