using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TravelNow.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;

namespace TravelNow.Controllers
{
    public class SearchController : Controller
    {
        private DBContext DBContext { get; }
        private List<string> States {get;} = new List<string>() {"Alaska", "Alabama", "Arkansas", "Arizona", "California", "Colorado", "Connecticut", "District of Columbia", "Delaware", "Florida", "Georgia", "Hawaii", "Iowa", "Idaho", "Illinois", "Indiana", "Kansas", "Kentucky", "Louisiana", "Massachusetts", "Maryland", "Maine", "Michigan", "Minnesota", "Missouri", "Mississippi", "Montana", "North Carolina", "North Dakota", "Nebraska", "New Hampshire", "New Jersey", "New Mexico", "Nevada", "New York", "Ohio", "Oklahoma", "Oregon", "Pennsylvania", "Puerto Rico", "Rhode Island", "South Carolina", "South Dakota", "Tennessee", "Texas", "Utah", "Virginia", "Vermont", "Washington", "Wisconsin", "West Virginia", "Wyoming"};

        public SearchController(DBContext context)
        {
            DBContext = context;
        }

        [HttpGet("TravelNow/SearchResults")]
        public ViewResult SearchResults()
        {
            SearchViewModel ViewModel = new SearchViewModel();
            if(HttpContext.Session.GetInt32("SearchFiter") == null)
            {
                ViewModel.Results = DBContext.Listings.Include(l => l.Address).ToList();
            }

            return View(ViewModel);
        }

        [HttpPost("TravelNow/SearchResults")]
        public ViewResult SearchResults(SearchViewModel modelData)
        {
            if(HttpContext.Session.GetInt32("LoggedIn") != null)
            {
                modelData.LoggedIn = (int)HttpContext.Session.GetInt32("LoggedIn");
            }

            modelData.Results = DBContext.Listings.Include(l => l.Address).ToList();
            if(modelData.SearchCriteria.Location != null)
            {
                modelData.Results = DBContext.Listings
                    .Include(l => l.Address)
                    .Where(l => (modelData.SearchCriteria.Location.Contains(l.Address.StreetAddress1) 
                        && modelData.SearchCriteria.Location.Contains(l.Address.City) 
                        && modelData.SearchCriteria.Location.Contains(l.Address.State))
                        || (modelData.SearchCriteria.Location.Contains(l.Address.StreetAddress1) 
                        && modelData.SearchCriteria.Location.Contains(l.Address.City))
                        || (modelData.SearchCriteria.Location.Contains(l.Address.StreetAddress1))
                        || (modelData.SearchCriteria.Location.Contains(l.Address.City) 
                        && modelData.SearchCriteria.Location.Contains(l.Address.State))
                        || modelData.SearchCriteria.Location.Equals(l.Address.City)
                        || modelData.SearchCriteria.Location.Equals(l.Address.State)
                        || States.Contains(modelData.SearchCriteria.Location))
                    .ToList();
            }
            

            if(modelData.SearchCriteria.GuestNum != 0)
            {
                modelData.Results = modelData.Results.Where(l => l.MaxGuests >= modelData.SearchCriteria.GuestNum).ToList();
            }

            if(modelData.SearchCriteria.PetFriendly == true)
            {
                modelData.Results = modelData.Results.Where(l => l.PetFriendly == true).ToList();
            }

            return View(modelData);
        }
    }
}