using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Threading;

namespace WpfApp1.Views
{
    /// <summary>
    /// Logique d'interaction pour Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        private DateTime lastActivationDate;
        private string activationKey;
        private string activationKeyFilePath = @"E:\fichier.txt"; // Chemin du fichier de clé d'activation.
        private string encryptekey;

        // Remplacez par votre clé secrète.

        public Login()
        {
            InitializeComponent();
            // Chargez la date de la dernière génération et la clé d'activation depuis le fichier s'ils existent.
            if (File.Exists(activationKeyFilePath))
            {
                string[] lines = File.ReadAllLines(activationKeyFilePath);
                if (lines.Length == 2)
                {
                    if (DateTime.TryParse(lines[0], out lastActivationDate))
                    {
                        activationKey = lines[1];
                        char[] chars = activationKey.ToCharArray();
                        for (int i = 0; i < chars.Length; i++)
                        {
                            if (char.IsLetter(chars[i]))
                            {
                                char baseChar = char.IsUpper(chars[i]) ? 'A' : 'a';
                                chars[i] = (char)(((chars[i] - baseChar + 2) % 26) + baseChar);
                            }
                        }
                        encryptekey = new string(chars);
                    }
                }
            }

            // Si les données n'existent pas, initialisez-les comme suit :
            if (string.IsNullOrEmpty(activationKey))
            {
                GenerateActivationKey();
            }

            if ((DateTime.Now - lastActivationDate).TotalDays >= 3) //
            {
                GenerateActivationKey();
            }


        }



        private void LocalLoginButton_Click(object sender, RoutedEventArgs e)
        {
            if (LocalPasswordBox.Password == encryptekey)
            {
                MessageBox.Show("La clé est valide");
                ChoixCompet choixCompet = new ChoixCompet();
                choixCompet.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("La clé n'est pas valide");
            }
        }


        private void GenerateActivationKey()
        {
            // Générez une nouvelle clé d'activation aléatoire.
            Random random = new Random();
            const string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789"; // Les caractères possibles pour la clé.
            char[] key = new char[8]; // La longueur de la clé.
            for (int i = 0; i < 8; i++)
            {
                key[i] = characters[random.Next(characters.Length)];
            }

            activationKey = new string(key);
            char[] chars = activationKey.ToCharArray();
            for (int i = 0; i < chars.Length; i++)
            {
                if (char.IsLetter(chars[i]))
                {
                    char baseChar = char.IsUpper(chars[i]) ? 'A' : 'a';
                    chars[i] = (char)(((chars[i] - baseChar + 2) % 26) + baseChar);
                }
            }
            encryptekey = new string(chars);

            lastActivationDate = DateTime.Now;
            // Sauvegardez la nouvelle clé et la date de génération dans le fichier.
            string[] lines = { lastActivationDate.ToString(), activationKey };
            File.WriteAllLines(activationKeyFilePath, lines);
        }


        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                LocalLoginButton_Click(sender, e);
            }
        }
    }
}





