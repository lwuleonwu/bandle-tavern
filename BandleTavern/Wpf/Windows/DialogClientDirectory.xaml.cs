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
using System.Windows.Shapes;

namespace BandleTavern.Wpf.Windows
{
    /// <summary>
    /// Interaction logic for DialogClientDirectory.xaml
    /// </summary>
    public partial class DialogClientDirectory : Window
    {
        public DialogClientDirectory()
        {
            InitializeComponent();
        }

        private void ButtonDone_Click(object sender, RoutedEventArgs e)
        {
            Options.Active.ClientDirectory = textBox.Text;
            Options.SaveOptions();
            this.DialogResult = true;
        }
    }
}
