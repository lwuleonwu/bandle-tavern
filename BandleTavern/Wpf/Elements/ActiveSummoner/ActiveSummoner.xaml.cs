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
    /// Panel for Active Summoner.
    /// Accepts SummonerName, SummonerIconSource.
    /// </summary>
    public partial class ActiveSummoner : UserControl
    {
        public ActiveSummoner()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Displays the given string.
        /// </summary>
        public string SummonerName
        {
            get => TextBlockSummonerName.Text;
            set
            {
                TextBlockSummonerName.Dispatcher.Invoke(() =>
                {
                    TextBlockSummonerName.Text = value;
                });
            }
        }

        /// <summary>
        /// Translates the given string to uri and obtains and displays bitmap if available from source.
        /// </summary>
        public string SummonerIconSource { get => Icon.Source; set => Icon.Source = value; }
    }
}
