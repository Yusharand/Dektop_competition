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

namespace WpfApp1.Views
{
    /// <summary>
    /// Logique d'interaction pour Brackets.xaml
    /// </summary>
    public partial class Brackets : Window
    {
        public Category categorieSelectionnee;

        public Brackets(Category category)
        {
            InitializeComponent();
            this.categorieSelectionnee = category;
            // Exemple de données de combattants


            // Exemple de données de brackets



        }

        private void Generateto8_Click(object sender, RoutedEventArgs e)
        {
            

            // Vérifiez si une catégorie est sélectionnée
            if (categorieSelectionnee != null)
            {
                // Spécifiez le nombre de poules à créer (dans cet exemple, 2)
                int nombrePoules = 2;

                // Appelez la méthode pour créer les poules
                categorieSelectionnee.CreerPoules(nombrePoules);

                // Maintenant, la liste de poules dans la catégorie contient les combattants répartis
                // Vous pouvez accéder aux poules comme suit :
                foreach (Poule poule in categorieSelectionnee.Poules)
                {
                    Console.WriteLine($"Poule ID: {poule.ID_Poule}, Nombre de combattants: {poule.Combattants.Count}");
                }
            }

            MessageBox.Show("Brackets attribué avec succès!");
        }

        private void Generateto7_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Generateto6_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Generateto5_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Generateto4_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Generateto3_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Generateto2_Click(object sender, RoutedEventArgs e)
        {

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



