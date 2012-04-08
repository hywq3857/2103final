/*************************************************************************** 
 * Class name:   Control                                                   *
 *                                                                         *
 * Author:  NUS CS2103 Project Group 30                                    *
 *                                                                         *
 * Purpose:  Show the Control wondow and its components to user.           *
 *                                                                         *
 * Usage:   After successful login from MainWindow.                        *
 *                                                                         *
 ***************************************************************************/


using System;
using System.IO;
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
using TaskMgt;
using RegistrationMgt;
using EventManagement;
using VenueManagement;
using Budgets;
using users;
using Microsoft.Win32;
using System.Windows.Threading;

namespace CS2103V01G30
{
    public partial class Control : Window
    {
        string matricNo;
        static string myEventsPath;
        static string myBudgetsPath;
        static List<Event> myEventList = new List<Event>();
        static List<Budget> myBudgetList = new List<Budget>();

        EventMgt eventMgt = new EventMgt();
        BudgetMgmt budgetMgt = new BudgetMgmt();
        Users stu = new Users();
        RegEvent regEvt = new RegEvent();
        RegProManagement regProMgt = new RegProManagement();
        RegNotification regNotification = new RegNotification();
        TaskNotification taskNotification;
        TopTaskManagement topTaskMgt = new TopTaskManagement("Task.txt");
        VenueMgt venueMgt = new VenueMgt();
        BudgetNotif budgetNtf;
        EventsNotification eventsNtf;

        private DispatcherTimer ShowTimer;

        public Control()
        {
            InitializeComponent();

            StreamReader matric_reader = new StreamReader("matricNo.txt");
            matricNo = matric_reader.ReadLine();
            matric_reader.Close();

            StreamReader name_reader = new StreamReader("name.txt");
            string myName = name_reader.ReadLine();
            name_reader.Close();

            //create the path of events
            myEventsPath = System.IO.Path.Combine(matricNo, "events.txt");
            eventsNtf = new EventsNotification(myEventsPath);

            //create the path of budgets
            myBudgetsPath = System.IO.Path.Combine(matricNo, "budgets.txt");

            budgetNtf = new BudgetNotif(myBudgetsPath);

            regEvt.setRegEvent(myName, matricNo);

            taskNotification = new TaskNotification(matricNo);

            stu.set_EventLists(matricNo);
            taskNotification = new TaskNotification(matricNo);
            for (int i = 0; i < myEventList.Count; i++)
            {
                topTaskMgt.addATaskManagement(myEventList[i].getEventName());
                taskNotification.topTaskMgtForNotification.addATaskManagement(myEventList[i].getEventName());
                regProMgt.addToRegProMgtList(myEventList[i].getEventName());
            }

            //initialize all the lists
            showlistViewAllEvent();
            showlistViewMyEvent();
            showlistBoxBudget();
            showlistBoxReg();
            showlistBoxAllVenue();
            showlistBoxNotification();

            //show the cuurent time
            ShowTimer = new DispatcherTimer();
            ShowTimer.Tick += new EventHandler(ShowCurTimer);
            ShowTimer.Interval = new TimeSpan(0, 0, 0, 1, 0);
            ShowTimer.Start();
            buttonRegister.Visibility = Visibility.Hidden;
            buttonCancel.Visibility = Visibility.Hidden;
        }

        #region notification

        void showlistBoxNotification()
        {
            //events notification
            eventsNtf.readFromFile();
            eventsNtf.eventNotification(myEventList);
            foreach (string notification in eventsNtf.notificationList)
            {
                listBoxNotification.Items.Add(notification);
            }

            //registration notification
            regNotification.recordChange(regEvt);
            foreach (string notification in regNotification.notificationList)
            {
                listBoxNotification.Items.Add(notification);
            }

            //task notification
            taskNotification.compareTopTaskManagement(topTaskMgt);
            foreach (string notification in taskNotification.notificationList)
            {
                listBoxNotification.Items.Add(notification);
            }

            //budget notification
            budgetNtf.readFromFile();
            budgetNtf.budgetNotifications(myBudgetList);
            foreach (int eventID in budgetNtf.notificationList)
            {
                foreach (Event eve in myEventList)
                {
                    if (eventID == eve.getEventID())
                    {
                        string notifInfo = "The budget of your event (" + eve.getEventName() + ") has been modified.";
                        listBoxNotification.Items.Add(notifInfo);
                    }
                }
            }
        }

        #endregion

        # region task

        void showlistViewTask()
        {
            listViewTask.Items.Clear();

            string eventName = myEventList[listViewMyEvent.SelectedIndex].getEventName();
            topTaskMgt.addATaskManagement(eventName);
            for (int i = 0; i < topTaskMgt.getNoOfTasksInATaskList(eventName); i++)
            {
                Users aUser = new Users();
                aUser.load_person_info(topTaskMgt.getPersonName(eventName, i));
                string studentInfo="";
                foreach (Users user in aUser.userlist)
                {
                    studentInfo += "ID:" + user.username + " HP: " + user.contact + " Email: " + user.email + "\t";
                }
                listViewTask.Items.Add(new { Task = topTaskMgt.getTaskName(eventName, i), Deadline = topTaskMgt.getDueDate(eventName, i), Person = topTaskMgt.getPersonName(eventName, i), Status = topTaskMgt.getStatus(eventName, i), StudentInfo = studentInfo });
            }

            labelTotaltask.Content = "Total Task: " + topTaskMgt.getNoOfTasksInATaskList(eventName);
            labelFinished.Content = "Finished: " + topTaskMgt.getNoOfDoneTasksInATaskList(eventName);
            int todo = topTaskMgt.getNoOfTasksInATaskList(eventName) - topTaskMgt.getNoOfDoneTasksInATaskList(eventName);
            labelTodo.Content = "To do: " + todo;
        }

        private void buttonAddanewtask_Click(object sender, RoutedEventArgs e)
        {
            topTaskMgt.addTaskInTaskList(myEventList[listViewMyEvent.SelectedIndex].getEventName());
            showlistViewTask();
        }

        private void buttonDelTask_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                topTaskMgt.deleteTaskInTaskList(myEventList[listViewMyEvent.SelectedIndex].getEventName(), listViewTask.SelectedIndex);
                showlistViewTask();
            }
            catch
            {
                MessageBox.Show("Please select a task.");
            }
        }

        private void listViewTask_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                int index = listViewTask.SelectedIndex;
                string eventName = listViewMyEvent.SelectedItem.ToString();

                string dateString = topTaskMgt.getDueDate(eventName, index);
                DateTime date = new DateTime();
                if (dateString != "Due Date")
                {
                    string[] parts = dateString.Split(' ');
                    int year = Convert.ToInt32(parts[0]);
                    int month = Convert.ToInt32(parts[1]);
                    int day = Convert.ToInt32(parts[2]);
                    date = new DateTime(year, month, day);
                }
                else
                {
                    date = System.DateTime.Now;
                }
                datePickerDeadline.SelectedDate = date;
                textBoxtName.Text = topTaskMgt.getTaskName(eventName, index);
                textBoxpName.Text = topTaskMgt.getPersonName(eventName, index);
                if (topTaskMgt.getStatus(eventName, index) == "Finished")
                    comboBoxStatus.SelectedIndex = 1;
                else
                    comboBoxStatus.SelectedIndex = 2;

            }
            catch { }
        }

        private void buttonEdit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (comboBoxStatus.SelectedIndex != 0)
                {
                    string cmbvalue = "";
                    System.Windows.Controls.ComboBoxItem curItem = ((System.Windows.Controls.ComboBoxItem)comboBoxStatus.SelectedItem);
                    cmbvalue = curItem.Content.ToString();
                    DateTime date = datePickerDeadline.SelectedDate.Value.Date;
                    int year = date.Year;
                    int month = date.Month;
                    int day = date.Day;
                    string monthStr;
                    string dayStr;
                    if (month < 10) 
                    {
                        monthStr = "0" + month.ToString();
                    }
                    else
                        monthStr = month.ToString();
                    if (day < 10)
                    {
                        dayStr = "0" + day.ToString();
                    }
                    else
                        dayStr = day.ToString();
                    string dateString = year.ToString() + " " + monthStr + " " + dayStr;
                    topTaskMgt.editATaskInTaskList(myEventList[listViewMyEvent.SelectedIndex].getEventName(), listViewTask.SelectedIndex, textBoxpName.Text, textBoxtName.Text, dateString, cmbvalue);
                }
                showlistViewTask();
            }
            catch
            {
                MessageBox.Show("Please select a task.");
            }
        }

        # endregion

        #region event

        void showlistViewAllEvent()
        {
            listViewAllEvent.Items.Clear();
            eventMgt.readFromFile();

            foreach (Event eve in eventMgt.eventList)
            {
                listViewAllEvent.Items.Add(eve.getEventName());
            }
        }

        void showlistViewMyEvent()
        {
            listViewMyEvent.Items.Clear();
            myEventList.Clear();
            eventMgt.readFromFile();

            foreach (int index in stu.createdEventList)
            {
                foreach (Event eve in eventMgt.eventList)
                {
                    if (eve.getEventID() == index)
                    {
                        listViewMyEvent.Items.Add(eve.getEventName());
                        myEventList.Add(eve);
                    }
                }
            }
        }

        private void buttonCreate1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //get event ID, name, starting and ending time
                int id = 0;
                string name = textBoxName1.Text;

                string cmbvalue1 = "";

                System.Windows.Controls.ComboBoxItem curItem1 = ((System.Windows.Controls.ComboBoxItem)comboBoxTime1.SelectedItem);
                cmbvalue1 = curItem1.Content.ToString();

                string cmbvalue2 = "";

                System.Windows.Controls.ComboBoxItem curItem2 = ((System.Windows.Controls.ComboBoxItem)comboBoxTime2.SelectedItem);
                cmbvalue2 = curItem2.Content.ToString();

                //get starting date
                int day = datePickerStartDate.SelectedDate.Value.Day;
                int month = datePickerStartDate.SelectedDate.Value.Month;
                int year = datePickerStartDate.SelectedDate.Value.Year;
                int startDate = 1000000 * day + 10000 * month + year;
                DateTime startDateTime = new DateTime(year, month, day);

                //get ending date
                day = datePickerEndDate.SelectedDate.Value.Day;
                month = datePickerEndDate.SelectedDate.Value.Month;
                year = datePickerEndDate.SelectedDate.Value.Year;
                int endDate = 1000000 * day + 10000 * month + year;
                DateTime endDateTime = new DateTime(year, month, day);

                if (DateTime.Compare(startDateTime, endDateTime) > 0)
                {
                    MessageBox.Show("The starting date must not be later than ending date.");
                    return;
                }
                else if (DateTime.Compare(startDateTime, DateTime.Now) < 0)
                {
                    MessageBox.Show("You cannot make an event earlier than current date.");
                    return;
                }

                int sTime = Convert.ToInt32(cmbvalue1);
                int eTime = Convert.ToInt32(cmbvalue2);

                if (sTime >= eTime && DateTime.Compare(startDateTime, endDateTime) == 0)
                {
                    MessageBox.Show("The starting time must be later than ending time.");
                    return;
                }

                //get venue and description
                string v = labelVenue.Content.ToString();
                string des = textBoxDescription.Text;

                //description cannot be empty
                if (des == String.Empty)
                {
                    MessageBox.Show("Please do not leave the description empty.");
                    return;
                }

                //check if the venue chosen has a clash with the current reservation status
                foreach (Venue ven in venueMgt.venueList)
                {
                    if (v == ven.getLocation())
                        for (DateTime date = startDateTime; date <= endDateTime; date = date.AddDays(1))
                        {
                            int intDate = 1000000 * date.Day + 10000 * date.Month + date.Year;
                            foreach (int occupiedDate in ven.occupiedDates)
                            {
                                if (occupiedDate == intDate)
                                {
                                    MessageBox.Show("The venue you chose has been occupied on " + date.ToString("d"));
                                    return;
                                }
                            }
                        }
                }

                //create the new event
                id = eventMgt.createEvent(name, startDate, endDate, sTime, eTime, v, des);
                if (id != 0)
                {
                    stu.add_oneCreatedEvent(matricNo, id);
                    stu.set_EventLists(matricNo);
                    MessageBox.Show("A event has been successfully created.");
                }

                //update the data
                eventMgt.writeToFile();
                budgetMgt.createBudget();
                budgetMgt.writeToFile();
                regProMgt.addToRegProMgtList(name);
                topTaskMgt.addATaskManagement(name);

                //update all the related list
                showlistViewMyEvent();
                showlistViewAllEvent();
                showlistBoxBudget();
            }
            catch
            {
                MessageBox.Show("Please fill in all the blank in edit Event Page!");
            }
        }

        private void buttonEditEvent_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int index = listViewMyEvent.SelectedIndex;

                if (index == -1)
                {
                    MessageBox.Show("Please select an event");
                    return;
                }

                //get event ID, name, starting and ending time
                int eventID = myEventList[index].getEventID();
                string name = textBoxName1.Text;

                //get starting date
                int day = datePickerStartDate.SelectedDate.Value.Day;
                int month = datePickerStartDate.SelectedDate.Value.Month;
                int year = datePickerStartDate.SelectedDate.Value.Year;
                int startDate = 1000000 * day + 10000 * month + year;
                DateTime startDateTime = new DateTime(year, month, day);

                //get ending date
                day = datePickerEndDate.SelectedDate.Value.Day;
                month = datePickerEndDate.SelectedDate.Value.Month;
                year = datePickerEndDate.SelectedDate.Value.Year;
                int endDate = 1000000 * day + 10000 * month + year;
                DateTime endDateTime = new DateTime(year, month, day);

                if (DateTime.Compare(startDateTime, endDateTime) > 0)
                {
                    MessageBox.Show("The starting date must not be later than ending date.");
                    return;
                }
                else if (DateTime.Compare(startDateTime, DateTime.Now) < 0)
                {
                    MessageBox.Show("You cannot make an event earlier than current date.");
                    return;
                }

                string cmbvalue1 = "";
                System.Windows.Controls.ComboBoxItem curItem1 = ((System.Windows.Controls.ComboBoxItem)comboBoxTime1.SelectedItem);
                cmbvalue1 = curItem1.Content.ToString();
                string cmbvalue2 = "";
                System.Windows.Controls.ComboBoxItem curItem2 = ((System.Windows.Controls.ComboBoxItem)comboBoxTime2.SelectedItem);
                cmbvalue2 = curItem2.Content.ToString();

                int sTime = Convert.ToInt32(cmbvalue1);
                int eTime = Convert.ToInt32(cmbvalue2);

                if (sTime >= eTime && DateTime.Compare(startDateTime, endDateTime) == 0)
                {
                    MessageBox.Show("The ending time must be later than starting time.");
                    return;
                }

                //get venue and description
                string v = labelVenue.Content.ToString();
                string des = textBoxDescription.Text;

                //description cannot be empty
                if (des == String.Empty)
                {
                    MessageBox.Show("Please do not leave the description empty.");
                    return;
                }

                //delete the occupied dates
                foreach (Venue ven in venueMgt.venueList)
                {
                    if (v == ven.getLocation())
                    {
                        for (int date = myEventList[index].getStartDate(); date <= myEventList[index].getEndDate(); date++)
                            ven.deleteOccupiedDate(date);
                        
                        venueMgt.writeToFile();
                        venueMgt.readFromFile();
                        break;
                    }
                }
                
                //check if there is a clash on venue booking
                foreach (Venue ven in venueMgt.venueList)
                {
                    if (v == ven.getLocation())
                        for (DateTime date = startDateTime; date <= endDateTime; date = date.AddDays(1))
                        {
                            int intDate = 1000000 * date.Day + 10000 * date.Month + date.Year;
                            foreach (int occupiedDate in ven.occupiedDates)
                            {
                                if (occupiedDate == intDate)
                                {
                                    for (int d = myEventList[index].getStartDate(); d < myEventList[index].getEndDate(); d++)
                                        ven.addOccupiedDate(d);
                                    MessageBox.Show("The venue you chose has been reserved on " + date.ToString("d"));
                                    return;
                                }
                            }
                        }
                }

                foreach (Event eve in eventMgt.eventList)
                {
                    if (eventID == eve.getEventID())
                    {
                        if (eventMgt.editEvent(eventID, name, startDate, endDate, sTime, eTime, v, des))
                        {
                            //change the event info
                            myEventList[index].setEventName(name);
                            myEventList[index].setStartDate(startDate);
                            myEventList[index].setEndDate(endDate);
                            myEventList[index].setStartTime(sTime);
                            myEventList[index].setEndTime(eTime);
                            myEventList[index].setVenue(v);
                            myEventList[index].setDescription(des);

                            foreach (Venue ven in venueMgt.venueList)
                            {
                                if (v == ven.getLocation())
                                {
                                    for (DateTime date = startDateTime; date <= endDateTime; date = date.AddDays(1))
                                    {
                                        int intDate = 1000000 * date.Day + 10000 * date.Month + date.Year;
                                        ven.addOccupiedDate(intDate);
                                        calendarAvailableDates.BlackoutDates.Add(new CalendarDateRange(startDateTime, endDateTime));
                                    }
                                    break;
                                }
                            }
                            MessageBox.Show("A event has been successfully modified.");
                            break;
                        }
                    }
                }
                //update the data
                venueMgt.writeToFile();
                eventMgt.writeToFile();
                showlistViewMyEvent();
                showlistViewAllEvent();
            }
            catch
            {
                MessageBox.Show("Please enter info in a correct formmat");
            }
        }

        private void buttonDeleteEvent_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //get the event id
                int eventID = myEventList[listViewMyEvent.SelectedIndex].getEventID();

                stu.remove_oneCreatedEvent(matricNo, eventID);
                stu.set_EventLists(matricNo);
                regProMgt.deleteFromRegProMgtList(myEventList[listViewMyEvent.SelectedIndex].getEventName());
                topTaskMgt.deleteATaskManagement(myEventList[listViewMyEvent.SelectedIndex].getEventName());
                File.Delete(myEventList[listViewMyEvent.SelectedIndex].getEventName() + ".jpg");
                myEventList.RemoveAt(listViewMyEvent.SelectedIndex);
                myBudgetList.RemoveAt(listViewMyEvent.SelectedIndex);
                eventMgt.deleteEvent(eventID);
                budgetMgt.deleteBudget(eventID);

                //update the data
                eventMgt.writeToFile();
                budgetMgt.writeToFile();
                showlistViewMyEvent();
                showlistViewAllEvent();
                showlistBoxBudget();

                MessageBox.Show("A event has been successfully deleted.");
            }
            catch
            {
                MessageBox.Show("Please select an event!");
            }
        }

        private void buttonAddEventOrganizer_Click(object sender, RoutedEventArgs e)   //to assign a new organizer for an event
        {
            Users assign = new Users();
            if (listViewMyEvent.SelectedIndex == -1)
            {
                MessageBox.Show("Please select an event first.");
                return;
            }
            else if (Convert.ToString(textBoxOriganizeMatricNum.Text) == matricNo)
            {
                MessageBox.Show("You cannot add yourself!!");
                return;
            }
            else if (assign.checkIfMatricExist(textBoxOriganizeMatricNum.Text) == 0)
            {
                MessageBox.Show("The matric number doesn't exist!");
                return;
            }
            else if (assign.checkIfAlreadyTheOrganizer(Convert.ToString(textBoxOriganizeMatricNum.Text), myEventList[listViewMyEvent.SelectedIndex].getEventID()) == 1)
            {
                MessageBox.Show("This user is already the organizer of this event!!!");
                return;
            }
            else
            {
                assign.add_oneCreatedEvent(Convert.ToString(textBoxOriganizeMatricNum.Text), myEventList[listViewMyEvent.SelectedIndex].getEventID());
                MessageBox.Show("Successful!");
                return;
            }
        }

        private void listViewMyEvent_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (Event i in eventMgt.eventList)
            {
                int index = listViewMyEvent.SelectedIndex;
                if (index <= myEventList.Count && index >= 0)
                {
                    textBoxName1.Text = myEventList[index].getEventName();
                    
                    int date = myEventList[index].getStartDate();
                    int day = date / 1000000;
                    int month = (date / 10000) % 10;
                    int year = date % 10000;
                    DateTime startDate = new DateTime(year, month, day);
                    datePickerStartDate.SelectedDate = startDate;

                    date = myEventList[index].getEndDate();
                    day = date / 1000000;
                    month = (date / 10000) % 10;
                    year = date % 10000;
                    DateTime endDate = new DateTime(year, month, day);
                    datePickerEndDate.SelectedDate = endDate;

                    comboBoxTime1.Text = myEventList[index].getStartTime().ToString();
                    comboBoxTime2.Text = myEventList[index].getEndTime().ToString();
                    labelVenue.Content = myEventList[index].getVenue();
                    textBoxDescription.Text = myEventList[index].getDescription();

                    topTaskMgt.addATaskManagement(myEventList[listViewMyEvent.SelectedIndex].getEventName());
                    regProMgt.addToRegProMgtList(myEventList[listViewMyEvent.SelectedIndex].getEventName());
                    showlistBoxBudget();
                    showlistViewTask();
                    showlistViewRegPro();

                    labelpName.Content = "Name:    " + myEventList[index].getEventName();
                    string dateString = day.ToString() + "/" + month.ToString() + "/" + year.ToString();
                    labelpDate.Content = "Date:     " + startDate.ToString("d") + " to " + endDate.ToString("d");
                    labelpTime.Content = "Time:     " + myEventList[index].getStartTime().ToString() + " to " + myEventList[index].getEndTime().ToString();
                    labelpVenue.Content = "Venue:    " + myEventList[index].getVenue();
                    textBlockDescription.Text = "Description:" + System.Environment.NewLine + myEventList[index].getDescription();

                    displayNumberOfRegistration();


                    string imagePath = myEventList[index].getEventName() + ".jpg";
                    if (File.Exists(imagePath))
                    {
                        BitmapImage bitmapImage = new BitmapImage();
                        bitmapImage.BeginInit();
                        bitmapImage.StreamSource = new FileStream(imagePath, FileMode.Open, FileAccess.Read);
                        
                        //load the image now so we can immediately dispose of the stream
                        bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                        bitmapImage.EndInit();

                        image1.Source = bitmapImage;
                        image2.Source = bitmapImage;

                        //clean up the stream to avoid file access exceptions when attempting to delete images
                        bitmapImage.StreamSource.Dispose();
                    }
                    if (!File.Exists(imagePath))
                    {
                        string imageDefaultSource = "imageDefault.jpg";
                        ImageSourceConverter imgConv = new ImageSourceConverter();
                        ImageSource imageSource = (ImageSource)imgConv.ConvertFromString(imageDefaultSource);
                        image1.Source = imageSource;
                        image2.Source = imageSource;
                    }
                }
            }
        }

        private void listViewAllEvent_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (Event i in eventMgt.eventList)
            {
                int index = listViewAllEvent.SelectedIndex;

                if (index <= eventMgt.eventList.Count && index >= 0)
                {
                    labelpName.Content = "Name:    " + eventMgt.eventList[index].getEventName();

                    int date = eventMgt.eventList[index].getStartDate();
                    int day = date / 1000000;
                    int month = (date / 10000) % 10;
                    int year = date % 10000;
                    DateTime startDate = new DateTime(year, month, day);
                    datePickerStartDate.SelectedDate = startDate;

                    date = eventMgt.eventList[index].getEndDate();
                    day = date / 1000000;
                    month = (date / 10000) % 10;
                    year = date % 10000;
                    DateTime endDate = new DateTime(year, month, day);
                    datePickerEndDate.SelectedDate = endDate;

                    labelpDate.Content = "Date:     " + startDate.ToString("d") + " to " + endDate.ToString("d");
                    labelpTime.Content = "Time:     " + eventMgt.eventList[index].getStartTime().ToString() + " to " + eventMgt.eventList[index].getEndTime().ToString();
                    labelpVenue.Content = "Venue:    " + eventMgt.eventList[index].getVenue();
                    textBlockDescription.Text = "Description:    " + System.Environment.NewLine + eventMgt.eventList[index].getDescription();

                    string imagePath = eventMgt.eventList[index].getEventName() + ".jpg";
                    if (File.Exists(imagePath))
                    {
                        BitmapImage bitmapImage = new BitmapImage();
                        bitmapImage.BeginInit();
                        bitmapImage.StreamSource = new FileStream(imagePath, FileMode.Open, FileAccess.Read);
                        
                        //load the image now so we can immediately dispose of the stream
                        bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                        bitmapImage.EndInit();

                        image1.Source = bitmapImage;
                        image2.Source = bitmapImage;

                        //clean up the stream to avoid file access exceptions when attempting to delete images
                        bitmapImage.StreamSource.Dispose();
                    }
                    else
                    {
                        string imageDefaultSource = "imageDefault.jpg";
                        ImageSourceConverter imgConv = new ImageSourceConverter();
                        ImageSource imageSource = (ImageSource)imgConv.ConvertFromString(imageDefaultSource);
                        image1.Source = imageSource;
                        image2.Source = imageSource;
                    }
                }
            }
        }

        private void allEvent_GotFocus(object sender, RoutedEventArgs e)
        {
            editEvent.Visibility = Visibility.Hidden;
            Task.Visibility = Visibility.Hidden;
            Budget.Visibility = Visibility.Hidden;
            processReg.Visibility = Visibility.Hidden;
            Venue.Visibility = Visibility.Hidden;
            editEventGrid.Visibility = Visibility.Hidden;
            processRegGrid.Visibility = Visibility.Hidden;
            BudgetGrid.Visibility = Visibility.Hidden;
            TaskGrid.Visibility = Visibility.Hidden;
            buttonRegister.Visibility = Visibility.Visible;
            buttonCancel.Visibility = Visibility.Visible;
        }

        private void myEvent_GotFocus(object sender, RoutedEventArgs e)
        {
            editEvent.Visibility = Visibility.Visible;
            Task.Visibility = Visibility.Visible;
            Budget.Visibility = Visibility.Visible;
            processReg.Visibility = Visibility.Visible;
            Venue.Visibility = Visibility.Visible;
            editEventGrid.Visibility = Visibility.Visible;
            processRegGrid.Visibility = Visibility.Visible;
            BudgetGrid.Visibility = Visibility.Visible;
            TaskGrid.Visibility = Visibility.Visible;
            buttonRegister.Visibility = Visibility.Hidden;
            buttonCancel.Visibility = Visibility.Hidden;
        }

        private void buttonUploadPoster_Click(object sender, RoutedEventArgs e)
        {
            if(listViewMyEvent.SelectedItems.Count!=0)
            {
                OpenFileDialog dlg;
                FileStream fs;
                byte[] data;

                dlg = new Microsoft.Win32.OpenFileDialog();

                dlg.ShowDialog();
                if (dlg.FileName == "")
                {
                    MessageBox.Show("Picture is not selected......");
                }
                else
                {
                    string extensionName = System.IO.Path.GetExtension(dlg.FileName);

                    if (extensionName.ToLower() == ".jpg" || extensionName.ToLower() == ".gif" || extensionName.ToLower() == ".bmp" || extensionName.ToLower() == ".png")
                    {
                        fs = new FileStream(dlg.FileName, FileMode.Open, FileAccess.Read);

                        data = new byte[fs.Length];
                        fs.Read(data, 0, System.Convert.ToInt32(fs.Length));

                        string imageFileName = textBoxName1.Text;
                        //string path = "pack://application:,,/CS2103V01G30;component/Images/" + imageFileName + ".jpg";
                        string path = imageFileName + ".jpg";

                        File.Copy(fs.Name, path, true);
                        fs.Close();

                        //create new stream and create bitmap frame
                        BitmapImage bitmapImage = new BitmapImage();
                        bitmapImage.BeginInit();
                        bitmapImage.StreamSource = new FileStream(path, FileMode.Open, FileAccess.Read);
                        //load the image now so we can immediately dispose of the stream
                        bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                        bitmapImage.EndInit();

                        image1.Source = bitmapImage;
                        image2.Source = bitmapImage;

                        //clean up the stream to avoid file access exceptions when attempting to delete images
                        bitmapImage.StreamSource.Dispose();
                    }
                    else
                    {
                        MessageBox.Show("File format wrong");
                    }
                }
            }
            else
                {
                    MessageBox.Show("Please select an event before upload poster");
                }
        }

        private void textBoxOriganizeMatricNum_GotFocus(object sender, RoutedEventArgs e)
        {
            textBoxOriganizeMatricNum.Text = "";
        }

        private void textBoxOriganizeMatricNum_LostFocus(object sender, RoutedEventArgs e)
        {
            textBoxOriganizeMatricNum.Text = "Matric Number";
        }

        #endregion

        #region venue

        //show the list of all venues
        void showlistBoxAllVenue()
        {
            listViewVenue.Items.Clear();
            venueMgt.venueList.Clear();
            venueMgt.readFromFile();

            foreach (Venue ven in venueMgt.venueList)
            {
                string location = ven.getLocation();
                string capacity = ven.getCapacity().ToString();
                string bookingFee = ven.getBookingFee().ToString();
                listViewVenue.Items.Add(new { Location = location, Capacity = capacity, Fee = bookingFee });
            }
        }

        //show the list venue after searching
        void showlistBoxSearchVenue()
        {
            listViewVenue.Items.Clear();

            foreach (Venue ven in venueMgt.venueList)
            {
                string location = ven.getLocation();
                string capacity = ven.getCapacity().ToString();
                string bookingFee = ven.getBookingFee().ToString();
                listViewVenue.Items.Add(new { Location = location, Capacity = capacity, Fee = bookingFee });
            }
        }

        void showCalendarAvailableDates(int venueIndex)
        {
            calendarAvailableDates.BlackoutDates.Clear();

            foreach (int date in venueMgt.venueList[venueIndex].occupiedDates)
            {
                int day = date / 1000000;
                int month = (date / 10000) % 10;
                int year = date % 10000;
                string dateString = day.ToString() + "/" + month.ToString() + "/" + year.ToString();
                DateTime occupiedDate = new DateTime(year, month, day);
                calendarAvailableDates.BlackoutDates.Add(new CalendarDateRange(occupiedDate, occupiedDate));
            }
        }

        private void listViewVenue_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = listViewVenue.SelectedIndex;
            if (index != -1)
                showCalendarAvailableDates(index);
        }

        private void buttonDisplayAllVenue_Click(object sender, RoutedEventArgs e)
        {
            showlistBoxAllVenue();
        }

        private void SubmitVenue_Click(object sender, RoutedEventArgs e)
        {
            int index = listViewVenue.SelectedIndex;
            
            if (index == -1)
            {
                MessageBox.Show("Please select a venue.");
                return;
            }
            
            string location = venueMgt.venueList[index].getLocation();
            //change the display of event info
            labelVenue.Content = location;
        }

        private void listViewVenue_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            int index = listViewVenue.SelectedIndex;
            if (index != -1)
            {
                string location = venueMgt.venueList[index].getLocation();
                labelVenue.Content = location;
            }
        }

        private void buttonSearchVenue_Click(object sender, RoutedEventArgs e)
        {
            //initialize the list of my venues
            showlistBoxAllVenue();

            //if there is no input condition
            try
            {
                if (textBoxSearchMinCapacity.Text != String.Empty)
                {
                    int minCapacity = Convert.ToInt32(textBoxSearchMinCapacity.Text);

                    for (int i = 0; i < venueMgt.venueList.Count; i++)
                        if (venueMgt.venueList[i].getCapacity() < minCapacity)
                        {
                            venueMgt.venueList.RemoveAt(i);
                            i = -1;
                        }
                }

                if (textBoxSearchMaxFee.Text != String.Empty)
                {
                    double maxFee = Convert.ToDouble(textBoxSearchMaxFee.Text);

                    for (int i = 0; i < venueMgt.venueList.Count; i++)
                        if (venueMgt.venueList[i].getBookingFee() > maxFee)
                        {
                            venueMgt.venueList.RemoveAt(i);
                            i = -1;
                        }
                }

                if (datePickerSearchVenueDate.SelectedDate != null)
                {                  
                    int year = datePickerSearchVenueDate.SelectedDate.Value.Year;
                    int month = datePickerSearchVenueDate.SelectedDate.Value.Month;
                    int day = datePickerSearchVenueDate.SelectedDate.Value.Day;
                    int targetDate = 1000000 * day + 10000 * month + year;

                    DateTime searchDate = new DateTime(year, month, day);
                    
                    if (DateTime.Compare(searchDate, DateTime.Now) < 0)
                    {
                        MessageBox.Show("The date you searched should not be earlier than current date.");
                        return;
                    }

                    for (int i = 0; i < venueMgt.venueList.Count; i++)
                    {
                        foreach (int date in venueMgt.venueList[i].occupiedDates)
                        {
                            if (date == targetDate)
                            {
                                venueMgt.venueList.RemoveAt(i);
                                i = -1;
                                break;
                            }
                        }
                    }
                }
            }
            catch
            {
                MessageBox.Show("Please enter at least one condition.");
            }
            showlistBoxSearchVenue();
        }

        #endregion

        #region budget

        void showlistBoxBudget()
        {
            budgetItemListView.Items.Clear();
            myBudgetList.Clear();
            budgetMgt.readFromFile();

            foreach (int index in stu.createdEventList)
            {
                foreach (Budget bud in budgetMgt.budgetList)
                {
                    if (bud.getEventID() == index)
                    {
                        myBudgetList.Add(bud);
                    }
                }
            }

            if (listViewMyEvent.SelectedIndex != -1)
            {
                int id = listViewMyEvent.SelectedIndex;
                if (id < myBudgetList.Count)
                {
                    labelTotalBudget.Content = "Total budget: $" + myBudgetList[id].getTotalBudget();

                    //calculate the available budget
                    double ava = myBudgetList[id].getTotalBudget();
                    double spent = myBudgetList[id].getBudgetSpent();
                    ava -= spent;

                    //if there is a budget surplus, the color is green, otherwise it is red
                    if (ava >= 0)
                        labelAvaBudget.Foreground = Brushes.Green;
                    else
                        labelAvaBudget.Foreground = Brushes.Red;
                    
                    labelAvaBudget.Content = "Budget available: $" + ava;
                    labelSpentBudget.Content = "Budget spent: $" + myBudgetList[id].getBudgetSpent();

                    for (int i = 0; i < myBudgetList[id].budgetItem.Count; i++)
                    {
                        Item item = myBudgetList[id].budgetItem[i];
                        budgetItemListView.Items.Add(new { Item = item.name, Spent = item.amountSpent, Total = item.amountTotal });
                    }
                }
            }
        }

        void buttonAddBudgetItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int id = myEventList[listViewMyEvent.SelectedIndex].getEventID();
                string name = textBoxItemName.Text;
                double amountSpent = Convert.ToDouble(textBoxAmountSpent.Text);
                double amountRemained = Convert.ToDouble(textBoxAmountRemained.Text);

                budgetMgt.addItem(id, name, amountSpent, amountRemained);
                budgetMgt.writeToFile();
                showlistBoxBudget();
            }
            catch
            {
                MessageBox.Show("Please enter correct info.");
            }
        }

        void buttonEditBudgetItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int id = myEventList[listViewMyEvent.SelectedIndex].getEventID();
                int itemID = budgetItemListView.SelectedIndex;
                string name = textBoxItemName.Text;
                double amountSpent = Convert.ToDouble(textBoxAmountSpent.Text);
                double amountRemained = Convert.ToDouble(textBoxAmountRemained.Text);

                budgetMgt.editItem(id, itemID, name, amountSpent, amountRemained);
                budgetMgt.writeToFile();
                showlistBoxBudget();
            }
            catch
            {
                MessageBox.Show("Please select an item of the budget.");
            }
        }

        void buttonDelBudgetItem_Click(object sender, RoutedEventArgs e)
        {   
            try
            {
                int id = myEventList[listViewMyEvent.SelectedIndex].getEventID();
                int itemID = budgetItemListView.SelectedIndex;

                budgetMgt.deleteItem(id, itemID);
                budgetMgt.writeToFile();
                showlistBoxBudget();
            }
            catch
            {
                MessageBox.Show("Please select an item of the budget.");
            }
        }
         
        private void budgetItemListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            textBoxItemName.Clear();
            textBoxAmountSpent.Clear();
            textBoxAmountRemained.Clear();

            int eventIndex = listViewMyEvent.SelectedIndex;
            int budgetIndex = budgetItemListView.SelectedIndex;

            if (eventIndex != -1 && budgetIndex >= 0 && budgetIndex < myBudgetList[eventIndex].budgetItem.Count)
            {
                Item item = myBudgetList[eventIndex].getBudgetItem(budgetIndex);
                textBoxItemName.Text = item.name;
                textBoxAmountSpent.Text = item.amountSpent.ToString();
                textBoxAmountRemained.Text = item.amountTotal.ToString();
            }
        }
        
        #endregion

        #region registration

        void showlistBoxReg()
        {
            RegGrid.Children.Clear();
            int row = 0;
            for (int i = 0; i < regEvt.regEventList.Count; i++)
            {
                if (regEvt.checkExistance(i))
                {
                    TextBlock eventName = new TextBlock();
                    TextBlock status = new TextBlock();

                    eventName.Text = regEvt.getEventName(i);
                    status.Text = regEvt.getStatus(i);

                    RowDefinition newRow = new RowDefinition();
                    newRow.Height = new GridLength(20);
                    RegGrid.RowDefinitions.Add(newRow);

                    Grid.SetRow(eventName, row);
                    Grid.SetColumn(eventName, 0);
                    Grid.SetRow(status, row);
                    Grid.SetColumn(status, 1);

                    RegGrid.Children.Add(eventName);
                    RegGrid.Children.Add(status);
                    row++;
                }
            }
            displayNumberOfEventsRegistrated();
        }

        void showlistViewRegPro()
        {

            listViewRegPro.Items.Clear();
            string eventName = myEventList[listViewMyEvent.SelectedIndex].getEventName();
            for (int i = 0; i < regProMgt.getNoOfRegInARegProList(myEventList[listViewMyEvent.SelectedIndex].getEventName()); i++)
            {
                listViewRegPro.Items.Add(new { StudentName = regProMgt.getStudentName(eventName, i), ID = regProMgt.getStudentID(eventName, i), Status = regProMgt.getStatus(eventName, i) });
            }
        }

        private void displayNumberOfRegistration()
        {
            string eventName = myEventList[listViewMyEvent.SelectedIndex].getEventName();
            labelTotalReg.Content = "Total number of registrations: " + regProMgt.getNoOfRegInARegProList(eventName);
            labelAccReg.Content = "Number of accepted: " + regProMgt.getNoOfAccInARegProList(eventName);
            labelRejReg.Content = "Number of rejected: " + regProMgt.getNoOfRejInARegProList(eventName);
            int numPen;
            numPen = regProMgt.getNoOfRegInARegProList(eventName) - regProMgt.getNoOfAccInARegProList(eventName) - regProMgt.getNoOfRejInARegProList(eventName);
            labelPenReg.Content = "Number of pending: " + numPen;

        }

        private void displayNumberOfEventsRegistrated()
        {
            labelNoReg.Content = "Total number of registrations: " + regEvt.getTotalNumOfReg();
            labelNoAcc.Content = "Number of accepted: " + regEvt.getTotalNumOfAcc();
            labelNoRej.Content = "Number of rejected: " + regEvt.getTotalNumOfRej();
            int numPen;
            numPen = regEvt.getTotalNumOfReg() - regEvt.getTotalNumOfAcc() - regEvt.getTotalNumOfRej();
            labelNoPen.Content = "Number of pending: " + numPen;
        }

        private void buttonRegister_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                    for (int i = 0; i < myEventList.Count; i++)
                    {
                        if (myEventList[i].getEventName() == listViewAllEvent.SelectedItem.ToString())
                        {
                            MessageBox.Show("You cannot register your own event!");
                            showlistBoxReg();
                            return;
                        }
                    }
                    if (regEvt.alreadyRegistered(listViewAllEvent.SelectedItem.ToString()))
                    {
                        MessageBox.Show("You have already registered the event!");
                        showlistBoxReg();
                        return;
                    }
                    regEvt.registerAnEvent(listViewAllEvent.SelectedItem.ToString());
                    showlistBoxReg();
                
            }
            catch
            {
                MessageBox.Show("Please select an event in all events list!");
            }
        }

        private void buttonCancel_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                string eventName = listViewAllEvent.SelectedItem.ToString();

                foreach (Event eve in eventMgt.eventList)
                {
                    if (eventName == eve.getEventName())
                    {
                        regEvt.cancelRegistration(listViewAllEvent.SelectedItem.ToString());
                        break;
                    }
                }
                showlistBoxReg();
            }
            catch
            {
                MessageBox.Show("Please select an event in all events list!");
            }

        }

        private void buttonApprove2_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                {
                    string cmbvalue = "Accepted";
                    regProMgt.setRegStatus(myEventList[listViewMyEvent.SelectedIndex].getEventName(), listViewRegPro.SelectedIndex, cmbvalue);
                    showlistViewRegPro();
                    displayNumberOfRegistration();
                }
            }
            catch
            {
                MessageBox.Show("Please select a student!");
            }
        }

        private void buttonPending2_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                {
                    string cmbvalue = "Pending";
                    regProMgt.setRegStatus(myEventList[listViewMyEvent.SelectedIndex].getEventName(), listViewRegPro.SelectedIndex, cmbvalue);
                    showlistViewRegPro();
                    displayNumberOfRegistration();
                }
            }
            catch
            {
                MessageBox.Show("Please select a student!");
            }
        }

        private void buttonRejected2_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                {
                    string cmbvalue = "Rejected";
                    regProMgt.setRegStatus(myEventList[listViewMyEvent.SelectedIndex].getEventName(), listViewRegPro.SelectedIndex, cmbvalue);
                    showlistViewRegPro();
                    displayNumberOfRegistration();
                }
            }
            catch
            {
                MessageBox.Show("Please select a student!");
            }
        }

        #endregion

        #region others

        private void validateTextDouble(object sender, TextChangedEventArgs e)
        {
            {
                Exception X = new Exception();
                TextBox T = (TextBox)sender;
                try
                {
                    if (T.Text != "-")
                    {
                        double x = double.Parse(T.Text);
                        if (T.Text.Contains(','))
                            throw X;
                    }
                }
                catch (Exception)
                {
                    try
                    {
                        int CursorIndex = T.SelectionStart - 1;
                        T.Text = T.Text.Remove(CursorIndex, 1);
                        
                        //Align Cursor to same index
                        T.SelectionStart = CursorIndex;
                        T.SelectionLength = 0;
                    }
                    catch (Exception) { }
                }
            }
        }

        private void validateTextInt(object sender, TextCompositionEventArgs e)
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


        private void buttonExit1_Click(object sender, RoutedEventArgs e)
        {
            regNotification.clear(regEvt);
            regEvt.saveChangesToFile();
            regProMgt.saveAllChangesToFile();
            topTaskMgt.saveAllChangesToFile();
            taskNotification.saveChangesToFile(topTaskMgt);
            if (myBudgetList.Count != 0)
                budgetNtf.writeToFile(myBudgetList);
            if (myEventList.Count != 0)
                eventsNtf.writeToFile(myEventList);
            this.Close();
        }

        private void buttonLogout1_Click(object sender, RoutedEventArgs e)
        {
            regNotification.clear(regEvt);
            regEvt.saveChangesToFile();
            regProMgt.saveAllChangesToFile();
            topTaskMgt.saveAllChangesToFile();
            taskNotification.saveChangesToFile(topTaskMgt);
            if(myBudgetList.Count!=0)
                budgetNtf.writeToFile(myBudgetList);
            if (myEventList.Count != 0)
                eventsNtf.writeToFile(myEventList);

            MainWindow restart = new MainWindow();
            restart.Show();
            this.Close();
        }

        //show timer by_songgp
        public void ShowCurTimer(object sender, EventArgs e)
        {

            //get Monday...
            timer1.Text = "Today is " + DateTime.Now.ToString("dddd", new System.Globalization.CultureInfo("en-sg"));
            timer1.Text += " ";
            //date
            timer1.Text += DateTime.Now.ToString("dd-MM-yyyy");   
            timer1.Text += "    ";
            //time
            timer1.Text += "Current time is: " + DateTime.Now.ToString("HH:mm:ss");
        }

        private void UpdateInfo_Click(object sender, RoutedEventArgs e)
        {
            var updatePersonalInfo = new UpdateInfo();
            updatePersonalInfo.Show();
        }
        #endregion
    }
}

     

 
        
 
