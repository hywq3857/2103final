using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using System.IO;
using System.Text;

namespace RegistrationMgt
{
    class RegProcessing
    {
        private string eventName;
        private int totalNumOfReg;
        private int totalNumOfAccepted;
        private int totalNumOfRejected;
        public List<Registration> regProcessingList = new List<Registration>();

        public RegProcessing(string evtName)
        {
            eventName = evtName;
            using (StreamReader reader = new StreamReader("Registration.txt"))
            {
                string regProLine;
                int countNumOfReg = 0;
                int countNumOfAcc = 0;
                int countNumOfRej = 0;
                while ((regProLine = reader.ReadLine()) != null)
                {
                    string[] regParts = regProLine.Split('|');
                    if (regParts[2] == eventName)
                    {
                        regProcessingList.Add(new Registration(eventName, regParts[0], regParts[1], regParts[3], Convert.ToBoolean(Convert.ToInt16(regParts[4]))));
                        countNumOfReg++;
                        if (regParts[3] == "Accepted")
                        {
                            countNumOfAcc++;
                        }
                        else if (regParts[3] == "Rejected")
                        {
                            countNumOfRej++;
                        }
                    }
                }
                totalNumOfReg = countNumOfReg;
                totalNumOfAccepted = countNumOfAcc;
                totalNumOfRejected = countNumOfRej;
            }
        }

        #region get method

        public string getEventName() 
        {
            return eventName;
        }

        public int getTotalNumOfReg()
        {
            return totalNumOfReg;
        }

        public int getTotalNumOfAcc()
        {
            return totalNumOfAccepted;
        }

        public int getTotalNumOfRej()
        {
            return totalNumOfRejected;
        }

        public string getStudentName(int index) 
        {
            return regProcessingList[index].getStudentName();
        }

        public string getStudentID(int index)
        {
            return regProcessingList[index].getStudentID();
        }

        public string getStatus(int index)
        {
            return regProcessingList[index].getStatus();
        }

        #endregion

        #region operations on RegProList

        public void setRegStatus(int index, string state)
        {

            if (index < regProcessingList.Count && regProcessingList[index].getStatus() != state)
            {
                if (state == "Accepted")
                {
                    totalNumOfAccepted++;
                    if (regProcessingList[index].getStatus() == "Rejected")
                        totalNumOfRejected--;
                }
                else if (state == "Rejected")
                {
                    totalNumOfRejected++;
                    if (regProcessingList[index].getStatus() == "Accepted")
                        totalNumOfAccepted--;
                }
                else
                {
                    if (regProcessingList[index].getStatus() == "Rejected")
                        totalNumOfRejected--;
                    else
                        totalNumOfAccepted--;
                }
                regProcessingList[index].setStatus(state);

            }
        }

        public void deleteRegPro(int index)
        {
            if (index < regProcessingList.Count)
            {

                if (regProcessingList[index].getStatus() == "Accepted")
                {
                    totalNumOfAccepted--;
                }
                else if (regProcessingList[index].getStatus() == "Rejected")
                {
                    totalNumOfRejected--;
                }
                regProcessingList.RemoveAt(index);
                totalNumOfReg--;
            }
        }

        public void clearRegProList() 
        {
            for (int i = 0; i < regProcessingList.Count; i++)
                deleteRegPro(0);
        }

        #endregion

       /* public string toString(int indexInRegProcessingList)
        {
            return regProcessingList[indexInRegProcessingList].toStringForOrganizer();
        }*/

        public void saveChangesToFile()
        {
            //write all Reg objects into file
            using (StreamReader reader = new StreamReader("Registration.txt"))
            using (StreamWriter writer = new StreamWriter("NewReg.txt"))
            {
                string regProLine;
                int i = 0;
                while ((regProLine = reader.ReadLine()) != null)
                {
                    string[] regParts = regProLine.Split('|');
                    if (regParts[2] != eventName)
                    {
                        writer.WriteLine(regProLine);
                    }
                    else
                    {
                        if (i < regProcessingList.Count)
                        {
                            string temp = "";
                            temp += regProcessingList[i].getStudentName() + '|' + regProcessingList[i].getStudentID() + '|' + regProcessingList[i].getEventName() + '|' + regProcessingList[i].getStatus() + '|' + Convert.ToString(Convert.ToInt16(regProcessingList[i].getChangeIndicator()));
                            writer.WriteLine(temp);
                            i++;
                        }
                    }
                }
            }
            using (StreamReader re = new StreamReader("NewReg.txt"))
            using (StreamWriter wr = new StreamWriter("Registration.txt"))
            {
                string regProLine;
                while ((regProLine = re.ReadLine()) != null)
                    wr.WriteLine(regProLine);
            }
        }

    }
}
