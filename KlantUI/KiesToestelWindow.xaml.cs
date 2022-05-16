using BLKlant.Domein;
using BLKlant.Managers;
using DLKlant;
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
using System.Windows.Shapes;

namespace KlantUI
{
    /// <summary>
    /// Interaction logic for KiesToestelWindow.xaml
    /// </summary>
    public partial class KiesToestelWindow : Window
    {
        private Klant klant;
        private Reservatie reservatie;
        private string slot;
        private ToestelManager tm;
        public KiesToestelWindow(Klant k, Reservatie r, string s)
        {
            InitializeComponent();
            tm = new ToestelManager(
new ToestelRepoADO(ConfigurationManager.ConnectionStrings["fitnessDBconnection"].ToString()),
new ReservatieRepoADO(ConfigurationManager.ConnectionStrings["fitnessDBconnection"].ToString()));


            klant = k;
            reservatie = r;
            slot = s;
            GebruikerLabel.Content = $"Gebruiker : {klant.Voornaam} {klant.Achternaam}";
            ReservatieLabel.Content = $"Reservatie : {reservatie.ToString()}, Slot : {slot}";
            ToestellenListBox.ItemsSource = tm.BeschikbareToestellen(reservatie.Datum, slot);
        }

        private void VoegToeButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TerugButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
