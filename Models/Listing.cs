using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TravelNow.Models
{
    public class Listing
    {
        [Key]
        public int ListingID {get; set;}

        [Required(ErrorMessage="Title required")]
        [MinLength(5, ErrorMessage="Must contain at least 5 characters")]
        [MaxLength(150)]
        public string Title {get; set;}

        [Required(ErrorMessage="Description required")]
        [DataType(DataType.Text)]
        [MinLength(10, ErrorMessage="Must contain at least 10 characters")]
        public string Description {get; set;}

        [Required]
        [MaxLength(45)]
        public string Type {get; set;}

        [Required]
        [DataType(DataType.Currency)]
        public double Price {get; set;}

        [Required(ErrorMessage="Number of guests required")]
        [Range(1, Int32.MaxValue, ErrorMessage="Must be greater than 1")]
        [Display(Name = "# Guests")]
        public int MaxGuests {get; set;}

        [Required(ErrorMessage="Number of bedroom(s) required")]
        [Range(1, Int32.MaxValue, ErrorMessage="Must be greater than 1")]
        [Display(Name="# Bedrooms")]
        public int BedNum {get; set;}

        [Required(ErrorMessage="Number of bathroom(s) required")]
        [Range(1, Int32.MaxValue, ErrorMessage="Must be greater than 1")]
        [Display(Name="# Bathrooms")]
        public double BathNum {get; set;}

        [Required(ErrorMessage="Specify if the property is pet friendly")]
        [Display(Name="Pet Friendly")]
        public bool PetFriendly {get; set;}


        public string Amenities {get; set;}

        [Required]
        public int HostID {get; set;}
        public User Host {get; set;}

        [Required]
        public int AddressID {get; set;}
        public Address Address {get; set;}

        public List<Booking> Bookings {get; set;}
        public List<ListingReview> Reviews {get; set;}
    }
}