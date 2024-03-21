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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp1.Models;

namespace WpfApp1.Views.UserControls
{
    /// <summary>
    /// Logique d'interaction pour UCResults.xaml
    /// </summary>
    public partial class UCResults : UserControl
    {
        public int Id;
        public ObservableCollection<Resultchamp> ListeResultats;
        public Competition_JJBEntities context;
        public UCResults(int id)
        {
            InitializeComponent();
            this.Id = id;
            Load_Champion();
        }

        public void Load_Champion()
        {
            context = new Competition_JJBEntities();
            var categories = context.Categories.Where(cat => cat.ID_Competition == this.Id).ToList();
            ListeResultats = new ObservableCollection<Resultchamp>();
            foreach(var categorie in categories)
            {
                var combattant1 = context.Combattants.FirstOrDefault(c=>c.ID_Categorie == categorie.ID_Categorie && c.Victoire_finale == "Champion");
                var combattant2 = context.Combattants.FirstOrDefault(c => c.ID_Categorie == categorie.ID_Categorie && c.Victoire_finale == "Vice-champion");
                if(combattant1 != null && combattant2 != null)
                {
                    ListeResultats.Add(new Resultchamp
                    {
                        NomCombattant1 = combattant1.Nom_Combattant,
                        NomCombattant2 = combattant2.Nom_Combattant,
                        PrenomCombattant1 = combattant1.Prenom_Combattant,
                        PrenomCombattant2 = combattant2.Prenom_Combattant,
                        ClubCombattant1 = combattant1?.Club.Nom_Club,
                        ClubCombattant2 = combattant2?.Club.Nom_Club,
                        CategorieChamp = categorie.Nom_Categorie
                    });
                }
            }
            ListeChampDataGrid.ItemsSource = ListeResultats;
        }
    }
}
