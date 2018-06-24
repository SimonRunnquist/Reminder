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
using System.Timers;
using System.Media;

namespace AlarmClock
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Datum och tider
        DateTime alarmTime = new DateTime();
        TimeSpan tiSp = new TimeSpan();


        //Referenser
        AlarmXML alarm = new AlarmXML();
        List<AlarmXML> alarmXMLRef = new List<AlarmXML>();

        //Globala variabler
        int hour = 0;
        int minute = 0;
        string xmlPath = "";
        string name;
        string phoneNumber;
        string caseNumber;
        bool bSeeAlarms = false;
        bool bAlarmActivated = false;

        

        public MainWindow()
        {
            InitializeComponent();
            b_ShowAlarms.Margin = new Thickness(192, 431, -32, -22);
            FillHourAndMinute();
            SetXmlFilePath();
            CreateXML();
            ReadXml();
            SortAlarms();

            //Skapa timer
            Timer aTimer = new Timer();
            aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            aTimer.Interval = 5000;
            aTimer.Enabled = true;
        }

        //Trycker på "Sätt Alarm"
        private void btn_setAlarm_Click(object sender, RoutedEventArgs e)
        {
            //Behöver hantera Nullvärde
            if (checkTime())
            {
                controlDate();

                if (ControlInformation())
                {
                    if (CheckID() < 8)
                    {

                        addAlarm(CheckID(), name, phoneNumber, caseNumber, alarmTime.Year, alarmTime.Month, alarmTime.Day, alarmTime.Hour, alarmTime.Minute);
                        ClearProps();

                    }

                    else {
                        Console.WriteLine("För många alarm, max antal är 8");
                    }

                }

                else {

                }
                
                ReadXml();
                SortAlarms();
            }
        }

        //Ger ett unikt ID och skickar tillbaka ID
        public int CheckID() {
            
            List<int> iDChecker = new List<int>();
            int freeID = 0;
            ReadXml();

            foreach (var item in alarmXMLRef) {
                iDChecker.Add(item.Id);
            }

            for (var i = 0; i < 8; i++)
            {
                if (iDChecker.Contains(freeID))
                {
                    freeID++;
                    Console.WriteLine(freeID);
                }

                else
                {
                    break;
                }
            }
            return freeID;
        }


        //Kollar efter alarm i alarmXMLRef
        public void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            /*
             * Blir en änring i listan
             * problem med att iterera igenom
             * Lös det
             * Get it fixed 
            */
            int alarmCount = 0;

            if (!bAlarmActivated && alarmCount == 0)
            {
                foreach (var item in alarmXMLRef)
                {
                    if (DateTime.Now.Month == item.Month && DateTime.Now.Day == item.Day && DateTime.Now.Hour == item.Hour && DateTime.Now.Minute == item.Minute)
                    {
                        createNotification(item.Id, item.Name, item.PhoneNumber, item.CaseNumber);
                        alarmCount = 1;
                        break;
                    }
                }
            }
         }

        //Spelar upp ljud vid alarm
        public void playSound() {
            SystemSounds.Asterisk.Play();
        }

        //Skapar popup
        public void createNotification(int id, string name, string number, string description)
        {
            try
            {
                //Kollar ifall alarmet redan är aktiverat
                if (!bAlarmActivated)
                {
                    Application.Current.Dispatcher.Invoke((Action)delegate
                   {
                       playSound();
                       bAlarmActivated = true;
                       AlarmPopupWindow alarmPopup = new AlarmPopupWindow();

                        //Assignar information till popup
                        alarmPopup.l_alarmName.Content = name;
                        alarmPopup.b_Number.Content = number;
                        alarmPopup.l_alarmDesc.Content = description;
                        alarmPopup.SetXmlFilePath(xmlPath);
                        alarmPopup.DeleteAlarm(id);

                        //Ändrar position på popup
                        alarmPopup.Top = 0;
                        alarmPopup.Left = SystemParameters.PrimaryScreenWidth - alarmPopup.Width;

                        //visar popup
                        Nullable<bool> dialogResult = alarmPopup.ShowDialog();


                       if (dialogResult == false)
                       {
                           bAlarmActivated = true;
                       }
                       else
                       {
                            bAlarmActivated = false;
                           SortAlarms();
                       }
                   });
                }
                else {
                    Console.WriteLine("Alarmet redan aktiverat, men inte stängt");
                }
            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
            }
        }

        //Tar bort alarm
        public void DeleteAlarm(int id)
        {
            try
            {
                XDocument doc = XDocument.Load(xmlPath + @"\Alarms.xml");
                doc.Root.Descendants("Alarm")
                       .Where(el => (string)el.Attribute("id") == id.ToString())
                       .Remove();
                
                SortAlarms();
            }

            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        //Fyll "frames" med "AlarmInfo"
        public void SortAlarms() {

            string year = "";
            string month = "";
            string day = "";
            string hour = "";
            string minute = "";

            //Tar bort alla kids
            sb_AlarmHolder.Children.Clear();
            ReadXml();

            //Itererar genom alarm
            foreach (var item in alarmXMLRef)
            {

                if (item.Day < 10)
                {
                    day = "0" + item.Day;
                }

                else {
                    day = item.Day.ToString();
                }

                if (item.Month < 10)
                {
                    month = "0" + item.Month;
                }

                else
                {
                    month = item.Month.ToString();
                }

                if (item.Hour < 10)
                {
                    hour = "0" + item.Hour;
                }

                else
                {
                    hour = item.Hour.ToString();
                }

                if (item.Minute < 10)
                {
                    minute = "0" + item.Minute;
                }

                else
                {
                    minute = item.Minute.ToString();
                }

                //Bygger datum
                string dateBuilder = item.Year + "-" + month + "-" + day + " " + hour + ":" + minute;

                //Sätter värden för alarm
                AlarmInfo alarmInfoRef = new AlarmInfo();
                alarmInfoRef.alarmID = item.Id;
                alarmInfoRef.l_alarmName.Content = item.Name;
                alarmInfoRef.l_alarmInfo.Content = dateBuilder;
                alarmInfoRef.l_alarmDesc.Content = item.CaseNumber;
                alarmInfoRef.l_alarmNumber.Content = item.PhoneNumber;
                
                //Skapar frame, ändrar storlek
                Frame frameRef = new Frame();
                frameRef.Width = 196;
                frameRef.Height = 40;

                //Skapar en avskiljare, ändrar storlek
                Frame divider = new Frame();
                divider.Width = 225;
                divider.Height = 5;

                //Sätter in alarm i "Frame"
                frameRef.Navigate(alarmInfoRef);

                //Sätter in "Frame" i stackPanel
                sb_AlarmHolder.Children.Add(frameRef);
                sb_AlarmHolder.Children.Add(divider);
            }

        }

        //Rensar textFields
        public void ClearProps() {
            NameOfUser.Text = "";
            PhoneNumber.Text = "";
            CaseNumber.Text = "";
        }

        //Sätter rätt path till projektet
        public void SetXmlFilePath() {
            xmlPath = Environment.CurrentDirectory;
        }
        

        //Kontrollerar datum
        public void controlDate() {
            if (PickDate.SelectedDate != null) {
                alarmTime = new DateTime(PickDate.SelectedDate.Value.Year, PickDate.SelectedDate.Value.Month, PickDate.SelectedDate.Value.Day, hour, minute, 0, 0, DateTimeKind.Local);
            }

        }


        //Kontrollerar input från användaren
        public bool ControlInformation() {
            if (NameOfUser.Text != null || NameOfUser.Text != "")
            {
                if (PhoneNumber.Text != null || PhoneNumber.Text != "")
                {
                    if (CaseNumber.Text != null || CaseNumber.Text != "")
                    {
                        name = NameOfUser.Text;
                        phoneNumber = PhoneNumber.Text;
                        caseNumber = CaseNumber.Text;
                        return true;
                    } else {
                        return false;
                    }
                } else {
                    return false;
                }
            } else {
                return false;
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
                return false;
            }


        }

        //Sätter tiden
        public void setTime() {
            tiSp = new TimeSpan(hour, minute, 0);
            alarmTime.Add(tiSp);
        }

        

        //=====================================================================//
        //Läs XML-filen                                                        //
        //=====================================================================//
        public void ReadXml() {
            try {
                alarmXMLRef.Clear();
                XDocument doc = XDocument.Load(xmlPath + @"\Alarms.xml");
                var alarms = from alarm in doc.Descendants("Alarm")
                             select new
                             {
                                 ID = alarm.Attribute("id").Value,
                                 Name = alarm.Element("name").Value,
                                 PhoneNumber = alarm.Element("phoneNumber").Value,
                                 CaseNumber = alarm.Element("caseNumber").Value,
                                 Year = alarm.Element("year").Value,
                                 Month = alarm.Element("month").Value,
                                 Day = alarm.Element("day").Value,
                                 Hour = alarm.Element("hour").Value,
                                 Minute = alarm.Element("minute").Value,
                             };

                foreach (var alarm in alarms)
                {
                    AlarmXML alarmRef = new AlarmXML();
                    alarmRef.Id = Int32.Parse(alarm.ID);
                    alarmRef.Name = alarm.Name;
                    alarmRef.PhoneNumber = alarm.PhoneNumber;
                    alarmRef.CaseNumber = alarm.CaseNumber;
                    alarmRef.Year = Int32.Parse(alarm.Year);
                    alarmRef.Month = Int32.Parse(alarm.Month);
                    alarmRef.Day = Int32.Parse(alarm.Day);
                    alarmRef.Hour = Int32.Parse(alarm.Hour);
                    alarmRef.Minute = Int32.Parse(alarm.Minute);
                    

                    alarmXMLRef.Add(alarmRef);
                }
            }
            catch(Exception e) {
                Console.WriteLine(e.Message);
            }
        }

        //Skapa XML dokument
        public void CreateXML() {

            if (File.Exists(xmlPath + @"\Alarms.xml"))
            {
                Console.WriteLine("File Already Exists");
            }
            else
            {
                using (XmlWriter writer = XmlWriter.Create(xmlPath + @"\Alarms.xml"))
                {
                    writer.WriteStartDocument();
                    writer.WriteStartElement("Alarms");
                }
            }
        }
        
        //Lägg till alarm
        public void addAlarm(int id, string name, string phoneNumber, string caseNumber, int year, int month, int day, int _hour, int _minute) {
            try
            {
                XDocument doc = XDocument.Load(xmlPath + @"\Alarms.xml");
                XElement alarm = doc.Element("Alarms");
                alarm.Add(new XElement("Alarm",
                    //Testat lägga till Attribute
                            new XAttribute("id", id),
                            new XElement("name", name),
                            new XElement("phoneNumber", phoneNumber),
                            new XElement("caseNumber", caseNumber),
                            new XElement("year", year),
                            new XElement("month", month),
                            new XElement("day", day),
                            new XElement("hour", _hour),
                            new XElement("minute", _minute)));

                doc.Save(xmlPath + @"\Alarms.xml");


            }
            catch(Exception e) {
                Console.WriteLine(e.Message.ToString());
            }
        }

        private void lb_Alarms_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        //Visar alarmlistan
        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (!bSeeAlarms)
            {
                MainWindowProp.Width = 447;
                bSeeAlarms = true;
                b_ShowAlarms.Margin = new Thickness(407, 431, -32, -22);
            }
            else {

                MainWindowProp.Width = 228;
                bSeeAlarms = false;
                b_ShowAlarms.Margin = new Thickness(191, 431, -32, -22);
            }
        }
    }
}
