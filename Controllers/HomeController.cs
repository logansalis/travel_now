using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TravelNow.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace TravelNow.Controllers
{
    public class HomeController : Controller
    {
        private DBContext DBContext { get; }

        public HomeController(DBContext context)
        {
            DBContext = context;
        }
        public IActionResult Index()
        {
            SearchViewModel ViewModel = new SearchViewModel();
            if(HttpContext.Session.GetInt32("LoggedIn") != null)
            {
                ViewModel.LoggedIn = (int)HttpContext.Session.GetInt32("LoggedIn");
            }

            return View(ViewModel);
        }

        [HttpGet("TravelNow/Account/Profile")]
        public IActionResult Profile()
        {
            if(HttpContext.Session.GetInt32("UserID") == null)
            {
                return RedirectToAction("Login", "Login");
            }
            User user = DBContext.Users
                .Include(u => u.Address)
                .Include(u => u.Listings)
                .Include(u => u.Bookings)
                .FirstOrDefault(u => u.UserID == (int)HttpContext.Session.GetInt32("UserID"));
            
            List<Booking> Bookings = DBContext.Bookings
                .Include(b => b.Listing)
                .Where(b => user.Listings.Any(l => l.ListingID == b.ListingID)).ToList();

            ProfileViewModel ViewModel = new ProfileViewModel()
            {
                User = user,
                ListCount = user.Listings.Count(),
                BookCount = user.Bookings.Count(),
            };

            if(user.Listings.Count() > 0)
            {
                ViewModel.AvgListPrice = user.Listings.Select(l => l.Price).Average();

            }
            return View(ViewModel);
        }

        [HttpGet("TravelNow/Account/Listings")]
        public IActionResult AccountListings()
        {
            if(HttpContext.Session.GetInt32("UserID") == null)
            {
                return RedirectToAction("Login", "Login");
            }

            User user = DBContext.Users
                .Include(u => u.Listings)
                .ThenInclude(l => l.Address)
                .FirstOrDefault(u => u.UserID == (int)HttpContext.Session.GetInt32("UserID"));

            user.Listings = user.Listings.OrderBy(l => l.Title).ToList();
            foreach (Listing item in user.Listings)
            {
                Console.WriteLine($"{item.Address.StreetAddress1}");
            }
            return View(user);
        }

        [HttpGet("TravelNow/Account/Bookings")]
        public IActionResult AccountBookings()
        {
            if(HttpContext.Session.GetInt32("UserID") == null)
            {
                return RedirectToAction("Login", "Login");
            }

            User user = DBContext.Users
                .Include(u => u.Bookings)
                .ThenInclude(b =>b.Listing)
                .ThenInclude(l => l.Address)
                .FirstOrDefault(u => u.UserID == (int)HttpContext.Session.GetInt32("UserID"));

            user.Bookings = user.Bookings.OrderBy(b => b.CheckIn).ToList();

            return View(user);
        }

        [HttpGet("TravelNow/Account/Logout")]
        public RedirectToActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
