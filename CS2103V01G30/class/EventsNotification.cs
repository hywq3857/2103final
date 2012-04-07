using System;
using System.Windows;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EventManagement
{
    class EventsNotification
    {
        private string myEventsPath;
        public List<Event> myHistoryEventList = new List<Event>();
        public List<string> notificationList = new List<string>();
        
        public EventsNotification(string path)
	    {
            myEventsPath = path;
	    }

        public void readFromFile()
        {
            using (StreamReader reader = new StreamReader(myEventsPath))
            {
                myHistoryEventList.Clear();
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] parts = line.Split('|');
                    int id = Convert.ToInt32(parts[0]);
                    string name = parts[1];
                    int sDate = Convert.ToInt32(parts[2]);
                    int eDate = Convert.ToInt32(parts[3]);
                    int sTime = Convert.ToInt32(parts[4]);
                    int eTime = Convert.ToInt32(parts[5]);
                    string v = parts[6];
                    string des = parts[7];
                    Event tempEvent = new Event(id, name, sDate, eDate, sTime, eTime, v, des);
                    myHistoryEventList.Add(tempEvent);
                }
            }
        }

        public void writeToFile(List<Event> myEventList)
        {
            //write all event objects into file
            using (StreamWriter writer = new StreamWriter(myEventsPath))
            {
                int i;
                for (i = 0; i < myEventList.Count-1; i++)
                {
                    writer.Write("{0}", myEventList[i].getEventID());
                    writer.Write("|{0}", myEventList[i].getEventName());
                    writer.Write("|{0}", myEventList[i].getStartDate());
                    writer.Write("|{0}", myEventList[i].getEndDate());
                    writer.Write("|{0}", myEventList[i].getStartTime());
                    writer.Write("|{0}", myEventList[i].getEndTime());
                    writer.Write("|{0}", myEventList[i].getVenue());
                    writer.Write("|{0}", myEventList[i].getDescription());
                    writer.WriteLine();
                }
                writer.Write("{0}", myEventList[i].getEventID());
                writer.Write("|{0}", myEventList[i].getEventName());
                writer.Write("|{0}", myEventList[i].getStartDate());
                writer.Write("|{0}", myEventList[i].getEndDate());
                writer.Write("|{0}", myEventList[i].getStartTime());
                writer.Write("|{0}", myEventList[i].getEndTime());
                writer.Write("|{0}", myEventList[i].getVenue());
                writer.Write("|{0}", myEventList[i].getDescription());
            }
        }

        public void recordDeletedEvent(List<Event> myEventList)
        {
           foreach (Event myEvent in myHistoryEventList)
           {
              bool isDeleted=true;
               for (int i = 0; i < myEventList.Count; i++)
               {
                   if (myEvent.getEventID() == myEventList[i].getEventID())
                   {
                       isDeleted = false;
                       break;
                   }
                }
               if (isDeleted == true) 
               {
                   notificationList.Add("Your event ("+myEvent.getEventName() + ") has been deleted.");
               }
            }
        }

        public void recordAddedEvent(List<Event> myEventList)
        {
            foreach (Event myEvent in myEventList)
            {
                bool isAdded = true;
                for (int i = 0; i < myHistoryEventList.Count; i++)
                {
                    if (myEvent.getEventID() == myHistoryEventList[i].getEventID())
                    {
                        isAdded = false;
                        break;
                    }
                }
                if (isAdded == true)
                {
                    //additionList.Add(myEvent.getEventName());
                    notificationList.Add("Event (" + myEvent.getEventName() + ") has been added to your events list.");
                }
            }
        }

        public bool compareEvents(Event a, Event b)
        {
            if (a.getEventName() != b.getEventName())
                return false;
            if (a.getStartDate() != b.getStartDate())
                return false;
            if (a.getEndDate() != b.getEndDate())
                return false;
            if (a.getStartTime() != b.getStartTime())
                return false;
            if (a.getEndTime() != b.getEndTime())
                return false;
            if (a.getVenue() != b.getVenue())
                return false;
            if (a.getDescription() != b.getDescription())
                return false;
            else return true;
        }
        
        public void recordChangedEvent(List<Event> myEventList)
        {
            foreach (Event myEvent in myHistoryEventList) 
            {
                for (int i = 0; i < myEventList.Count; i++)
                {
                    if (myEvent.getEventID() == myEventList[i].getEventID())
                    {
                        if(!compareEvents(myEvent,myEventList[i]))
                        {
                            notificationList.Add("Your event (" + myEvent.getEventName() + ") has been modified.");
                            break;
                        }
                    }
                }
             
            }
        }

        public void eventNotification(List<Event> myEventList)
        {
            recordDeletedEvent(myEventList);
            recordAddedEvent(myEventList);
            recordChangedEvent(myEventList);
        }
    }
}
