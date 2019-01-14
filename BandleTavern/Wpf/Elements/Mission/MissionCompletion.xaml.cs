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
    /// Interaction logic for MissionCompletion.xaml
    /// </summary>
    public partial class MissionCompletion : UserControl
    {
        public MissionCompletion()
        {
            InitializeComponent();
        }

        public string Status
        {
            get => TextblockStatus.Text;
            set
            {
                TextblockStatus.Dispatcher.Invoke(() =>
                {
                    TextblockStatus.Text = value;
                });
            }
        }

        public string Description
        {
            get => TextblockDescription.Text;
            set
            {
                TextblockDescription.Dispatcher.Invoke(() =>
                {
                    TextblockDescription.Text = value;
                });
            }
        }

        public int BarPercent
        {
            set
            {
                double d = (double)value / 100;
                XpScale.Dispatcher.Invoke(() => { XpScale.ScaleX = d; });
            }
        }
    }
}
