using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Collections;
using System.Text;

namespace TaskMgt
{

    class TaskManagement
    {
        private string eventName;
        private int totalNumOfTasks;
        private int totalNumOfDoneTasks;
        private string fileName;
        public List<Task> taskList = new List<Task>();

        public TaskManagement(string eName, string fName)
        {
            eventName = eName;
            fileName = fName;
            using (StreamReader reader = new StreamReader(fileName))
            {
                string taskLine;
                int count1 = 0;
                int count2 = 0;
                while ((taskLine = reader.ReadLine()) != null)
                {
                    string[] taskParts = taskLine.Split('|');
                    if (taskParts[0] == eventName)
                    {
                        taskList.Add(new Task(eventName, taskParts[1], taskParts[2], taskParts[3], taskParts[4]));
                        count1++;
                        if (taskParts[4] == "Finished")
                        {
                            count2++;
                        }
                    }
                }
                totalNumOfTasks = count1;
                totalNumOfDoneTasks = count2;
            }
            sortAll();
        }

        #region get method

        public int getTotalNumOfTasks()
        {
            return totalNumOfTasks;
        }

        public int getTotalNumOfDoneTasks()
        {
            return totalNumOfDoneTasks;
        }

        public string getEventName()
        {
            return eventName;
        }

        public string getPersonName(int index) 
        {
            return taskList[index].getPersonName();
        }

        public string getTaskName(int index) 
        {
            return taskList[index].getTaskName();
        }

        public string getDueDate(int index) 
        {
            return taskList[index].getDueDate();
        }

        public string getStatus(int index) 
        {
            return taskList[index].getTaskStatus();
        }

        #endregion

        #region operations on task

        public void updateTaskStatus(string tName, string state)
        {


            for (int i = 0; i < taskList.Count; i++)
            {
                if (taskList[i].getTaskName() == tName)
                {
                    if (taskList[i].getTaskStatus() == state)
                    {
                        return;
                    }
                    else
                    {
                        if (state == "Finished")
                            totalNumOfDoneTasks++;
                        else if (state == "Unfinished")
                            totalNumOfDoneTasks--;
                        taskList[i].setTaskStatus(state);
                    }
                    break;
                }
            }
            sortAll();
        }

        public void addTask()
        {
            taskList.Add(new Task(eventName));
            totalNumOfTasks++;
            sortAll();
        }

        public void editATask(int index, string pName,string tName,string dueDate,string status)
        {
            taskList[index].setPersonName(pName);
            taskList[index].setTaskName(tName);
            taskList[index].setDueDate(dueDate);
            updateTaskStatus(tName, status);
            sortAll();
        }

        public void deleteTask(string tName)
        {
            for (int i = 0; i < taskList.Count; i++)
            {
                if (taskList[i].getTaskName() == tName)
                {
                    if (taskList[i].getTaskStatus() == "Finished")
                    {
                        totalNumOfDoneTasks--;
                    }
                    taskList.RemoveAt(i);
                    break;
                }
            }
            totalNumOfTasks--;
            sortAll();
        }

        public void deleteTask(int index)
        {
            if (taskList[index].getTaskStatus() == "Finished")
            {
                totalNumOfDoneTasks--;
            }
            taskList.RemoveAt(index);
            totalNumOfTasks--;
            //sortTaskList();
        }

        public void clearTaskList() 
        {
            while (taskList.Count != 0)
                deleteTask(0);
        }

        #endregion

        #region sort taskList

        public void sortTaskListByDueDate() 
        {
            for (int i = 0; i < taskList.Count; i++)
            {
                for (int j = 0; j < taskList.Count - 1; j++)
                {
                    if (taskList[j].getDueDate().CompareTo(taskList[j+1].getDueDate())>0)
                    {
                        Task temp = taskList[j + 1];

                        taskList[j + 1] = taskList[j];

                        taskList[j] = temp;
                    }
                }
            }
        }

        public void sortTaskListByPersonName() 
        {
            for (int i = 0; i < taskList.Count; i++)
            {
                for (int j = 0; j < taskList.Count - 1; j++)
                {
                    if (taskList[j].getPersonName().CompareTo(taskList[j + 1].getPersonName()) > 0)
                    {
                        Task temp = taskList[j + 1];

                        taskList[j + 1] = taskList[j];

                        taskList[j] = temp;
                    }
                }
            }
        }

        public void sortTaskListByTaskName() 
        {
            for (int i = 0; i < taskList.Count; i++)
            {
                for (int j = 0; j < taskList.Count - 1; j++)
                {
                    if (taskList[j].getTaskName().CompareTo(taskList[j + 1].getTaskName()) > 0)
                    {
                        Task temp = taskList[j + 1];

                        taskList[j + 1] = taskList[j];

                        taskList[j] = temp;
                    }
                }
            }
        }

        public void sortTaskListByTaskStatus() 
        {
            for (int i = 0; i < taskList.Count; i++)
            {
                for (int j = 0; j < taskList.Count - 1; j++)
                {
                    if (taskList[j].getTaskStatus().CompareTo(taskList[j + 1].getTaskStatus()) < 0)
                    {
                        Task temp = taskList[j + 1];

                        taskList[j + 1] = taskList[j];

                        taskList[j] = temp;
                    }
                }
            }
        }
        
        public void sortAll() 
        {
            sortTaskListByTaskStatus();
            sortTaskListByTaskName();
            sortTaskListByPersonName();
            sortTaskListByDueDate();
        }

        #endregion

        public string toString(int index)
        {
            return taskList[index].toString();
        }

        public void saveChangesToFile()
        {
            int i = 0;
            using (StreamReader reader = new StreamReader(fileName))
            using (StreamWriter writer = new StreamWriter("temp.txt"))
            {
                string taskLine;
                while ((taskLine = reader.ReadLine()) != null)
                {
                    string[] taskParts = taskLine.Split('|');
                    if (taskParts[0] != eventName)
                    {
                        writer.WriteLine(taskLine);
                    }
                    else if (taskList.Count != 0)
                    {
                        if (i < taskList.Count)
                        {
                            string temp = "";
                            temp += taskList[i].getEventName() + '|' + taskList[i].getPersonName() + '|' + taskList[i].getTaskName() + '|' + taskList[i].getDueDate() + '|' + taskList[i].getTaskStatus();
                            writer.WriteLine(temp);
                        }
                        i++;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            using (StreamWriter w = File.AppendText("temp.txt"))
            {
                for (; i < taskList.Count; i++)
                {
                    //string s = studentID;
                    string s = "";
                    s += taskList[i].getEventName() + '|' + taskList[i].getPersonName() + '|' + taskList[i].getTaskName() + '|' + taskList[i].getDueDate() + '|' + taskList[i].getTaskStatus();
                    w.WriteLine(s);
                }
            }
            using (StreamReader re = new StreamReader("temp.txt"))
            using (StreamWriter wr = new StreamWriter(fileName))
            {
                string taskLine;
                while ((taskLine = re.ReadLine()) != null)
                {
                    wr.WriteLine(taskLine);
                }
            }
        }
    }
}




