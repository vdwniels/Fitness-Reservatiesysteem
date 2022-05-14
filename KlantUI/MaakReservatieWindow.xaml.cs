using BLKlant.Domein;
using System;
using System.Collections.Generic;
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
        private List<DateTime> BeschikbareDagen = new List<DateTime>();
        private bool DatumIngevuld = false;
        private bool SlotIngevuld = false;
        private Klant klant;
        public MaakReservatieWindow(Klant k)
        {
            InitializeComponent();
            klant = k;
            GebruikerLabel.Content = $"Gebruiker : {klant.Voornaam} {klant.Achternaam}";

            for(int i = 1; i < 8; i++)
            {
                BeschikbareDagen.Add(DateTime.Today.AddDays(i));
            }

            DatumComboBox.ItemsSource = BeschikbareDagen;
        }

        private void LogUitButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TerugButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ZoekToestelButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DatumComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DatumIngevuld = true;
            if (DatumIngevuld == true && SlotIngevuld == true)
            {
                ZoekToestelButton.IsEnabled = true;
            }
        }

        private void SlotComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SlotIngevuld = true;
            if (DatumIngevuld == true && SlotIngevuld == true)
            {
                ZoekToestelButton.IsEnabled = true;
            }
        }
    }
}
