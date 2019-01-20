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

namespace BandleTavern.Wpf.Elements.PartyLists
{
    /// <summary>
    /// Interaction logic for Party.xaml
    /// </summary>
    public partial class Party : UserControl
    {
        private Rectangle[] PartySlots;
        private TextBlock[] PartySlotText;

        public Party()
        {
            InitializeComponent();
            PartySlots = new Rectangle[] {
                RectangleSlot1,
                RectangleSlot2,
                RectangleSlot3,
                RectangleSlot4,
                RectangleSlot5
            };
            PartySlotText = new TextBlock[] {
                textblockMember2,
                textblockMember3,
                textblockMember4,
                textblockMember5
            };
            PartyLeader.Master = this;
        }

        public bool IsExpanded
        {
            set
            {
                stackpanelMembers.Dispatcher.Invoke(() =>
                {
                    if (value)
                    {
                        stackpanelMembers.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        stackpanelMembers.Visibility = Visibility.Collapsed;
                    }
                });
            }
        }
        
        /*
        public void PartyMembers(FirebasePartyMember[] members)
        {
            int count = 0;
            string[] ranks = new string[5];
            if (members != null)
            {
                if (members[count] != null)
                {
                    PartyLeader.LeaderName = members[count].Name;
                    ranks[count] = members[count].Rank;
                }
                else
                {
                    PartyLeader.LeaderName = "";
                    ranks[count] = "";
                }
                count++;

                foreach (TextBlock tb in PartySlotText)
                {
                    string name = "";
                    string rank = "";
                    if (count < members.Count())
                    {
                        name = members[count].Name;
                        rank = members[count].Rank;
                    }

                    tb.Dispatcher.Invoke(() =>
                    {
                        tb.Text = name;
                        //
                        //if (name == "")
                        //{
                        //    tb.Visibility = Visibility.Collapsed;
                        //}
                        //else
                        //{
                        //    tb.Visibility = Visibility.Visible;
                        //}
                        //
                    });

                    ranks[count] = rank;

                    count++;
                }
                SlotRanks(ranks);
            }
        }
        */



        public void SlotRanks(string[] stringRanks)
        {
            int count = 0;
            foreach (Rectangle r in PartySlots)
            {
                LinearGradientBrush Fill;
                string firstWord = "";
                if (count < stringRanks.Count())
                {
                    firstWord = stringRanks[count].Split(' ').First();
                }
                switch (firstWord)
                {
                    case "UNRANKED":
                        Fill = Application.Current.FindResource("BrushUnranked") as LinearGradientBrush;
                        break;
                    case "IRON":
                        Fill = Application.Current.FindResource("BrushIron") as LinearGradientBrush;
                        break;
                    case "BRONZE":
                        Fill = Application.Current.FindResource("BrushBronze") as LinearGradientBrush;
                        break;
                    case "SILVER":
                        Fill = Application.Current.FindResource("BrushSilver") as LinearGradientBrush;
                        break;
                    case "GOLD":
                        Fill = Application.Current.FindResource("BrushGold") as LinearGradientBrush;
                        break;
                    case "PLATINUM":
                        Fill = Application.Current.FindResource("BrushPlatinum") as LinearGradientBrush;
                        break;
                    case "DIAMOND":
                        Fill = Application.Current.FindResource("BrushDiamond") as LinearGradientBrush;
                        break;
                    case "MASTER":
                        Fill = Application.Current.FindResource("BrushMaster") as LinearGradientBrush;
                        break;
                    case "GRANDMASTER":
                        Fill = Application.Current.FindResource("BrushGrandMaster") as LinearGradientBrush;
                        break;
                    case "CHALLENGER":
                        Fill = Application.Current.FindResource("BrushChallenger") as LinearGradientBrush;
                        break;
                    default:
                        Fill = Application.Current.FindResource("BrushNoRank") as LinearGradientBrush;
                        break;
                }
                r.Dispatcher.Invoke(() =>
                {
                    r.Fill = Fill;
                });
                count++;
            }
        }
    }
}
