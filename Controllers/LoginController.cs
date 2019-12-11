using System;
using Microsoft.AspNetCore.Mvc;
using TravelNow.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Web;

namespace TravelNow.Controllers
{
    public class LoginController : Controller
    {
        private DBContext DBContext { get; }

        public LoginController(DBContext context)
        {
            DBContext = context;
        }

        [HttpGet("TravelNow/Login")]
        public ViewResult Login()
        {
            return View();
        }

        [HttpPost("TravelNow/Login")]
        public IActionResult Login(Credentials credentials)
        {
            if (ModelState.IsValid)
            {
                User user = DBContext.Users.FirstOrDefault(u => u.Email == credentials.Email);
                if (user == null)
                {
                    ModelState.AddModelError("Email", "Invalid Email or Password");
                    return View();
                }

                PasswordHasher<Credentials> hasher = new PasswordHasher<Credentials>();
                PasswordVerificationResult result = hasher.VerifyHashedPassword(credentials, user.Password, credentials.Password);
                if (result == 0)
                {
                    ModelState.AddModelError("NewLogin.Email", "Invalid Email or Password");
                    return View();
                }
                HttpContext.Session.SetInt32("UserID", user.UserID);
                HttpContext.Session.SetInt32("LoggedIn", 1);
                return RedirectToAction("Profile", "Home");
            }
            return View();
        }

        [HttpGet("TravelNow/SignUp")]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost("TravelNow/SignUp")]
        public IActionResult SignUp(UserAddressViewModel modelData)
        {
            if(ModelState.IsValid)
            {
                User user = DBContext.Users.FirstOrDefault(u => u.Email == modelData.NewUser.Email);
                if(user != null)
                {
                    ModelState.AddModelError("NewUser.Email", "There is already an account linked to this email.");
                    return View();
                }

                DBContext.Addresses.Add(modelData.Address);
                DBContext.SaveChanges();

                user = modelData.NewUser;
                user.AddressID = DBContext.Addresses.LastOrDefault().AddressID;
                PasswordHasher<User> hasher = new PasswordHasher<User>();
                user.Password = hasher.HashPassword(user, user.Password);
                DBContext.Users.Add(user);
                DBContext.SaveChanges();

                user = DBContext.Users.FirstOrDefault(u => u.Email == user.Email);
                HttpContext.Session.SetInt32("UserID", user.UserID);
                HttpContext.Session.SetInt32("LoggedIn", 1);
                return RedirectToAction("Profile", "Home");
            }
            
            string validationErrors = string.Join(",",
                    ModelState.Values.Where(E => E.Errors.Count > 0)
                    .SelectMany(E => E.Errors)
                    .Select(E => E.ErrorMessage)
                    .ToArray());
            Console.WriteLine($"********** {ModelState.ErrorCount} **********");
            Console.WriteLine($"********** {validationErrors} **********");
            return View();
        }
    }
}