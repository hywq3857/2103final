/*************************************************************************** 
 * Class name:   BudgetMgmt                                                *
 *                                                                         *
 * Author:  NUS CS2103 Project Group 30                                    *
 *                                                                         *
 * Purpose:  Manage the budget of different events                         *
 *                                                                         *
 * Usage:   Called from class 'Control'                                    *
 *                                                                         *
 ***************************************************************************/
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Budgets
{
    class BudgetMgmt
    {
        public int numBudgets;
        public List<Budget> budgetList = new List<Budget>();

        public void readFromFile()
        {
            using (StreamReader reader = new StreamReader("budgets.txt"))
            {
                //initialization
                numBudgets = 0;
                budgetList.Clear();

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
                    budgetList.Add(tempBudget);

                    //increment the total number of events by 1
                    numBudgets++;
                }
            }
        }

        //add a budget item
        public void addItem(int id, string name, double amountSpent, double amountTotal)
        {
            for (int i = 0; i < numBudgets; i++)
            {
                if (id == budgetList[i].getEventID())
                {
                    int numItems = budgetList[i].getNumItem();

                    //create a budget item from parameters passed in
                    budgetList[i].addBudgetItem(name, amountSpent, amountTotal);

                    //update the number of items in the budget
                    numItems++;
                    budgetList[i].setNumItem(numItems);
                }
            }
        }

        //edit a budget item
        public void editItem(int id, int itemID, string name, double amountSpent, double amountTotal)
        {
            for (int i = 0; i < numBudgets; i++)
            {
                if (id == budgetList[i].getEventID())
                {
                    int numItems = budgetList[i].getNumItem();

                    //using set methods to modify the budget item
                    budgetList[i].setBudgetItem(itemID, name, amountSpent, amountTotal);
                }
            }
        }

        //delete a budget item
        public void deleteItem(int id, int itemID)
        {
            for (int i = 0; i < numBudgets; i++)
            {
                if (id == budgetList[i].getEventID())
                {
                    int numItems = budgetList[i].getNumItem();

                    //delete the budget item from the chosen budget
                    budgetList[i].deleteBudgetItem(itemID);

                    //update the number of items in the budget
                    numItems--;
                    budgetList[i].setNumItem(numItems);
                }
            }
        }

        //add a budget of an event
        public void createBudget()
        {
            int id;
            if (numBudgets != 0)
                id = budgetList[numBudgets - 1].getEventID() + 1;
            else
                id = 1;

            Budget tempBudget = new Budget(id);
            budgetList.Add(tempBudget);
            numBudgets++;
        }

        //delete a budget of an event
        public void deleteBudget(int id)
        {
            for (int i = 0; i < budgetList.Count; i++)
                if (id == budgetList[i].getEventID())
                    budgetList.RemoveAt(i);
            numBudgets--;
        }

        public void writeToFile()
        {
            //write all budget objects into file
            using (StreamWriter writer = new StreamWriter("budgets.txt"))
            {
                int i;
                for (i = 0; i < numBudgets - 1; i++)
                {
                    writer.Write(budgetList[i].getEventID());
                    int numItems = budgetList[i].getNumItem();

                    for (int j = 0; j < numItems; j++)
                    {
                        Item tempItem = budgetList[i].getBudgetItem(j);
                        writer.Write("|{0}|{1}|{2}", tempItem.name, tempItem.amountSpent, tempItem.amountTotal);
                    }
                    writer.WriteLine();
                }

                writer.Write(budgetList[i].getEventID());
                int numItem = budgetList[i].getNumItem();

                for (int j = 0; j < numItem; j++)
                {
                    Item tempItem = budgetList[i].getBudgetItem(j);
                    writer.Write("|{0}|{1}|{2}", tempItem.name, tempItem.amountSpent, tempItem.amountTotal);
                }


            }
        }
        public string displayItemName(int i, int n)
        {
            return budgetList[i].getBudgetItem(n).name;
        }
        public double displayItemSpent(int i, int n)
        {
            return budgetList[i].getBudgetItem(n).amountSpent;
        }
        public double displayItemTotal(int i, int n)
        {
            return budgetList[i].getBudgetItem(n).amountTotal;
        }
    }
}