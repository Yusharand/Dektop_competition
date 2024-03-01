using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
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
    /// Logique d'interaction pour ListePoule.xaml
    /// </summary>
    public partial class ListePoule : Window
    {
        public ObservableCollection<CombattantViewModel_1> Classement1;
        public ObservableCollection<CombattantViewModel_1> Classement2;
        private int Id;


        public ListePoule(int id)
        {
            InitializeComponent();
            this.Id = id;
            Charger();
            Charger_Match_Poule();
            
            

            // Initialisation des données (vous devriez récupérer ces données de votre modèle réel)
            /*Classement1 = new ObservableCollection<CombattantViewModel>
            {
                new CombattantViewModel { Position = 1, Nom = "Rakotoarison", Prenom = "Diary", Club = "Southside", Points = 0 },
                new CombattantViewModel { Position = 2, Nom = "Razafimahandry", Prenom="Tantelintsoa", Club = "Attila", Points = 0 },
                new CombattantViewModel { Position = 3, Nom = "Andrianina", Prenom="Lova", Club = "Checkmat", Points = 0 },
                new CombattantViewModel { Position = 4, Nom = "Raharimbolamanana", Prenom="Rajo", Club = "Checkmat", Points = 0 },
            };

            classement1ListView.ItemsSource = Classement1;

            Classement2 = new ObservableCollection<CombattantViewModel>
            {
                new CombattantViewModel { Position = 1, Nom = "Randimbinirina", Prenom = "Yusha", Club = "Attila", Points = 0 },
                new CombattantViewModel { Position = 2, Nom = "Nantenaina", Prenom="Zawa", Club = "Checkmat", Points = 0 },
                new CombattantViewModel { Position = 3, Nom = "Rakotondralambo", Prenom="Fifaliana", Club = "One Tribe", Points = 0 },
                new CombattantViewModel { Position = 4, Nom = "Rakotosoa", Prenom="Mahefa", Club = "One Tribe", Points = 0 },
            };
            classement2ListView.ItemsSource = Classement2;*/
        }

        public void Charger_Match_Poule()
        {

        }

        public void Charger()
        {
            ConnexionBD connection1 = new ConnexionBD();
            ConnexionBD connection2 = new ConnexionBD();
            Classement1 = new ObservableCollection<CombattantViewModel_1>();
            Classement2 = new ObservableCollection<CombattantViewModel_1>();
            SqlDataReader reader1 = connection1.Select("SELECT Combattants.*, Poules.*, Clubs.Nom_Club " +
                                          "FROM Combattants " +
                                          "JOIN Poules ON Combattants.ID_Poule = Poules.ID_Poule " +
                                          "JOIN Clubs ON Combattants.ID_Club = Clubs.ID_Club " +
                                          "WHERE Poules.Nom_poule = 'Poule A' AND Combattants.ID_Categorie = " + this.Id + "ORDER BY Combattants.Pointspoules ");

            SqlDataReader reader2 = connection2.Select("SELECT Combattants.*, Poules.*, Clubs.nom_club " +
                                          "FROM Combattants " +
                                          "JOIN Poules ON Combattants.ID_Poule = Poules.ID_Poule " +
                                          "JOIN Clubs ON Combattants.ID_Club = Clubs.id_club " +
                                          "WHERE Poules.Nom_poule = 'Poule B' AND Combattants.ID_Categorie = " + this.Id + " ORDER BY Combattants.Pointspoules ");

            //Classement 1
            int i = 1;
            while (reader1.Read())
            {
                
                    
                    string club1 = reader1["Nom_Club"].ToString();
                    string nom1 = reader1["Nom_Combattant"].ToString();
                    string prenom1 = reader1["Prenom_Combattant"].ToString();
                    string points = reader1["Pointspoules"].ToString();

                    Classement1.Add(new CombattantViewModel_1 { Position = i, Nom = nom1, Prenom = prenom1, Club = club1, Points = int.Parse(points) });
                    

                if (Classement1 == null)
                {
                    Console.WriteLine("Pas de liste");
                }



                i += 1;

            }
            connection1.Close();

            classement1.ItemsSource = Classement1;

            //Classement 2
            int j = 1;
            while (reader2.Read())
            {

                string club2 = reader2["Nm_Club"].ToString();
                string nom2 = reader2["Nom_Combattant"].ToString();
                string prenom2 = reader2["Prenom_Combattant"].ToString();
                string points = reader2["Pointspoules"].ToString();
                Classement2.Add(new CombattantViewModel_1 { Position = j, Nom = nom2, Prenom = prenom2, Club = club2, Points = int.Parse(points) });

                if (Classement1 == null)
                {
                    Console.WriteLine("Pas de liste");
                }



                j += 1;

            }
            connection2.Close();

            classement2.ItemsSource = Classement2;
        }

        private void RemovePouleButton_Click(object sender, RoutedEventArgs e)
        {
            
            ConnexionBD connection = new ConnexionBD();
            ConnexionBD connection2 = new ConnexionBD();
            connection2.Update("UPDATE Combattants JOIN Poules ON Combattants.ID_Poule = Poules.ID_Poule WHERE Poules.Nom_poule = 'Poule A' AND Combattants.ID_Categorie= " + this.Id  + " SET ID_Poule = NULL");
            connection2.Close();
            connection.Delete("DELETE FROM Poules WHERE Nom_Poule = 'Poule A' AND ID_Categorie= " + this.Id);
            connection.Close();
            Charger();
        }

        private void RemovePouleButton_2_Click(object sender, RoutedEventArgs e)
        {
            ConnexionBD connection = new ConnexionBD();
            ConnexionBD connection2 = new ConnexionBD();
            connection2.Update("UPDATE Combattants JOIN Poules ON Combattants.ID_Poule = Poules.ID_Poule WHERE Poules.Nom_poule = 'Poule B' AND Combattants.ID_Categorie= " + this.Id + " SET ID_Poule = NULL");
            connection2.Close();
            connection.Delete("DELETE FROM Poules WHERE Nom_Poule = 'Poule B' AND ID_Categorie= " +this.Id);
            connection.Close();
            Charger();
        }
    }



    public class CombattantViewModel_1
    {
        public int Position { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Club { get; set; }
        public string Poids { get; set; }
        public int Points { get; set; }
    }

    public class CombattantViewModel_2
    {
        public string _nom { get; set; }
        public int _points { get; set; }
        public int _av { get; set; }
        public int _pen { get; set; }
        public int _sub { get; set; }

    }

}




