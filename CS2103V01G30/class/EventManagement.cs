//Editing of events and budget
//Coded by Wan Wenli Simon
using System;
using System.Windows;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EventManagement
{
    //main method
    class EventMgt
    {
        public int numEvents = 0;
        public List<Event> eventList = new List<Event>();
        public string eventNameList = "";

        public void readFromFile()
        {
            //read in the information of events from the file
            using (StreamReader reader = new StreamReader("events.txt"))
            {
                eventList.Clear();
                eventNameList = "";
                numEvents = 0;

                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    //split every line with the character '|'
                    string[] parts = line.Split('|');

                    //create a list of event object
                    int id = Convert.ToInt32(parts[0]);
                    string name = parts[1];
                    int sDate = Convert.ToInt32(parts[2]);
                    int eDate = Convert.ToInt32(parts[3]);
                    int sTime = Convert.ToInt32(parts[4]);
                    int eTime = Convert.ToInt32(parts[5]);
                    string v = parts[6];
                    string des = parts[7];
                    Event tempEvent = new Event(id, name, sDate, eDate, sTime, eTime, v, des);
                    //add the event object to eventList
                    eventList.Add(tempEvent);
                    if (tempEvent.getEventID() != 1)
                        eventNameList += ",";
                    eventNameList += tempEvent.getEventName();

                    //increment the total number of events by 1
                    numEvents++;
                }
            }
        }

        //create a new event
        public int createEvent(string name, int startDate, int endDate, int startTime, int endTime, string venue, string description)
        {
            int id;

            //if the number of events is NOT 0, the current index is the index of last event in the list plus 1
            if (numEvents != 0)
                id = eventList[numEvents - 1].getEventID() + 1;
            //otherwise the event index starts from 1
            else
                id = 1;

            Event tempEvent = new Event(id, name, startDate, endDate, startTime, endTime, venue, description);

            //check if the event just created clashes with the existing events or not
            bool clash = false;
            for (int i = 0; i < numEvents; i++)
            {
                if (eventList[i].ifClash(tempEvent))
                {
                    clash = true;
                    i = numEvents;
                }
            }

            //if there is a clash, generate a warning message
            //if there is no, then create push the event object to the list and update total number of events
            if (clash)
            {
                MessageBox.Show("There is a clash in terms of name or time and venue.");
                return 0;
            }
            else
            {
                eventList.Add(tempEvent);
                numEvents++;
                return id;
            }
        }

        public bool editEvent(int id, string name, int startDate, int endDate, int startTime, int endTime, string venue, string description)
        {

            Event tempEvent = new Event(id, name, startDate, endDate, startTime, endTime, venue, description);

            //check if the event just edited clashes with OTHER the existing events or not
            bool clash = false;
            
            for (int i = 0; i < numEvents; i++)
            {
                if (eventList[i].getEventID() != id && eventList[i].ifClash(tempEvent))
                {
                    clash = true;
                    break;
                }
            }

            //search through the event list and replace the original event with the new one
            if (clash)
            {
                MessageBox.Show("There is a clash in terms of time and venue.");
                return false;
            }
            else
            {
                for (int i = 0; i < numEvents; i++)
                {
                    if (eventList[i].getEventID() == id)
                        eventList[i] = tempEvent;
                }
                return true;
            }
        }

        //delete an event
        public void deleteEvent(int id)
        {
            //delete the event with a certain event ID and update the total number of events               
            for (int i = 0; i < eventList.Count; i++)
                if (id == eventList[i].getEventID())
                {
                    eventList.RemoveAt(i);
                    numEvents--;
                    break;
                }
        }

        //write the list of events to a file
        public void writeToFile()
        {
            using (StreamWriter writer = new StreamWriter("events.txt"))
            {
                int i;
                for (i = 0; i < numEvents - 1; i++)
                {
                    writer.Write("{0}", eventList[i].getEventID());
                    writer.Write("|{0}", eventList[i].getEventName());
                    writer.Write("|{0}", eventList[i].getStartDate());
                    writer.Write("|{0}", eventList[i].getEndDate());
                    writer.Write("|{0}", eventList[i].getStartTime());
                    writer.Write("|{0}", eventList[i].getEndTime());
                    writer.Write("|{0}", eventList[i].getVenue());
                    writer.Write("|{0}", eventList[i].getDescription());
                    writer.WriteLine();
                }
                writer.Write("{0}", eventList[i].getEventID());
                writer.Write("|{0}", eventList[i].getEventName());
                writer.Write("|{0}", eventList[i].getStartDate());
                writer.Write("|{0}", eventList[i].getEndDate());
                writer.Write("|{0}", eventList[i].getStartTime());
                writer.Write("|{0}", eventList[i].getEndTime());
                writer.Write("|{0}", eventList[i].getVenue());
                writer.Write("|{0}", eventList[i].getDescription());
            }
        }
    }
}