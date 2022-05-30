using BLKlant.Domein;
using BLKlant.Managers;
using DLKlant;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public Reservatie reservatie;
        public GereserveerdeSlotsManager GSManager;
        public ObservableCollection<GereserveerdSlot> gs = new ObservableCollection<GereserveerdSlot>();
        public ObservableCollection<string> beschikbareSloten = new ObservableCollection<string>();
        public ReservatieAanvullenWindow(Klant k, Reservatie r)
        {
            InitializeComponent();
            klant = k;
            reservatie = r;
            GSManager = new GereserveerdeSlotsManager(new GereserveerdeSlotenRepoADO(ConfigurationManager.ConnectionStrings["fitnessDBconnection"].ToString()),
                new SlotsRepoADO(ConfigurationManager.ConnectionStrings["fitnessDBconnection"].ToString())); 
            GebruikerLabel.Content = $"Gebruiker : {klant.Voornaam} {klant.Achternaam}";
            ReservatieLabel.Content = $"Reservatie : {r.ToString()}";
            List<GereserveerdSlot> sloten = GSManager.SelecteerGereserveerdeSloten(reservatie.ReservatieNummer);
            foreach (GereserveerdSlot GS in sloten)
            {
                gs.Add(GS);
            }
            SlotenListBox.ItemsSource = gs;
            List<string> beschikbaar = GSManager.BeschikbareSloten();
            foreach(string s in beschikbaar)
            {
                beschikbareSloten.Add(s);
            }
            SlotComboBox.ItemsSource = beschikbareSloten;//moet na listbox komen
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
            if (KT_Window.ShowDialog() == true)
            {
                gs.Add(new GereserveerdSlot(KT_Window.t.ToestelNummer, (string)SlotComboBox.SelectedItem));
                beschikbareSloten.Remove(KT_Window.slot);
            }
                SlotenListBox.ItemsSource = gs;
        }
    }
}
