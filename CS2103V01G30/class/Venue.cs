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