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
using System.Xml.Linq;
using System.Timers;

namespace AlarmClock
{
    /// <summary>
    /// Interaction logic for AlarmPopupWindow.xaml
    /// </summary>
    public partial class AlarmPopupWindow : Window
    {
        string xmlPath = "";

        public AlarmPopupWindow()
        {
            InitializeComponent();
        }

        

        private void b_Number_Click(object sender, RoutedEventArgs e)
        {
            if (b_Number.Content != null)
            {
                Clipboard.SetText(b_Number.Content.ToString());
                this.DialogResult = true;
                Close();
            }
        }

        //Sätter rätt path till projektet
        public void SetXmlFilePath(string path)
        {
            xmlPath = path;
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
