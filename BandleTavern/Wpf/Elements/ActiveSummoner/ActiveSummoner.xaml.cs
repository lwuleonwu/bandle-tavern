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
using BandleTavern.Lcu.Cache;

namespace BandleTavern.Wpf.Elements.ActiveSummoner
{
    /// <summary>
    /// Panel for Active Summoner.
    /// Accepts SummonerName, SummonerIconSource.
    /// </summary>
    public partial class ActiveSummoner : UserControl
    {
        public static ActiveSummoner Active;

        public ActiveSummoner()
        {
            InitializeComponent();
            SetVisibility = Visibility.Collapsed;
            Active = this;
        }

        public LcuApiTavern.Plugins.LolSummoner.V1.CurrentSummoner Summoner
        {
            set
            {
                if (value != null)
                {
                    SummonerName = value.DisplayName;
                    SummonerIconSource = value.ProfileIcon;
                    SummonerXp = value.PercentCompleteForNextLevel;
                    SummonerLevel = value.SummonerLevel;
                    if (value.DisplayName == "")
                    {
                        SetVisibility = Visibility.Collapsed;
                    }
                    else
                    {
                        SetVisibility = Visibility.Visible;
                    }
                }
                else
                {
                    SummonerName = "";
                    SummonerXp = 0;
                    SummonerLevel = 0;
                    SetVisibility = Visibility.Collapsed;
                }
            }
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
        public BitmapImage SummonerIconSource { get => Icon.Source; set => Icon.Source = value; }

        /// <summary>
        /// 
        /// </summary>
        public int SummonerXp { set => XpBar.XpPercent = value; }

        /// <summary>
        /// 
        /// </summary>
        public int SummonerLevel {
            set
            {
                TextBlockSummonerLevel.Dispatcher.Invoke(() =>
                {
                    if (value > 0)
                    {
                        TextBlockSummonerLevel.Text = string.Format("Level: {0}", value);
                    }
                    else
                    {
                        TextBlockSummonerLevel.Text = "";
                    }
                });
            }
        }

        private void ButtonRefresh_Click(object sender, RoutedEventArgs e)
        {
            UserSummoner.GetActiveSummoner();
        }

        public Visibility SetVisibility
        {
            set
            {
                if (value != MainGrid.Visibility)
                {
                    MainGrid.Dispatcher.Invoke(() =>
                    {
                        MainGrid.Visibility = value;
                    });
                }
            }
        }
    }
}
