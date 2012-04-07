using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Collections;
using System.Text;

namespace RegistrationMgt
{
    class RegEvent
    {
        private string studentName;
        private string studentID;
        private int totalNumOfReg;
        private int totalNumOfAccepted;
        private int totalNumOfRejected;
        public List<Registration> regEventList = new List<Registration>();

        public void saveChangesToFile()
        {
            int i = 0;
            using (StreamReader reader = new StreamReader("Registration.txt"))
            using (StreamWriter writer = new StreamWriter("NewReg.txt"))
            {
                string regEventLine;
                while ((regEventLine = reader.ReadLine()) != null)
                {
                    string[] regParts = regEventLine.Split('|');
                    if (regParts[0] != studentName || regParts[1] != studentID)
                    {
                        writer.WriteLine(regEventLine);
                    }
                    else if (regEventList.Count != 0)
                    {
                        if (i < regEventList.Count && regEventList[i].getStatus() != "Meaningless")
                        {
                            string temp = "";
                            temp += regEventList[i].getStudentName() + '|' + regEventList[i].getStudentID() + '|' + regEventList[i].getEventName() + '|' + regEventList[i].getStatus() + '|' + Convert.ToString(Convert.ToInt16(regEventList[i].getChangeIndicator()));
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
            using (StreamWriter w = File.AppendText("NewReg.txt"))
            {
                for (; i < regEventList.Count; i++)
                {
                    if (regEventList[i].getStatus() != "Meaningless")
                    {
                        // string s = studentID;
                        string s = "";
                        s += regEventList[i].getStudentName() + '|' + regEventList[i].getStudentID() + '|' + regEventList[i].getEventName() + '|' + regEventList[i].getStatus() + '|' + Convert.ToString(Convert.ToInt16(regEventList[i].getChangeIndicator()));
                        w.WriteLine(s);
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

        public void deleteRegEventList(int index)
        {
            if (index < regEventList.Count)
            {
                if (regEventList[index].getStatus() == "Accepted")
                {
                    totalNumOfAccepted--;
                }
                else if (regEventList[index].getStatus() == "Rejected")
                {
                    totalNumOfRejected--;
                }
                regEventList.RemoveAt(index);
                totalNumOfReg--;
            }
        }
       
        public RegEvent() { }
        
        public void setRegEvent(string stdName, string stdID)
        {
            studentName = stdName;
            studentID = stdID;
            using (StreamReader reader = new StreamReader("Registration.txt"))
            {
                string regEventLine;
                int countNumOfReg = 0;
                int countNumOfAcc = 0;
                int countNumOfRej = 0;
                while ((regEventLine = reader.ReadLine()) != null)
                {
                    string[] regEventParts = regEventLine.Split('|');
                    if (regEventParts[0] == studentName && regEventParts[1] == studentID)
                    {
                        regEventList.Add(new Registration(0, studentName, studentID, regEventParts[2], regEventParts[3],Convert.ToBoolean (Convert.ToInt16(regEventParts[4]))));
                        countNumOfReg++;
                        if (regEventParts[3] == "Accepted")
                        {
                            countNumOfAcc++;
                        }
                        else if (regEventParts[3] == "Rejected")
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

        public string getEventName(int indexInRegEventList) 
        {
            return regEventList[indexInRegEventList].getEventName();
        }

        public string getStatus(int indexInRegEventList) 
        {
            return regEventList[indexInRegEventList].getStatus();
        }

        public bool getChangeIndicator(int indexInRegEventList) 
        {
            return regEventList[indexInRegEventList].getChangeIndicator();
        }

        public void setChangeIndicator(int indexInRegEventList, bool indicator) 
        {
            regEventList[indexInRegEventList].setChangeIndicator(indicator);
        }

        public bool alreadyRegistered(string evtName)
        {
            bool indicator = false;//to check if the student has alrdy registered the event
            for (int i = 0; i < regEventList.Count; i++)
            {
                if (regEventList[i].getEventName() == evtName && regEventList[i].getStatus() != "Meaningless")
                {
                    indicator = true;
                    return indicator;
                }  
            }
            return indicator;
        }

        public void registerAnEvent(string evtName)
        {

            if (!alreadyRegistered(evtName))
            {
                regEventList.Add(new Registration(0, studentName, studentID, evtName));
                totalNumOfReg++;
            }
        }

        public bool checkExistance(int index)
        {
            if (regEventList[index].getStatus() == "Meaningless")
                return false;
            else return true;
        }

        public void cancelRegistration(string evtName)
        {
            for (int i = 0; i < regEventList.Count; i++)
            {
                if (regEventList[i].getEventName() == evtName && regEventList[i].getStatus() != "Meaningless")
                {
                    if (regEventList[i].getStatus() == "Accepted")
                        totalNumOfAccepted--;
                    else if (regEventList[i].getStatus() == "Rejected")
                        totalNumOfRejected--;

                    regEventList[i].setStatus("Meaningless");//do not really delete the Reg object in the list but set its eventID to -1 instead.
                    //this is to preserve the sequence when eventually saving changes to the txt file.
                    totalNumOfReg--;              //remember not to display the Reg object which has an eventID of -1.
                }

            }
        }

    }
}
