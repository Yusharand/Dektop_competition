using System;
using System.Collections.Generic;
using System.Data.Entity;
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

namespace WpfApp1.Views
{
    /// <summary>
    /// Logique d'interaction pour Brackets.xaml
    /// </summary>
    public partial class Brackets : Window
    {
        public Category categorieSelectionnee;
        private int Id;
        private Competition_JJBEntities context = new Competition_JJBEntities();

        public Brackets(int id)
        {
            InitializeComponent();
            this.Id = id;
            //this.categorieSelectionnee = category;
            // Exemple de données de combattants


            // Exemple de données de brackets



        }

        private void Generateto8_Click(object sender, RoutedEventArgs e)
        {


            try
            {
            
                    // Créer Poule A
                    Poule Poule_A = new Poule
                    {
                        Nom_poule = "Poule A",
                        ID_Categorie = this.Id
                    };
                    context.Poules.Add(Poule_A);
                    
                    //Créer Poule B
                    Poule Poule_B = new Poule
                    {
                        Nom_poule = "Poule B",
                        ID_Categorie = this.Id
                    };
                   context.Poules.Add(Poule_B);

                context.SaveChanges();

                MessageBox.Show("Bracket attribué avec succès!");
                ListeCombattantCat listeCombattantCat = new ListeCombattantCat(this.Id);
                listeCombattantCat.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur" + ex.Message);
            }

        }

        private void Generateto7_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                // Créer Poule A
                Poule Poule_A = new Poule
                {
                    Nom_poule = "Poule A",
                    ID_Categorie = this.Id
                };
                context.Poules.Add(Poule_A);

                //Créer Poule B
                Poule Poule_B = new Poule
                {
                    Nom_poule = "Poule B",
                    ID_Categorie = this.Id
                };
                context.Poules.Add(Poule_B);

                context.SaveChanges();

                MessageBox.Show("Bracket attribué avec succès!");
                ListeCombattantCat listeCombattantCat = new ListeCombattantCat(this.Id);
                listeCombattantCat.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur" + ex.Message);
            }
        }

        private void Generateto6_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                // Créer Poule A
                Poule Poule_A = new Poule
                {
                    Nom_poule = "Poule A",
                    ID_Categorie = this.Id
                };
                context.Poules.Add(Poule_A);

                //Créer Poule B
                Poule Poule_B = new Poule
                {
                    Nom_poule = "Poule B",
                    ID_Categorie = this.Id
                };
                context.Poules.Add(Poule_B);

                context.SaveChanges();

                MessageBox.Show("Bracket attribué avec succès!");
                ListeCombattantCat listeCombattantCat = new ListeCombattantCat(this.Id);
                listeCombattantCat.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur" + ex.Message);
            }
        }

        public Poule ObtenirPoule(string nomPoule)
        {
            using (var context = new Competition_JJBEntities())
            {
                var poule = context.Poules.FirstOrDefault(p => p.Nom_poule == nomPoule);
                return poule;
            }
        }

        private void Generateto5_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                // Créer Poule A
                Poule Poule_A = new Poule
                {
                    Nom_poule = "Poule A",
                    ID_Categorie = this.Id
                };
                context.Poules.Add(Poule_A);

                var combattantscategorie = context.Combattants.Where(c => c.ID_Categorie == this.Id);
                Poule pouleselectionne = ObtenirPoule("Poule A");
                foreach (var combattant in combattantscategorie)
                {
                    combattant.ID_Poule = pouleselectionne.ID_Poule;
                    combattant.ID_Categorie = this.Id;
                    context.Combattants.Attach(combattant);
                    context.Entry(combattant).State = EntityState.Modified;
                }
                context.SaveChanges();

                MessageBox.Show("Bracket attribué avec succès!");                
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur" + ex.Message);
            }
        }

        private void Generateto4_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                // Créer Poule A
                Poule Poule_A = new Poule
                {
                    Nom_poule = "Poule A",
                    ID_Categorie = this.Id
                };
                context.Poules.Add(Poule_A);

                var combattantscategorie = context.Combattants.Where(c => c.ID_Categorie == this.Id);
                Poule pouleselectionne = ObtenirPoule("Poule A");
                foreach (var combattant in combattantscategorie)
                {
                    combattant.ID_Poule = pouleselectionne.ID_Poule;
                    combattant.ID_Categorie = this.Id;
                    context.Combattants.Attach(combattant);
                    context.Entry(combattant).State = EntityState.Modified;
                }

                context.SaveChanges();

                MessageBox.Show("Bracket attribué avec succès!");                
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur" + ex.Message);
            }
        }

        private void Generateto3_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                // Créer Poule A
                Poule Poule_A = new Poule
                {
                    Nom_poule = "Poule A",
                    ID_Categorie = this.Id
                };
                context.Poules.Add(Poule_A);

                var combattantscategorie = context.Combattants.Where(c => c.ID_Categorie == this.Id);
                Poule pouleselectionne = ObtenirPoule("Poule A");
                foreach (var combattant in combattantscategorie)
                {
                    combattant.ID_Poule = pouleselectionne.ID_Poule;
                    combattant.ID_Categorie = this.Id;
                    context.Combattants.Attach(combattant);
                    context.Entry(combattant).State = EntityState.Modified;
                }

                context.SaveChanges();

                MessageBox.Show("Bracket attribué avec succès!");                
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur" + ex.Message);
            }
        }

        private void Generateto2_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                // Créer Poule A
                Poule Poule_A = new Poule
                {
                    Nom_poule = "Poule A",
                    ID_Categorie = this.Id
                };
                context.Poules.Add(Poule_A);

                var combattantscategorie = context.Combattants.Where(c => c.ID_Categorie == this.Id);
                Poule pouleselectionne = ObtenirPoule("Poule A");
                foreach (var combattant in combattantscategorie)
                {
                    combattant.ID_Poule = pouleselectionne.ID_Poule;
                    combattant.ID_Categorie = this.Id;
                    context.Combattants.Attach(combattant);
                    context.Entry(combattant).State = EntityState.Modified;
                }

                context.SaveChanges();

                MessageBox.Show("Bracket attribué avec succès!");                
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur" + ex.Message);
            }
        }
    }

    public class Fighter
    {
        public string Name { get; set; }
        public int Weight { get; set; }
    }

    public class Match
    {
        public int MatchNumber { get; set; }
        public string Winner { get; set; }
    }
}



