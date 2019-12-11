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
    public class ListingController : Controller
    {
        private DBContext DBContext { get; }

        public ListingController(DBContext context)
        {
            DBContext = context;
        }

        [HttpGet("TravelNow/Listing/About")]
        public ViewResult About()
        {
            int ViewModel = -1;
            if(HttpContext.Session.GetInt32("UserID") != null)
            {
                ViewModel = (int)HttpContext.Session.GetInt32("UserID");
            }
            return View(ViewModel);
        }

        [HttpGet("TravelNow/Listing/New")]
        public IActionResult NewListing()
        {
            if(HttpContext.Session.GetInt32("UserID") == null)
            {
                return RedirectToAction("Login", "Login");
            }

            ListingAddressViewModel ViewModel = new ListingAddressViewModel();

            return View(ViewModel);
        }

        [HttpPost("TravelNow/Listing/New")]
        public IActionResult NewListing(ListingAddressViewModel modelData)
        {
            if(ModelState.IsValid)
            {
                DBContext.Addresses.Add(modelData.Address);
                DBContext.SaveChanges();

                modelData.NewListing.HostID = (int)HttpContext.Session.GetInt32("UserID");
                modelData.NewListing.AddressID = DBContext.Addresses.LastOrDefault().AddressID;
                modelData.NewListing.Amenities = string.Join( ", ", modelData.AmenitiesDict.Where(d => d.Value != false).Select(d => d.Key));
                DBContext.Listings.Add(modelData.NewListing);
                DBContext.SaveChanges();
                return RedirectToAction("Profile", "Home");
            }

            return View(modelData);
        }

        [HttpGet("TravelNow/Listing/Edit/{ListID}")]
        public IActionResult EditListing(int ListID)
        {
            if(HttpContext.Session.GetInt32("UserID") == null)
            {
                return RedirectToAction("Login", "Login");
            }

            ListingAddressViewModel ViewModel = new ListingAddressViewModel()
            {
                NewListing = DBContext.Listings
                    .Include(l => l.Address)
                    .Include(l => l.Bookings)
                    .ThenInclude(b => b.User)
                    .FirstOrDefault(l => l.ListingID == ListID)
            };
            ViewModel.Address = ViewModel.NewListing.Address;

            foreach(string entry in ViewModel.AmenitiesDict.Keys.ToList())
            {
                if(ViewModel.NewListing.Amenities.Contains(entry))
                {
                    ViewModel.AmenitiesDict[entry] = true;
                }
            }

            return View(ViewModel);
            
        }

        [HttpPost("TravelNow/Listing/Edit/{ListID}")]
        public IActionResult EditListing(ListingAddressViewModel modelData, int ListID)
        {
            if(ModelState.IsValid)
            {
                modelData.NewListing.Amenities = string.Join( ", ", modelData.AmenitiesDict.Where(d => d.Value != false).Select(d => d.Key));

                Listing ListingUpdate  = DBContext.Listings.FirstOrDefault(l => l.ListingID == ListID);
                ListingUpdate.Title = modelData.NewListing.Title;
                ListingUpdate.Type = modelData.NewListing.Type;
                ListingUpdate.Price = modelData.NewListing.Price;
                ListingUpdate.BedNum = modelData.NewListing.BedNum;
                ListingUpdate.BathNum = modelData.NewListing.BathNum;
                ListingUpdate.MaxGuests = modelData.NewListing.MaxGuests;
                ListingUpdate.PetFriendly = modelData.NewListing.PetFriendly;
                ListingUpdate.Description = modelData.NewListing.Description;
                ListingUpdate.Amenities = string.Join( ", ", modelData.AmenitiesDict.Where(d => d.Value != false).Select(d => d.Key));

                Address AddressUpdate = DBContext.Addresses.FirstOrDefault(a => a.AddressID == modelData.Address.AddressID);
                AddressUpdate.StreetAddress1 = modelData.Address.StreetAddress1;
                AddressUpdate.StreetAddress2 = modelData.Address.StreetAddress2;
                AddressUpdate.City = modelData.Address.City;
                AddressUpdate.State = modelData.Address.State;
                AddressUpdate.Zip = modelData.Address.Zip;
                
                DBContext.SaveChanges();
                return Redirect($"/TravelNow/Listing/Details/{ListID}");
            }
            return View(modelData);
        }

        [HttpGet("TravelNow/Listing/Delete/{ListID}")]
        public RedirectToActionResult Delete(int ListID)
        {
            DBContext.Remove(DBContext.Listings.FirstOrDefault(l => l.ListingID == ListID));
            DBContext.SaveChanges();
            return RedirectToAction("AccountListings", "Home");
        }

        [HttpGet("TravelNow/Listing/Details/{ListID}")]
        public ViewResult ListingDetails(int ListID)
        {
            ListDetailsViewModel ViewModel = new ListDetailsViewModel()
            {
                Listing = DBContext.Listings
                    .Include(l => l.Host)
                    .Include(l => l.Address)
                    .Include(l => l.Bookings)
                    .FirstOrDefault(l => l.ListingID == ListID),
            };

            ViewModel.Amenities = ViewModel.Listing.Amenities.Split(", ").ToList();

            if(HttpContext.Session.GetInt32("UserID") != null)
            {
                ViewModel.UserID = (int)HttpContext.Session.GetInt32("UserID");
            }

            return View(ViewModel);
        }
        [HttpPost("TravelNow/Listing/Details/{ListID}")]
        public IActionResult ListingDetails(ListDetailsViewModel modelData, int ListID)
        {
            if(HttpContext.Session.GetInt32("UserID") != null)
            {
                modelData.UserID = (int)HttpContext.Session.GetInt32("UserID");
            }
            modelData.Listing = DBContext.Listings
                    .Include(l => l.Host)
                    .Include(l => l.Address)
                    .Include(l => l.Bookings)
                    .FirstOrDefault(l => l.ListingID == ListID);

            if(ModelState.IsValid)
            {
                modelData.NewBooking.Total = (modelData.Listing.Price * (modelData.NewBooking.CheckOut - modelData.NewBooking.CheckIn).TotalDays) + (20 * modelData.NewBooking.PetNum);
                modelData.NewBooking.ListingID = ListID;
                modelData.NewBooking.UserID = modelData.UserID;
                DBContext.Bookings.Add(modelData.NewBooking);
                DBContext.SaveChanges();

                modelData.NewBooking.BookingID = DBContext.Bookings.LastOrDefault().BookingID;

                return Redirect($"/TravelNow/Booking/{modelData.NewBooking.BookingID}/Confirm");    
            }
            modelData.Amenities = modelData.Listing.Amenities.Split(", ").ToList();
            return View(modelData);
        }

        [HttpGet("TravelNow/Listing/{ListID}/Host/{HostID}")]
        public ViewResult HostDetails(int ListID, int HostID)
        {
            HostViewModel ViewModel = new HostViewModel()
            {
                Listings = DBContext.Listings.Include(l => l.Address).Where(l => l.HostID == HostID).ToList(),
                User = DBContext.Users.Include(u => u.Address).FirstOrDefault(u => u.UserID == HostID),
                ListID = ListID
            };

            if(HttpContext.Session.GetInt32("LoggedIn") != null)
            {
                ViewModel.LoggedIn = (int)HttpContext.Session.GetInt32("LoggedIn");
            }

            return View(ViewModel);
        }
    }
}