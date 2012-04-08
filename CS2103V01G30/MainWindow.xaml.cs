/*************************************************************************** 
 * Class name:   MainWindow                                                *
 *                                                                         *
 * Author:  NUS CS2103 Project Group 30                                    *
 *                                                                         *
 * Purpose:  Show the Login wondow to user.                                *
 *                                                                         *
 * Usage:   Runs the program and the window appears.                       *
 *                                                                         *
 ***************************************************************************/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace CS2103V01G30
{


    public partial class MainWindow : Window
    {
        public List<string> nameList = new List<string>();          //create a list of names of all the users
        public List<string> usernameList = new List<string>();      //create a list of matric numbers of all the users
        public List<string> passwordList = new List<string>();    //create a list of passwords of all the users

        //for a particular user, if his name is stored at nameList[3], then his username is stored at userNameList[3]
        //and his password is stored at passwordList[3]

        public MainWindow()
        {
            InitializeComponent();
            FocusManager.SetFocusedElement(this, txtUsername);
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)  //*****the function of login button*****
        {
            if (txtUsername.Text == "")      //check if the username field is empty
            {
                MessageBox.Show("You have to enter your Matric number (username) to log in!");
                return;
            }
            else if (passwordBox1.Password == "") //check if the password field is empty
            {
                MessageBox.Show("You have to enter your password to log in!");
                return;
            }
            else
            {
                StreamReader SR;
                string S;
                SR = File.OpenText(@"students.txt");     //load data from 'students.txt' using StreamReader
                S = SR.ReadLine();

                while (S != null)    //using the while loop to load data to the 3 lists
                {
                    string[] words = S.Split(',');
                    if (words.Count() > 4)
                    {
                        nameList.Add(words[0]);
                        usernameList.Add(words[1]);
                        passwordList.Add(words[2]);
                    }

                    S = SR.ReadLine();

                }

                SR.Close();

                bool check = false;

                for (int i = 0; i < usernameList.Count(); i++)
                {
                    if (txtUsername.Text == usernameList[i])      //to check if the username entered by the user exists in the data base
                    {
                        if (passwordBox1.Password == passwordList[i])  //the case which the password is correct
                        {
                            StreamWriter writer = new StreamWriter(@"matricNo.txt");
                            writer.WriteLine(usernameList[i]);
                            writer.Close();
                            StreamWriter writers = new StreamWriter(@"name.txt");
                            writers.WriteLine(nameList[i]);
                            writers.Close();
                            Control controlwindow = new Control();
                            controlwindow.Show();
                            controlwindow.labelwelcome.Content = nameList[i]+"  (" + usernameList[i] +")   ";
                           // MessageBox.Show("Welcome, " + nameList[i] + "!");
                            this.Close();
                            check = true;
                            return;
                        }
                        else if (passwordBox1.Password != passwordList[i])  //the case which the password is incorrect
                        {
                            MessageBox.Show("The password you entered is invalid. Please try again.");
                            check = true;
                            return;

                        }
                    }
                }

                if (check == false)   //the case the username is not registered in the data base
                {
                    MessageBox.Show("No such user! Please check again!");
                }
            }
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e) //*****the function of the register button*****
        {
            UserRegistration userRegistration = new UserRegistration();                   //show the registration window
            userRegistration.Show();
        }
    }
}
