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

namespace BandleTavern.Wpf.Elements.ActiveSummoner
{
    /// <summary>
    /// Interaction logic for ActiveSummonerXpBar.xaml
    /// </summary>
    public partial class ActiveSummonerXpBar : UserControl
    {
        public ActiveSummonerXpBar()
        {
            InitializeComponent();
        }

        public int XpPercent
        {
            set
            {
                double d = (double)value / 100;
                XpScale.Dispatcher.Invoke(() => { XpScale.ScaleX = d; });
            }
        }
    }
}
