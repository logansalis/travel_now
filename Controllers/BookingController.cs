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
    public class BookingController : Controller
    {
        private DBContext DBContext { get; }

        public BookingController(DBContext context)
        {
            DBContext = context;
        }

        [HttpGet("TravelNow/Booking/{BookID}/Confirm")]
        public ViewResult ConfirmBooking(int BookID)
        {
            Booking booking = DBContext.Bookings
                .Include(b => b.Listing)
                .ThenInclude(l => l.Address)
                .Include(b => b.Listing)
                .ThenInclude(l => l.Host)
                .Include(b => b.User)
                .FirstOrDefault(b => b.BookingID == BookID);

            return View(booking);
        }

        [HttpGet("TravelNow/Booking/{BookID}/Confirm/Success")]
        public ViewResult Success(int BookID)
        {
            Booking booking = DBContext.Bookings
                .Include(b => b.Listing)
                .ThenInclude(l => l.Address)
                .Include(b => b.Listing)
                .ThenInclude(l => l.Host)
                .Include(b => b.User)
                .FirstOrDefault(b => b.BookingID == BookID);

            return View(booking);
        }
    }
}