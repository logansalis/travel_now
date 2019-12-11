using System.Collections.Generic;

namespace TravelNow.Models
{
    public class ListDetailsViewModel
    {
        public Listing Listing {get; set;}
        public List<string> Amenities {get; set;}
        public int UserID {get; set;} = -1;
        public Booking NewBooking {get; set;}
    }
}