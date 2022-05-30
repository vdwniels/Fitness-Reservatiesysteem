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
    /// Interaction logic for MaakReservatieWindow.xaml
    /// </summary>
    public partial class MaakReservatieWindow : Window
    {
        private ReservatieManager rm;
        public Reservatie reservatie;
        private List<DateTime> BeschikbareDagen = new List<DateTime>();
        private bool DatumIngevuld = false;
        private Klant klant;
        public MaakReservatieWindow(Klant k)
        {
            InitializeComponent();
            rm = new ReservatieManager(
new ReservatieRepoADO(ConfigurationManager.ConnectionStrings["fitnessDBconnection"].ToString()),
new GereserveerdeSlotenRepoADO(ConfigurationManager.ConnectionStrings["fitnessDBconnection"].ToString()));

            klant = k;
            GebruikerLabel.Content = $"Gebruiker : {klant.Voornaam} {klant.Achternaam}";

            for(int i = 1; i < 8; i++)
            {
                
                BeschikbareDagen.Add(DateTime.Today.AddDays(i));
            }
            
            DatumComboBox.ItemsSource = BeschikbareDagen;
        }


        private void TerugButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }


        private void DatumComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DatumIngevuld = true;
            if (DatumIngevuld == true)
            {
                SlotenButton.IsEnabled = true;
            }
        }


        private void SlotenButton_Click(object sender, RoutedEventArgs e)
        {
            DateTime d = (DateTime)DatumComboBox.SelectedItem;
            reservatie = rm.MaakReservatie(klant.KlantNummer, klant.Email, klant.Voornaam, klant.Achternaam, d);
            ReservatieAanvullenWindow RA_Window = new ReservatieAanvullenWindow(klant, reservatie);
            RA_Window.ShowDialog();
            if (rm.reservatieBestaatAl == false)
            {
                DialogResult = true;
            }
            this.Close();

        }
    }
}
