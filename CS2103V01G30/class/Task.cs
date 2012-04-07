using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TaskMgt
{
    class Task
    {

        private string eventName;
        private string personName;
        private string taskName;
        private string dueDate;
        private string taskStatus;//Finished/Unfinished

        //constructor
        public Task(string evtName, string pName = "Person", string tName = "Task", string dDate = "Due Date", string state ="Unfinished")
        {
            eventName = evtName;
            personName = pName;
            taskName = tName;
            dueDate = dDate;
            taskStatus = state;
        }
       
        //get method
        public string getEventName()
        {
            return eventName;
        }
       
        public string getPersonName()
        {
            return personName;
        }

        public string getTaskName()
        {
            return taskName;
        }

        public string getDueDate()
        {
            return dueDate;
        }

        public string getTaskStatus()
        {
            return taskStatus;
        }

        //set method
        public void setPersonName(string pName)
        {
            personName = pName;
        }

        public void setTaskName(string tName)
        {
            taskName = tName;
        }

        public void setDueDate(string dDate)
        {
            dueDate = dDate;
        }

        public void setTaskStatus(string state)
        {
            taskStatus = state;
        }

        public string toString() 
        {
            string str = dueDate + ","+taskName + ","+personName + "," + taskStatus;
            return str;
        }
    }
}

