using System;
using System.Collections.Generic;

namespace TravelNow.Models
{
    public class SearchViewModel
    {
        public int LoggedIn {get; set;} = 0;
        public List<Listing> Featured {get; set;}
        public List<Listing> Results {get; set;}
        public SearchCriteria SearchCriteria {get; set;}
    }
}