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
using LcuApiTavern.Plugins.LolMissions.V1;

namespace BandleTavern.Wpf.Elements.Mission
{
    /// <summary>
    /// Interaction logic for Mission.xaml
    /// </summary>
    public partial class Mission : UserControl
    {
        private Missions _missionObject;

        public Mission()
        {
            InitializeComponent();
        }

        public Missions MissionObject
        {
            get => _missionObject;
            set
            {
                _missionObject = value;
                Title = value.Title;
                Description = value.Description;

                stackPanelCompletionStatus.Dispatcher.Invoke(() =>
                {
                    stackPanelCompletionStatus.Children.Clear();
                    foreach (var i in value.Objectives)
                    {
                        int percentComplete = 0;
                        string statusString = "";
                        if (i.Progress.TotalCount > 0)
                        {
                            percentComplete = (i.Progress.CurrentProgress / i.Progress.TotalCount);
                            statusString = string.Format("{0} / {1}", i.Progress.CurrentProgress, i.Progress.TotalCount);
                        }
                        stackPanelCompletionStatus.Children.Add(new MissionCompletion()
                        {
                            Description = i.Description,
                            Status = statusString,
                            BarPercent = percentComplete
                        });
                    }
                });

                if ((value.MissionType != "REPEATING") && (value.EndTimeUnix > -1))
                {
                    TimeSpan ts = value.EndTime - DateTime.UtcNow;
                    if (ts.Days > 0)
                    {
                        TimeStatus = string.Format("{0} days.", ts.Days);
                    }
                    else if (ts.Hours > 0)
                    {
                        TimeStatus = string.Format("{0} hours.", ts.Hours);
                    }
                    else
                    {
                        TimeStatus = string.Format("{0} minutes.", ts.Minutes);
                    }
                }
                else
                {
                    TimeStatus = "Repeating.";
                }
                imageMissionIcon.Source = value.IconImage;
                HelperText = value.HelperText;
            }
        }

        private string Title
        {
            get => textBlockTitle.Text;
            set
            {
                textBlockTitle.Dispatcher.Invoke(() =>
                {
                    textBlockTitle.Text = value;
                });
            }
        }
        private string Description
        {
            get => textBlockDescription.Text;
            set
            {
                textBlockDescription.Dispatcher.Invoke(() =>
                {
                    textBlockDescription.Text = value;
                });
            }
        }
        private string TimeStatus
        {
            get => textBlockTimeStatus.Text;
            set
            {
                textBlockTimeStatus.Dispatcher.Invoke(() =>
                {
                    textBlockTimeStatus.Text = value;
                });
            }
        }
        private string HelperText
        {
            get => textBlockHelper.Text;
            set
            {
                textBlockHelper.Dispatcher.Invoke(() =>
                {
                    textBlockHelper.Text = value;
                    if (value == "")
                    {
                        textBlockHelper.Visibility = Visibility.Collapsed;
                    }
                    else
                    {
                        textBlockHelper.Visibility = Visibility.Visible;
                    }
                });
            }
        }
    }
}
