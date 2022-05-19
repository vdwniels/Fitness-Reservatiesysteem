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

namespace BeheerderUI
{
    /// <summary>
    /// Interaction logic for VerwijderToestelWindow.xaml
    /// </summary>
    public partial class VerwijderToestelWindow : Window
    {
        private ToestelManager tm;
        private List<Toestel> toestellen;

        public VerwijderToestelWindow()
        {
            InitializeComponent();
            tm = new ToestelManager(new ToestelRepoADO(ConfigurationManager.ConnectionStrings["fitnessDBconnection"].ToString()),
    new ReservatieRepoADO(ConfigurationManager.ConnectionStrings["fitnessDBconnection"].ToString()));
            toestellen = tm.GetAlleToestellen();
            ToestellenListBox.ItemsSource = toestellen;

        }

        private void ToestellenListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            VerwijderToestellenButton.IsEnabled = true;
        }

        private void VerwijderToestellenButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                List<Toestel> geselecteerdeToestellen = new List<Toestel>();
                //geselecteerdeToestellen = ToestellenListBox.SelectedItems as List<Toestel>;
                string bericht = $"De volgende toestellen zijn verwijdert :\n";
                foreach (Toestel t in ToestellenListBox.SelectedItems)
                {
                    geselecteerdeToestellen.Add(t);
                    bericht += $"• {t.ToString()}\n";

                }
                tm.VerwijderToestel(geselecteerdeToestellen);
                MessageBox.Show(bericht, "toestellen verwijdert");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Probleem bij verwijderen");
            }
            finally
            {
                MainWindow main = new MainWindow();
                Close();
                main.ShowDialog();

            }
        }
    }
}
