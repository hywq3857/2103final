using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RegistrationMgt
{
    class Registration
    {
        private string eventName;
        private string studentName;
        private string studentID;
        private string status;//"Pending","Rejected","Accepted","Meaningless"
        private string statusChangeRecords="";
        private bool changeIndicator;//1 means change and 0 means no change


        //constructor
        public Registration(int type, string stdName, string stdID, string evtName = "Event Name", string state = "Pending", bool indicator = false)
        {
            studentID = stdID;
            studentName = stdName;
            eventName = evtName;
            status = state;
            changeIndicator = indicator;

            if (state == "Accepted")
                statusChangeRecords += "a";
            else if (state == "Rejected")
                statusChangeRecords+="r";
            else if (state == "Pending")
                statusChangeRecords+="p";
        }

        public Registration(string evtName, string stdName = " ", string stdID = " ", string state = "Pending", bool indicator = false)
        {
            eventName = evtName;
            studentName = stdName;
            studentID = stdID;
            status = state;
            changeIndicator = indicator;

            if (state == "Accepted")
                statusChangeRecords += "a";
            else if (state == "Rejected")
                statusChangeRecords += "r";
            else if (state == "Pending")
                statusChangeRecords += "p";
        }

        public string getEventName()
        {
            return eventName;
        }

        public string getStudentName()
        {
            return studentName;
        }

        public string getStudentID()
        {
            return studentID;
        }

        public string getStatus()
        {
            return status;
        }

        public void setStatus(string state)
        {

            status = state;
            if (state == "Accepted")
                statusChangeRecords += "a";
            else if (state == "Rejected")
                statusChangeRecords += "r";
            else if (state == "Pending")
                statusChangeRecords += "p";
        }

        public void setChangeIndicator(bool indicator) 
        {
            changeIndicator = indicator;
        }

        public bool getChangeIndicator()
        {

            if (statusChangeRecords.Count() == 1||statusChangeRecords.Count()==0)
                return changeIndicator;
            else
            {
                if (statusChangeRecords.ElementAt(0) == statusChangeRecords.ElementAt(statusChangeRecords.Count() - 1))
                    changeIndicator = false;
                else
                    changeIndicator = true;
                return changeIndicator;
            }
        }

        public string toStringForOrganizer()
        {
            string s = studentName + ',' + studentID + ',' + status;
            return s;
        }

    }

}
