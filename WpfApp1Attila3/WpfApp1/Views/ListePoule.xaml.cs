using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Logique d'interaction pour ListePoule.xaml
    /// </summary>
    public partial class ListePoule : Window
    {
        public ObservableCollection<CombattantViewModel> Classement1 { get; set; }
        public ObservableCollection<CombattantViewModel> Classement2 { get; set; }
       


        public ListePoule(List<Poule> poules)
        {
            InitializeComponent();

            if (poules != null && poules.Count >= 2)
            {
                classement1.ItemsSource = poules[0].Combattants;
                classement2.ItemsSource = poules[1].Combattants;
            }

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

        public ListePoule(ICollection<Poule> poules)
        {
        }
    }

    public class CombattantViewModel
    {
        public int Position { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Club { get; set; }
        public string Poids { get; set; }
        public int Points { get; set; }
    }
}




