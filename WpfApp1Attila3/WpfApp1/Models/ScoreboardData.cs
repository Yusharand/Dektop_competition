using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;


namespace WpfApp1.Models
{
    class ScoreboardData : INotifyPropertyChanged
    {
        public int rougescore;
        public int bleuscore;
        public int rougeavantage;
        public int bleuavantage;
        public int rougepenalite;
        public int bleupenalite;

        public int RougeScore
        {
            get { return rougescore; }
            set { rougescore = value; OnPropertyChanged("RougeScore"); }
        }

        public int BleuScore
        {
            get { return bleuscore; }
            set { bleuscore = value; OnPropertyChanged("BleuScore"); }
        }

        public int RougeAvantage
        {
            get { return rougeavantage; }
            set { rougeavantage = value; OnPropertyChanged("RougeAvantage"); }
        }

        public int BleuAvantage
        {
            get { return bleuavantage; }
            set { bleuavantage = value; OnPropertyChanged("BleuAvantage"); }
        }

        public int RougePenalite
        {
            get { return rougepenalite; }
            set { rougepenalite = value; OnPropertyChanged("RougePenalite"); }
        }

        public int BleuPenalite
        {
            get { return bleupenalite; }
            set { bleupenalite = value; OnPropertyChanged("BleuPenalite"); }
        }

        public ScoreboardData()
        {
            RougeScore = 0;
            BleuScore = 0;
            RougeAvantage = 0;
            BleuAvantage = 0;
            RougePenalite = 0;
            BleuPenalite = 0;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

    }
}
