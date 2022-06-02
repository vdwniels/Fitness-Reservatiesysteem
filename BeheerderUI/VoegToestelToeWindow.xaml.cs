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

namespace BeheerderUI
{
    /// <summary>
    /// Interaction logic for VoegToestelToeWindow.xaml
    /// </summary>
    public partial class VoegToestelToeWindow : Window
    {
        private ToestelManager tm;
        private ObservableCollection<Toestel> toestellen = new ObservableCollection<Toestel>();
        public VoegToestelToeWindow()
        {
            InitializeComponent();
            tm = new ToestelManager(new ToestelRepoADO(ConfigurationManager.ConnectionStrings["fitnessDBconnection"].ToString()),
                new ReservatieRepoADO(ConfigurationManager.ConnectionStrings["fitnessDBconnection"].ToString()));
            ToestelTpeComboBox.ItemsSource = tm.GetToestelTypes();
            List<Toestel> alleToestellen =  tm.GetAlleToestellen();
            foreach( Toestel t in alleToestellen)
            {
                toestellen.Add(t);
            }
            ToestellenListBox.ItemsSource = toestellen;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            Close();
            main.ShowDialog();
        }

        private void MaakToestelButton_Click(object sender, RoutedEventArgs e)
        {
            Toestel t;
            t = tm.VoegToestelToe((string)ToestelTpeComboBox.SelectedItem);
            toestellen.Add(t);
            ToestellenListBox.ItemsSource = toestellen;
            //ToestellenListBox.Items.Add(t);

        }

        private void ToestelTpeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MaakToestelButton.IsEnabled = true;
        }
    }
}
