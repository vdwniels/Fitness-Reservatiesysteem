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
    /// Interaction logic for ReservatieWindow.xaml
    /// </summary>
    public partial class ReservatieWindow : Window
    {
        private ReservatieManager reservatieManager;
        public Reservatie geselecteerdeReservatie;
        private Klant klant;
        private ObservableCollection<Reservatie> allereservaties = new ObservableCollection<Reservatie>();
        public ReservatieWindow(Klant k)
        {
            InitializeComponent();
            klant = k;
            reservatieManager = new ReservatieManager(
    new ReservatieRepoADO(ConfigurationManager.ConnectionStrings["fitnessDBconnection"].ToString()),
    new GereserveerdeSlotenRepoADO(ConfigurationManager.ConnectionStrings["fitnessDBconnection"].ToString()));

            GebruikerLabel.Content = $"Gebruiker : {klant.Voornaam} {klant.Achternaam}";
            List<Reservatie> res = reservatieManager.SelecteerReservatiesOpKlantnummer(klant.KlantNummer);
            foreach( Reservatie r in res)
            {
                allereservaties.Add(r);
            }
            ReservatiesListBox.ItemsSource = allereservaties;
        }

        private void ReservatiesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            VulReservatieAanButton.IsEnabled = true;
            geselecteerdeReservatie = (Reservatie)ReservatiesListBox.SelectedItem;
        }

        private void NieuweReservatieButton_Click(object sender, RoutedEventArgs e)
        {
            MaakReservatieWindow MaakWindow = new MaakReservatieWindow(klant);
            if (MaakWindow.ShowDialog() == true)
            {
                allereservaties.Add(MaakWindow.reservatie);
            }
            //this.Close();
            ReservatiesListBox.ItemsSource = allereservaties;

        }

        private void VulReservatieAanButton_Click(object sender, RoutedEventArgs e)
        {
            ReservatieAanvullenWindow RA_Window = new ReservatieAanvullenWindow(klant,geselecteerdeReservatie);
            RA_Window.ShowDialog();

        }

        private void TerugButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow LoginWindow = new MainWindow();
            this.Close();
            LoginWindow.ShowDialog();

        }
    }
}
