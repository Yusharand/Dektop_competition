using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp1.Models;

namespace WpfApp1.Views.UserControls
{
    /// <summary>
    /// Logique d'interaction pour UCListeCombat.xaml
    /// </summary>
    public partial class UCListeCombat : UserControl
    {
        public ObservableCollection<CombatInfo> ListeCombats;
        public Competition_JJBEntities context;
        private int Id;
        public UCListeCombat(int id)
        {
            InitializeComponent();
            this.Id = id;
            Charger();        
        }


        public void Charger()
        {
            using (var context = new Competition_JJBEntities())
            {
                // Charger les données des combats à partir de la base de données
                var combats = context.Combats.ToList();

                // Créer une liste pour stocker les informations sur les combats
                var listeCombatInfo = new List<CombatInfo>();

                // Pour chaque combat, récupérez le prénom du combattant correspondant
                foreach (var combat in combats)
                {
                    // Récupérez le prénom du combattant 1
                    var categorie = context.Categories.FirstOrDefault(c => c.ID_Categorie == combat.ID_Categorie);
                    string nom_categorie = categorie != null ? categorie.Nom_Categorie : "";
                    var combattant1 = context.Combattants.FirstOrDefault(c => c.ID_Combattant == combat.ID_Combattant1);
                    string nom1 = combattant1 != null ? combattant1.Nom_Combattant : "";
                    string prenom1 = combattant1 != null ? combattant1.Prenom_Combattant : "";
                    string club1 = combattant1 != null ? combattant1.Club?.Nom_Club : "";
                    string point1 = combat.Points_Combattant1.ToString();
                    // Récupérez le prénom du combattant 2
                    var combattant2 = context.Combattants.FirstOrDefault(c => c.ID_Combattant == combat.ID_Combattant2);
                    string nom2 = combattant2 != null ? combattant2.Nom_Combattant : "";
                    string prenom2 = combattant2 != null ? combattant2.Prenom_Combattant : "";
                    string club2 = combattant1 != null ? combattant2.Club?.Nom_Club : "";
                    string point2 = combat.Points_Combattant2.ToString();
                    // Ajoutez les informations sur le combat à la liste
                    listeCombatInfo.Add(new CombatInfo
                    {
                        ID_Combat = combat.ID_Combat,
                        Nom_Combattant1 = "-" + nom1,
                        Nom_Combattant2 = "-" + nom2,
                        Prenom_Combattant1 = prenom1,
                        Prenom_Combattant2 = prenom2,
                        Club_Combattant1 = "(" + club1 + ")",
                        Club_Combattant2 = "(" + club2 + ")",
                        Points_Combattant1 = "Points:" + point1,
                        Points_Combattant2 = "Points:" + point2,
                        Duree_combat = combat.Duree_combat,
                        Categorie_Combat = nom_categorie,
                        Tour_Match = combat.Tour_Match,
                        Victoire1 = combat.Victoire_Combattant1,
                        Victoire2 = combat.Victoire_Combattant2
                    });
                }

                // Assurez-vous que vous avez une propriété ObservableCollection<CombatInfo> dans votre ViewModel
                // Pour stocker les informations sur les combats
                ListeCombats = new ObservableCollection<CombatInfo>(listeCombatInfo);

                ListeCombatsDataGrid.ItemsSource = ListeCombats;
            }
        }

        private void DeleteMatch_Click(object sender, RoutedEventArgs e)
        {
            CombatInfo combatInfoSelectionnee = (CombatInfo)ListeCombatsDataGrid.SelectedItem;
            int id = combatInfoSelectionnee.ID_Combat;
            ConnexionBD connexion = new ConnexionBD();
            connexion.Delete("DELETE FROM Combats WHERE ID_Combat = " + id);
            connexion.Close();
            Charger();
        }

        private void PlayMatch_Click(object sender, RoutedEventArgs e)
        {
            CombatInfo combatInfoSelectionne = (CombatInfo)ListeCombatsDataGrid.SelectedItem;
            int id_combat = combatInfoSelectionne.ID_Combat;
            context = new Competition_JJBEntities();
            var combat = context.Combats.FirstOrDefault(c => c.ID_Combat == id_combat);
            var combattant1 = context.Combattants.FirstOrDefault(c => c.ID_Combattant == combat.ID_Combattant1);
            var combattant2 = context.Combattants.FirstOrDefault(c => c.ID_Combattant == combat.ID_Combattant2);
            var competition = context.Competitions.FirstOrDefault(c => c.ID_Competition == this.Id);
            string nom1 = combattant1.Nom_Combattant;
            string nom2 = combattant2.Nom_Combattant;
            string prenom1 = combattant1.Prenom_Combattant;
            string prenom2 = combattant2.Prenom_Combattant;
            string club1 = combattant1.Club?.Nom_Club;
            string club2 = combattant2.Club?.Nom_Club;
            string logoclub1 = combattant1.Club?.Logo_Club;
            string logoclub2 = combattant2.Club?.Logo_Club;
            string tour = combat.Tour_Match;
            string categorie = combat.Category?.Nom_Categorie;
            string fondscoreboard = competition.FondScoreboard_Competition;
            Formulaire formulaire = new Formulaire(this.Id, id_combat);
            
            /*Window dashboardCombattant = Window.GetWindow(this);
            if(dashboardCombattant != null)
            {
                dashboardCombattant.Close();
            }*/
            formulaire.Load_Data(nom1, nom2, prenom1, prenom2, club1, club2, logoclub1, logoclub2, tour, categorie, fondscoreboard);
            formulaire.Show();
            
        }
    }
}
