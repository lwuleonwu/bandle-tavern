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

namespace BandleTavern
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private LcuInteraction LcuInteraction;
        public MainWindow()
        {
            InitializeComponent();
            InitOptions();
            LcuInteraction = new LcuInteraction();
            LcuInteraction.InitLcuApi(this);
        }

        private void ButtonOptions_Click(object sender, RoutedEventArgs e)
        {
            PanelOptions.Dispatcher.Invoke(() =>
            {
                PanelOptions.LoadOptionsValues();
                PanelOptions.Visibility = Visibility.Visible;
            });
        }

        /// <summary>
        /// Essentially Reset the app as if there were a new user. Used for new lockfile, league client, summoner etc.
        /// </summary>
        public void InitModules()
        {
            LcuCache.GetActiveSummoner();
            WindowBanner.InitBanner();
        }

        public void InitOptions()
        {
            Options.LoadOptions();
            if (Options.Active.ClientDirectory == "")
            {
                Wpf.Windows.DialogClientDirectory dcd = new Wpf.Windows.DialogClientDirectory();
                dcd.ShowDialog();
            }
        }
    }
}
