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
    /// Interaction logic for ReservatieAanvullenWindow.xaml
    /// </summary>
    public partial class ReservatieAanvullenWindow : Window
    {
        private Klant klant;
        private Reservatie reservatie;
        public GereserveerdeSlotsManager GSManager;
        public ReservatieAanvullenWindow(Klant k, Reservatie r)
        {
            InitializeComponent();
            klant = k;
            reservatie = r;
            GSManager = new GereserveerdeSlotsManager(new GereserveerdeSlotenRepoADO(ConfigurationManager.ConnectionStrings["fitnessDBconnection"].ToString()),
                new SlotsRepoADO(ConfigurationManager.ConnectionStrings["fitnessDBconnection"].ToString())); 
            GebruikerLabel.Content = $"Gebruiker : {klant.Voornaam} {klant.Achternaam}";
            ReservatieLabel.Content = $"Reservatie : {r.ToString()}";
            SlotenListBox.ItemsSource = GSManager.SelecteerGereserveerdeSloten(reservatie.ReservatieNummer);
            SlotComboBox.ItemsSource = GSManager.BeschikbareSloten();//moet na listbox komen
        }

        private void TerugButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void SlotComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ZoekToestelButton.IsEnabled = true;
        }

        private void ZoekToestelButton_Click(object sender, RoutedEventArgs e)
        {
            
            KiesToestelWindow KT_Window = new KiesToestelWindow(klant, reservatie, (string)SlotComboBox.SelectedItem);
            KT_Window.ShowDialog();

        }
    }
}
