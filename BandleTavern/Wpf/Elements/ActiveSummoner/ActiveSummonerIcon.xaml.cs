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
using BandleTavern.Extensions;

namespace BandleTavern.Wpf.Elements.ActiveSummoner
{
    /// <summary>
    /// Handles the view of the summoner icon.
    /// Accepts a string (Source) and trys to convert it into a usable uri to obtain and display the available bitmap.
    /// </summary>
    public partial class ActiveSummonerIcon : UserControl
    {
        private BitmapImage _source;

        public ActiveSummonerIcon()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Returns current source in string format.
        /// Tries to obtain a bitmap from a given string location.
        /// </summary>
        public BitmapImage Source
        {
            get => _source;
            set
            {
                _source = value;
                SummonerIcon.Dispatcher.Invoke(() => {
                    SummonerIcon.ImageSource = value;
                });
            }
        }
    }
}
