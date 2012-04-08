/*************************************************************************** 
 * Class name:   BudgetNotif                                               *
 *                                                                         *
 * Author:  NUS CS2103 Project Group 30                                    *
 *                                                                         *
 * Purpose:  Generate notifications about updates in budget                *
 *                                                                         *
 * Usage:   Automatically called every time then programs starts           *
 *                                                                         *
 ***************************************************************************/
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Budgets
{
	class BudgetNotif
	{
        string myBudgetPath;
        public List<Budget> myHistoryBudgetList = new List<Budget>();
        public List<int> notificationList = new List<int>();

        public BudgetNotif(string path)
        {
            myBudgetPath = path;
        }

        public void readFromFile()
        {
            using (StreamReader reader = new StreamReader(myBudgetPath))
            {
                myHistoryBudgetList.Clear();
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    //split every line with the character '|'
                    string[] parts = line.Split('|');

                    int id = Convert.ToInt32(parts[0]);
                    Budget tempBudget = new Budget(id);
                    int eventID = Convert.ToInt32(parts[0]);
                    for (int i = 1; i < parts.Length; i++)
                    {
                        if (i % 3 == 1)
                        {
                            //add a budget item to the budget
                            string name = parts[i];
                            double amountSpent = Convert.ToDouble(parts[i + 1]);
                            double amountTotal = Convert.ToDouble(parts[i + 2]);
                            tempBudget.addBudgetItem(name, amountSpent, amountTotal);
                        }
                    }
                    //push one budget item to the list
                    myHistoryBudgetList.Add(tempBudget);
                }
            }
        }

        public void writeToFile(List<Budget> myBudgetList)
        {
            //write all budget objects into file
            using (StreamWriter writer = new StreamWriter(myBudgetPath))
            {
                int i;
                for (i = 0; i < myBudgetList.Count - 1; i++)
                {
                    writer.Write(myBudgetList[i].getEventID());
                    int numItems = myBudgetList[i].getNumItem();

                    for (int j = 0; j < numItems; j++)
                    {
                        Item tempItem = myBudgetList[i].getBudgetItem(j);
                        writer.Write("|{0}|{1}|{2}", tempItem.name, tempItem.amountSpent, tempItem.amountTotal);
                    }
                    writer.WriteLine();
                }
                //write the last line without the next line character
                writer.Write(myBudgetList[i].getEventID());
                int numItem = myBudgetList[i].getNumItem();

                for (int j = 0; j < numItem; j++)
                {
                    Item tempItem = myBudgetList[i].getBudgetItem(j);
                    writer.Write("|{0}|{1}|{2}", tempItem.name, tempItem.amountSpent, tempItem.amountTotal);
                }
            }
        }

        public void budgetNotifications(List<Budget> myBudgetList)
        {
            //check if there is any addtional budget or change.
            for (int i = 0; i < myBudgetList.Count; i++)
            {
                foreach (Budget budget in myHistoryBudgetList)
                {
                    if (budget.getEventID() == myBudgetList[i].getEventID())
                    {
                        string myBudgetItems = "";
                        string myHistoryBudgetItems = "";

                        foreach (Item item in myBudgetList[i].budgetItem)
                        {
                            myBudgetItems += item.name;
                            myBudgetItems += item.amountSpent;
                            myBudgetItems += item.amountTotal;
                        }
                        foreach (Item item in budget.budgetItem)
                        {
                            myHistoryBudgetItems += item.name;
                            myHistoryBudgetItems += item.amountSpent;
                            myHistoryBudgetItems += item.amountTotal;
                        }

                        //if there is a difference between two strings, there is a change in the budget
                        if (String.Compare(myBudgetItems, myHistoryBudgetItems) != 0)
                            notificationList.Add(budget.getEventID());
                    }
                }
            }
        }
	}
}
