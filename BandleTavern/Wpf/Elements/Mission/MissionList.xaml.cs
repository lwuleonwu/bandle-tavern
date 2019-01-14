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

namespace BandleTavern.Wpf.Elements.Mission
{
    /// <summary>
    /// Interaction logic for MissionList.xaml
    /// </summary>
    public partial class MissionList : UserControl
    {
        private LcuApiTavern.Plugins.LolMissions.V1.Missions[] _missionsRaw;

        public MissionList()
        {
            InitializeComponent();
        }

        public void RefreshMissions()
        {
            MissionsRaw = LcuApiTavern.Plugins.LolMissions.V1.Missions.Get();
        }

        public LcuApiTavern.Plugins.LolMissions.V1.Missions[] MissionsRaw
        {
            get => _missionsRaw;
            set
            {
                _missionsRaw = value;
                stackPanelMissions.Dispatcher.Invoke(() =>
                {
                    stackPanelMissions.Children.Clear();
                });
                if (value != null)
                {
                    foreach (var i in value)
                    {
                        stackPanelMissions.Dispatcher.Invoke(() =>
                        {
                            stackPanelMissions.Children.Add(new Mission() {
                                MissionObject = i
                            });
                        });
                    }
                }
            }
        }

        private void ScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            ScrollViewer scv = (ScrollViewer)sender;
            scv.ScrollToHorizontalOffset(scv.HorizontalOffset - e.Delta);
            e.Handled = true;
        }
    }
}
