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
        private int Id_compet;
        private int Id_cat;
        private Competition_JJBEntities context = new Competition_JJBEntities();

        public Brackets(int id_compet, int id_cat)
        {
            InitializeComponent();
            this.Id_compet = id_compet;
            this.Id_cat = id_cat;
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
                        ID_Categorie = this.Id_cat,
                        ID_Competition = this.Id_compet,
                        
                    };
                    context.Poules.Add(Poule_A);
                    
                    //Créer Poule B
                    Poule Poule_B = new Poule
                    {
                        Nom_poule = "Poule B",
                        ID_Categorie = this.Id_cat,
                        ID_Competition = this.Id_compet,
                    };
                   context.Poules.Add(Poule_B);

                context.SaveChanges();

                MessageBox.Show("Bracket attribué avec succès!");
                ListeCombattantCat listeCombattantCat = new ListeCombattantCat(this.Id_compet, this.Id_cat);
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
                    ID_Categorie = this.Id_cat,
                    ID_Competition = this.Id_compet,
                };
                context.Poules.Add(Poule_A);

                //Créer Poule B
                Poule Poule_B = new Poule
                {
                    Nom_poule = "Poule B",
                    ID_Categorie = this.Id_cat,
                    ID_Competition = this.Id_compet,
                };
                context.Poules.Add(Poule_B);

                context.SaveChanges();

                MessageBox.Show("Bracket attribué avec succès!");
                ListeCombattantCat listeCombattantCat = new ListeCombattantCat(this.Id_compet, this.Id_cat);
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
                    ID_Categorie = this.Id_cat,
                    ID_Competition = this.Id_compet,
                };
                context.Poules.Add(Poule_A);

                //Créer Poule B
                Poule Poule_B = new Poule
                {
                    Nom_poule = "Poule B",
                    ID_Categorie = this.Id_cat,
                    ID_Competition = this.Id_compet,
                };
                context.Poules.Add(Poule_B);

                context.SaveChanges();

                MessageBox.Show("Bracket attribué avec succès!");
                ListeCombattantCat listeCombattantCat = new ListeCombattantCat(this.Id_compet, this.Id_cat);
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
                    ID_Categorie = this.Id_cat,
                    ID_Competition = this.Id_compet,
                };
                context.Poules.Add(Poule_A);
                context.SaveChanges();

                var combattantscategorie = context.Combattants.Where(c => c.ID_Categorie == this.Id_cat).ToList();
                Poule pouleselectionne = ObtenirPoule("Poule A");
                foreach (var combattant in combattantscategorie)
                {
                    combattant.ID_Poule = pouleselectionne.ID_Poule;
                    combattant.ID_Categorie = this.Id_cat;
                    combattant.ID_Competition = this.Id_compet;
                    context.Combattants.Attach(combattant);
                    context.Entry(combattant).State = EntityState.Modified;
                }
                List<Combat> combats = new List<Combat>();
                for (int i = 0; i < combattantscategorie.Count - 1; i++)
                {
                    for (int j = i + 1; j < combattantscategorie.Count; j++)
                    {
                        int k = j;
                        if (j == combattantscategorie.Count - 1)
                        {
                            k = 1;
                        }
                        Combat combat = new Combat
                        {
                            // Assigner les propriétés du combat
                            Nom_Combat = $"Combat {combattantscategorie[i].Prenom_Combattant } vs {combattantscategorie[j].Prenom_Combattant}",
                            ID_Categorie = this.Id_cat,
                            ID_Competition = this.Id_compet,
                            ID_Poule = pouleselectionne.ID_Poule,
                            Points_Combattant1 = 0,
                            Points_Combattant2 = 0,
                            Avantages_Combattant1 = 0,
                            Avantages_Combattant2 = 0,
                            Penalites_Combattant1 = 0,
                            Penalites_Combattant2 = 0,
                            ID_Combattant1 = combattantscategorie[i].ID_Combattant,
                            ID_Combattant2 = combattantscategorie[j].ID_Combattant,
                            Tour_Match = "Tour " + k,
                            // Vous pouvez également initialiser d'autres propriétés du combat selon vos besoins
                        };

                        // Ajouter le combat à la liste des combats
                        combats.Add(combat);
                        context.Combats.Attach(combat);
                        context.Entry(combat).State = EntityState.Added;
                    }
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
                    ID_Categorie = this.Id_cat,
                    ID_Competition = this.Id_compet,
                };
                context.Poules.Add(Poule_A);
                context.SaveChanges();

                var combattantscategorie = context.Combattants.Where(c => c.ID_Categorie == this.Id_cat).ToList();
                Poule pouleselectionne = ObtenirPoule("Poule A");
                foreach (var combattant in combattantscategorie)
                {
                    combattant.ID_Poule = pouleselectionne.ID_Poule;
                    combattant.ID_Categorie = this.Id_cat;
                    combattant.ID_Competition = this.Id_compet;
                    context.Combattants.Attach(combattant);
                    context.Entry(combattant).State = EntityState.Modified;
                }
                List<Combat> combats = new List<Combat>();
                for (int i = 0; i < combattantscategorie.Count - 1; i++)
                {
                    for (int j = i + 1; j < combattantscategorie.Count; j++)
                    {
                        int k = j;
                        if(i == combattantscategorie.Count - 2)
                        {
                            k = 1;
                        }
                        Combat combat = new Combat
                        {
                            // Assigner les propriétés du combat
                            Nom_Combat = $"Combat {combattantscategorie[i].Prenom_Combattant } vs {combattantscategorie[j].Prenom_Combattant}",
                            ID_Categorie = this.Id_cat,
                            ID_Competition = this.Id_compet,
                            ID_Poule = pouleselectionne.ID_Poule,
                            Points_Combattant1 = 0,
                            Points_Combattant2 = 0,
                            Avantages_Combattant1 = 0,
                            Avantages_Combattant2 = 0,
                            Penalites_Combattant1 = 0,
                            Penalites_Combattant2 = 0,
                            ID_Combattant1 = combattantscategorie[i].ID_Combattant,
                            ID_Combattant2 = combattantscategorie[j].ID_Combattant,
                            Tour_Match = "Tour " + k,
                            // Vous pouvez également initialiser d'autres propriétés du combat selon vos besoins
                        };

                        // Ajouter le combat à la liste des combats
                        combats.Add(combat);
                        context.Combats.Attach(combat);
                        context.Entry(combat).State = EntityState.Added;
                    }
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
                    ID_Categorie = this.Id_cat,
                    ID_Competition = this.Id_compet,
                };
                context.Poules.Add(Poule_A);
                context.SaveChanges();

                var combattantscategorie = context.Combattants.Where(c => c.ID_Categorie == this.Id_cat).ToList();
                Poule pouleselectionne = ObtenirPoule("Poule A");
                foreach (var combattant in combattantscategorie)
                {
                    combattant.ID_Poule = pouleselectionne.ID_Poule;
                    combattant.ID_Categorie = this.Id_cat;
                    combattant.ID_Competition = this.Id_compet;
                    context.Combattants.Attach(combattant);
                    context.Entry(combattant).State = EntityState.Modified;
                }
                List<Combat> combats = new List<Combat>();
                for (int i = 0; i < combattantscategorie.Count - 1; i++)
                {
                    for (int j = i + 1; j < combattantscategorie.Count; j++)
                    {
                        if (i == combattantscategorie.Count - 2)
                                {
                                    Combat combat = new Combat
                                    {
                                        // Assigner les propriétés du combat
                                        Nom_Combat = $"Combat {combattantscategorie[i].Prenom_Combattant } vs {combattantscategorie[j].Prenom_Combattant}",
                                        ID_Categorie = this.Id_cat,
                                        ID_Competition = this.Id_compet,
                                        ID_Poule = pouleselectionne.ID_Poule,
                                        Points_Combattant1 = 0,
                                        Points_Combattant2 = 0,
                                        Avantages_Combattant1 = 0,
                                        Avantages_Combattant2 = 0,
                                        Penalites_Combattant1 = 0,
                                        Penalites_Combattant2 = 0,
                                        ID_Combattant1 = combattantscategorie[i].ID_Combattant,
                                        ID_Combattant2 = combattantscategorie[j].ID_Combattant,
                                        Tour_Match = "Tour " + j + 1,
                                        // Vous pouvez également initialiser d'autres propriétés du combat selon vos besoins
                                    };
                                    combats.Add(combat);
                                    context.Combats.Attach(combat);
                                    context.Entry(combat).State = EntityState.Added;
                                }
                                else
                                {
                                    Combat combat = new Combat
                                    {
                                        // Assigner les propriétés du combat
                                        Nom_Combat = $"Combat {combattantscategorie[i].Prenom_Combattant } vs {combattantscategorie[j].Prenom_Combattant}",
                                        ID_Categorie = this.Id_cat,
                                        ID_Competition = this.Id_compet,
                                        ID_Poule = pouleselectionne.ID_Poule,
                                        Points_Combattant1 = 0,
                                        Points_Combattant2 = 0,
                                        Avantages_Combattant1 = 0,
                                        Avantages_Combattant2 = 0,
                                        Penalites_Combattant1 = 0,
                                        Penalites_Combattant2 = 0,
                                        ID_Combattant1 = combattantscategorie[i].ID_Combattant,
                                        ID_Combattant2 = combattantscategorie[j].ID_Combattant,
                                        Tour_Match = "Tour " + j,
                                        // Vous pouvez également initialiser d'autres propriétés du combat selon vos besoins
                                    };
                                    combats.Add(combat);
                                    context.Combats.Attach(combat);
                                    context.Entry(combat).State = EntityState.Added;
                                }
                    }
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
                    ID_Categorie = this.Id_cat,
                    ID_Competition = this.Id_compet,
                };
                context.Poules.Add(Poule_A);
                context.SaveChanges();

                var combattantscategorie = context.Combattants.Where(c => c.ID_Categorie == this.Id_cat).ToList();
                Poule pouleselectionne = ObtenirPoule("Poule A");
                foreach (var combattant in combattantscategorie)
                {
                    combattant.ID_Poule = pouleselectionne.ID_Poule;
                    combattant.ID_Categorie = this.Id_cat;
                    combattant.ID_Competition = this.Id_compet;
                    context.Combattants.Attach(combattant);
                    context.Entry(combattant).State = EntityState.Modified;
                }
                List<Combat> combats = new List<Combat>();
                string[] tour = { "Match Aller", "Match Retour" };

                    
                Combat combat1 = new Combat
                {
                    // Assigner les propriétés du combat
                    Nom_Combat = $"Combat {combattantscategorie[0].Prenom_Combattant } vs {combattantscategorie[1].Prenom_Combattant}",
                    ID_Categorie = this.Id_cat,
                    ID_Poule = pouleselectionne.ID_Poule,
                    Points_Combattant1 = 0,
                    Points_Combattant2 = 0,
                    Avantages_Combattant1 = 0,
                    Avantages_Combattant2 = 0,
                    Penalites_Combattant1 = 0,
                    Penalites_Combattant2 = 0,
                    ID_Combattant1 = combattantscategorie[0].ID_Combattant,
                    ID_Combattant2 = combattantscategorie[1].ID_Combattant,
                    Tour_Match = tour[0],
                    // Vous pouvez également initialiser d'autres propriétés du combat selon vos besoins
                };
                combats.Add(combat1);
                context.Combats.Attach(combat1);
                context.Entry(combat1).State = EntityState.Added;

                Combat combat2 = new Combat
                {
                    // Assigner les propriétés du combat
                    Nom_Combat = $"Combat {combattantscategorie[1].Prenom_Combattant } vs {combattantscategorie[0].Prenom_Combattant}",
                    ID_Categorie = this.Id_cat,
                    ID_Poule = pouleselectionne.ID_Poule,
                    Points_Combattant1 = 0,
                    Points_Combattant2 = 0,
                    Avantages_Combattant1 = 0,
                    Avantages_Combattant2 = 0,
                    Penalites_Combattant1 = 0,
                    Penalites_Combattant2 = 0,
                    ID_Combattant1 = combattantscategorie[1].ID_Combattant,
                    ID_Combattant2 = combattantscategorie[0].ID_Combattant,
                    Tour_Match = tour[1],
                    // Vous pouvez également initialiser d'autres propriétés du combat selon vos besoins
                };
                // Ajouter le combat à la liste des combats
                combats.Add(combat2);
                context.Combats.Attach(combat2);
                context.Entry(combat2).State = EntityState.Added;
                    
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



