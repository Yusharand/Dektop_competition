using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp1.Models
{

    public class Info
    {
        public string Entete { get; set; }
        public string NombreListe { get; set; }


        public int id;
        public int cpt;


        public Info()
        {
            Entete = "Informations de la compétition";


            /*Gestion affichage anle nb patient
            if ((cpt == 1) || (cpt == 0))
                NombreListe = "0" + cpt.ToString() + " combattant";
            if ((cpt < 10) && (1 < cpt))
                NombreListe = "0" + cpt.ToString() + " combattants";
            if (10 <= cpt)
                NombreListe = cpt.ToString() + " combattants";
            Console.WriteLine(NombreListe);*/

        }


    }

    public class ListeC
    {
        public string Entete { get; set; }
        public string NombreListe { get; set; }


        public int id;
        public int cpt;


        public ListeC()
        {
            Entete = "Liste des combattants";


            /*Gestion affichage anle nb patient
            if ((cpt == 1) || (cpt == 0))
                NombreListe = "0" + cpt.ToString() + " combattant";
            if ((cpt < 10) && (1 < cpt))
                NombreListe = "0" + cpt.ToString() + " combattants";
            if (10 <= cpt)
                NombreListe = cpt.ToString() + " combattants";
            Console.WriteLine(NombreListe);*/

        }


    }

    public class InputBox : Window
    {
        private TextBox txtInput;
        private Button btnOK;

        public string InputText { get { return txtInput.Text; } }

        public InputBox(string prompt, string title)
        {
            this.Title = title;
            this.Width = 300;
            this.Height = 180;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.WindowStyle = WindowStyle.None;


            StackPanel stackPanel = new StackPanel();

            TextBlock textBlock = new TextBlock();
            textBlock.Text = prompt;
            textBlock.Margin = new Thickness(10);

            txtInput = new TextBox();
            txtInput.Margin = new Thickness(10);

            btnOK = new Button();
            btnOK.Content = "OK";
            btnOK.Margin = new Thickness(10);
            btnOK.Click += BtnOK_Click;

            stackPanel.Children.Add(textBlock);
            stackPanel.Children.Add(txtInput);
            stackPanel.Children.Add(btnOK);

            this.Content = stackPanel;
        }

        private void BtnOK_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }
    }

    public class EventMediator
    {
        public event EventHandler VPointClick;

        public void RaiseButtonClickEvent()
        {
            VPointClick?.Invoke(this, EventArgs.Empty);
        }
    }

    public class ListeCat
    {
        public string Entete { get; set; }


        public int id;
        public int cpt;

        public ListeCat()
        {
            Entete = "Liste des catégories";

        }
    }

    public class ListeCombatEnCours
    {
        public string Entete { get; set; }


        public int id;
        public int cpt;

        public ListeCombatEnCours()
        {
            Entete = "Liste des combats en cours";

        }
    }

    public class ListeCombatEnregistres
    {
        public string Entete { get; set; }


        public int id;
        public int cpt;

        public ListeCombatEnregistres()
        {
            Entete = "Liste des combats enregistrés";

        }
    }

    public class UserAction
    {
        public Action ActionToUndo { get; set; } // L'action à annuler
        //public Action ActionToRedo { get; set; } // L'action à rétablir (en cas d'annulation)
    }
    public class Categorie
    {
        public string ID { get; set; }
        public string Nom_Cate { get; set; }
        public List<Combattant> Combattants { get; set; }
    }
    public class Combattant
    {
        public string Numero { get; set; }
        public string Club { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Genre { get; set; }
        public string Date_naiss { get; set; }
        public string Age { get; set; }
        public string Grade { get; set; }
        public string Categorie { get; set; }
        public string Poids { get; set; }
        public bool EstSelectionne { get; set; }

    }

    public class Competition
    {
        public string ID_Compet { get; set; }
        public string Nom_Compet { get; set; }
        public string Lieu_Compet { get; set; }
        public string Date_Compet { get; set; }
        public string FondSB { get; set; }
        public string Logo_Compet { get; set; }
        public string LieuPese { get; set; }
        public string DatePese { get; set; }
        public string ConditionPese { get; set; }
        public string Droit { get; set; }
        public string Remboursement { get; set; }

    }


    public class AgeRange
    {
        public int Min { get; set; }
        public int Max { get; set; }

        public AgeRange(int min, int max)
        {
            Min = min;
            Max = max;
        }

        public override string ToString()
        {
            return $"{Min}-{Max} ans";
        }
    }

    public class WeightRange
    {
        public double Min { get; set; }
        public double Max { get; set; }

        public WeightRange(double min, double max)
        {
            Min = min;
            Max = max;
        }

        public override string ToString()
        {
            return $"{Min}-{Max} kg";
        }
    }


}