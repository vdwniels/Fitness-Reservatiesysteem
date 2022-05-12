using System;
using System.Collections.Generic;
using System.Configuration;
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
using BLKlant.Domein;
using BLKlant.Managers;
using DLKlant;

namespace KlantUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private KlantManager klantManager;
        public MainWindow()
        {
            InitializeComponent();
            klantManager = new KlantManager(
                new KlantRepoADO(ConfigurationManager.ConnectionStrings["fitnessDBconnection"].ToString()));
        }

        private void OK_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int? klantnummer = null;
                string? email = null;
                if (!string.IsNullOrWhiteSpace(KlantnummerTextBox.Text)) klantnummer = int.Parse(KlantnummerTextBox.Text);
                if (!string.IsNullOrWhiteSpace(EmailTextBox.Text)) email = EmailTextBox.Text;

                Klant k = klantManager.SelecteerKlant(klantnummer, email);
                MessageBox.Show($"Welkom terug,\n{k.Voornaam} {k.Achternaam}!");
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "incorrecte login");
            }
        }
    }
}
