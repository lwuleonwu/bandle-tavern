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
using LcuApiTavern.Plugins.LolSummoner.V1;

namespace BandleTavern.Wpf.Elements.WindowBanner
{
    /// <summary>
    /// Element container for Window and Banner.
    /// </summary>
    public partial class WindowBanner : UserControl
    {
        public WindowBanner()
        {
            InitializeComponent();
        }

        public void InitBanner()
        {
            Summoner = LcuCache.ActiveSummoner;
        }

        public CurrentSummoner Summoner
        {
            get => _summoner;
            set
            {
                _summoner = value;
                if (value != null)
                {
                    ActiveSummoner.SummonerIconSource = value.ProfileIcon;
                    ActiveSummoner.SummonerName = value.DisplayName;
                }
                else
                {
                    ActiveSummoner.SummonerIconSource = null;
                    ActiveSummoner.SummonerName = "";
                }
            }
        }

        private CurrentSummoner _summoner;
    }
}
