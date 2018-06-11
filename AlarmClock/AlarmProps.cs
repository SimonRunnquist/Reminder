using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlarmClock
{
    class AlarmProps
    {
        private String name;
        private String phone;
        private String currentCase;
        private DateTime alarm;


        public String Name {
            get {
                return name;
            }
            set {
                name = value;
            }
        }

        public String Phone
        {
            get
            {
                return phone;
            }
            set
            {
                phone = value;
            }
        }

        public String CurrentCase
        {
            get
            {
                return currentCase;
            }
            set
            {
                currentCase = value;
            }
        }


        public DateTime Alarm
        {
            get
            {
                return alarm;
            }
            set
            {
                alarm = value;
            }
        }
    }
}
