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
using System.Windows.Shapes;
using users;
using System.IO;
using System.Text.RegularExpressions;




namespace CS2103V01G30
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class UserRegistration : Window
    {
        public UserRegistration()
        {
            InitializeComponent();
            FocusManager.SetFocusedElement(this, txtName);
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            string testing;

            if (txtName.Text == "")
            {
                MessageBox.Show("You cannot leave 'Name' empty!");
                return;
            }

            if (txtUsername.Text == "")
            {
                MessageBox.Show("You cannot leave 'Username' empty!");
                return;
            }
            else
            {      //to check if the username already exists

                testing = txtUsername.Text;
                if (testing.Length < 8)
                {
                    MessageBox.Show("The format of your username is not correct!");
                    return;
                }
                else
                {
                    StreamReader SR;
                    string S;
                    SR = File.OpenText(@"students.txt");
                    S = SR.ReadLine();

                    List<string> searchList = new List<string>();

                    while (S != null)    //using the while loop to load the usernames to a list
                    {
                        string[] words = S.Split(',');
                        if (words.Count() > 3)
                        {
                            searchList.Add(words[1]);
                        }
                        S = SR.ReadLine();

                    }

                    SR.Close();

                    for (int i = 0; i < searchList.Count(); i++)
                    {
                        if (searchList[i] == txtUsername.Text)
                        {
                            MessageBox.Show("INVALID. The username already exists!");
                            return;
                        }

                    }
                    if (passwordBox1.Password == "")
                    {
                        MessageBox.Show("You cannot leave 'Password' empty!");
                        return;

                    }
                    else
                    {
                        testing = passwordBox1.Password;
                        if (testing.Length < 6)
                        {
                            MessageBox.Show("Your password needs at least 6 characters");
                            return;
                        }
                    }

                    if (passwordBox2.Password == "")
                    {
                        MessageBox.Show("Please re-enter your password for confirmation!");
                        return;
                    }

                    if (passwordBox1.Password != passwordBox2.Password)
                    {
                        MessageBox.Show("The two passwords you entered are not the same!!");
                        return;
                    }

                    if (txtEmail.Text == "")
                    {
                        MessageBox.Show("You cannot leave 'E-mail' empty!");
                        return;
                    }
                    else
                    {
                        testing = txtEmail.Text;
                        if (!(testing.IndexOf('@') > -1))
                        {
                            MessageBox.Show("Your E-mail address is invalid");
                            return;
                        }
                    }

                    if (txtContact.Text == "")
                    {
                        MessageBox.Show("You cannot leave 'Contact Number' empty!");
                        return;
                    }
                    else
                    {
                        testing = txtContact.Text;

                        if (testing.Length != 8)
                        {
                            MessageBox.Show("Your contact number is invalid!");
                            return;
                        }
                    }
                    //testing = txtGender.Text;
                    if ((comboBoxGender.SelectedIndex) < 1 || (comboBoxGender.SelectedIndex)>2)
                    {
                        MessageBox.Show("The format of 'Gender' is wrong!");
                        return;
                    }
                    else
                    {
                        addStudent();
                    }
                }
            }
        }

        private void addStudent()
        {
            StreamWriter sw;
            sw = File.AppendText("students.txt");
            Users newStudent = new Users();
            newStudent.name = txtName.Text;
            newStudent.username = txtUsername.Text;
            newStudent.password = passwordBox1.Password;
            newStudent.email = txtEmail.Text;
            newStudent.contact = txtContact.Text;

            string cmbvalue = "";
            System.Windows.Controls.ComboBoxItem curItem = ((System.Windows.Controls.ComboBoxItem)comboBoxGender.SelectedItem);
            cmbvalue = curItem.Content.ToString();

            newStudent.gender = cmbvalue;


            Directory.CreateDirectory(txtUsername.Text);
            string myEventPath = System.IO.Path.Combine(txtUsername.Text, "events.txt");
            File.Create(myEventPath);
            string myBudgetPath = System.IO.Path.Combine(txtUsername.Text, "budgets.txt");
            File.Create(myBudgetPath);

            sw.WriteLine();

            sw.Write("{0},{1},{2},{3},{4},{5},0,0", newStudent.name, newStudent.username, newStudent.password, newStudent.email, newStudent.contact, newStudent.gender);
            sw.Close();
            MessageBox.Show("Successful!");
            this.Close();
        }

        private void validateTextGender(object sender, TextCompositionEventArgs e)
        {
            foreach (char c in e.Text)
            {
                if (c != 'M' || c != 'F')
                {
                    e.Handled = true;
                    break;
                }
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void txtContact_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            foreach (char c in e.Text)
            {
                if (!char.IsDigit(c))
                {
                    e.Handled = true;
                    break;
                }
            }

        }

    }

}
