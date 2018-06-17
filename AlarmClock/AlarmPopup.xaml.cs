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

namespace AlarmClock
{
    /// <summary>
    /// Interaction logic for AlarmPopup.xaml
    /// </summary>
    public partial class AlarmPopup : Page
    {
        public AlarmPopup()
        {
            InitializeComponent();
        }

        private void b_Number_Click(object sender, RoutedEventArgs e)
        {
            if (b_Number.Content != null)
            {
                Clipboard.SetText(b_Number.Content.ToString());
            }
        }
    }
}
