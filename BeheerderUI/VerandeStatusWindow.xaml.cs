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
    /// Interaction logic for VerandeStatusWindow.xaml
    /// </summary>
    public partial class VerandeStatusWindow : Window
    {
        private ToestelManager tm;
        private List<Toestel> toestellen;

        public VerandeStatusWindow()
        {
            InitializeComponent();
            tm = new ToestelManager(new ToestelRepoADO(ConfigurationManager.ConnectionStrings["fitnessDBconnection"].ToString()),
new ReservatieRepoADO(ConfigurationManager.ConnectionStrings["fitnessDBconnection"].ToString()));
            toestellen = tm.GetAlleToestellen();
            ToestellenListBox.ItemsSource = toestellen;

        }

        private void WijzigStatusButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                List<Toestel> geselecteerdeToestellen = new List<Toestel>();
                //geselecteerdeToestellen = ToestellenListBox.SelectedItems as List<Toestel>;
                string bericht = $"De volgende toestellen zijn aangepast :\n";
                foreach (Toestel t in ToestellenListBox.SelectedItems)
                {
                    geselecteerdeToestellen.Add(t);
                    bericht += $"• {t.ToString()}";
                    if (t.Status == "Beschikbaar") 
                    { 
                        bericht += " => Buiten Gebruik\n"; 
                    }
                    else
                    {
                        bericht += " => Beschikbaar\n";
                    }

                }
                tm.VeranderStatus(geselecteerdeToestellen);
                MessageBox.Show(bericht, "toestellen aangepast");
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

        private void ToestellenListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            WijzigStatusButton.IsEnabled = true;

        }
    }
}
