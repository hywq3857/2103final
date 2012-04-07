/*************************************************************************** 
 * Class name:   Budget                                                    *
 *                                                                         *
 * Author:  NUS CS2103 Project Group 30                                    *
 *                                                                         *
 * Purpose:  Store the budget of an event                                  *
 *                                                                         *
 * Usage:   Reuqested by budgetManagement or budgetNotification            *
 *                                                                         *
 ***************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Budgets
{
    struct Item
    {
        public string name;
        public double amountSpent;
        public double amountTotal;
    };

    class Budget
    {

        int eventID;
        int numItem;
        double totalBudget;
        double budgetSpent;
        public List<Item> budgetItem = new List<Item>();

        //constructor
        public Budget(int id)
        {
            eventID = id;
            numItem = 0;
            totalBudget = 0.0;
            budgetSpent = 0.0;
        }

        //modifiers
        public void setEventID(int id) { eventID = id; }
        public void setNumItem(int n) { numItem = n; }
        public void setTotalBudget(double t) { totalBudget = t; }
        public void setBudgetSpent(double s) { budgetSpent = s; }

        //modifiers of budget items
        public void addBudgetItem(string name, double spent, double total)
        {
            //add one item to the list
            Item tempItem;
            tempItem.name = name;
            tempItem.amountSpent = spent;
            tempItem.amountTotal = total;
            budgetItem.Add(tempItem);

            //modifty the attributes of budget accordingly
            numItem++;
            totalBudget += total;
            budgetSpent += spent;
        }
        
        public void setBudgetItem(int index, string name, double spent, double total)
        {
            //modify the original total amount and spent amount
            totalBudget -= budgetItem[index].amountTotal;
            budgetSpent -= budgetItem[index].amountSpent;

            Item tempItem;
            tempItem.name = name;
            tempItem.amountSpent = spent;
            tempItem.amountTotal = total;
            budgetItem[index] = tempItem;

            //modify the total amount and amount spent with the new item
            totalBudget += total;
            budgetSpent += spent;
        }
        
        public void deleteBudgetItem(int index)
        {
            //reduce the total budget and spent budget accordingly
            totalBudget -= budgetItem[index].amountTotal;
            budgetSpent -= budgetItem[index].amountSpent;
            budgetItem.RemoveAt(index);
            numItem--;
        }

        //get methods
        public int getEventID() { return eventID; }
        public int getNumItem() { return numItem; }
        public double getTotalBudget() { return totalBudget; }
        public double getBudgetSpent() { return budgetSpent; }
        public Item getBudgetItem(int n){ return budgetItem[n]; }

        //other methods
        public bool ifAvailable()
        {
            if (totalBudget >= budgetSpent)
                return true;
            else
                return false;
        }
    }
}
