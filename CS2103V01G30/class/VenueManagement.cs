using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VenueManagement
{
    class VenueMgt
    {
        public List<Venue> venueList = new List<Venue>();

        public void readFromFile()
        {
            using (StreamReader reader = new StreamReader("venues.txt"))
            {
                venueList.Clear();
                string line;

                while ((line = reader.ReadLine()) != null && line != null)
                {
                    string[] parts = line.Split('|');
                    int venueID = Convert.ToInt32(parts[0]);
                    string location = parts[1];
                    int capacity = Convert.ToInt32(parts[2]);
                    double bookingFee = Convert.ToDouble(parts[3]);

                    //create a venue object
                    Venue tempVenue = new Venue(venueID, location, capacity, bookingFee);

                    //input the list of available dates
                    if (parts[4] != null)
                    {
                        string[] dates = parts[4].Split(',');
                        foreach (string date in dates)
                            tempVenue.occupiedDates.Add(Convert.ToInt32(date));
                    }

                    //add the venue object to the venue list
                    venueList.Add(tempVenue);
                }
            }
        }

        public void writeToFile()
        {
            using (StreamWriter writer = new StreamWriter("venues.txt"))
            {
                for (int i = 0; i < venueList.Count; i++)
                {
                    string venueID = venueList[i].getVenueID().ToString();
                    string location = venueList[i].getLocation();
                    string capacity = venueList[i].getCapacity().ToString();
                    string bookingFee = venueList[i].getBookingFee().ToString();
                    string venueInfo = venueID + "|" + location + "|" + capacity + "|" + bookingFee + "|";

                    //if the list of available dates is not empty
                    //write them to the file according the a certain format
                    if (venueList[i].occupiedDates.Count != 0)
                    {
                        int j;
                        for (j = 0; j < venueList[i].occupiedDates.Count - 1; j++)
                        {
                            venueInfo += venueList[i].occupiedDates[j].ToString();
                            venueInfo += ",";
                        }
                        venueInfo += venueList[i].occupiedDates[j].ToString();
                    }
                    writer.WriteLine(venueInfo);
                }
            }
        }
    }
}
