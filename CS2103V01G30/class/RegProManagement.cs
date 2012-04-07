using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RegistrationMgt
{
    class RegProManagement
    {
        public List<RegProcessing> regProMgtList = new List<RegProcessing>();

        public RegProManagement() { }

        public int findSelectedEventRegPro(string eventName)
        {
            for (int i = 0; i < regProMgtList.Count; i++)
            {
                if (regProMgtList[i].getEventName() == eventName)
                {
                    return i;
                }
            }
            return -1;
        }

        #region get method

        public int getNoOfRegInARegProList(string eventName)
        {
            //addToRegProMgtList(eventName);
            return regProMgtList[findSelectedEventRegPro(eventName)].getTotalNumOfReg();
        }

        public int getNoOfAccInARegProList(string eventName)
        {
            //addToRegProMgtList(eventName);
            return regProMgtList[findSelectedEventRegPro(eventName)].getTotalNumOfAcc();
        }

        public int getNoOfRejInARegProList(string eventName)
        {
            //addToRegProMgtList(eventName);
            return regProMgtList[findSelectedEventRegPro(eventName)].getTotalNumOfRej();
        }

        public string getStudentName(string eventName, int index) 
        {
            //addToRegProMgtList(eventName);
            return regProMgtList[findSelectedEventRegPro(eventName)].getStudentName(index);
        }

        public string getStudentID(string eventName, int index)
        {
           // addToRegProMgtList(eventName);
            return regProMgtList[findSelectedEventRegPro(eventName)].getStudentID(index);
        }

        public string getStatus(string eventName, int index)
        {
            //addToRegProMgtList(eventName);
            return regProMgtList[findSelectedEventRegPro(eventName)].getStatus(index);
        }

        #endregion

        public void setRegStatus(string eventName, int index, string status) 
        {
            regProMgtList[findSelectedEventRegPro(eventName)].setRegStatus(index,status);
        }

        public void addToRegProMgtList(string eventName)
        {
            if (findSelectedEventRegPro(eventName) == -1)
                regProMgtList.Add(new RegProcessing(eventName));
        }

        public void deleteFromRegProMgtList(string eventName) 
        {
            regProMgtList[findSelectedEventRegPro(eventName)].clearRegProList();
        }

        public void saveAllChangesToFile()
        {
            for (int i = 0; i < regProMgtList.Count; i++)
                regProMgtList[i].saveChangesToFile();
        }

       /* public string toString(string eventName, int indexInRegProcessingList) 
        {
            return regProMgtList[findSelectedEventRegPro(eventName)].toString(indexInRegProcessingList);
        }*/

    }
}

