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
        public ObservableCollection<CombatInfo> ListeCombats_A;
        public ObservableCollection<CombatInfo> ListeCombats_B;
        private Competition_JJBEntities context;
        private int Id;


        public ListePoule(int id)
        {
            InitializeComponent();
            this.Id = id;
            Charger_Pointspoules();
            Charger();
            Charger_Match_Poule();
        }

        public void Charger_Pointspoules()
        {
            context = new Competition_JJBEntities();
            var combattants_poule = context.Combattants.Where(c => c.ID_Categorie == this.Id).ToList();
            foreach (var combattant in combattants_poule)
            {
                if (combattant.Pointspoules == null)
                {
                    ConnexionBD connexionBD = new ConnexionBD();
                    connexionBD.Insert("UPDATE Combattants SET Pointspoules = '" + 0 + "' WHERE ID_Combattant = '" + combattant.ID_Combattant + "'");
                    connexionBD.Close();
                }
            }
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
                                          "WHERE Poules.Nom_poule = 'Poule A' AND Combattants.ID_Categorie = " + this.Id + "ORDER BY Combattants.Pointspoules DESC, Combattants.Sub_Marque DESC, Combattants.Points_Marque DESC , Combattants.Avantage_Marque DESC, Combattants.Penalite_Marque, Combattants.Sub_Concede, Combattants.Points_Concede , Combattants.Avantage_Concede, Combattants.Penalite_Concede DESC");

            SqlDataReader reader2 = connection2.Select("SELECT Combattants.*, Poules.*, Clubs.nom_club " +
                                          "FROM Combattants " +
                                          "JOIN Poules ON Combattants.ID_Poule = Poules.ID_Poule " +
                                          "JOIN Clubs ON Combattants.ID_Club = Clubs.id_club " +
                                          "WHERE Poules.Nom_poule = 'Poule B' AND Combattants.ID_Categorie = " + this.Id + " ORDER BY Combattants.Pointspoules DESC, Combattants.Sub_Marque DESC, Combattants.Points_Marque DESC , Combattants.Avantage_Marque DESC, Combattants.Penalite_Marque, Combattants.Sub_Concede, Combattants.Points_Concede , Combattants.Avantage_Concede, Combattants.Penalite_Concede DESC");

            //Classement 1
            int i = 1;
            while (reader1.Read())
            {
                
                    
                    string club1 = reader1["Nom_Club"].ToString();
                    string nom1 = reader1["Nom_Combattant"].ToString();
                    string prenom1 = reader1["Prenom_Combattant"].ToString();
                    string points = reader1["Pointspoules"].ToString();

                    Classement1.Add(new CombattantViewModel_1 { Position = i, Nom = nom1, Prenom = prenom1, Club = club1, Points = points });
                    

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

                string club2 = reader2["Nom_Club"].ToString();
                string nom2 = reader2["Nom_Combattant"].ToString();
                string prenom2 = reader2["Prenom_Combattant"].ToString();
                string points = reader2["Pointspoules"].ToString();
                Classement2.Add(new CombattantViewModel_1 { Position = j, Nom = nom2, Prenom = prenom2, Club = club2, Points = points });

                if (Classement1 == null)
                {
                    Console.WriteLine("Pas de liste");
                }



                j += 1;

            }
            connection2.Close();

            classement2.ItemsSource = Classement2;
        }

        private void Charger_Match_Poule()
        {
            using (var context = new Competition_JJBEntities())
            {
                // Charger les données des combats à partir de la base de données
                var poule_A = context.Poules.FirstOrDefault(p => p.ID_Categorie == this.Id && p.Nom_poule == "Poule A");
                var poule_B = context.Poules.FirstOrDefault(p => p.ID_Categorie == this.Id && p.Nom_poule == "Poule B");
                var combatspoules_A = context.Combats.Where(c => c.ID_Poule == poule_A.ID_Poule).ToList();
                
                // Créer une liste pour stocker les informations sur les combats
                var listeCombatInfo_A = new List<CombatInfo>();
                
                // Pour chaque combat, récupérons le prénom du combattant correspondant
                foreach (var combat in combatspoules_A)
                {
                    // Récupérez le prénom du combattant 1
                    var categorie = context.Categories.FirstOrDefault(c => c.ID_Categorie == combat.ID_Categorie);
                    string nom_categorie = categorie != null ? categorie.Nom_Categorie : "";
                    var combattant1 = context.Combattants.FirstOrDefault(c => c.ID_Combattant == combat.ID_Combattant1);
                    string nom1 = combattant1 != null ? combattant1.Nom_Combattant : "";
                    string prenom1 = combattant1 != null ? combattant1.Prenom_Combattant : "";
                    string club1 = combattant1 != null ? combattant1.Club?.Nom_Club : "";
                    string point1 = combat.Points_Combattant1.ToString();
                    string avantage1 = combat.Avantages_Combattant1.ToString();
                    string penalite1 = combat.Penalites_Combattant1.ToString();
                    string sub1 = combat.Sub_Combattant1.ToString();
                    // Récupérez le prénom du combattant 2
                    var combattant2 = context.Combattants.FirstOrDefault(c => c.ID_Combattant == combat.ID_Combattant2);
                    string nom2 = combattant2 != null ? combattant2.Nom_Combattant : "";
                    string prenom2 = combattant2 != null ? combattant2.Prenom_Combattant : "";
                    string club2 = combattant1 != null ? combattant2.Club?.Nom_Club : "";
                    string point2 = combat.Points_Combattant2.ToString();
                    string avantage2 = combat.Avantages_Combattant2.ToString();
                    string penalite2 = combat.Penalites_Combattant2.ToString();
                    string sub2 = combat.Sub_Combattant2.ToString();
                    // Ajoutez les informations sur le combat à la liste
                    listeCombatInfo_A.Add(new CombatInfo
                    {
                        ID_Combat = combat.ID_Combat,
                        Nom_Combattant1 = "-" + nom1,
                        Nom_Combattant2 = "-" + nom2,
                        Prenom_Combattant1 = prenom1,
                        Prenom_Combattant2 = prenom2,
                        Club_Combattant1 = "(" + club1 + ")",
                        Club_Combattant2 = "(" + club2 + ")",
                        Points_Combattant1 = "PTS:" + point1,
                        Points_Combattant2 = "PTS:" + point2,
                        Avantages_Combattant1 = "AV:" + avantage1,
                        Avantages_Combattant2 = "AV:" + avantage2,
                        Penalites_Combattant1 = "PEN:" + penalite1,
                        Penalites_Combattant2 = "PEN:" + penalite2,
                        Sub_Combattant1 = "SUB:" + sub1,
                        Sub_Combattant2 = "SUB:" + sub2,
                        Duree_combat = combat.Duree_combat,
                        Tour_Match = combat.Tour_Match,
                        Victoire1 = combat.Victoire_Combattant1,
                        Victoire2 = combat.Victoire_Combattant2
                    });
                }

                ListeCombats_A = new ObservableCollection<CombatInfo>(listeCombatInfo_A);

                ListeCombatsPouleADataGrid.ItemsSource = ListeCombats_A;

                if (poule_B != null)
                {
                    var listeCombatInfo_B = new List<CombatInfo>();
                    var combatspoules_B = context.Combats.Where(c => c.ID_Poule == poule_B.ID_Poule).ToList();
                    foreach (var combat in combatspoules_B)
                    {
                        // Récupérez le prénom du combattant 1
                        var categorie = context.Categories.FirstOrDefault(c => c.ID_Categorie == combat.ID_Categorie);
                        string nom_categorie = categorie != null ? categorie.Nom_Categorie : "";
                        var combattant1 = context.Combattants.FirstOrDefault(c => c.ID_Combattant == combat.ID_Combattant1);
                        string nom1 = combattant1 != null ? combattant1.Nom_Combattant : "";
                        string prenom1 = combattant1 != null ? combattant1.Prenom_Combattant : "";
                        string club1 = combattant1 != null ? combattant1.Club?.Nom_Club : "";
                        string point1 = combat.Points_Combattant1.ToString();
                        string avantage1 = combat.Avantages_Combattant1.ToString();
                        string penalite1 = combat.Penalites_Combattant1.ToString();
                        string sub1 = combat.Sub_Combattant1.ToString();
                        // Récupérez le prénom du combattant 2
                        var combattant2 = context.Combattants.FirstOrDefault(c => c.ID_Combattant == combat.ID_Combattant2);
                        string nom2 = combattant2 != null ? combattant2.Nom_Combattant : "";
                        string prenom2 = combattant2 != null ? combattant2.Prenom_Combattant : "";
                        string club2 = combattant1 != null ? combattant2.Club?.Nom_Club : "";
                        string point2 = combat.Points_Combattant2.ToString();
                        string avantage2 = combat.Avantages_Combattant2.ToString();
                        string penalite2 = combat.Penalites_Combattant2.ToString();
                        string sub2 = combat.Sub_Combattant2.ToString();
                        // Ajoutez les informations sur le combat à la liste
                        listeCombatInfo_B.Add(new CombatInfo
                        {
                            ID_Combat = combat.ID_Combat,
                            Nom_Combattant1 = "-" + nom1,
                            Nom_Combattant2 = "-" + nom2,
                            Prenom_Combattant1 = prenom1,
                            Prenom_Combattant2 = prenom2,
                            Club_Combattant1 = "(" + club1 + ")",
                            Club_Combattant2 = "(" + club2 + ")",
                            Points_Combattant1 = "PTS:" + point1,
                            Points_Combattant2 = "PTS:" + point2,
                            Avantages_Combattant1 = "AV:" + avantage1,
                            Avantages_Combattant2 = "AV:" + avantage2,
                            Penalites_Combattant1 = "PEN:" + penalite1,
                            Penalites_Combattant2 = "PEN:" + penalite2,
                            Sub_Combattant1 = "SUB:" + sub1,
                            Sub_Combattant2 = "SUB:" + sub2,
                            Duree_combat = combat.Duree_combat,
                            Tour_Match = combat.Tour_Match,
                            Victoire1 = combat.Victoire_Combattant1,
                            Victoire2 = combat.Victoire_Combattant2
                        });
                    }

                    // Assurez-vous que vous avez une propriété ObservableCollection<CombatInfo> dans votre ViewModel
                    // Pour stocker les informations sur les combats
                    ListeCombats_B = new ObservableCollection<CombatInfo>(listeCombatInfo_B);

                    ListeCombatsPouleBDataGrid.ItemsSource = ListeCombats_B;
                }
                
            }
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

        private void SemifinalButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }



    public class CombattantViewModel_1
    {
        public int Position { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Club { get; set; }
        public string Poids { get; set; }
        public string Points { get; set; }
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




