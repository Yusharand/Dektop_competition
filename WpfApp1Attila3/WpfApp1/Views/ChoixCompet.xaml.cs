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
    /// Logique d'interaction pour ChoixCompet.xaml
    /// </summary>
    public partial class ChoixCompet : Window
    {
        public ChoixCompet()
        {
            InitializeComponent();
        }

        private void NewCompet_Click(object sender, RoutedEventArgs e)
        {
            NouvelleCompetition nouvelleCompetition = new NouvelleCompetition();
            nouvelleCompetition.Show();
            this.Close();
        }

        private void ChargeCompet_Click(object sender, RoutedEventArgs e)
        {
            CompetitionExist competitionExist = new CompetitionExist();
            competitionExist.Show();
            this.Close();
        }
    }
}




