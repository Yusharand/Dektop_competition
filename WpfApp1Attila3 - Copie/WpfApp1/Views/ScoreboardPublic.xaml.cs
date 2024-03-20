using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using WpfApp1.Models;

namespace WpfApp1.Views
{
    /// <summary>
    /// Logique d'interaction pour ScoreboardPublic.xaml
    /// </summary>
    public partial class ScoreboardPublic : Window
    {
        private ScoreboardData scoreboardData;
        DispatcherTimer timer = new DispatcherTimer();
        DispatcherTimer timer1 = new DispatcherTimer();
        DispatcherTimer timer2 = new DispatcherTimer();

        List<Action> actions = new List<Action>();

        private int minutes, secondes, minutesMed1, secondesMed1, minutesMed2, secondesMed2;

        string tmp1, tmp2, tmp3, tmp4, tmp5, tmp6, tmp7, tmp8;

        public static ScoreboardPublic Instance = null;

        public static MainWindow mainWindow;
        public BleuFinCombat bleuFinCombat;
        public RougeFinCombat rougeFinCombat;
        public UserControl UC { get; set; }

        bool valide;
        bool play = true;
        bool play1 = true;
        bool play2 = true;
        bool active = false;
        private string imagefndPath;
        public int Id;
        public int Id_combat;

        public ScoreboardPublic(int id, int id_combat)
        {
            InitializeComponent();
            this.Id = id;
            this.Id_combat = id_combat;

            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
            if (mainWindow != null)
            {
                mainWindow.StartMed2Clicked += StartMed2;
                mainWindow.StartMed1Clicked += StartMed1;
                mainWindow.BoutonFinDeMatchClicked += Button_Click_FinMatch;
                mainWindow.SwitchClicked += Switch;
                mainWindow.RougeResetMedClicked += RougeResetMed;
                mainWindow.BleuResetMedClicked += BleuResetMed;
                mainWindow.Plus1secondesClicked += Plus1secondes;
                mainWindow.Moins10secondesClicked += Moins10secondes;
                mainWindow.Plus10secondesClicked += Plus10secondes;
                mainWindow.Moins1secondesClicked += Moins1secondes;
                mainWindow.StartClicked += Start;
                mainWindow.RougeIncrement1Clicked += RougeIncrement1;
                mainWindow.RougeIncrement2Clicked += RougeIncrement2;
                mainWindow.RougeIncrement3Clicked += RougeIncrement3;
                mainWindow.RougeIncrement4Clicked += RougeIncrement4;
                mainWindow.RougeAvantageClicked += RougeAvantage;
                mainWindow.RougePenaliteClicked += RougePenalite;
                mainWindow.BleuIncrement1Clicked += BleuIncrement1;
                mainWindow.BleuIncrement2Clicked += BleuIncrement2;
                mainWindow.BleuIncrement3Clicked += BleuIncrement3;
                mainWindow.BleuIncrement4Clicked += BleuIncrement4;
                mainWindow.BleuAvantageClicked += BleuAvantage;
                mainWindow.BleuPenaliteClicked += BleuPenalite;
                mainWindow.RougeDecrement1Clicked += RougeDecrement1;
                mainWindow.RougeDecrement2Clicked += RougeDecrement2;
                mainWindow.RougeDecrement3Clicked += RougeDecrement3;
                mainWindow.RougeDecrement4Clicked += RougeDecrement4;
                mainWindow.RougeMoinsAvantageClicked += RougeMoinsAvantage;
                mainWindow.RougeMoinsPenaliteClicked += RougeMoinsPenalite;
                mainWindow.BleuDecrement1Clicked += BleuDecrement1;
                mainWindow.BleuDecrement2Clicked += BleuDecrement2;
                mainWindow.BleuDecrement3Clicked += BleuDecrement3;
                mainWindow.BleuDecrement4Clicked += BleuDecrement4;
                mainWindow.BleuMoinsAvantageClicked += BleuMoinsAvantage;
                mainWindow.BleuMoinsPenaliteClicked += BleuMoinsPenalite;
                mainWindow.RetourFormClicked += RetourForm;
                mainWindow.UndoClicked += Undo;



                /*      bleuFinCombat.VPointClicked += VPoint_;
                      bleuFinCombat.VSoumissionClicked += VSoumission_;
                      bleuFinCombat.VDecisionClicked += VDecision_;
                      bleuFinCombat.VDisqualificationClicked += VDisqualification_;

                      rougeFinCombat.VPointClicked += VPoint__;
                      rougeFinCombat.VSoumissionClicked += VSoumission__;
                      rougeFinCombat.VDecisionClicked += VDecision__;
                      rougeFinCombat.VDisqualificationClicked += VDisqualification__;
                */

            }



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
        }

        private void Undo(object sender, EventArgs e)
        {
            // Annuler la dernière action des boutons

            if (actions.Count > 0)
            {
                int lastIndex = actions.Count - 1;
                actions[lastIndex](); // Exécute l'action d'annulation
                actions.RemoveAt(lastIndex); // Supprime l'action de la liste
            }
        }

        public void Load_Data(string nom1, string nom2, string prenom1, string prenom2, string club1, string club2, string logoclub1, string logoclub2, string tour, string categorie, string fondscoreboard, string couleur1, string couleur2, string minute)
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

            bordercl1.Background = (Brush)new System.Windows.Media.BrushConverter().ConvertFromString(couleur1);
            bordercl2.Background = (Brush)new System.Windows.Media.BrushConverter().ConvertFromString(couleur2);

            tb_Minutes_2.Text = minute;
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
            textblockPays1.Text = textPath3;

            BitmapImage img4 = new BitmapImage(new Uri(imagePath4, UriKind.RelativeOrAbsolute));
            imagepays2.Source = img4;
            textblockPays2.Text = textPath4;

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

            tb_Minutes_2.Text = MinuteC;
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

        public void StartMed2(object sender, EventArgs e)
        {
            timer.Stop();
            tb_Minutes_2.Foreground = Brushes.Yellow;
            tb_Secondes_2.Foreground = Brushes.Yellow;
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

        private void StartMed1(object sender, EventArgs e)
        {
            timer.Stop();
            tb_Minutes_2.Foreground = Brushes.Yellow;
            tb_Secondes_2.Foreground = Brushes.Yellow;
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

        private void Button_Click_FinMatch(object sender, EventArgs e)
        {
            timer.Stop();
            tb_Minutes_2.Foreground = Brushes.Yellow;
            tb_Secondes_2.Foreground = Brushes.Yellow;
            tb_separator.Foreground = Brushes.Yellow;

            tb_Minutes_2.Visibility = Visibility.Visible;
            tb_Secondes_2.Visibility = Visibility.Visible;
            tb_separator.Visibility = Visibility.Visible;

            /*BleuFinCombat ChoixBleu = new BleuFinCombat();
            //getbouton2.Visibility = Visibility.Collapsed;
            btnContent2.Children.Add(ChoixBleu);
            btnContent2.Visibility = Visibility.Visible;
            bleuFinCombat = ChoixBleu;

            RougeFinCombat ChoixRouge = new RougeFinCombat();
            //getbouton.Visibility = Visibility.Collapsed;
            btnContent1.Children.Add(ChoixRouge);
            btnContent1.Visibility = Visibility.Visible;
            rougeFinCombat = ChoixRouge;*/


        }

        private void Switch(object sender, EventArgs e)
        {
            tmp1 = textblockPrenom2.Text;
            textblockPrenom2.Text = textblockPrenom1.Text;
            textblockPrenom1.Text = tmp1;

            tmp2 = textblockNom2.Text;
            textblockNom2.Text = textblockNom1.Text;
            textblockNom1.Text = tmp2;

            tmp3 = textblockClub2.Text;
            textblockClub2.Text = textblockClub1.Text;
            textblockClub1.Text = tmp3;

            tmp4 = textblockPays1.Text;
            textblockPays1.Text = textblockPays2.Text;
            textblockPays2.Text = tmp4;

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

        private void RougeResetMed(object sender, EventArgs e)
        {
            tb_MinutesMed1.Text = "02";
            tb_SecondesMed1.Text = "00";

            tb_MinutesMed1.Foreground = Brushes.Black;
            tb_SecondesMed1.Foreground = Brushes.Black;
            tb_separatorMed1.Foreground = Brushes.Black;
        }

        private void BleuResetMed(object sender, EventArgs e)
        {
            tb_MinutesMed2.Text = "02";
            tb_SecondesMed2.Text = "00";

            tb_MinutesMed2.Foreground = Brushes.Black;
            tb_SecondesMed2.Foreground = Brushes.Black;
            tb_separatorMed2.Foreground = Brushes.Black;
        }


        private void timer_Tick(object sender, EventArgs e)
        {
            minutes = Convert.ToInt32(tb_Minutes_2.Text);
            secondes = Convert.ToInt32(tb_Secondes_2.Text);
            secondes--;
            if (secondes < 0)
            {
                if (minutes == 0)
                {
                    timer.Stop();
                    tb_Minutes_2.Text = "00";
                    tb_Secondes_2.Text = "00";
                }
                else
                {
                    minutes--;
                    tb_Minutes_2.Text = minutes.ToString();
                    secondes = 59;
                    tb_Secondes_2.Text = secondes.ToString();
                }
            }


            if (minutes == 0 && secondes <= 10)
            {

                tb_Minutes_2.Foreground = Brushes.Red;
                tb_Secondes_2.Foreground = Brushes.Red;
                tb_separator.Foreground = Brushes.Red;




                /*tb_Minutes.Visibility = tb_Minutes.Visibility == Visibility.Visible ? Visibility.Hidden : Visibility.Visible;
                tb_Secondes.Visibility = tb_Secondes.Visibility == Visibility.Visible ? Visibility.Hidden : Visibility.Visible;
                tb_separator.Visibility = tb_separator.Visibility == Visibility.Visible ? Visibility.Hidden : Visibility.Visible;*/
            }

            if (minutes == 0 && secondes == 0)
            {
                timer.Stop();
                tb_Minutes_2.Visibility = Visibility.Visible;
                tb_Secondes_2.Visibility = Visibility.Visible;
                tb_separator.Visibility = Visibility.Visible;

                /*SoundPlayer player = new SoundPlayer(@"/Images/23070.wav");
                player.Load();
                player.Play();*/
            }


            if (minutes < 10)
            {
                tb_Minutes_2.Text = "0" + minutes.ToString();
            }
            else
            {
                tb_Minutes_2.Text = minutes.ToString();
            }
            if (secondes < 10 && secondes >= 0)
            {
                tb_Secondes_2.Text = "0" + secondes.ToString();
            }

            else
            {
                tb_Secondes_2.Text = secondes.ToString();
            }
        }

        private void Start(object sender, EventArgs e)
        {
            timer1.Stop();


            timer2.Stop();


            active = true;




            if (play)
            {
                timer.Start();
                tb_Minutes_2.Foreground = Brushes.White;
                tb_Secondes_2.Foreground = Brushes.White;
                tb_separator.Foreground = Brushes.White;

                tb_Minutes_2.Visibility = Visibility.Visible;
                tb_Secondes_2.Visibility = Visibility.Visible;
                tb_separator.Visibility = Visibility.Visible;


                play = false;
            }
            else
            {
                timer.Stop();
                tb_Minutes_2.Foreground = Brushes.Yellow;
                tb_Secondes_2.Foreground = Brushes.Yellow;
                tb_separator.Foreground = Brushes.Yellow;

                tb_Minutes_2.Visibility = Visibility.Visible;
                tb_Secondes_2.Visibility = Visibility.Visible;
                tb_separator.Visibility = Visibility.Visible;

                play = true;
            }

        }

        private void Plus1secondes(object sender, EventArgs e)
        {
            minutes = Convert.ToInt32(this.tb_Minutes_2.Text);
            secondes = Convert.ToInt32(this.tb_Secondes_2.Text);

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
                tb_Minutes_2.Text = "0" + minutes.ToString();
            }
            else
            {
                tb_Minutes_2.Text = minutes.ToString();
            }
            if (secondes < 10 && secondes >= 0)
            {
                tb_Secondes_2.Text = "0" + secondes.ToString();
            }

            else
            {
                tb_Secondes_2.Text = secondes.ToString();
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
                    tb_Minutes_2.Text = "0" + minutes.ToString();
                }
                else
                {
                    tb_Minutes_2.Text = minutes.ToString();
                }
                if (secondes < 10 && secondes >= 0)
                {
                    tb_Secondes_2.Text = "0" + secondes.ToString();
                }

                else
                {
                    tb_Secondes_2.Text = secondes.ToString();
                }

            });
        }

        private void Moins10secondes(object sender, EventArgs e)
        {
            minutes = Convert.ToInt32(this.tb_Minutes_2.Text);
            secondes = Convert.ToInt32(this.tb_Secondes_2.Text);

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
                tb_Minutes_2.Text = "0" + minutes.ToString();
            }
            else
            {
                tb_Minutes_2.Text = minutes.ToString();
            }
            if (secondes < 10 && secondes >= 0)
            {
                tb_Secondes_2.Text = "0" + secondes.ToString();
            }

            else
            {
                tb_Secondes_2.Text = secondes.ToString();
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
                    tb_Minutes_2.Text = "0" + minutes.ToString();
                }
                else
                {
                    tb_Minutes_2.Text = minutes.ToString();
                }
                if (secondes < 10 && secondes >= 0)
                {
                    tb_Secondes_2.Text = "0" + secondes.ToString();
                }

                else
                {
                    tb_Secondes_2.Text = secondes.ToString();
                }

            });
        }



        private void Plus10secondes(object sender, EventArgs e)
        {
            minutes = Convert.ToInt32(this.tb_Minutes_2.Text);
            secondes = Convert.ToInt32(this.tb_Secondes_2.Text);

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
                tb_Minutes_2.Text = "0" + minutes.ToString();
            }
            else
            {
                tb_Minutes_2.Text = minutes.ToString();
            }
            if (secondes < 10 && secondes >= 0)
            {
                tb_Secondes_2.Text = "0" + secondes.ToString();
            }

            else
            {
                tb_Secondes_2.Text = secondes.ToString();
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
                    tb_Minutes_2.Text = "0" + minutes.ToString();
                }
                else
                {
                    tb_Minutes_2.Text = minutes.ToString();
                }
                if (secondes < 10 && secondes >= 0)
                {
                    tb_Secondes_2.Text = "0" + secondes.ToString();
                }

                else
                {
                    tb_Secondes_2.Text = secondes.ToString();
                }
            });
        }

        private void Moins1secondes(object sender, EventArgs e)
        {

            minutes = Convert.ToInt32(this.tb_Minutes_2.Text);
            secondes = Convert.ToInt32(this.tb_Secondes_2.Text);

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
                tb_Minutes_2.Text = "0" + minutes.ToString();
            }
            else
            {
                tb_Minutes_2.Text = minutes.ToString();
            }
            if (secondes < 10 && secondes >= 0)
            {
                tb_Secondes_2.Text = "0" + secondes.ToString();
            }

            else
            {
                tb_Secondes_2.Text = secondes.ToString();
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
                    tb_Minutes_2.Text = "0" + minutes.ToString();
                }
                else
                {
                    tb_Minutes_2.Text = minutes.ToString();
                }
                if (secondes < 10 && secondes >= 0)
                {
                    tb_Secondes_2.Text = "0" + secondes.ToString();
                }

                else
                {
                    tb_Secondes_2.Text = secondes.ToString();
                }

            });
        }

        private void RougeIncrement1(object sender, EventArgs e)
        {

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


        private void RougeIncrement2(object sender, EventArgs e)
        {

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




        private void RougeIncrement3(object sender, EventArgs e)
        {

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


        private void RougeIncrement4(object sender, EventArgs e)
        {

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


        private void RougeAvantage(object sender, EventArgs e)
        {

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



        private void RougePenalite(object sender, EventArgs e)
        {


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




        private void RougeDecrement1(object sender, EventArgs e)
        {
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

        private void RougeDecrement2(object sender, EventArgs e)
        {
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

        private void RougeDecrement3(object sender, EventArgs e)
        {
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

        private void RougeDecrement4(object sender, EventArgs e)
        {
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

        private void RougeMoinsAvantage(object sender, EventArgs e)
        {
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

        private void RougeMoinsPenalite(object sender, EventArgs e)
        {
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
        private void BleuIncrement1(object sender, EventArgs e)
        {

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


        private void BleuIncrement2(object sender, EventArgs e)
        {

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


        private void BleuIncrement3(object sender, EventArgs e)
        {

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





        private void BleuIncrement4(object sender, EventArgs e)
        {

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





        private void BleuDecrement1(object sender, EventArgs e)
        {
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

        private void BleuDecrement2(object sender, EventArgs e)
        {
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

        private void BleuDecrement3(object sender, EventArgs e)
        {
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

        private void BleuDecrement4(object sender, EventArgs e)
        {
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

        private void BleuMoinsAvantage(object sender, EventArgs e)
        {
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

        private void BleuMoinsPenalite(object sender, EventArgs e)
        {
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

        private void BleuAvantage(object sender, EventArgs e)
        {

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


        private void BleuPenalite(object sender, EventArgs e)
        {

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


        private void RetourForm(object sender, EventArgs e)
        {
            this.Close();
        }

        public void SetBorderColor(Color color)
        {
            bordercl1.Background = new SolidColorBrush(color);
        }





        public RougeFinCombat GetRouge()
        {
            return rougeFinCombat;
        }

        public BleuFinCombat GetBleu()
        {
            return bleuFinCombat;
        }



        public void VPoint_(object sender, EventArgs e)
        {
            VPoint vpointbleu = new VPoint();
            this.Visibility = Visibility.Collapsed;
            Grid btnContent2 = MainWindow.Instance.getContent2();
            btnContent2.Children.Add(vpointbleu);
            Grid btnContent1 = MainWindow.Instance.getContent1();
            btnContent1.Visibility = Visibility.Collapsed;
            btnContent2.Visibility = Visibility.Visible;
        }

        private void VSoumission_(object sender, EventArgs e)
        {
            VSoumission vsoumissionbleu = new VSoumission();
            this.Visibility = Visibility.Collapsed;
            Grid btnContent2 = MainWindow.Instance.getContent2();
            btnContent2.Children.Add(vsoumissionbleu);
            Grid btnContent1 = MainWindow.Instance.getContent1();
            btnContent1.Visibility = Visibility.Collapsed;
            btnContent2.Visibility = Visibility.Visible;
        }

        private void VDisqualification_(object sender, EventArgs e)
        {
            VDisqualification vdisqualificationbleu = new VDisqualification();
            this.Visibility = Visibility.Collapsed;
            Grid btnContent2 = MainWindow.Instance.getContent2();
            btnContent2.Children.Add(vdisqualificationbleu);
            Grid btnContent1 = MainWindow.Instance.getContent1();
            btnContent1.Visibility = Visibility.Collapsed;
            btnContent2.Visibility = Visibility.Visible;
        }

        private void VDecision_(object sender, EventArgs e)
        {
            VDecision vdecisionbleu = new VDecision();
            this.Visibility = Visibility.Collapsed;
            Grid btnContent2 = MainWindow.Instance.getContent2();
            btnContent2.Children.Add(vdecisionbleu);
            Grid btnContent1 = MainWindow.Instance.getContent1();
            btnContent1.Visibility = Visibility.Collapsed;
            btnContent2.Visibility = Visibility.Visible;
        }

        private void VPoint__(object sender, EventArgs e)
        {

            VPoint vpointrouge = new VPoint();
            this.Visibility = Visibility.Collapsed;
            Grid btnContent1 = MainWindow.Instance.getContent1();
            btnContent1.Children.Add(vpointrouge);
            Grid btnContent2 = MainWindow.Instance.getContent2();
            btnContent2.Visibility = Visibility.Collapsed;
            btnContent1.Visibility = Visibility.Visible;

        }

        private void VSoumission__(object sender, EventArgs e)
        {

            VSoumission vsoumissionrouge = new VSoumission();
            this.Visibility = Visibility.Collapsed;
            Grid btnContent1 = MainWindow.Instance.getContent1();
            Grid btnContent2 = MainWindow.Instance.getContent2();
            btnContent2.Visibility = Visibility.Collapsed;
            btnContent1.Children.Add(vsoumissionrouge);
            btnContent1.Visibility = Visibility.Visible;
        }

        private void VDisqualification__(object sender, EventArgs e)
        {

            VDisqualification vdisqualificationrouge = new VDisqualification();
            this.Visibility = Visibility.Collapsed;
            Grid btnContent1 = MainWindow.Instance.getContent1();
            btnContent1.Children.Add(vdisqualificationrouge);
            Grid btnContent2 = MainWindow.Instance.getContent2();
            btnContent2.Visibility = Visibility.Collapsed;
            btnContent1.Visibility = Visibility.Visible;
        }

        private void VDecision__(object sender, EventArgs e)
        {

            VDecision vdecisionrouge = new VDecision();
            this.Visibility = Visibility.Collapsed;
            Grid btnContent1 = MainWindow.Instance.getContent1();
            Grid btnContent2 = MainWindow.Instance.getContent2();
            btnContent2.Visibility = Visibility.Collapsed;
            btnContent1.Children.Add(vdecisionrouge);
            btnContent1.Visibility = Visibility.Visible;
        }

        public Grid UcbgetContent()
        {
            return grContentAllBleu;
        }
        public Grid UcrgetContent()
        {
            return grContentAllRouge;
        }
        public Grid getContent1()
        {
            return btnContent3;
        }

        public Grid getContent2()
        {
            return btnContent4;
        }

        public TextBox txtboxmin()
        {
            return tb_Minutes_2;
        }

        public TextBox txtboxsec()
        {
            return tb_Secondes_2;
        }
    }
}







