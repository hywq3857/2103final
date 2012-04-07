using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.IO;


//students' data is stored in the file "students.txt" in the format shown below:
//name,matric number,password,e-mail,contact number,gender,created event id, joined event id

namespace users   
{

    class Users
    {

        public string name;
        public string username;
        public string password;
        public string email;
        public string gender;
        public string contact; 
        public List<int> createdEventList= new List<int>(); //list of events a student organized
        public List<int> joinedEventList=new List<int>();  //list of events a student joined
        public List<Users> userlist= new List<Users>(); //list of users objects

        //methods contained in this class:
        //1.set_EventLists(string matricNumber)---load data to createdEventList and joinedEventList of a particualr user
        //2.remove_oneCreatedEvent(string matricNumber,int id)---remove an event created by the user in "students.txt"
        //3.remove_oneJoinedEvent(string matricNumber,int id)-----remove an event joined by the user in "students.txt"
        //4.add_oneCreatedEvent(string matricNumber, int id)---add an event id of an event created by the user in "students.txt"
        //5.add_oneJoinedEvent(string matricNumber, int id)---add an event id of an event joined by the user in "students.txt"
        //6.checkIfMatricExist(string aMatricNumber)---to check if a matric number exist in "students.txt"
        //7.checkIfAlreadyTheOrganizer(string matric, int ID)--to check if the user is already the organizer of the organizer
        //8.load_person_info(string name)---to load a students information with the name given

        public void set_EventLists(string matricNumber) //*****method 1*****
        {
            createdEventList.Clear();
            joinedEventList.Clear();

            StreamReader SR;

            string S;

            SR = File.OpenText(@"students.txt");
            S = SR.ReadLine();

            string MatricNumber;      
            string createdID="";           //the id list of created events
            string joinedID="";            //the id list of joined events


            while (S != null)
            {
                string[] words = S.Split(',');

                MatricNumber = words[1];

                if(MatricNumber==matricNumber )
                {
                    createdID = words[6];
                    joinedID = words[7];
                    break;
                }
                S = SR.ReadLine();
               
            }

            string[] cids = createdID.Split('|');
            string[] jids = joinedID.Split('|');

            foreach (string cid in cids)
            {
                if (cid != "")
                    createdEventList.Add(Convert.ToInt16(cid));
            }

            foreach (string jid in jids)
            {
                if (jid != "")
                    joinedEventList.Add(Convert.ToInt16(jid));
            }

            SR.Close();


        }

        public void remove_oneCreatedEvent(string matricNumber, int id)  //***** method 2*****
        {
            StreamReader SR;

            string S;

            string MatricNumber;
            string NAME;
            string PASSWORD;
            string EMAIL;
            string CONTACT;
            string GENDER;
            string CLIST;
            string JLIST;

            List<string> lines = new List<string>();

            SR = File.OpenText(@"students.txt");
            S = SR.ReadLine();

            while (S != null)
            {
                string[] elements = S.Split(',');

                if (elements.Count() >= 8)
                {

                    MatricNumber = elements[1];

                    if (MatricNumber == matricNumber)
                    {
                        NAME = elements[0];
                        PASSWORD = elements[2];
                        EMAIL = elements[3];
                        CONTACT = elements[4];
                        GENDER = elements[5];
                        CLIST = elements[6];
                        JLIST = elements[7];

                        string[] cs = CLIST.Split('|');

                        List<int> c_list = new List<int>();

                        foreach (string c in cs)
                        {
                            c_list.Add(Convert.ToInt32(c));
                        }

                        c_list.Remove(id);

                        CLIST = "";
                        if (c_list.Count() > 0)
                        {
                            CLIST += c_list[0];
                        }
                        for (int z = 1; z < c_list.Count(); z++)
                        {
                            CLIST += "|";
                            CLIST += c_list[z];

                        }

                        S = NAME + "," + MatricNumber + "," + PASSWORD + "," + EMAIL + "," + CONTACT + "," + GENDER + "," + CLIST + "," + JLIST;


                    }
                }
                lines.Add(S);

                S = SR.ReadLine();

            }

            SR.Close();

            StreamWriter sw = new StreamWriter(@"students.txt");

            for (int i = 0; i < lines.Count(); i++)
            {
                sw.WriteLine(lines[i]);
            }

            sw.Close();

        }

        public void remove_oneJoinedEvent(string matricNumber, int id)  //*****Method 3*****
        {
            StreamReader SR;

            string S;

            string MatricNumber;
            string NAME;
            string PASSWORD;
            string EMAIL;
            string CONTACT;
            string GENDER;
            string CLIST;
            string JLIST;

            List<string> lines = new List<string>();

            SR = File.OpenText(@"students.txt");
            S = SR.ReadLine();


            while (S != null)
            {
                string[] elements = S.Split(',');

                if (elements.Count() >= 8)
                {

                    MatricNumber = elements[1];

                    if (MatricNumber == matricNumber)
                    {
                        NAME = elements[0];
                        PASSWORD = elements[2];
                        EMAIL = elements[3];
                        CONTACT = elements[4];
                        GENDER = elements[5];
                        CLIST = elements[6];
                        JLIST = elements[7];

                        string[] js = JLIST.Split('|');

                        List<int> j_list = new List<int>();

                        foreach (string j in js)
                        {
                            j_list.Add(Convert.ToInt32(j));
                        }

                        j_list.Remove(id);

                        JLIST = "";
                        if (j_list.Count() > 0)
                        {
                            JLIST += j_list[0];
                        }
                        for (int z = 1; z < j_list.Count(); z++)
                        {
                            JLIST += "|";
                            JLIST += j_list[z];

                        }

                        S = NAME + "," + MatricNumber + "," + PASSWORD + "," + EMAIL + "," + CONTACT + "," + GENDER + "," + CLIST + "," + JLIST;


                    }
                }
                lines.Add(S);

                S = SR.ReadLine();

            }

            SR.Close();

            StreamWriter sw = new StreamWriter(@"students.txt");

            for (int i = 0; i < lines.Count(); i++)
            {
                sw.WriteLine(lines[i]);
            }

            sw.Close();
        }

        public void add_oneCreatedEvent(string matricNumber, int id) //*****Method 4*****
        {
            StreamReader SR;

            string S;

            string MatricNumber;
            string NAME;
            string PASSWORD;
            string EMAIL;
            string CONTACT;
            string GENDER;
            string CLIST;
            string JLIST;

            List<string> lines = new List<string>();

            SR = File.OpenText(@"students.txt");
            S = SR.ReadLine();

            while (S != null)
            {
                string[] elements = S.Split(',');

                if (elements.Count() >= 8)
                {

                    MatricNumber = elements[1];

                    if (MatricNumber == matricNumber)
                    {
                        NAME = elements[0];
                        PASSWORD = elements[2];
                        EMAIL = elements[3];
                        CONTACT = elements[4];
                        GENDER = elements[5];
                        CLIST = elements[6];
                        JLIST = elements[7];

                        string[] cs = CLIST.Split('|');

                        List<int> c_list = new List<int>();

                        
                        foreach (string c in cs)
                        {
                            c_list.Add(Convert.ToInt32(c));
                        }

                        c_list.Add(id);

                        CLIST = "";
                        if (c_list.Count() > 0)
                        {
                            CLIST += c_list[0];
                        }
                        for (int z = 1; z < c_list.Count(); z++)
                        {
                            CLIST += "|";
                            CLIST += c_list[z];

                        }

                        S = NAME + "," + MatricNumber + "," + PASSWORD + "," + EMAIL + "," + CONTACT + "," + GENDER + "," + CLIST + "," + JLIST;


                    }
                }
                lines.Add(S);

                S = SR.ReadLine();

            }

            SR.Close();

            StreamWriter sw = new StreamWriter(@"students.txt");

            for (int i = 0; i < lines.Count(); i++)
            {
                sw.WriteLine(lines[i]);
            }

            sw.Close();

           
        }

        public void add_oneJoinedEvent(string matricNumber, int id)  //****Method 5*****
        {
            StreamReader SR;

            string S;

            string MatricNumber;
            string NAME;
            string PASSWORD;
            string EMAIL;
            string CONTACT;
            string GENDER;
            string CLIST;
            string JLIST;

            List<string> lines = new List<string>();

            SR = File.OpenText(@"students.txt");
            S = SR.ReadLine();

            while (S != null)
            {
                string[] elements = S.Split(',');

                if (elements.Count() >= 8)
                {

                    MatricNumber = elements[1];

                    if (MatricNumber == matricNumber)
                    {
                        NAME = elements[0];
                        PASSWORD = elements[2];
                        EMAIL = elements[3];
                        CONTACT = elements[4];
                        GENDER = elements[5];
                        CLIST = elements[6];
                        JLIST = elements[7];

                        string[] js = JLIST.Split('|');

                        List<int> j_list = new List<int>();

                        foreach (string j in js)
                        {
                            j_list.Add(Convert.ToInt32(j));
                        }

                        j_list.Add(id);

                        JLIST = "";
                        if (j_list.Count() > 0)
                        {
                            JLIST += j_list[0];
                        }
                        for (int z = 1; z < j_list.Count(); z++)
                        {
                            JLIST += "|";
                            JLIST += j_list[z];

                        }

                        S = NAME + "," + MatricNumber + "," + PASSWORD + "," + EMAIL + "," + CONTACT + "," + GENDER + "," + CLIST + "," + JLIST;


                    }
                }
                lines.Add(S);

                S = SR.ReadLine();

            }

            SR.Close();

            StreamWriter sw = new StreamWriter(@"students.txt");

            for (int i = 0; i < lines.Count(); i++)
            {
                sw.WriteLine(lines[i]);
            }

            sw.Close();

        }

        public int checkIfMatricExist(string aMatricNumber)//***method 6***
        {
            StreamReader SR;
            string S;
            string MatricNumber;
            List<string> lines = new List<string>();

            SR = File.OpenText(@"students.txt");
            S = SR.ReadLine();

            while (S != null)
            {
                string[] elements = S.Split(',');

                if (elements.Count() >= 8)
                {

                    MatricNumber = elements[1];

                    if (MatricNumber == aMatricNumber)
                    {
                        SR.Close();
                        return 1;
                    }

                }
                S = SR.ReadLine();
            }

            SR.Close();
            return 0;
        }

        public int checkIfAlreadyTheOrganizer(string matric, int ID) //***method 7***
        {
            StreamReader SR;

            string S;

            string MatricNumber;
            string CLIST;

            SR = File.OpenText(@"students.txt");
            S = SR.ReadLine();

            while (S != null)
            {
                string[] elements = S.Split(',');
                if (elements.Count() >= 8)
                {
                    MatricNumber = elements[1];
                    if (MatricNumber == matric)
                    {
                        CLIST = elements[6];
                        string[] cs = CLIST.Split('|');
                        foreach (string CID in cs)
                        {
                            if (CID ==Convert.ToString(ID))
                            {
                                SR.Close();
                                return 1;
                            }

                        }
                    }
                }
                S = SR.ReadLine();

            }
            SR.Close();
            return 0;
        }

        public int load_person_info(string name)
        {
            userlist.Clear();

            StreamReader SR;
            string NAME;
            string S;
            SR = File.OpenText(@"students.txt");
            S = SR.ReadLine();
            while (S != null)
            {
                string[] elements = S.Split(',');

                if (elements.Count() >= 8)
                {

                    NAME = elements[0];

                    if (name == NAME)
                    {
                        Users user = new Users();
                        user.username = elements[1];
                        user.password = elements[2];
                        user.email = elements[3];
                        user.contact = elements[4];
                        user.gender = elements[5];
                        userlist.Add(user);
                    }

                }
                S = SR.ReadLine();

            }
            SR.Close();
            return 0;
        }
    }
}

