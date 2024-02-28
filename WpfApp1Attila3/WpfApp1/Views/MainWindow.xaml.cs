using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;
using WpfApp1.Views;
using System.Media;
using System.ComponentModel;

namespace WpfApp1
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ScoreboardData scoreboardData;
        DispatcherTimer timer = new DispatcherTimer();
        DispatcherTimer timer1 = new DispatcherTimer();
        DispatcherTimer timer2 = new DispatcherTimer();



        private int minutes, secondes, minutesMed1, secondesMed1, minutesMed2, secondesMed2;

        string tmp1, tmp2, tmp3, tmp4, tmp5, tmp6, tmp7, tmp8;

        List<Action> actions = new List<Action>();

        public static MainWindow Instance = null;

        public static BleuFinCombat bleuFinCombat;
        public static RougeFinCombat rougeFinCombat;
        public UserControl UC { get; set; }

        private List<UserAction> userActions = new List<UserAction>();



        bool valide;
        bool play = true;
        bool play1 = true;
        bool play2 = true;
        bool active = false;
        private string imagefndPath;

        public event EventHandler StartMed2Clicked;
        public event EventHandler StartMed1Clicked;
        public event EventHandler BoutonFinDeMatchClicked;
        public event EventHandler SwitchClicked;
        public event EventHandler RougeResetMedClicked;
        public event EventHandler BleuResetMedClicked;
        public event EventHandler StartClicked;
        public event EventHandler Plus1secondesClicked;
        public event EventHandler Moins10secondesClicked;
        public event EventHandler Plus10secondesClicked;
        public event EventHandler Moins1secondesClicked;
        public event EventHandler RougeIncrement1Clicked;
        public event EventHandler RougeIncrement2Clicked;
        public event EventHandler RougeIncrement3Clicked;
        public event EventHandler RougeIncrement4Clicked;
        public event EventHandler RougeAvantageClicked;
        public event EventHandler RougePenaliteClicked;
        public event EventHandler BleuIncrement1Clicked;
        public event EventHandler BleuIncrement2Clicked;
        public event EventHandler BleuIncrement3Clicked;
        public event EventHandler BleuIncrement4Clicked;
        public event EventHandler BleuAvantageClicked;
        public event EventHandler BleuPenaliteClicked;
        public event EventHandler RougeDecrement1Clicked;
        public event EventHandler RougeDecrement2Clicked;
        public event EventHandler RougeDecrement3Clicked;
        public event EventHandler RougeDecrement4Clicked;
        public event EventHandler RougeMoinsAvantageClicked;
        public event EventHandler RougeMoinsPenaliteClicked;
        public event EventHandler BleuDecrement1Clicked;
        public event EventHandler BleuDecrement2Clicked;
        public event EventHandler BleuDecrement3Clicked;
        public event EventHandler BleuDecrement4Clicked;
        public event EventHandler BleuMoinsAvantageClicked;
        public event EventHandler BleuMoinsPenaliteClicked;
        public event EventHandler RetourFormClicked;
        public event EventHandler UndoClicked;

        public int Id;



        public MainWindow(int id)
        {
            InitializeComponent();
            this.Id = id;


            btn_Start.PreviewKeyDown += Windows_Keydown;
            BleuMed.PreviewKeyDown += Windows_Keydown;
            RougeMed.PreviewKeyDown += Windows_Keydown;

            scoreboardData = new ScoreboardData();
            this.DataContext = scoreboardData;

            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = new TimeSpan(0, 0, 1);



            timer1.Tick += new EventHandler(timer1_Tick);
            timer1.Interval = new TimeSpan(0, 0, 1);

            timer2.Tick += new EventHandler(timer2_Tick);
            timer2.Interval = new TimeSpan(0, 0, 1);


            valide = true;

            Instance = null;
            if (Instance == null)
            {
                Instance = this;
            }

            RougeIncrement1.IsEnabled = false;
            RougeDecrement1.IsEnabled = false;
            RougeIncrement2.IsEnabled = false;
            RougeDecrement2.IsEnabled = false;
            RougeIncrement3.IsEnabled = false;
            RougeDecrement3.IsEnabled = false;
            RougeIncrement4.IsEnabled = false;
            RougeDecrement4.IsEnabled = false;
            RougePlusAvantage.IsEnabled = false;
            RougeMoinsAvantage.IsEnabled = false;
            RougePlusPenalite.IsEnabled = false;
            RougeMoinsPenalite.IsEnabled = false;
            RougeMed.IsEnabled = false;
            RougeResetMed.IsEnabled = false;

            BleuIncrement1.IsEnabled = false;
            BleuDecrement1.IsEnabled = false;
            BleuIncrement2.IsEnabled = false;
            BleuDecrement2.IsEnabled = false;
            BleuIncrement3.IsEnabled = false;
            BleuDecrement3.IsEnabled = false;
            BleuIncrement4.IsEnabled = false;
            BleuDecrement4.IsEnabled = false;
            BleuPlusAvantage.IsEnabled = false;
            BleuMoinsAvantage.IsEnabled = false;
            BleuPlusPenalite.IsEnabled = false;
            BleuMoinsPenalite.IsEnabled = false;
            BleuMed.IsEnabled = false;
            BleuResetMed.IsEnabled = false;

        }




        private void Undo_Click(object sender, RoutedEventArgs e)
        {
            // Annuler la dernière action des boutons
            UndoClicked?.Invoke(this, EventArgs.Empty);
            if (actions.Count > 0)
            {
                int lastIndex = actions.Count - 1;
                actions[lastIndex](); // Exécute l'action d'annulation
                actions.RemoveAt(lastIndex); // Supprime l'action de la liste
            }
        }


        string nom1;
        string prenom1;
        string nom2;
        string prenom2;
        string couleur1;
        string couleur2;
        string minuteC;
        string phaseC;
        string categorieC;

        public void Load_Data(string nom1, string nom2, string prenom1, string prenom2, string club1, string club2, string logoclub1, string logoclub2, string tour, string categorie, string fondscoreboard)
        {
            textblockNom1.Text = nom1;
            textblockNom2.Text = nom2;
            textblockPrenom1.Text = prenom1;
            textblockPrenom2.Text = prenom2;
            textblockClub1.Text = club1;
            textblockClub2.Text = club2;
            BitmapImage imgclub1 = new BitmapImage(new Uri(logoclub1, UriKind.RelativeOrAbsolute));
            imageclub1.Source = imgclub1;
            BitmapImage imgclub2 = new BitmapImage(new Uri(logoclub2, UriKind.RelativeOrAbsolute));
            imageclub2.Source = imgclub2;
            tb_Phase.Text = tour;
            tb_Categorie.Text = categorie;
            BitmapImage imgfond = new BitmapImage(new Uri(fondscoreboard, UriKind.RelativeOrAbsolute));
            imgfnd.Source = imgfond;
        }

        public void SetImage(string textPath1, string imagePath1, string textPath2, string imagePath2, string selectedImagePath, string textPath3, string imagePath3, string textPath4, string imagePath4, string Nom1, string Prenom1, string Nom2, string Prenom2, string Couleur1, string Couleur2, string MinuteC, string PhaseC, string CategorieC)
        {
            // Chargez l'image dans l'élément Image.
            BitmapImage img1 = new BitmapImage(new Uri(imagePath1, UriKind.RelativeOrAbsolute));
            imageclub1.Source = img1;
            textblockClub1.Text = textPath1;

            BitmapImage img2 = new BitmapImage(new Uri(imagePath2, UriKind.RelativeOrAbsolute));
            imageclub2.Source = img2;
            textblockClub2.Text = textPath2;

            BitmapImage img3 = new BitmapImage(new Uri(imagePath3, UriKind.RelativeOrAbsolute));
            imagepays1.Source = img3;
            textblockClub3.Text = textPath3;

            BitmapImage img4 = new BitmapImage(new Uri(imagePath4, UriKind.RelativeOrAbsolute));
            imagepays2.Source = img4;
            textblockClub4.Text = textPath4;

            if (selectedImagePath != null)
            {
                BitmapImage img5 = new BitmapImage(new Uri(selectedImagePath, UriKind.RelativeOrAbsolute));
                imgfnd.Source = img5;
            }

            textblockNom1.Text = Nom1;
            textblockPrenom1.Text = Prenom1;

            textblockNom2.Text = Nom2;
            textblockPrenom2.Text = Prenom2;

            bordercl1.Background = (Brush)new System.Windows.Media.BrushConverter().ConvertFromString(Couleur1);
            bordercl2.Background = (Brush)new System.Windows.Media.BrushConverter().ConvertFromString(Couleur2);

            tb_Minutes.Text = MinuteC;
            tb_Phase.Text = PhaseC;
            tb_Categorie.Text = CategorieC;


        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            minutesMed2 = Convert.ToInt32(tb_MinutesMed2.Text);
            secondesMed2 = Convert.ToInt32(tb_SecondesMed2.Text);
            secondesMed2--;
            if (secondesMed2 < 0)
            {
                if (minutesMed2 == 0)
                {
                    timer2.Stop();
                    tb_MinutesMed2.Text = "00";
                    tb_SecondesMed2.Text = "00";
                }
                else
                {
                    minutesMed2--;
                    tb_MinutesMed2.Text = minutesMed2.ToString();
                    secondesMed2 = 59;
                    tb_SecondesMed2.Text = secondesMed2.ToString();
                }
            }

            if (minutesMed2 == 0 && secondesMed2 == 0)
            {
                timer2.Stop();
            }

            if (minutesMed2 == 0 && secondesMed2 <= 10)
            {
                tb_MinutesMed2.Foreground = Brushes.Red;
                tb_SecondesMed2.Foreground = Brushes.Red;
                tb_separatorMed2.Foreground = Brushes.Red;
            }


            if (minutesMed2 < 10)
            {
                tb_MinutesMed2.Text = "0" + minutesMed2.ToString();
            }
            else
            {
                tb_MinutesMed2.Text = minutesMed2.ToString();
            }
            if (secondesMed2 < 10 && secondesMed2 >= 0)
            {
                tb_SecondesMed2.Text = "0" + secondesMed2.ToString();
            }

            else
            {
                tb_SecondesMed2.Text = secondesMed2.ToString();
            }

        }

        private void StartMed2_Click(object sender, RoutedEventArgs e)
        {
            /* ScoreboardPublic scoreboardPublic = new ScoreboardPublic(nom1, prenom1, nom2, prenom2, couleur1, couleur2, minuteC, phaseC, categorieC);
             scoreboardPublic.StartMed2();*/
            StartMed2Clicked?.Invoke(this, EventArgs.Empty);

            timer.Stop();
            tb_Minutes.Foreground = Brushes.Yellow;
            tb_Secondes.Foreground = Brushes.Yellow;
            tb_separator.Foreground = Brushes.Yellow;

            if (play2)
            {
                timer2.Start();
                tb_MinutesMed2.Foreground = Brushes.Black;
                tb_SecondesMed2.Foreground = Brushes.Black;
                tb_separatorMed2.Foreground = Brushes.Black;


                play2 = false;
            }
            else
            {
                timer2.Stop();
                tb_MinutesMed2.Foreground = Brushes.White;
                tb_SecondesMed2.Foreground = Brushes.White;
                tb_separatorMed2.Foreground = Brushes.White;


                play2 = true;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            minutesMed1 = Convert.ToInt32(tb_MinutesMed1.Text);
            secondesMed1 = Convert.ToInt32(tb_SecondesMed1.Text);
            secondesMed1--;
            if (secondesMed1 < 0)
            {
                if (minutesMed1 == 0)
                {
                    timer1.Stop();
                    tb_MinutesMed1.Text = "00";
                    tb_SecondesMed1.Text = "00";
                }
                else
                {
                    minutesMed1--;
                    tb_MinutesMed1.Text = minutesMed1.ToString();
                    secondesMed1 = 59;
                    tb_SecondesMed1.Text = secondesMed1.ToString();
                }
            }

            if (minutesMed1 == 0 && secondesMed1 == 0)
            {
                timer1.Stop();

            }

            if (minutesMed1 == 0 && secondesMed1 <= 10)
            {

                tb_MinutesMed1.Foreground = Brushes.Red;
                tb_SecondesMed1.Foreground = Brushes.Red;
                tb_separatorMed1.Foreground = Brushes.Red;
            }


            if (minutesMed1 < 10)
            {
                tb_MinutesMed1.Text = "0" + minutesMed1.ToString();
            }
            else
            {
                tb_MinutesMed1.Text = minutesMed1.ToString();
            }
            if (secondesMed1 < 10 && secondesMed1 >= 0)
            {
                tb_SecondesMed1.Text = "0" + secondesMed1.ToString();
            }

            else
            {
                tb_SecondesMed1.Text = secondesMed1.ToString();
            }

        }

        private void StartMed1_Click(object sender, RoutedEventArgs e)
        {
            StartMed1Clicked?.Invoke(this, EventArgs.Empty);
            timer.Stop();
            tb_Minutes.Foreground = Brushes.Yellow;
            tb_Secondes.Foreground = Brushes.Yellow;
            tb_separator.Foreground = Brushes.Yellow;

            if (play1)
            {
                timer1.Start();
                tb_MinutesMed1.Foreground = Brushes.Black;
                tb_SecondesMed1.Foreground = Brushes.Black;
                tb_separatorMed1.Foreground = Brushes.Black;


                play1 = false;
            }
            else
            {
                timer1.Stop();
                tb_MinutesMed1.Foreground = Brushes.White;
                tb_SecondesMed1.Foreground = Brushes.White;
                tb_separatorMed1.Foreground = Brushes.White;


                play1 = true;
            }

        }

        private void Windows_Keydown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                Start_Click(sender, e);
            }
            if (e.Key == Key.H)
            {
                StartMed1_Click(sender, e);
            }
            if (e.Key == Key.B)
            {
                StartMed2_Click(sender, e);
            }
            if (e.Key == Key.Escape)
            {
                Button_Click_RetourForm(sender, e);
            }
            if (e.Key == Key.Z)
            {
                Undo_Click(sender, e);
            }
            if (e.Key == Key.NumPad0)
            {
                tb_Minutes.Text = "00";
                TextBox tb_Minutes_2 = ScoreboardPublic.Instance.txtboxmin();
                tb_Minutes_2.Text = tb_Minutes.Text;
            }
            if (e.Key == Key.NumPad1)
            {
                tb_Minutes.Text = "01";
                TextBox tb_Minutes_2 = ScoreboardPublic.Instance.txtboxmin();
                tb_Minutes_2.Text = tb_Minutes.Text;
            }
            if ((e.Key == Key.NumPad2))
            {
                tb_Minutes.Text = "02";
                TextBox tb_Minutes_2 = ScoreboardPublic.Instance.txtboxmin();
                tb_Minutes_2.Text = tb_Minutes.Text;
            }
            if ((e.Key == Key.NumPad3))
            {
                tb_Minutes.Text = "03";
                TextBox tb_Minutes_2 = ScoreboardPublic.Instance.txtboxmin();
                tb_Minutes_2.Text = tb_Minutes.Text;
            }
            if ((e.Key == Key.NumPad4))
            {
                tb_Minutes.Text = "04";
                TextBox tb_Minutes_2 = ScoreboardPublic.Instance.txtboxmin();
                tb_Minutes_2.Text = tb_Minutes.Text;
            }
            if ((e.Key == Key.NumPad5))
            {
                tb_Minutes.Text = "05";
                TextBox tb_Minutes_2 = ScoreboardPublic.Instance.txtboxmin();
                tb_Minutes_2.Text = tb_Minutes.Text;
            }
            if ((e.Key == Key.NumPad6))
            {
                tb_Minutes.Text = "006";
                TextBox tb_Minutes_2 = ScoreboardPublic.Instance.txtboxmin();
                tb_Minutes_2.Text = tb_Minutes.Text;
            }
            if ((e.Key == Key.NumPad7))
            {
                tb_Minutes.Text = "07";
                TextBox tb_Minutes_2 = ScoreboardPublic.Instance.txtboxmin();
                tb_Minutes_2.Text = tb_Minutes.Text;
            }
            if ((e.Key == Key.NumPad8))
            {
                tb_Minutes.Text = "08";
                TextBox tb_Minutes_2 = ScoreboardPublic.Instance.txtboxmin();
                tb_Minutes_2.Text = tb_Minutes.Text;
            }
            if ((e.Key == Key.NumPad9))
            {
                tb_Minutes.Text = "09";
                TextBox tb_Minutes_2 = ScoreboardPublic.Instance.txtboxmin();
                tb_Minutes_2.Text = tb_Minutes.Text;
            }
            e.Handled = true;

        }

        private void Button_Click_FinMatch(object sender, RoutedEventArgs e)
        {
            BoutonFinDeMatchClicked?.Invoke(this, EventArgs.Empty);
            if (valide)
            {
                timer.Stop();
                tb_Minutes.Foreground = Brushes.Yellow;
                tb_Secondes.Foreground = Brushes.Yellow;
                tb_separator.Foreground = Brushes.Yellow;

                tb_Minutes.Visibility = Visibility.Visible;
                tb_Secondes.Visibility = Visibility.Visible;
                tb_separator.Visibility = Visibility.Visible;

                BleuFinCombat ChoixBleu = new BleuFinCombat();
                getbouton2.Visibility = Visibility.Collapsed;
                btnContent2.Children.Add(ChoixBleu);
                btnContent2.Visibility = Visibility.Visible;
                bleuFinCombat = ChoixBleu;

                RougeFinCombat ChoixRouge = new RougeFinCombat();
                getbouton.Visibility = Visibility.Collapsed;
                btnContent1.Children.Add(ChoixRouge);
                btnContent1.Visibility = Visibility.Visible;
                rougeFinCombat = ChoixRouge;

                valide = false;

                finDeMatch.Content = "RETOUR";


            }
            else
            {
                StackPanel getbouton = MainWindow.Instance.GetBouton();


                StackPanel getbouton2 = MainWindow.Instance.GetBouton2();


                Grid btnContent1 = MainWindow.Instance.getContent1();
                Grid btnContent2 = MainWindow.Instance.getContent2();

                BleuFinCombat ChoixBleu = new BleuFinCombat();
                bleuFinCombat = ChoixBleu;
                RougeFinCombat ChoixRouge = new RougeFinCombat();
                rougeFinCombat = ChoixRouge;
                btnContent2.Children.Remove(bleuFinCombat);
                getbouton.Visibility = Visibility.Visible;
                btnContent2.Visibility = Visibility.Collapsed;
                btnContent1.Children.Remove(rougeFinCombat);
                getbouton2.Visibility = Visibility.Visible;
                btnContent1.Visibility = Visibility.Collapsed;

                valide = true;

                finDeMatch.Content = "FIN DE MATCH";

            }
        }

        private void Switch_Click(object sender, RoutedEventArgs e)
        {
            SwitchClicked?.Invoke(this, EventArgs.Empty);
            tmp1 = textblockPrenom2.Text;
            textblockPrenom2.Text = textblockPrenom1.Text;
            textblockPrenom1.Text = tmp1;

            tmp2 = textblockNom2.Text;
            textblockNom2.Text = textblockNom1.Text;
            textblockNom1.Text = tmp2;

            tmp3 = textblockClub2.Text;
            textblockClub2.Text = textblockClub1.Text;
            textblockClub1.Text = tmp3;

            tmp4 = textblockClub3.Text;
            textblockClub3.Text = textblockClub4.Text;
            textblockClub4.Text = tmp4;

            tmp5 = tb_MinutesMed1.Text;
            tb_MinutesMed1.Text = tb_MinutesMed2.Text;
            tb_MinutesMed2.Text = tmp5;

            tmp6 = tb_SecondesMed1.Text;
            tb_SecondesMed1.Text = tb_SecondesMed2.Text;
            tb_SecondesMed2.Text = tmp6;

            string img1 = imageclub1.Source.ToString();
            string img2 = imageclub2.Source.ToString();

            tmp7 = img1;
            img1 = img2;
            img2 = tmp7;

            BitmapImage imgc1 = new BitmapImage(new Uri(img1, UriKind.RelativeOrAbsolute));
            imageclub1.Source = imgc1;

            BitmapImage imgc2 = new BitmapImage(new Uri(img2, UriKind.RelativeOrAbsolute));
            imageclub2.Source = imgc2;

            string img3 = imagepays1.Source.ToString();
            string img4 = imagepays2.Source.ToString();

            tmp8 = img3;
            img3 = img4;
            img4 = tmp8;

            BitmapImage imgc3 = new BitmapImage(new Uri(img3, UriKind.RelativeOrAbsolute));
            imagepays1.Source = imgc3;

            BitmapImage imgc4 = new BitmapImage(new Uri(img4, UriKind.RelativeOrAbsolute));
            imagepays2.Source = imgc4;


        }
        private void timer_Tick(object sender, EventArgs e)
        {
            minutes = Convert.ToInt32(tb_Minutes.Text);
            secondes = Convert.ToInt32(tb_Secondes.Text);
            secondes--;
            if (secondes < 0)
            {
                if (minutes == 0)
                {
                    timer.Stop();
                    tb_Minutes.Text = "00";
                    tb_Secondes.Text = "00";
                }
                else
                {
                    minutes--;
                    tb_Minutes.Text = minutes.ToString();
                    secondes = 59;
                    tb_Secondes.Text = secondes.ToString();
                }
            }


            if (minutes == 0 && secondes <= 10)
            {

                tb_Minutes.Foreground = Brushes.Red;
                tb_Secondes.Foreground = Brushes.Red;
                tb_separator.Foreground = Brushes.Red;




                /*tb_Minutes.Visibility = tb_Minutes.Visibility == Visibility.Visible ? Visibility.Hidden : Visibility.Visible;
                tb_Secondes.Visibility = tb_Secondes.Visibility == Visibility.Visible ? Visibility.Hidden : Visibility.Visible;
                tb_separator.Visibility = tb_separator.Visibility == Visibility.Visible ? Visibility.Hidden : Visibility.Visible;*/
            }

            if (minutes == 0 && secondes == 0)
            {
                timer.Stop();
                tb_Minutes.Visibility = Visibility.Visible;
                tb_Secondes.Visibility = Visibility.Visible;
                tb_separator.Visibility = Visibility.Visible;

                SoundPlayer player = new SoundPlayer(@"C:\23070.wav");
                player.Load();
                player.Play();
            }


            if (minutes < 10)
            {
                tb_Minutes.Text = "0" + minutes.ToString();
            }
            else
            {
                tb_Minutes.Text = minutes.ToString();
            }
            if (secondes < 10 && secondes >= 0)
            {
                tb_Secondes.Text = "0" + secondes.ToString();
            }

            else
            {
                tb_Secondes.Text = secondes.ToString();
            }

        }

        private void RougeResetMed_Click(object sender, RoutedEventArgs e)
        {
            RougeResetMedClicked?.Invoke(this, EventArgs.Empty);
            tb_MinutesMed1.Text = "02";
            tb_SecondesMed1.Text = "00";

            tb_MinutesMed1.Foreground = Brushes.Black;
            tb_SecondesMed1.Foreground = Brushes.Black;
            tb_separatorMed1.Foreground = Brushes.Black;
        }

        private void BleuResetMed_Click(object sender, RoutedEventArgs e)
        {
            BleuResetMedClicked?.Invoke(this, EventArgs.Empty);
            tb_MinutesMed2.Text = "02";
            tb_SecondesMed2.Text = "00";

            tb_MinutesMed2.Foreground = Brushes.Black;
            tb_SecondesMed2.Foreground = Brushes.Black;
            tb_separatorMed2.Foreground = Brushes.Black;
        }
        private void Start_Click(object sender, RoutedEventArgs e)
        {
            StartClicked?.Invoke(this, EventArgs.Empty);
            timer1.Stop();


            timer2.Stop();


            active = true;

            RougeIncrement1.IsEnabled = true;
            RougeDecrement1.IsEnabled = true;
            RougeIncrement2.IsEnabled = true;
            RougeDecrement2.IsEnabled = true;
            RougeIncrement3.IsEnabled = true;
            RougeDecrement3.IsEnabled = true;
            RougeIncrement4.IsEnabled = true;
            RougeDecrement4.IsEnabled = true;
            RougePlusAvantage.IsEnabled = true;
            RougeMoinsAvantage.IsEnabled = true;
            RougePlusPenalite.IsEnabled = true;
            RougeMoinsPenalite.IsEnabled = true;
            RougeMed.IsEnabled = true;
            RougeResetMed.IsEnabled = true;

            BleuIncrement1.IsEnabled = true;
            BleuDecrement1.IsEnabled = true;
            BleuIncrement2.IsEnabled = true;
            BleuDecrement2.IsEnabled = true;
            BleuIncrement3.IsEnabled = true;
            BleuDecrement3.IsEnabled = true;
            BleuIncrement4.IsEnabled = true;
            BleuDecrement4.IsEnabled = true;
            BleuPlusAvantage.IsEnabled = true;
            BleuMoinsAvantage.IsEnabled = true;
            BleuPlusPenalite.IsEnabled = true;
            BleuMoinsPenalite.IsEnabled = true;
            BleuMed.IsEnabled = true;
            BleuResetMed.IsEnabled = true;


            if (play)
            {
                timer.Start();
                tb_Minutes.Foreground = Brushes.White;
                tb_Secondes.Foreground = Brushes.White;
                tb_separator.Foreground = Brushes.White;

                tb_Minutes.Visibility = Visibility.Visible;
                tb_Secondes.Visibility = Visibility.Visible;
                tb_separator.Visibility = Visibility.Visible;

                btn_Start.Content = "Pause";
                play = false;
            }
            else
            {
                timer.Stop();
                tb_Minutes.Foreground = Brushes.Yellow;
                tb_Secondes.Foreground = Brushes.Yellow;
                tb_separator.Foreground = Brushes.Yellow;

                tb_Minutes.Visibility = Visibility.Visible;
                tb_Secondes.Visibility = Visibility.Visible;
                tb_separator.Visibility = Visibility.Visible;

                btn_Start.Content = "Play";
                play = true;
            }

        }

        /*private void Btn_Pause_Click(object sender, RoutedEventArgs e)
        {
            timer.Stop();
            tb_Minutes.Foreground = Brushes.Yellow;
            tb_Secondes.Foreground = Brushes.Yellow;
            tb_separator.Foreground = Brushes.Yellow;
        }*/
        private void Plus1secondes_Click(object sender, RoutedEventArgs e)
        {
            Plus1secondesClicked?.Invoke(this, EventArgs.Empty);
            minutes = Convert.ToInt32(this.tb_Minutes.Text);
            secondes = Convert.ToInt32(this.tb_Secondes.Text);

            if (secondes < 59)
            {
                secondes++;
            }
            else
            {
                secondes = 0;
                minutes++;

            }

            if (minutes < 10)
            {
                tb_Minutes.Text = "0" + minutes.ToString();
            }
            else
            {
                tb_Minutes.Text = minutes.ToString();
            }
            if (secondes < 10 && secondes >= 0)
            {
                tb_Secondes.Text = "0" + secondes.ToString();
            }

            else
            {
                tb_Secondes.Text = secondes.ToString();
            }

            actions.Add(() =>
            {
                // Code pour annuler l'action ici
                // Par exemple, supprimez l'élément ajouté à la liste.
                if (secondes > 0)
                {
                    secondes--;
                }
                else
                {
                    if (minutes > 0)
                    {
                        secondes = 59;
                        minutes--;
                    }
                }

                if (minutes < 10)
                {
                    tb_Minutes.Text = "0" + minutes.ToString();
                }
                else
                {
                    tb_Minutes.Text = minutes.ToString();
                }
                if (secondes < 10 && secondes >= 0)
                {
                    tb_Secondes.Text = "0" + secondes.ToString();
                }

                else
                {
                    tb_Secondes.Text = secondes.ToString();
                }

            });
        }

        private void Moins10secondes_Click(object sender, RoutedEventArgs e)
        {
            Moins10secondesClicked?.Invoke(this, EventArgs.Empty);
            minutes = Convert.ToInt32(this.tb_Minutes.Text);
            secondes = Convert.ToInt32(this.tb_Secondes.Text);

            if (secondes >= 10)
            {
                secondes -= 10;
            }
            else
            {
                if (minutes > 0)
                {
                    secondes = (secondes - 10) + 60;
                    minutes--;
                }
            }

            if (minutes < 10)
            {
                tb_Minutes.Text = "0" + minutes.ToString();
            }
            else
            {
                tb_Minutes.Text = minutes.ToString();
            }
            if (secondes < 10 && secondes >= 0)
            {
                tb_Secondes.Text = "0" + secondes.ToString();
            }

            else
            {
                tb_Secondes.Text = secondes.ToString();
            }
            actions.Add(() =>
            {
                // Code pour annuler l'action ici
                // Par exemple, supprimez l'élément ajouté à la liste.
                if (secondes < 50)
                {
                    secondes += 10;
                }
                else
                {
                    secondes = (secondes + 10) - 60;
                    minutes++;

                }

                if (minutes < 10)
                {
                    tb_Minutes.Text = "0" + minutes.ToString();
                }
                else
                {
                    tb_Minutes.Text = minutes.ToString();
                }
                if (secondes < 10 && secondes >= 0)
                {
                    tb_Secondes.Text = "0" + secondes.ToString();
                }

                else
                {
                    tb_Secondes.Text = secondes.ToString();
                }

            });
        }

        private void Plus10secondes_Click(object sender, RoutedEventArgs e)
        {
            Plus10secondesClicked?.Invoke(this, EventArgs.Empty);
            minutes = Convert.ToInt32(this.tb_Minutes.Text);
            secondes = Convert.ToInt32(this.tb_Secondes.Text);

            if (secondes < 50)
            {
                secondes += 10;
            }
            else
            {
                secondes = (secondes + 10) - 60;
                minutes++;

            }

            if (minutes < 10)
            {
                tb_Minutes.Text = "0" + minutes.ToString();
            }
            else
            {
                tb_Minutes.Text = minutes.ToString();
            }
            if (secondes < 10 && secondes >= 0)
            {
                tb_Secondes.Text = "0" + secondes.ToString();
            }

            else
            {
                tb_Secondes.Text = secondes.ToString();
            }

            actions.Add(() =>
            {
                // Code pour annuler l'action ici
                // Par exemple, supprimez l'élément ajouté à la liste.
                if (secondes >= 10)
                {
                    secondes -= 10;
                }
                else
                {
                    if (minutes > 0)
                    {
                        secondes = (secondes - 10) + 60;
                        minutes--;
                    }
                }

                if (minutes < 10)
                {
                    tb_Minutes.Text = "0" + minutes.ToString();
                }
                else
                {
                    tb_Minutes.Text = minutes.ToString();
                }
                if (secondes < 10 && secondes >= 0)
                {
                    tb_Secondes.Text = "0" + secondes.ToString();
                }

                else
                {
                    tb_Secondes.Text = secondes.ToString();
                }
            });
        }

        private void Moins1secondes_Click(object sender, RoutedEventArgs e)
        {
            Moins1secondesClicked?.Invoke(this, EventArgs.Empty);
            minutes = Convert.ToInt32(this.tb_Minutes.Text);
            secondes = Convert.ToInt32(this.tb_Secondes.Text);

            if (secondes > 0)
            {
                secondes--;
            }
            else
            {
                if (minutes > 0)
                {
                    secondes = 59;
                    minutes--;
                }
            }

            if (minutes < 10)
            {
                tb_Minutes.Text = "0" + minutes.ToString();
            }
            else
            {
                tb_Minutes.Text = minutes.ToString();
            }
            if (secondes < 10 && secondes >= 0)
            {
                tb_Secondes.Text = "0" + secondes.ToString();
            }

            else
            {
                tb_Secondes.Text = secondes.ToString();
            }

            actions.Add(() =>
            {
                // Code pour annuler l'action ici
                // Par exemple, supprimez l'élément ajouté à la liste.
                if (secondes < 59)
                {
                    secondes++;
                }
                else
                {
                    secondes = 0;
                    minutes++;

                }

                if (minutes < 10)
                {
                    tb_Minutes.Text = "0" + minutes.ToString();
                }
                else
                {
                    tb_Minutes.Text = minutes.ToString();
                }
                if (secondes < 10 && secondes >= 0)
                {
                    tb_Secondes.Text = "0" + secondes.ToString();
                }

                else
                {
                    tb_Secondes.Text = secondes.ToString();
                }

            });
        }

        private void RougeIncrement1_Click(object sender, RoutedEventArgs e)
        {
            RougeIncrement1Clicked?.Invoke(this, EventArgs.Empty);

            scoreboardData.RougeScore += 1;

            actions.Add(() =>
            {
                // Code pour annuler l'action ici
                // Par exemple, supprimez l'élément ajouté à la liste.
                if (scoreboardData.RougeScore > 0)
                {
                    scoreboardData.RougeScore -= 1;
                }
            });
        }


        private void RougeIncrement2_Click_1(object sender, RoutedEventArgs e)
        {
            RougeIncrement2Clicked?.Invoke(this, EventArgs.Empty);
            scoreboardData.RougeScore += 2;
            actions.Add(() =>
            {
                // Code pour annuler l'action ici
                // Par exemple, supprimez l'élément ajouté à la liste.
                if (scoreboardData.RougeScore > 1)
                {
                    scoreboardData.RougeScore -= 2;
                }
            });
        }




        private void RougeIncrement3_Click_1(object sender, RoutedEventArgs e)
        {
            RougeIncrement3Clicked?.Invoke(this, EventArgs.Empty);
            scoreboardData.RougeScore += 3;
            actions.Add(() =>
            {
                // Code pour annuler l'action ici
                // Par exemple, supprimez l'élément ajouté à la liste.

                if (scoreboardData.RougeScore > 2)
                {
                    scoreboardData.RougeScore -= 3;
                }
            });
        }


        private void RougeIncrement4_Click(object sender, RoutedEventArgs e)
        {
            RougeIncrement4Clicked?.Invoke(this, EventArgs.Empty);
            scoreboardData.RougeScore += 4;
            actions.Add(() =>
            {
                // Code pour annuler l'action ici
                // Par exemple, supprimez l'élément ajouté à la liste.

                if (scoreboardData.RougeScore > 3)
                {
                    scoreboardData.RougeScore -= 4;
                }
            });
        }


        private void RougeAvantage_Click_1(object sender, RoutedEventArgs e)
        {
            RougeAvantageClicked?.Invoke(this, EventArgs.Empty);
            scoreboardData.RougeAvantage += 1;
            actions.Add(() =>
            {
                // Code pour annuler l'action ici
                // Par exemple, supprimez l'élément ajouté à la liste.

                if (scoreboardData.RougeAvantage > 0)
                {
                    scoreboardData.RougeAvantage -= 1;
                }
            });
        }



        private void RougePenalite_Click(object sender, RoutedEventArgs e)
        {
            RougePenaliteClicked?.Invoke(this, EventArgs.Empty);

            scoreboardData.RougePenalite += 1;
            if (scoreboardData.RougePenalite == 2)
            {
                scoreboardData.BleuAvantage += 1;
            }

            if (scoreboardData.RougePenalite == 3)
            {
                scoreboardData.BleuScore += 2;
            }
            actions.Add(() =>
            {
                // Code pour annuler l'action ici
                // Par exemple, supprimez l'élément ajouté à la liste.

                if (scoreboardData.RougePenalite > 0)
                {
                    if (scoreboardData.RougePenalite == 2)
                    {
                        scoreboardData.BleuAvantage -= 1;
                    }

                    if (scoreboardData.RougePenalite == 3)
                    {
                        scoreboardData.BleuScore -= 2;
                    }
                    scoreboardData.RougePenalite -= 1;

                }
            });
        }




        private void RougeDecrement1_Click(object sender, RoutedEventArgs e)
        {
            RougeDecrement1Clicked?.Invoke(this, EventArgs.Empty);
            if (scoreboardData.RougeScore > 0)
            {
                scoreboardData.RougeScore -= 1;
            }
            actions.Add(() =>
            {
                // Code pour annuler l'action ici
                // Par exemple, supprimez l'élément ajouté à la liste.
                scoreboardData.RougeScore += 1;
            });
        }

        private void RougeDecrement2_Click(object sender, RoutedEventArgs e)
        {
            RougeDecrement2Clicked?.Invoke(this, EventArgs.Empty);
            if (scoreboardData.RougeScore > 1)
            {
                scoreboardData.RougeScore -= 2;
            }
            actions.Add(() =>
            {
                // Code pour annuler l'action ici
                // Par exemple, supprimez l'élément ajouté à la liste.
                scoreboardData.RougeScore += 2;
            });
        }

        private void RougeDecrement3_Click(object sender, RoutedEventArgs e)
        {
            RougeDecrement3Clicked?.Invoke(this, EventArgs.Empty);
            if (scoreboardData.RougeScore > 2)
            {
                scoreboardData.RougeScore -= 3;
            }
            actions.Add(() =>
            {
                // Code pour annuler l'action ici
                // Par exemple, supprimez l'élément ajouté à la liste.
                scoreboardData.RougeScore += 3;
            });
        }

        private void RougeDecrement4_Click(object sender, RoutedEventArgs e)
        {
            RougeDecrement4Clicked?.Invoke(this, EventArgs.Empty);
            if (scoreboardData.RougeScore > 3)
            {
                scoreboardData.RougeScore -= 4;
            }
            actions.Add(() =>
            {
                // Code pour annuler l'action ici
                // Par exemple, supprimez l'élément ajouté à la liste.
                scoreboardData.RougeScore += 4;
            });
        }

        private void RougeMoinsAvantage_Click(object sender, RoutedEventArgs e)
        {
            RougeMoinsAvantageClicked?.Invoke(this, EventArgs.Empty);
            if (scoreboardData.RougeAvantage > 0)
            {
                scoreboardData.RougeAvantage -= 1;
            }
            actions.Add(() =>
            {
                // Code pour annuler l'action ici
                // Par exemple, supprimez l'élément ajouté à la liste.
                scoreboardData.RougeAvantage += 1;
            });
        }

        private void RougeMoinsPenalite_Click(object sender, RoutedEventArgs e)
        {
            RougeMoinsPenaliteClicked?.Invoke(this, EventArgs.Empty);
            if (scoreboardData.RougePenalite > 0)
            {
                scoreboardData.RougePenalite -= 1;
            }
            actions.Add(() =>
            {
                // Code pour annuler l'action ici
                // Par exemple, supprimez l'élément ajouté à la liste.
                scoreboardData.RougePenalite += 1;
                if (scoreboardData.RougePenalite == 2)
                {
                    scoreboardData.BleuAvantage += 1;
                }

                if (scoreboardData.RougePenalite == 3)
                {
                    scoreboardData.BleuScore += 2;
                }
            });
        }
        private void BleuIncrement1_Click(object sender, RoutedEventArgs e)
        {
            BleuIncrement1Clicked?.Invoke(this, EventArgs.Empty);
            scoreboardData.BleuScore += 1;
            actions.Add(() =>
            {
                // Code pour annuler l'action ici
                // Par exemple, supprimez l'élément ajouté à la liste.
                if (scoreboardData.BleuScore > 0)
                {
                    scoreboardData.BleuScore -= 1;
                }
            });
        }


        private void BleuIncrement2_Click_1(object sender, RoutedEventArgs e)
        {
            BleuIncrement2Clicked?.Invoke(this, EventArgs.Empty);
            scoreboardData.BleuScore += 2;
            actions.Add(() =>
            {
                // Code pour annuler l'action ici
                // Par exemple, supprimez l'élément ajouté à la liste.
                if (scoreboardData.BleuScore > 1)
                {
                    scoreboardData.BleuScore -= 2;
                }
            });
        }


        private void BleuIncrement3_Click(object sender, RoutedEventArgs e)
        {
            BleuIncrement3Clicked?.Invoke(this, EventArgs.Empty);
            scoreboardData.BleuScore += 3;
            actions.Add(() =>
            {
                // Code pour annuler l'action ici
                // Par exemple, supprimez l'élément ajouté à la liste.
                if (scoreboardData.BleuScore > 2)
                {
                    scoreboardData.BleuScore -= 3;
                }
            });
        }





        private void BleuIncrement4_Click(object sender, RoutedEventArgs e)
        {
            BleuIncrement4Clicked?.Invoke(this, EventArgs.Empty);
            scoreboardData.BleuScore += 4;
            actions.Add(() =>
            {
                // Code pour annuler l'action ici
                // Par exemple, supprimez l'élément ajouté à la liste.
                if (scoreboardData.BleuScore > 3)
                {
                    scoreboardData.BleuScore -= 4;
                }
            });
        }





        private void BleuDecrement1_Click(object sender, RoutedEventArgs e)
        {
            BleuDecrement1Clicked?.Invoke(this, EventArgs.Empty);
            if (scoreboardData.BleuScore > 0)
            {
                scoreboardData.BleuScore -= 1;
            }
            actions.Add(() =>
            {
                // Code pour annuler l'action ici
                // Par exemple, supprimez l'élément ajouté à la liste.
                scoreboardData.BleuScore += 1;
            });
        }

        private void BleuDecrement2_Click(object sender, RoutedEventArgs e)
        {
            BleuDecrement2Clicked?.Invoke(this, EventArgs.Empty);
            if (scoreboardData.BleuScore > 1)
            {
                scoreboardData.BleuScore -= 2;
            }
            actions.Add(() =>
            {
                // Code pour annuler l'action ici
                // Par exemple, supprimez l'élément ajouté à la liste.
                scoreboardData.BleuScore += 2;
            });
        }

        private void BleuDecrement3_Click(object sender, RoutedEventArgs e)
        {
            BleuDecrement3Clicked?.Invoke(this, EventArgs.Empty);
            if (scoreboardData.BleuScore > 2)
            {
                scoreboardData.BleuScore -= 3;
            }
            actions.Add(() =>
            {
                // Code pour annuler l'action ici
                // Par exemple, supprimez l'élément ajouté à la liste.
                scoreboardData.BleuScore += 3;
            });
        }

        private void BleuDecrement4_Click(object sender, RoutedEventArgs e)
        {
            BleuDecrement4Clicked?.Invoke(this, EventArgs.Empty);
            if (scoreboardData.BleuScore > 3)
            {
                scoreboardData.BleuScore -= 4;
            }
            actions.Add(() =>
            {
                // Code pour annuler l'action ici
                // Par exemple, supprimez l'élément ajouté à la liste.
                scoreboardData.BleuScore += 4;
            });
        }

        private void Save_Match_Click(object sender, RoutedEventArgs e)
        {

        }

        /*private void Tb_Minutes_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox tb_Minutes_2 = ScoreboardPublic.Instance.txtboxmin();
            tb_Minutes_2.Text = tb_Minutes.Text;
        }

        private void Tb_Secondes_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox tb_Secondes_2 = ScoreboardPublic.Instance.txtboxsec();
            tb_Secondes_2.Text = tb_Secondes.Text;
        }*/

        private void BleuMoinsAvantage_Click(object sender, RoutedEventArgs e)
        {
            BleuMoinsAvantageClicked?.Invoke(this, EventArgs.Empty);
            if (scoreboardData.BleuAvantage > 0)
            {
                scoreboardData.BleuAvantage -= 1;
            }
            actions.Add(() =>
            {
                // Code pour annuler l'action ici
                // Par exemple, supprimez l'élément ajouté à la liste.
                scoreboardData.BleuAvantage += 1;
            });
        }

        private void BleuMoinsPenalite_Click(object sender, RoutedEventArgs e)
        {
            BleuMoinsPenaliteClicked?.Invoke(this, EventArgs.Empty);
            if (scoreboardData.BleuPenalite > 0)
            {
                scoreboardData.BleuPenalite -= 1;
            }
            actions.Add(() =>
            {
                // Code pour annuler l'action ici
                // Par exemple, supprimez l'élément ajouté à la liste.
                scoreboardData.BleuPenalite += 1;
                if (scoreboardData.BleuPenalite == 2)
                {
                    scoreboardData.RougeAvantage += 1;
                }

                if (scoreboardData.BleuPenalite == 3)
                {
                    scoreboardData.RougeScore += 2;
                }
            });
        }

        private void BleuAvantage_Click(object sender, RoutedEventArgs e)
        {
            BleuAvantageClicked?.Invoke(this, EventArgs.Empty);
            scoreboardData.BleuAvantage += 1;
            actions.Add(() =>
            {
                // Code pour annuler l'action ici
                // Par exemple, supprimez l'élément ajouté à la liste.

                if (scoreboardData.BleuAvantage > 0)
                {
                    scoreboardData.BleuAvantage -= 1;
                }
            });
        }


        private void BleuPenalite_Click(object sender, RoutedEventArgs e)
        {
            BleuPenaliteClicked?.Invoke(this, EventArgs.Empty);
            scoreboardData.BleuPenalite += 1;
            if (scoreboardData.BleuPenalite == 2)
            {
                scoreboardData.RougeAvantage += 1;
            }

            if (scoreboardData.BleuPenalite == 3)
            {
                scoreboardData.RougeScore += 2;
            }

            actions.Add(() =>
            {
                // Code pour annuler l'action ici
                // Par exemple, supprimez l'élément ajouté à la liste.

                if (scoreboardData.BleuPenalite > 0)
                {
                    if (scoreboardData.BleuPenalite == 2)
                    {
                        scoreboardData.RougeAvantage -= 1;
                    }

                    if (scoreboardData.BleuPenalite == 3)
                    {
                        scoreboardData.RougeScore -= 2;
                    }
                    scoreboardData.BleuPenalite -= 1;

                }
            });
        }




        private void Button_Click_RetourForm(object sender, RoutedEventArgs e)
        {
            RetourFormClicked?.Invoke(this, EventArgs.Empty);


            DashboardCombattant dashboardCombattant = new DashboardCombattant(this.Id);
            Application.Current.MainWindow = dashboardCombattant;
            dashboardCombattant.Show();
            this.Close();
        }

        public void SetBorderColor(Color color)
        {
            bordercl1.Background = new SolidColorBrush(color);
        }



        public Grid getContent1()
        {
            return btnContent1;
        }

        public Grid getContent2()
        {
            return btnContent2;
        }
        public StackPanel GetBouton()
        {
            return getbouton;
        }

        public StackPanel GetBouton2()
        {
            return getbouton2;

        }



        public RougeFinCombat GetRouge()
        {
            return rougeFinCombat;
        }

        public BleuFinCombat GetBleu()
        {
            return bleuFinCombat;
        }




    }
}




