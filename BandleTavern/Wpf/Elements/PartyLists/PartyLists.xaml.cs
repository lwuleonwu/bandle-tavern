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
    /// Interaction logic for PartyLists.xaml
    /// </summary>
    public partial class PartyLists : UserControl
    {
        public static PartyLists Active;

        public PartyLists()
        {
            InitializeComponent();
            Active = this;
        }

        public void PopulateParties(List<FirebaseKey> fList)
        {
            WrapPanel.Dispatcher.Invoke(() =>
            {
                WrapPanel.Children.Clear();
                foreach(FirebaseKey fk in fList)
                {
                    Party newParty = new Party();
                    newParty.PartyMembers(fk.firebaseData.party.partyInfo.members);
                    WrapPanel.Children.Add(newParty);
                }
            });
        }
    }
}
