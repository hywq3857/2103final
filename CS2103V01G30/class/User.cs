/**************************************************************************** 
 * Class name:   Users                                                      *
 *                                                                          *
 * Author:  NUS CS2103 Project Group 30                                     *
 *                                                                          *
 * Purpose:  Load user information and set the connection of user and event.*
 *                                                                          *
 * Usage:   Used in UserRegistration,Updateinformation and Control classes. *
 *                                                                          *
 ****************************************************************************/
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

            string Str;

            SR = File.OpenText(@"students.txt");
            Str = SR.ReadLine();

            string MatricNumber;      
            string createdID="";           //the id list of created events
            string joinedID="";            //the id list of joined events


            while (Str != null)
            {
                string[] words = Str.Split(',');

                MatricNumber = words[1];

                if(MatricNumber==matricNumber )
                {
                    createdID = words[6];
                    joinedID = words[7];
                    break;
                }
                Str = SR.ReadLine();
               
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

            string Str;

            string MatricNumber;
            string Name;
            string Password;
            string Email;
            string Contact;
            string Gender;
            string Clist;
            string Jlist;

            List<string> lines = new List<string>();

            SR = File.OpenText(@"students.txt");
            Str = SR.ReadLine();

            while (Str != null)
            {
                string[] elements = Str.Split(',');

                if (elements.Count() >= 8)
                {

                    MatricNumber = elements[1];

                    if (MatricNumber == matricNumber)
                    {
                        Name = elements[0];
                        Password = elements[2];
                        Email = elements[3];
                        Contact = elements[4];
                        Gender = elements[5];
                        Clist = elements[6];
                        Jlist = elements[7];

                        string[] cs = Clist.Split('|');

                        List<int> c_list = new List<int>();

                        foreach (string c in cs)
                        {
                            c_list.Add(Convert.ToInt32(c));
                        }

                        c_list.Remove(id);

                        Clist = "";
                        if (c_list.Count() > 0)
                        {
                            Clist += c_list[0];
                        }
                        for (int z = 1; z < c_list.Count(); z++)
                        {
                            Clist += "|";
                            Clist += c_list[z];

                        }

                        Str = Name + "," + MatricNumber + "," + Password + "," + Email + "," + Contact + "," + Gender + "," + Clist + "," + Jlist;


                    }
                }
                lines.Add(Str);

                Str = SR.ReadLine();

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
            StreamReader sr;

            string Str;

            string MatricNumber;
            string Name;
            string Password;
            string Email;
            string Contact;
            string Gender;
            string Clist;
            string Jlist;

            List<string> lines = new List<string>();

            sr = File.OpenText(@"students.txt");
            Str = sr.ReadLine();


            while (Str != null)
            {
                string[] elements = Str.Split(',');

                if (elements.Count() >= 8)
                {

                    MatricNumber = elements[1];

                    if (MatricNumber == matricNumber)
                    {
                        Name = elements[0];
                        Password = elements[2];
                        Email = elements[3];
                        Contact = elements[4];
                        Gender = elements[5];
                        Clist = elements[6];
                        Jlist = elements[7];

                        string[] js = Jlist.Split('|');

                        List<int> j_list = new List<int>();

                        foreach (string j in js)
                        {
                            j_list.Add(Convert.ToInt32(j));
                        }

                        j_list.Remove(id);

                        Jlist = "";
                        if (j_list.Count() > 0)
                        {
                            Jlist += j_list[0];
                        }
                        for (int z = 1; z < j_list.Count(); z++)
                        {
                            Jlist += "|";
                            Jlist += j_list[z];

                        }

                        Str = Name + "," + MatricNumber + "," + Password + "," + Email + "," + Contact + "," + Gender + "," + Clist + "," + Jlist;


                    }
                }
                lines.Add(Str);

                Str = sr.ReadLine();

            }

            sr.Close();

            StreamWriter sw = new StreamWriter(@"students.txt");

            for (int i = 0; i < lines.Count(); i++)
            {
                sw.WriteLine(lines[i]);
            }

            sw.Close();
        }

        public void add_oneCreatedEvent(string matricNumber, int id) //*****Method 4*****
        {
            StreamReader sr;

            string Str;

            string MatricNumber;
            string Name;
            string Password;
            string Email;
            string Contact;
            string Gender;
            string Clist;
            string Jlist;

            List<string> lines = new List<string>();

            sr = File.OpenText(@"students.txt");
            Str = sr.ReadLine();

            while (Str != null)
            {
                string[] elements = Str.Split(',');

                if (elements.Count() >= 8)
                {

                    MatricNumber = elements[1];

                    if (MatricNumber == matricNumber)
                    {
                        Name = elements[0];
                        Password = elements[2];
                        Email = elements[3];
                        Contact = elements[4];
                        Gender = elements[5];
                        Clist = elements[6];
                        Jlist = elements[7];

                        string[] cs = Clist.Split('|');

                        List<int> c_list = new List<int>();

                        
                        foreach (string c in cs)
                        {
                            c_list.Add(Convert.ToInt32(c));
                        }

                        c_list.Add(id);

                        Clist = "";
                        if (c_list.Count() > 0)
                        {
                            Clist += c_list[0];
                        }
                        for (int z = 1; z < c_list.Count(); z++)
                        {
                            Clist += "|";
                            Clist += c_list[z];

                        }

                        Str = Name + "," + MatricNumber + "," + Password + "," + Email + "," + Contact + "," + Gender + "," + Clist + "," + Jlist;


                    }
                }
                lines.Add(Str);

                Str = sr.ReadLine();

            }

            sr.Close();

            StreamWriter sw = new StreamWriter(@"students.txt");

            for (int i = 0; i < lines.Count(); i++)
            {
                sw.WriteLine(lines[i]);
            }

            sw.Close();

           
        }

        public void add_oneJoinedEvent(string matricNumber, int id)  //****Method 5*****
        {
            StreamReader sr;

            string Str;

            string MatricNumber;
            string Name;
            string Password;
            string Email;
            string Contact;
            string Gender;
            string Clist;
            string Jlist;

            List<string> lines = new List<string>();

            sr = File.OpenText(@"students.txt");
            Str = sr.ReadLine();

            while (Str != null)
            {
                string[] elements = Str.Split(',');

                if (elements.Count() >= 8)
                {

                    MatricNumber = elements[1];

                    if (MatricNumber == matricNumber)
                    {
                        Name = elements[0];
                        Password = elements[2];
                        Email = elements[3];
                        Contact = elements[4];
                        Gender = elements[5];
                        Clist = elements[6];
                        Jlist = elements[7];

                        string[] js = Jlist.Split('|');

                        List<int> j_list = new List<int>();

                        foreach (string j in js)
                        {
                            j_list.Add(Convert.ToInt32(j));
                        }

                        j_list.Add(id);

                        Jlist= "";
                        if (j_list.Count() > 0)
                        {
                            Jlist += j_list[0];
                        }
                        for (int z = 1; z < j_list.Count(); z++)
                        {
                            Jlist += "|";
                            Jlist += j_list[z];

                        }

                        Str = Name + "," + MatricNumber + "," + Password + "," + Email + "," + Contact + "," + Gender + "," + Clist + "," + Jlist;


                    }
                }
                lines.Add(Str);

                Str = sr.ReadLine();

            }

            sr.Close();

            StreamWriter sw = new StreamWriter(@"students.txt");

            for (int i = 0; i < lines.Count(); i++)
            {
                sw.WriteLine(lines[i]);
            }

            sw.Close();

        }

        public int checkIfMatricExist(string aMatricNumber)//***method 6***
        {
            StreamReader sr;
            string Str;
            string MatricNumber;
            List<string> lines = new List<string>();

            sr = File.OpenText(@"students.txt");
            Str = sr.ReadLine();

            while (Str != null)
            {
                string[] elements = Str.Split(',');

                MatricNumber = elements[1];
                if (MatricNumber == aMatricNumber)
                {
                    sr.Close();
                    return 1;
                }
                Str = sr.ReadLine();
            }
            sr.Close();
            return 0;
        }

        public int checkIfAlreadyTheOrganizer(string matric, int ID) //***method 7***
        {
            StreamReader sr;

            string Str;

            string MatricNumber;
            string Clist;

            sr = File.OpenText(@"students.txt");
            Str = sr.ReadLine();

            while (Str != null)
            {
                string[] elements = Str.Split(',');
                if (elements.Count() >= 8)
                {
                    MatricNumber = elements[1];
                    if (MatricNumber == matric)
                    {
                        Clist = elements[6];
                        string[] cs = Clist.Split('|');
                        foreach (string CID in cs)
                        {
                            if (CID ==Convert.ToString(ID))
                            {
                                sr.Close();
                                return 1;
                            }

                        }
                    }
                }
                Str = sr.ReadLine();

            }
            sr.Close();
            return 0;
        }

        public int load_person_info(string name)
        {
            userlist.Clear();

            StreamReader sr;
            string Name;
            string Str;
            sr = File.OpenText(@"students.txt");
            Str = sr.ReadLine();
            while (Str != null)
            {
                string[] elements = Str.Split(',');

                if (elements.Count() >= 8)
                {

                    Name = elements[0];

                    if (name == Name)
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
                Str = sr.ReadLine();

            }
            sr.Close();
            return 0;
        }
    }
}

