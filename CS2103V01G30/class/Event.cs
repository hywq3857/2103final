/*************************************************************************** 
 * Class name:   Event                                                     *
 *                                                                         *
 * Author:  NUS CS2103 Project Group 30                                    *
 *                                                                         *
 * Purpose:  Store the infomation of an evnet                              *
 *                                                                         *
 * Usage:   Called by class EventManagement or EventNotif                  *
 *                                                                         *
 ***************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EventManagement
{
    class Event
    {
        int eventID;
        string eventName;
        int startDate;
        int endDate;
        int startTime;
        int endTime;
        string venue;
        string description;

        //constructor
        public Event(int id, string name, int sDate, int eDate, int sTime, int eTime, string v, string des)
        {
            eventID = id;
            eventName = name;
            startDate = sDate;
            endDate = eDate;
            startTime = sTime;
            endTime = eTime;
            venue = v;
            description = des;
        }

        //set methods (modifiers)
        public void setEventID(int id) { eventID = id; }
        public void setEventName(string name) { eventName = name; }
        public void setStartDate(int date) { startDate = date; }
        public void setEndDate(int date) { endDate = date; }
        public void setStartTime(int start) { startTime = start; }
        public void setEndTime(int end) { endTime = end; }
        public void setVenue(string v) { venue = v; }
        public void setDescription(string d) { description = d; }

        //get methods
        public int getEventID() { return eventID; }
        public string getEventName() { return eventName; }
        public int getStartDate() { return startDate; }
        public int getEndDate() { return endDate; }
        public int getStartTime() { return startTime; }
        public int getEndTime() { return endTime; }
        public string getVenue() { return venue; }
        public string getDescription() { return description; }

        //check if there is any clash in terms of time and venue
        public bool ifClash(Event eventObj)
        {
            string str1 = eventName;
            string str2 = eventObj.getEventName();
            str1 = str1.ToLower();
            str2 = str2.ToLower();
            
            if (str1 == str2)
                return true;
            else if (startDate == eventObj.getStartDate() && endDate == eventObj.getEndDate())
            {
                if (startTime <= eventObj.getEndTime() || endTime >= eventObj.getStartTime())
                {
                    //store two venues in two strings then transform them into lower cases
                    string str3 = venue;
                    string str4 = eventObj.getVenue();
                    str3 = str3.ToLower();
                    str4 = str4.ToLower();
                    if (String.Compare(str3, str4) == 0)
                        return true;
                    else
                        return false;
                }
                else
                    return false;
            }
            else
                return false;
        }
    }
}
