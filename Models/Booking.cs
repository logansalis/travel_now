using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TravelNow.Models
{
    public class Booking
    {
        [Key]
        public int BookingID {get; set;}

        [Required]
        [DataType(DataType.Currency)]
        public double Total {get; set;}

        [Required]
        [DataType(DataType.Date)]
        [Display(Name="Check In")]
        public DateTime CheckIn {get; set;}

        [Required]
        [DataType(DataType.Date)]
        [Display(Name="Check Out")]
        public DateTime CheckOut {get; set;}

        [Required(ErrorMessage="Number of guests required")]
        [Range(1, Int32.MaxValue, ErrorMessage="Must be a positive number.")]
        [Display(Name="# Guests")]
        public int GuestNum {get; set;}

        [Required(ErrorMessage="Number of pets required")]
        [Range(0, Int32.MaxValue, ErrorMessage="Must be a positive number.")]
        [Display(Name="# Pets")]
        public int PetNum {get; set;}

        [Required]
        public int ListingID {get; set;}

        [Required]
        public int UserID {get; set;}

        public Listing Listing {get; set;}
        public User User {get; set;}
    }
}