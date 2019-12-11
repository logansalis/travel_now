using System;

namespace TravelNow.Models
{
    public class ProfileViewModel
    {
        public User User {get; set;}
        public int ListCount {get; set;}
        public double AvgListPrice {get; set;}
        public double AvgNumBooking {get; set;}
        public int BookCount {get; set;}
        public double AvgSpend {get; set;}
        public double AvgStayLength {get; set;}

    }
}