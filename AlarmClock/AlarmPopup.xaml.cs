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
using System.Xml.Linq;

namespace AlarmClock
{
    /// <summary>
    /// Interaction logic for AlarmPopup.xaml
    /// </summary>
    public partial class AlarmPopup : Page
    {
        string xmlPath = "";
        public int ID = 0;

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

        //Sätter rätt path till projektet
        public void SetXmlFilePath()
        {
            xmlPath = Environment.CurrentDirectory;
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
    }
}
