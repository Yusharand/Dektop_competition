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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp1.Models;

namespace WpfApp1.Views
{
    /// <summary>
    /// Logique d'interaction pour RougeFinCombat.xaml
    /// </summary>
    public partial class RougeFinCombat : UserControl
    {
        public UserControl UC { get; set; }


        public static RougeFinCombat instance = null;

        public event EventHandler VPointClicked;
        public event EventHandler VSoumissionClicked;
        public event EventHandler VDecisionClicked;
        public event EventHandler VDisqualificationClicked;



        public RougeFinCombat()
        {
            InitializeComponent();
            instance = null;
            if (instance == null)
            {
                instance = this;
            }


        }



        private void VPoint_Click(object sender, RoutedEventArgs e)
        {

            VPointClicked?.Invoke(this, EventArgs.Empty);

            VPoint vpointrouge = new VPoint();
            VPointPublic vpointrouge_1 = new VPointPublic();
            this.Visibility = Visibility.Collapsed;
            Grid btnContent1 = MainWindow.Instance.getContent1();
            Grid btnContent3 = ScoreboardPublic.Instance.getContent1();
            Grid grContentAllBleu = ScoreboardPublic.Instance.UcbgetContent();
            Grid grContentAllRouge = ScoreboardPublic.Instance.UcrgetContent();
            grContentAllRouge.Visibility = Visibility.Visible;
            grContentAllBleu.Visibility = Visibility.Collapsed;
            btnContent3.Children.Add(vpointrouge_1);
            btnContent1.Children.Add(vpointrouge);
            btnContent3.Visibility = Visibility.Visible;

            Grid btnContent2 = MainWindow.Instance.getContent2();

            btnContent2.Visibility = Visibility.Collapsed;
            btnContent1.Visibility = Visibility.Visible;

        }

        private void VSoumission_Click(object sender, RoutedEventArgs e)
        {
            VSoumissionClicked?.Invoke(this, EventArgs.Empty);
            VSoumission vsoumissionrouge = new VSoumission();
            VSoumissionPublic vsoumissionrouge_1 = new VSoumissionPublic();
            this.Visibility = Visibility.Collapsed;
            Grid btnContent1 = MainWindow.Instance.getContent1();
            Grid btnContent3 = ScoreboardPublic.Instance.getContent1();
            Grid grContentAllBleu = ScoreboardPublic.Instance.UcbgetContent();
            Grid grContentAllRouge = ScoreboardPublic.Instance.UcrgetContent();
            grContentAllRouge.Visibility = Visibility.Visible;
            grContentAllBleu.Visibility = Visibility.Collapsed;
            btnContent3.Children.Add(vsoumissionrouge_1);
            btnContent3.Visibility = Visibility.Visible;
            Grid btnContent2 = MainWindow.Instance.getContent2();
            btnContent2.Visibility = Visibility.Collapsed;
            btnContent1.Children.Add(vsoumissionrouge);
            btnContent1.Visibility = Visibility.Visible;

        }

        private void VDisqualification_Click(object sender, RoutedEventArgs e)
        {
            VDisqualificationClicked?.Invoke(this, EventArgs.Empty);
            VDisqualification vdisqualificationrouge = new VDisqualification();
            VDisqualificationPublic vdisqualificationrouge_1 = new VDisqualificationPublic();
            this.Visibility = Visibility.Collapsed;
            Grid btnContent1 = MainWindow.Instance.getContent1();
            Grid btnContent3 = ScoreboardPublic.Instance.getContent1();
            Grid grContentAllBleu = ScoreboardPublic.Instance.UcbgetContent();
            Grid grContentAllRouge = ScoreboardPublic.Instance.UcrgetContent();
            grContentAllRouge.Visibility = Visibility.Visible;
            grContentAllBleu.Visibility = Visibility.Collapsed;
            btnContent3.Children.Add(vdisqualificationrouge_1);

            btnContent3.Visibility = Visibility.Visible;
            btnContent1.Children.Add(vdisqualificationrouge);
            Grid btnContent2 = MainWindow.Instance.getContent2();
            btnContent2.Visibility = Visibility.Collapsed;
            btnContent1.Visibility = Visibility.Visible;
        }

        private void VDecision_Click(object sender, RoutedEventArgs e)
        {
            VDecisionClicked?.Invoke(this, EventArgs.Empty);
            VDecision vdecisionrouge = new VDecision();
            VDecisionPublic vdecisionrouge_1 = new VDecisionPublic();
            this.Visibility = Visibility.Collapsed;
            Grid btnContent1 = MainWindow.Instance.getContent1();
            Grid btnContent3 = ScoreboardPublic.Instance.getContent1();
            Grid grContentAllBleu = ScoreboardPublic.Instance.UcbgetContent();
            Grid grContentAllRouge = ScoreboardPublic.Instance.UcrgetContent();
            grContentAllRouge.Visibility = Visibility.Visible;
            grContentAllBleu.Visibility = Visibility.Collapsed;
            btnContent3.Children.Add(vdecisionrouge_1);

            btnContent3.Visibility = Visibility.Visible;
            Grid btnContent2 = MainWindow.Instance.getContent2();
            btnContent2.Visibility = Visibility.Collapsed;
            btnContent1.Children.Add(vdecisionrouge);
            btnContent1.Visibility = Visibility.Visible;
        }

        public Grid UcrgetContent()
        {
            return UCRContent;
        }
    }
}




