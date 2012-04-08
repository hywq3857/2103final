/****************************************************************************
 * Class name:   TaskNotification                                           *
 *                                                                          *
 * Author:  NUS CS2103 Project Group 30                                     *
 *                                                                          *
 * Purpose:  Generate notifications about updates in task for all my events *
 *                                                                          *
 * Usage:   Called by class Control                                         *
 *                                                                          *
 ****************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using TaskMgt;

namespace CS2103V01G30
{
	class TaskNotification
	{
        public List<string> notificationList=new List<string>();
        public TopTaskManagement topTaskMgtForNotification;

        public TaskNotification(string matricNo) 
        {
            string s = matricNo + "_Task.txt";
            if (!File.Exists(s))
            {
                using (FileStream fs = File.Create(s))
                fs.Close();
            }
            topTaskMgtForNotification = new TopTaskManagement(s);  
        }
        
        public void compareTopTaskManagement(TopTaskManagement topTaskMgt) 
        {
            for (int i = 0; i < topTaskMgtForNotification.getTotalNoOfTaskMgt(); i++) 
            {
               //for(int j=0;j<topTaskMgtForNotification.taskMgtList[i].getTotalNumOfTasks();j++)
                for (int j = 0; j < topTaskMgtForNotification.getNoOfTasksInATaskList(i); j++)
                  // if (topTaskMgtForNotification.taskMgtList[i].getTotalNumOfTasks() != topTaskMgt.taskMgtList[i].getTotalNumOfTasks() || topTaskMgtForNotification.taskMgtList[i].toString(j) != topTaskMgt.taskMgtList[i].toString(j))
                   if (topTaskMgtForNotification.getNoOfTasksInATaskList(i) != topTaskMgt.getNoOfTasksInATaskList(i) || topTaskMgtForNotification.toString(i,j) != topTaskMgt.toString(i,j))
                  {
                       //notificationList.Add("The task for your event ("+topTaskMgtForNotification.taskMgtList[i].getEventName()+") has been modified.");
                      notificationList.Add("The task for your event (" + topTaskMgtForNotification.getEventName(i) + ") has been modified.");
                       break;
                   }
            }
        }

        public void saveChangesToFile(TopTaskManagement topTaskMgt) 
        {
             using (StreamWriter writer = new StreamWriter(topTaskMgtForNotification.getFileName()))
            {
               for(int i=0;i<topTaskMgt.getTotalNoOfTaskMgt();i++)
                   for(int j=0;j<topTaskMgt.getNoOfTasksInATaskList(i);j++)
                        {
                            string temp = "";
                            temp += topTaskMgt.getEventName(i) + '|' + topTaskMgt.getPersonName(i,j) + '|' + topTaskMgt.getTaskName(i,j) + '|' + topTaskMgt.getDueDate(i,j) + '|' + topTaskMgt.getStatus(i,j);
                            writer.WriteLine(temp);
                        }
            }
        }

	}
}
