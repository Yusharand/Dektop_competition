using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Globalization;
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
    /// Logique d'interaction pour UCListeClubs.xaml
    /// </summary>
    public partial class UCListeClubs : UserControl
    {
        private int Id;
        public UCListeClubs(int id)
        {
            InitializeComponent();
            this.Id = id;
            Charger();
        }

        public void Charger()
        {
            ConnexionBD connection = new ConnexionBD();
            ObservableCollection<Club> ListeClubs = new ObservableCollection<Club>();
            SqlDataReader reader = connection.Select("SELECT * FROM Clubs WHERE ID_Competition = " + this.Id);
            while (reader.Read())
            {
                string nom = reader["Nom_Club"].ToString();
                string logo = reader["Logo_Club"].ToString();

                if (ListeClubs == null)
                {
                    Console.WriteLine("Pas de liste");
                }
                else
                {
                    ListeClubs.Add(new Club { Nom_Club = nom, Logo_Club = logo});
                }
            }
            connection.Close();
            ListeClubsDataGrid.ItemsSource = ListeClubs;
        }

        private void Insert_Logo_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Fichiers image (*.png;*.jpg;*.jpeg)|*.png;*.jpg;*.jpeg|Tous les fichiers|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                // Charger l'image dans le contrôle Image
                Uri fileUri = new Uri(openFileDialog.FileName);
                BitmapImage bitmap = new BitmapImage(fileUri);
                ConnexionBD connexionBD = new ConnexionBD();
                string requete = "INSERT INTO Clubs(Logo_Club) VALUES ('" + bitmap + "')";
                connexionBD.Insert(requete);
                connexionBD.Close();
                Charger();
            }
        }

        public class ImagePathToBitmapConverter : IValueConverter
        {
            public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {
                if (value is string imagePath && !string.IsNullOrEmpty(imagePath))
                {
                    BitmapImage image = new BitmapImage();
                    image.BeginInit();
                    image.CacheOption = BitmapCacheOption.OnLoad;
                    image.UriSource = new Uri(imagePath, UriKind.RelativeOrAbsolute);
                    image.EndInit();
                    return image;
                }
                return null;
            }

            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            {
                throw new NotImplementedException();
            }

           
        }
    }
}
