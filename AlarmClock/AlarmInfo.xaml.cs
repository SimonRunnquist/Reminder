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
using System.Windows;

namespace AlarmClock
{
    /// <summary>
    /// Interaction logic for AlarmInfo.xaml
    /// </summary>
    public partial class AlarmInfo : Page
    {

        bool moreInformation = false;

        public AlarmInfo()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (!moreInformation)
            {
                l_alarmDesc.Visibility = Visibility.Visible;
                l_alarmNumber.Visibility = Visibility.Visible;
                copyNumber_button.Visibility = Visibility.Visible;
                l_alarmName.Visibility = Visibility.Hidden;
                l_alarmInfo.Visibility = Visibility.Hidden;
                moreInformation = true;
            }
            else {
                l_alarmDesc.Visibility = Visibility.Hidden;
                l_alarmNumber.Visibility = Visibility.Hidden;
                copyNumber_button.Visibility = Visibility.Hidden;
                l_alarmName.Visibility = Visibility.Visible;
                l_alarmInfo.Visibility = Visibility.Visible;
                moreInformation = false;
            }


        }

        private void copyNumber_button_Click(object sender, RoutedEventArgs e)
        {
            if (l_alarmNumber.Content != null) {
                Clipboard.SetText(l_alarmNumber.Content.ToString());
            }
        }
    }
}
