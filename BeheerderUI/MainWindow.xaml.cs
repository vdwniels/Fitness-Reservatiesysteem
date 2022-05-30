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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BeheerderUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void VerwijderToestelButton_Click(object sender, RoutedEventArgs e)
        {
            VerwijderToestelWindow VT = new VerwijderToestelWindow();
            Close();
            VT.ShowDialog();

        }

        private void VoegToestelToeButton_Click(object sender, RoutedEventArgs e)
        {
            VoegToestelToeWindow VTT = new VoegToestelToeWindow();
            Close();
            VTT.ShowDialog();
        }

        private void VeranderStatusToestelButton_Click(object sender, RoutedEventArgs e)
        {
            VerandeStatusWindow VS = new VerandeStatusWindow();
            Close();
            VS.ShowDialog();

        }
    }
}
