using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using users;

namespace CS2103V01G30
{
    public partial class UpdateInfo : Window
    {

        public UpdateInfo()
        {
            InitializeComponent();
        }

        public void update_Click(object sender, RoutedEventArgs e)
        {

            if (passwordBox1.Password != passwordBox2.Password)
            {
                MessageBox.Show("The two passwords you entered are not the same!");
                return;
            }
            if (txtContact.Text.Length != 8)
            {
                MessageBox.Show("Your contact number must be 8 digits!");
                return;
            }
            if (txtEmail.Text == null)
            {
                MessageBox.Show("You cannot leave the e-mail field empty!");
                return;
            }

            var update = new Users();
            StreamReader sr = File.OpenText(@"matricNo.txt");
            string S = sr.ReadLine();
            update.username = S;
            sr.Close();

            update.email = txtEmail.Text;
            update.contact = txtContact.Text;
            update.password = passwordBox1.Password;

            List<string> userlist = new List<string>();
            sr = File.OpenText(@"students.txt");
            S = sr.ReadLine();
            while (S != null)
            {
                string[] elements = S.Split(',');
                if (elements.Count() >= 8)
                {
                    if (update.username == elements[1])
                    {
                        S = elements[0] + "," + update.username + "," + update.password + "," + update.email + "," +
                            update.contact + "," + elements[5] + "," + elements[6] + "," + elements[7];
                    }
                }
                userlist.Add(S);

                S = sr.ReadLine();

            }

            sr.Close();
            var sw = new StreamWriter(@"students.txt");
            for (var i = 0; i < userlist.Count(); i++)
            {
                sw.WriteLine(userlist[i]);
            }

            sw.Close();
            MessageBox.Show("successful!");
            this.Close();
        }

        private void ShowInfo_Click(object sender, RoutedEventArgs e)
        {
            Users update = new Users();
            string S;
            StreamReader SR = File.OpenText(@"matricNo.txt");
            S = SR.ReadLine();
            update.username = S;
            SR.Close();
            StreamReader SR2 = File.OpenText(@"students.txt");
            S = SR2.ReadLine();
            while (S != null)
            {
                string[] elements = S.Split(',');
                if (update.username == elements[1])
                {
                    update.password = elements[2];
                    update.email = elements[3];
                    update.contact = elements[4];
                }
                S = SR2.ReadLine();
            }
            SR2.Close();
            txtEmail.Text = update.email;
            txtContact.Text = update.contact;
            passwordBox1.Password = update.password;
            passwordBox2.Password = update.password;

        }


        private void txtContact_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
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

