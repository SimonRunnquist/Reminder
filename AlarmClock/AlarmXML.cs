using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Xml.Linq;
using System.Xml;

namespace AlarmClock
{
    class AlarmXML
    {
        int _id;
        string _name;
        string _phoneNumber;
        string _caseNumber;
        int _year;
        int _month;
        int _day;
        int _hour;
        int _minute;

        public void saveAlarm(int id, string name, string phoneNumber, string caseNumber, int year, int month, int day, int hour, int minute) {

            this._id = id;
            this._name = name;
            this._phoneNumber = phoneNumber;
            this._caseNumber = caseNumber;
            this._year = year;
            this._month = month;
            this._day = day;
            this._hour = hour;
            this._minute = minute;


        }

        public int Id { get { return _id; } set { this._id = value; } }
        public string Name { get { return _name; } set { this._name = value; } }
        public string PhoneNumber { get { return _phoneNumber; } set { this._phoneNumber = value; } }
        public string CaseNumber { get { return _caseNumber; } set { this._caseNumber = value; } }
        public int Year { get { return _year; } set { this._year = value; } }
        public int Month { get { return _month; } set { this._month = value; } }
        public int Day { get { return _day; } set { this._day = value; } }
        public int Hour { get { return _hour; } set { this._hour = value; } }
        public int Minute { get { return _minute; } set { this._minute = value; } }

        /*
        public override string ToString()
        {
            return Name;
        }
        */
    }
}
