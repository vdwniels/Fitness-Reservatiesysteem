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
    /// Interaction logic for ReservatieWindow.xaml
    /// </summary>
    public partial class ReservatieWindow : Window
    {
        private KlantManager klantManager;
        private ReservatieManager reservatieManager;

        public ReservatieWindow(KlantManager klantManager)
        {
            InitializeComponent();
            reservatieManager = new ReservatieManager(
    new ReservatieRepoADO(ConfigurationManager.ConnectionStrings["fitnessDBconnection"].ToString()),
    new GereserveerdeSlotenRepoADO(ConfigurationManager.ConnectionStrings["fitnessDBconnection"].ToString()));

            GebruikerLabel.Content = $"Gebruiker : {klantManager.k.Voornaam} {klantManager.k.Achternaam}";
            ReservatiesListBox.ItemsSource = reservatieManager.SelecteerReservatiesOpKlantnummer(klantManager.k.KlantNummer);
        }

        private void ReservatiesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void NieuweReservatieButton_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void VulReservatieAanButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TerugButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
