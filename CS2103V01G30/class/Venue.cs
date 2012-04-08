/*************************************************************************** 
 * Class name:   Venue                                                     *
 *                                                                         *
 * Author:  NUS CS2103 Project Group 30                                    *
 *                                                                         *
 * Purpose:  Store the information of a venue                              *
 *                                                                         *
 * Usage:   Automatically called every time then programs starts           *
 *                                                                         *
 ***************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VenueManagement
{
    class Venue
    {
        int venueID;
        string location;
        int capacity;
        double bookingFee;
        
        public List<int> occupiedDates;

        //constructor
        public Venue(int id, string loc, int cap, double fee)
        {
            venueID = id;
            location = loc;
            capacity = cap;
            bookingFee = fee;
            occupiedDates = new List<int>();
        }


        //modifiers
        public void setVenueID(int id) { venueID = id; }
        public void setLocation(string loc) { location = loc; }
        public void setCapacity(int cap) { capacity = cap; }
        public void setBookingFee(double fee) { bookingFee = fee; }

        //modifiers of available dates
        //add an available date in ascending order
        public void addOccupiedDate(int date)
        {
            occupiedDates.Add(date);
        }

        public void addOccupiedPeriod(int startDate, int endDate)
        {
            int day = startDate / 1000000;
            int month = (startDate / 10000) % 10;
            int year = startDate % 10000;
            DateTime startDateTime = new DateTime(year, month, day);

            day = endDate / 1000000;
            month = (endDate / 10000) % 10;
            year = endDate % 10000;
            DateTime endDateTime = new DateTime(year, month, day);

            for (DateTime date = startDateTime; date <= endDateTime; date = date.AddDays(1))
            {
                int intDate = 1000000 * date.Day + 10000 * date.Month + date.Year;
                addOccupiedDate(intDate);
            }
        }

        //delete a certain date
        public void deleteOccupiedDate(int date)
        {
            for (int i = 0; i < occupiedDates.Count; i++)
                if (date == occupiedDates[i])
                {
                    occupiedDates.RemoveAt(i);
                    break;
                }
        }

        //accessors
        public int getVenueID() { return venueID; }
        public string getLocation() { return location; }
        public int getCapacity() { return capacity; }
        public double getBookingFee() { return bookingFee; }
    }
}