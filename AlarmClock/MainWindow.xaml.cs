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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DateTime alarmTime = new DateTime();
        DateTime currentTime = new DateTime();
        AlarmProps alarmProps = new AlarmProps();
        TimeSpan tiSp = new TimeSpan();
        int hour = 0;
        int minute = 0;

        public MainWindow()
        {
            InitializeComponent();
            FillHourAndMinute();
        }

        public void setAlarm(String name, String phoneNumber, String currentCase, DateTime alarm)
        {
            //Sätter alarmet
            alarmProps.Name = name;
            alarmProps.Phone = phoneNumber;
            alarmProps.CurrentCase = currentCase;
            alarmProps.Alarm = alarm;
        }

        public void controlDate() {
            if (PickDate.SelectedDate != null) {
                alarmTime = new DateTime(PickDate.SelectedDate.Value.Year, PickDate.SelectedDate.Value.Month, PickDate.SelectedDate.Value.Day, hour, minute, 0, 0, DateTimeKind.Local);
            }

        }

        //Fyll cb_hour och cb_minute med data
        private void FillHourAndMinute() {
            for (var i = 0; i < 25; i++) {
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
                Console.WriteLine(alarmTime.ToString());
            }

            getTime(hour, minute);
        }

        private void getTime(int alarmHour, int alarmMinute) {
            get

            if (alarmHour == hour && alarmMinute == minute)
        }
    }
}
