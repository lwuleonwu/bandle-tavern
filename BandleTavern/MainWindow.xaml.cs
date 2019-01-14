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
using BandleTavern.Lcu;

namespace BandleTavern
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private LcuInteraction LcuInteraction;

        public static MainWindow Active;

        public MainWindow()
        {
            InitializeComponent();
            Active = this;
            InitOptions();
            LcuInteraction = new LcuInteraction();
            LcuInteraction.InitLcuApi(this);
        }

        public static void OptionsPanelVisible()
        {
            Active.PanelOptions.Dispatcher.Invoke(() =>
            {
                Active.PanelOptions.LoadOptionsValues();
                Active.PanelOptions.Visibility = Visibility.Visible;
            });
        }

        /// <summary>
        /// Essentially Reset the app as if there were a new user. Used for new lockfile, league client, summoner etc.
        /// </summary>
        public void InitModules()
        {
            Lcu.Cache.UserSummoner.ListenForSummoner();
            WindowBanner.MissionList.RefreshMissions();
        }

        public void InitOptions()
        {
            Options.LoadOptions();
        }

        public Visibility ClientConnectionErrorVis
        {
            set
            {
                textblockClientConnectionError.Dispatcher.Invoke(() =>
                {
                    textblockClientConnectionError.Visibility = value;
                });
            }
        }
    }
}
