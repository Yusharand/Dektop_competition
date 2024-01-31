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

namespace WpfApp1.Views
{
    /// <summary>
    /// Logique d'interaction pour BleuFinCombat.xaml
    /// </summary>
    public partial class BleuFinCombat : UserControl
    {
        public UserControl UC { get; set; }
        public static BleuFinCombat instance;

        public event EventHandler VPointClicked;
        public event EventHandler VSoumissionClicked;
        public event EventHandler VDecisionClicked;
        public event EventHandler VDisqualificationClicked;

        public BleuFinCombat()
        {
            instance = null;
            InitializeComponent();
            if (instance == null)
            {
                instance = this;
            }

        }

        private void VPoint_Click_1(object sender, RoutedEventArgs e)
        {
            VPointClicked?.Invoke(this, EventArgs.Empty);
            VPoint vpointbleu = new VPoint();
            this.Visibility = Visibility.Collapsed;
            Grid btnContent2 = MainWindow.Instance.getContent2();
            VPointPublic vpointbleu_1 = new VPointPublic();
            Grid btnContent4 = ScoreboardPublic.Instance.getContent2();
            Grid grContentAllRouge = ScoreboardPublic.Instance.UcrgetContent();
            Grid grContentAllBleu = ScoreboardPublic.Instance.UcbgetContent();
            grContentAllBleu.Visibility = Visibility.Visible;
            grContentAllRouge.Visibility = Visibility.Collapsed;
            btnContent4.Children.Add(vpointbleu_1);
            btnContent4.Visibility = Visibility.Visible;
            btnContent2.Children.Add(vpointbleu);
            Grid btnContent1 = MainWindow.Instance.getContent1();
            btnContent1.Visibility = Visibility.Collapsed;
            btnContent2.Visibility = Visibility.Visible;



        }

        private void VSoumission_Click_1(object sender, RoutedEventArgs e)
        {
            VSoumissionClicked?.Invoke(this, EventArgs.Empty);
            VSoumission vsoumissionbleu = new VSoumission();
            this.Visibility = Visibility.Collapsed;
            Grid btnContent2 = MainWindow.Instance.getContent2();
            VSoumissionPublic vsoumissionrouge_1 = new VSoumissionPublic();
            Grid btnContent4 = ScoreboardPublic.Instance.getContent2();
            Grid grContentAllRouge = ScoreboardPublic.Instance.UcrgetContent();
            Grid grContentAllBleu = ScoreboardPublic.Instance.UcbgetContent();
            grContentAllBleu.Visibility = Visibility.Visible;
            grContentAllRouge.Visibility = Visibility.Collapsed;
            btnContent4.Children.Add(vsoumissionrouge_1);
            btnContent4.Visibility = Visibility.Visible;
            btnContent2.Children.Add(vsoumissionbleu);
            Grid btnContent1 = MainWindow.Instance.getContent1();
            btnContent1.Visibility = Visibility.Collapsed;
            btnContent2.Visibility = Visibility.Visible;
        }

        private void VDisqualification_Click_1(object sender, RoutedEventArgs e)
        {
            VDisqualificationClicked?.Invoke(this, EventArgs.Empty);
            VDisqualification vdisqualificationbleu = new VDisqualification();
            this.Visibility = Visibility.Collapsed;
            Grid btnContent2 = MainWindow.Instance.getContent2();
            VDisqualificationPublic vdisqualificationbleu_1 = new VDisqualificationPublic();
            Grid btnContent4 = ScoreboardPublic.Instance.getContent2();
            Grid grContentAllRouge = ScoreboardPublic.Instance.UcrgetContent();
            Grid grContentAllBleu = ScoreboardPublic.Instance.UcbgetContent();
            grContentAllBleu.Visibility = Visibility.Visible;
            grContentAllRouge.Visibility = Visibility.Collapsed;
            btnContent4.Children.Add(vdisqualificationbleu_1);
            btnContent4.Visibility = Visibility.Visible;
            btnContent2.Children.Add(vdisqualificationbleu);
            Grid btnContent1 = MainWindow.Instance.getContent1();
            btnContent1.Visibility = Visibility.Collapsed;
            btnContent2.Visibility = Visibility.Visible;
        }

        private void VDecision_Click_1(object sender, RoutedEventArgs e)
        {
            VDecisionClicked?.Invoke(this, EventArgs.Empty);
            VDecision vdecisionbleu = new VDecision();
            this.Visibility = Visibility.Collapsed;
            Grid btnContent2 = MainWindow.Instance.getContent2();
            VDecisionPublic vdecisionbleu_1 = new VDecisionPublic();
            Grid btnContent4 = ScoreboardPublic.Instance.getContent2();
            Grid grContentAllRouge = ScoreboardPublic.Instance.UcrgetContent();
            Grid grContentAllBleu = ScoreboardPublic.Instance.UcbgetContent();
            grContentAllBleu.Visibility = Visibility.Visible;
            grContentAllRouge.Visibility = Visibility.Collapsed;
            btnContent4.Children.Add(vdecisionbleu_1);
            btnContent4.Visibility = Visibility.Visible;
            btnContent2.Children.Add(vdecisionbleu);
            Grid btnContent1 = MainWindow.Instance.getContent1();
            btnContent1.Visibility = Visibility.Collapsed;
            btnContent2.Visibility = Visibility.Visible;
        }

        public Grid UcbgetContent()
        {
            return UCBContent;
        }

    }
}








