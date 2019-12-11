using System.Collections.Generic;

namespace TravelNow.Models
{
    public class HostViewModel
    {
        public List<Listing> Listings {get; set;}
        public User User {get; set;}
        public int ListID {get; set;}
        public int LoggedIn {get; set;} = 0;
    }
}