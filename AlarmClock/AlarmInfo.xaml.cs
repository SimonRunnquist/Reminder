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
using System.Xml;
using System.Xml.Linq;

namespace AlarmClock
{
    /// <summary>
    /// Interaction logic for AlarmInfo.xaml
    /// </summary>
    public partial class AlarmInfo : Page
    {

        bool moreInformation = false;
        public int alarmID = 0;
        string xmlPath = "";
        
        


        public AlarmInfo()
        {
            InitializeComponent();
            SetXmlFilePath();
        }

        

        //Sätter rätt path till projektet
        public void SetXmlFilePath()
        {
            xmlPath = Environment.CurrentDirectory;
        }

        
        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (!moreInformation)
            {
                l_alarmDesc.Visibility = Visibility.Visible;
                l_alarmNumber.Visibility = Visibility.Visible;
                l_alarmName.Visibility = Visibility.Hidden;
                l_alarmInfo.Visibility = Visibility.Hidden;
                moreInformation = true;
            }
            else {
                l_alarmDesc.Visibility = Visibility.Hidden;
                l_alarmNumber.Visibility = Visibility.Hidden;
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



        //Tar bort alarm
        public void DeleteAlarm(int id)
        {
            try
            {
                XDocument xdoc = XDocument.Load(xmlPath + @"\Alarms.xml");
                xdoc.Descendants("Alarm")
                    .Where(x => (string)x.Attribute("id") == id.ToString())
                    .Remove();

                xdoc.Save(xmlPath + @"\Alarms.xml");
                ((MainWindow)System.Windows.Application.Current.MainWindow).SortAlarms();
            }

            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void b_Delete_Click(object sender, RoutedEventArgs e)
        {
            DeleteAlarm(alarmID);

        }
    }
}
