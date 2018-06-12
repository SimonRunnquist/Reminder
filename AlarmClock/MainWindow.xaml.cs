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
using System.IO;


namespace AlarmClock
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DateTime alarmTime = new DateTime();
        DateTime currentTime = new DateTime();
        //AlarmProps alarmProps = new AlarmProps();
        TimeSpan tiSp = new TimeSpan();
        AlarmXML alarm = new AlarmXML();
        int hour = 0;
        int minute = 0;
        string xmlPath = "";
        string name;
        string phoneNumber;
        string caseNumber;

        public MainWindow()
        {
            InitializeComponent();
            FillHourAndMinute();
            SetXmlFilePath();
            CreateXML();
            ReadXml();
        }

        /*
        public void setAlarm(String name, String phoneNumber, String currentCase, DateTime alarm)
        {
            AlarmProps alarmProps = new AlarmProps();
            //Sätter alarmet
            alarmProps.Name = name;
            alarmProps.Phone = phoneNumber;
            alarmProps.CurrentCase = currentCase;
            alarmProps.Alarm = alarm;


        }*/

        public void SetXmlFilePath() {
            xmlPath = Environment.CurrentDirectory;
        }
        

        public void controlDate() {
            if (PickDate.SelectedDate != null) {
                alarmTime = new DateTime(PickDate.SelectedDate.Value.Year, PickDate.SelectedDate.Value.Month, PickDate.SelectedDate.Value.Day, hour, minute, 0, 0, DateTimeKind.Local);
            }

        }

        public void controlInformation() {
            if (NameOfUser.Text != null || NameOfUser.Text != "")
            {
                if (PhoneNumber.Text != null || PhoneNumber.Text != "")
                {
                    if (CaseNumber.Text != null || CaseNumber.Text != "")
                    {
                        name = NameOfUser.Text;
                        phoneNumber = PhoneNumber.Text;
                        caseNumber = CaseNumber.Text;
                    }

                }

            }
        }

        //Fyll cb_hour och cb_minute med data
        private void FillHourAndMinute() {
            for (var i = 0; i < 24; i++) {
                if (i < 10) {
                    cb_hour.Items.Add("0" + i);
                }
                else
                {
                    cb_hour.Items.Add(i);
                }
            }
            for (var a = 0; a < 60; a = a + 5) {
                if (a < 10)
                {
                    cb_minute.Items.Add("0" + a);
                }
                else {
                    cb_minute.Items.Add(a);
                }
            }

        }


        //Kollar tiden
        public bool checkTime() {

            if (cb_hour.SelectedItem != null && cb_minute.SelectedItem != null) {
                hour = int.Parse(cb_hour.SelectedItem.ToString());
                minute = int.Parse(cb_minute.SelectedItem.ToString());
                setTime();
                return true;
            }
            else {
                Console.WriteLine("Kolla tiden, något är fel");
                return false;
            }


        }
        //Sätter tiden
        public void setTime() {
            tiSp = new TimeSpan(hour, minute, 0);
            alarmTime.Add(tiSp);
        }

        private void btn_setAlarm_Click(object sender, RoutedEventArgs e)
        {

            //Behöver hantera Nullvärde
            if (checkTime())
            {
                controlDate();
                controlInformation();

                addAlarm(1, name, phoneNumber, caseNumber, alarmTime);
            }

            getTime(hour, minute);
        }

        private void getTime(int alarmHour, int alarmMinute) {

            //Push mot XML
            //if (alarmHour == hour && alarmMinute == minute)
        }


        //Läs XML-filen

        public void ReadXml() {
            try {
                XDocument doc = XDocument.Load(xmlPath + @"\Alarmsss.xml");
                var alarms = from alarm in doc.Descendants("Alarm")
                             select new
                             {
                                 ID = alarm.Element("id").Value,
                                 Name = alarm.Element("name").Value,
                                 PhoneNumber = alarm.Element("phoneNumber").Value,
                                 CaseNumber = alarm.Element("caseNumber").Value,
                                 DateOfAlarm = alarm.Element("dateTime").Value
                             };

                foreach (var alarm in alarms)
                {
                    Console.WriteLine(alarm.DateOfAlarm);
                }

            }
            catch(Exception e) {
                Console.WriteLine(e.Message);
            }
        }

        public void CreateXML() {

            if (File.Exists(xmlPath + @"\Alarmsss.xml"))
            {
                Console.WriteLine("File Already Exists");
            }
            else
            {
                using (XmlWriter writer = XmlWriter.Create(xmlPath + @"\Alarmsss.xml"))
                {
                    writer.WriteStartDocument();
                    writer.WriteStartElement("Alarms");
                }
            }
        }

        public void addAlarm(int id, string name, string phoneNumber, string caseNumber, DateTime callDate) {
            try
            {
                XDocument doc = XDocument.Load(xmlPath + @"\Alarmsss.xml");
                XElement alarm = doc.Element("Alarms");
                alarm.Add(new XElement("Alarm",
                            new XElement("id", id),
                            new XElement("name", name),
                            new XElement("phoneNumber", phoneNumber),
                            new XElement("caseNumber", caseNumber),
                            new XElement("dateTime", callDate.ToString())));

                doc.Save(xmlPath + @"\Alarmsss.xml");


            }
            catch(Exception e) {
                Console.WriteLine(e.Message.ToString());
            }
        }
    }
}
