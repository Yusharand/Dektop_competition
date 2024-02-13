using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
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
using System.Windows.Shapes;
using WpfApp1.Models;
using WpfApp1.Views.UserControls;

namespace WpfApp1.Views
{
    /// <summary>
    /// Logique d'interaction pour ListeCombattantCat.xaml
    /// </summary>
    public partial class ListeCombattantCat : Window
    {
        public UCListeCombattant UCListeC;
        public ObservableCollection<Combattant> ListeCombattantsCat;
        public int Id;
        private Competition_JJBEntities context = new Competition_JJBEntities();

        public ListeCombattantCat(int id)
        {
            InitializeComponent();
            this.Id = id;

            Charger();
        }

        public void Charger()
        {
            ConnexionBD connection = new ConnexionBD();
            ListeCombattantsCat = new ObservableCollection<Combattant>();
            SqlDataReader reader = connection.Select("SELECT * FROM Combattants WHERE ID_Categorie= " + this.Id);
            while (reader.Read())
            {
                if(reader["ID_Poule"].ToString() == "")
                {
                    string numero = reader["ID_Combattant"].ToString();
                    string club = reader["Club_Combattant"].ToString();
                    string nom = reader["Nom_Combattant"].ToString();
                    string prenom = reader["Prenom_Combattant"].ToString();
                    string genre = reader["Genre_Combattant"].ToString();
                    string date = reader["Date_Naiss"].ToString();
                    string age = reader["Age"].ToString();
                    string grade = reader["Grade"].ToString();
                    string poids = reader["Poids"].ToString();

                    ListeCombattantsCat.Add(new Combattant { ID_Combattant = int.Parse(numero), Club_Combattant = club, Nom_Combattant = nom, Prenom_Combattant = prenom, Genre_Combattant = genre, Date_Naiss = DateTime.Parse(date), Age = int.Parse(age), Grade = grade, Poids = double.Parse(poids) });

                
                }
                


                if (ListeCombattantsCat == null)
                {
                    Console.WriteLine("Pas de liste");
                }
                
                    



            }
            connection.Close();

            ListeCombattantsCatDataGrid.ItemsSource = ListeCombattantsCat;

        }

        private void DeleteCatCombattant_Click(object sender, RoutedEventArgs e)
        {
            var combattantsSelectionnes = ListeCombattantsCatDataGrid.SelectedItems.Cast<Combattant>().ToList();
            foreach (var combattant in combattantsSelectionnes)
            {
                //nouvelleCategorie.Combattants.Add(combattant);
                combattant.ID_Categorie = null;
                context.Combattants.Attach(combattant);
                context.Entry(combattant).State = EntityState.Modified;
                ListeCombattantsCat.Remove(combattant);

            }



            // Ajouter la catégorie à la base de données via Entity Framework


            context.SaveChanges();

        }

        

        private void AjoutCombattant_Click(object sender, RoutedEventArgs e)
        {
            AjoutCombattantCat ajoutCombattantCat = new AjoutCombattantCat(this.Id);
            ajoutCombattantCat.Show();

        }

        private void Move_Click(object sender, RoutedEventArgs e)
        {

        }

        public Poule ObtenirPoule(string nomPoule)
        {
            using(var context = new Competition_JJBEntities())
            {
                var poule = context.Poules.FirstOrDefault(p => p.Nom_poule == nomPoule);
                return poule;
            }
        }

        private void Creer_PouleA_Click(object sender, RoutedEventArgs e)
        {
            var combattantsSelectionnes = ListeCombattantsCatDataGrid.SelectedItems.Cast<Combattant>().ToList();
            Poule pouleselectionne = ObtenirPoule("Poule A");
            string[] index = { "a", "b", "c",  "d", "e"};
            int ind = 0;
            foreach(var combattant in combattantsSelectionnes)
            {
                combattant.ID_Poule = pouleselectionne.ID_Poule;
                combattant.ID_Categorie = this.Id;
                combattant.Index_Poule = index[ind];
                context.Combattants.Attach(combattant);
                context.Entry(combattant).State = EntityState.Modified;
                ListeCombattantsCat.Remove(combattant);
                ind++;
            }

            List<Combat> combats = new List<Combat>();
            for(int i = 0; i < combattantsSelectionnes.Count - 1; i++)
            {  
                for(int j = i+1; j < combattantsSelectionnes.Count; j++)
                {
                    Combat combat = new Combat
                    {
                        // Assigner les propriétés du combat
                        Nom_Combat = $"Combat {i + 1} vs {j + 1}",
                        ID_Categorie = this.Id,
                        ID_Poule = pouleselectionne.ID_Poule,
                        Points_Combattant1 = 0,
                        Points_Combattant2 = 0,
                        Avantages_Combattant1 = 0,
                        Avantages_Combattant2 = 0,
                        Penalites_Combattant1 = 0,
                        Penalites_Combattant2 = 0,
                        ID_Combattant1 = combattantsSelectionnes[i].ID_Combattant,
                        ID_Combattant2 = combattantsSelectionnes[j].ID_Combattant,
                        // Vous pouvez également initialiser d'autres propriétés du combat selon vos besoins
                    };

                    // Ajouter le combat à la liste des combats
                    combats.Add(combat);
                    context.Combats.Attach(combat);
                    context.Entry(combat).State = EntityState.Added;
                }
            }


            context.SaveChanges();

            MessageBox.Show("Poule créée avec succès");

        }

        private void Creer_PouleB_Click(object sender, RoutedEventArgs e)
        {
            var combattantsSelectionnes = ListeCombattantsCatDataGrid.SelectedItems.Cast<Combattant>().ToList();
            Poule pouleselectionne = ObtenirPoule("Poule B");
            foreach (var combattant in combattantsSelectionnes)
            {
                combattant.ID_Poule = pouleselectionne.ID_Poule;
                combattant.ID_Categorie = this.Id;
                context.Combattants.Attach(combattant);
                context.Entry(combattant).State = EntityState.Modified;
                ListeCombattantsCat.Remove(combattant);
            }
            context.SaveChanges();
            MessageBox.Show("Poule créée avec succès");
        }
    }
}




