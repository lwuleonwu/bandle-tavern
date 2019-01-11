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
using BandleTavern.Lcu;

namespace BandleTavern.Wpf.Elements.OptionsPanel
{
    /// <summary>
    /// Interaction logic for OptionsPanel.xaml
    /// </summary>
    public partial class OptionsPanel : UserControl
    {
        public OptionsPanel()
        {
            InitializeComponent();
        }

        public void LoadOptionsValues()
        {
            textBoxClientDirectory.Dispatcher.Invoke(() =>
            {
                textBoxClientDirectory.Text = Options.Active.ClientDirectory;
            });
        }

        private void ButtonDone_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
            bool IsNewClientDirectory = false;
            if (Options.Active.ClientDirectory != textBoxClientDirectory.Text)
            {
                IsNewClientDirectory = true;
            }
            Options.Active.ClientDirectory = textBoxClientDirectory.Text;
            Options.SaveOptions();
            if (IsNewClientDirectory)
            {
                LcuInteraction.ListenForLockfile();
            }
        }
    }
}
