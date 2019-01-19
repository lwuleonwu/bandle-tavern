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
using System.Windows.Media.Animation;

namespace BandleTavern.Wpf.Elements.PartyLists
{
    /// <summary>
    /// Interaction logic for PartyLeader.xaml
    /// </summary>
    public partial class PartyLeader : UserControl
    {
        private bool IsChecked = false;
        private Storyboard ContractAnimation;
        private Storyboard ExpandAnimation;
        public Party Master;

        public PartyLeader()
        {
            InitializeComponent();
            /*
            ContractAnimation = this.FindResource("PlayContract") as Storyboard;
            ExpandAnimation = this.FindResource("PlayExpand") as Storyboard;
            */
        }

        public string LeaderName
        {
            set
            {
                TextBoxContent.Dispatcher.Invoke(() =>
                {
                    TextBoxContent.Text = value;
                });
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            /*
            if (IsChecked)
            {
                if (ContractAnimation != null)
                {
                    BeginStoryboard(ContractAnimation);
                }
            }
            else
            {
                if (ExpandAnimation != null)
                {
                    BeginStoryboard(ExpandAnimation);
                }
            }
            IsChecked = !IsChecked;
            if (Master != null)
            {
                Master.IsExpanded = IsChecked;
            }
            */
        }
    }
}
