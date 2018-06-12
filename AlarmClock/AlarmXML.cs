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
        DateTime _callDate = new DateTime();

        public void saveAlarm(int id, string name, string phoneNumber, string caseNumber, DateTime callDate) {

            this._id = id;
            this._name = name;
            this._phoneNumber = phoneNumber;
            this._caseNumber = caseNumber;
            this._callDate = callDate;

        }

        public int Id { get { return _id; } }
        public string Name { get { return _name; } }
        public string PhoneNumber { get { return _phoneNumber; } }
        public string CaseNumber { get { return _caseNumber; } }
        public DateTime CallDate { get { return _callDate; } }
    }
}
