/******************************************************************************
 * Class name:   TopTaskManagement                                            *
 *                                                                            *
 * Author:  NUS CS2103 Project Group 30                                       *
 *                                                                            *
 * Purpose:  Store and process task info for all my events                    *
 *                                                                            *
 * Usage:   Called by class Control and TaskNotification                      *
 *                                                                            *
 ******************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TaskMgt
{
    class TopTaskManagement
    {
        string fileName;
        
        public List<TaskManagement> taskMgtList = new List<TaskManagement>();

        
        public TopTaskManagement(string fName) { fileName = fName; }

        #region get method

        public int getTotalNoOfTaskMgt() 
        {
            return taskMgtList.Count;
        }

        public string getFileName() 
        {
            return fileName;
        }

        public int getNoOfTasksInATaskList(string eventName)
        {
            //addATaskManagement(eventName);
            int index = findSelectedTaskMgt(eventName);
            return taskMgtList[index].getTotalNumOfTasks();
        }

        public int getNoOfTasksInATaskList(int index)
        {
            return taskMgtList[index].getTotalNumOfTasks();
        }

        public int getNoOfDoneTasksInATaskList(string eventName)
        {
            //addATaskManagement(eventName);
            int index = findSelectedTaskMgt(eventName);
            return taskMgtList[index].getTotalNumOfDoneTasks();
        }

        public string getEventName(int indexInTaskMgtList)
        {
            return taskMgtList[indexInTaskMgtList].getEventName();
        }

        public string getTaskName(int indexInTaskMgtList, int indexInTaskList)
        {
            return taskMgtList[indexInTaskMgtList].getTaskName(indexInTaskList);
        }

        public string getPersonName(int indexInTaskMgtList, int indexInTaskList)
        {
            return taskMgtList[indexInTaskMgtList].getPersonName(indexInTaskList);
        }

        public string getDueDate(int indexInTaskMgtList, int indexInTaskList)
        {
            return taskMgtList[indexInTaskMgtList].getDueDate(indexInTaskList);
        }

        public string getStatus(int indexInTaskMgtList, int indexInTaskList)
        {
            return taskMgtList[indexInTaskMgtList].getStatus(indexInTaskList);
        }

        public string getTaskName(string eventName, int indexInTaskList)
        {
            //addATaskManagement(eventName);
            int index = findSelectedTaskMgt(eventName);
            return taskMgtList[index].getTaskName(indexInTaskList);
        }

        public string getPersonName(string eventName, int indexInTaskList)
        {
            //addATaskManagement(eventName);
            int index = findSelectedTaskMgt(eventName);
            return taskMgtList[index].getPersonName(indexInTaskList);
        }

        public string getDueDate(string eventName, int indexInTaskList)
        {
            //addATaskManagement(eventName);
            int index = findSelectedTaskMgt(eventName);
            return taskMgtList[index].getDueDate(indexInTaskList);
        }

        public string getStatus(string eventName, int indexInTaskList)
        {
            //addATaskManagement(eventName);
            int index = findSelectedTaskMgt(eventName);
            return taskMgtList[index].getStatus(indexInTaskList);
        }
        # endregion

        public int findSelectedTaskMgt(string eventName)
        {
            for (int i = 0; i < taskMgtList.Count; i++)
            {
                if (taskMgtList[i].getEventName() == eventName)
                {
                    return i;
                }
            }
            return -1;
        }

        public void addATaskManagement(string eventName)
        {
            if (findSelectedTaskMgt(eventName) == -1)
                taskMgtList.Add(new TaskManagement(eventName,fileName));
        }

        public void deleteATaskManagement(string eventName) 
        {
            int index = findSelectedTaskMgt(eventName);
            if (index == -1)
                return;
            else
                taskMgtList[index].clearTaskList();
        }

        #region operations on taskList

        public void addTaskInTaskList(string eventName)
        {
            int index = findSelectedTaskMgt(eventName);
            taskMgtList[index].addTask();
        }

        public void deleteTaskInTaskList(string eventName, int indexInTaskList)
        {
            int index = findSelectedTaskMgt(eventName);
            taskMgtList[index].deleteTask(indexInTaskList);
        }

        public void editATaskInTaskList(string eventName,int indexInTaskList,string personName,string taskName,string dueDate,string status) 
        {
            int index = findSelectedTaskMgt(eventName);
            taskMgtList[index].editATask(indexInTaskList, personName, taskName, dueDate, status);    
        }

        #endregion

        public void saveAllChangesToFile()
        {
            for (int i = 0; i < taskMgtList.Count; i++)
                taskMgtList[i].saveChangesToFile();
        }

       /* public string toString(string eventName, int indexInTaskList)
        {
            addATaskManagement(eventName);
            int index = findSelectedTaskMgt(eventName);
            return taskMgtList[index].toString(indexInTaskList);
        }*/
        public string toString(int indexInTaskMgtList, int indexInTaskList)
        {
            return taskMgtList[indexInTaskMgtList].toString(indexInTaskList);
        }
    }
}


