using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelNow.Models
{
    public class User
    {
        [Key]
        public int UserID {get; set;}

        [Required(ErrorMessage = "First name required")]
        [MinLength(2, ErrorMessage="Must contain at least 2 characters")]
        [MaxLength(45)]
        [Display(Name="First Name")]
        public string FirstName {get; set;}

        [Required(ErrorMessage = "Last name required")]
        [MinLength(2, ErrorMessage="Must contain at least 2 characters")]
        [MaxLength(45)]
        [Display(Name="Last Name")]
        public string LastName {get; set;}

        [Required(ErrorMessage="Birthday required")]
        [DataType(DataType.Date)]
        [NotFutureDate]
        [Over18]
        [Display(Name = "Date of Birth")]
        public DateTime DOB {get;set;}

        [Required(ErrorMessage="Email address required")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email {get; set;}

        [Required(ErrorMessage="Password required")]
        [MinLength(8, ErrorMessage="Password must contain at least 8 characters")]
        [RegularExpression("^(?=.*[a-zA-Z])(?=.*[0-9])(?=.*[!@#$%^&*?]).+$", ErrorMessage="Must contain at least 1 letter, 1 number, and 1 special character (!@#$%^&*?)")]
        [DataType(DataType.Password)]
        public string Password {get; set;}

        [Required(ErrorMessage="Password conformation required")]
        [DataType(DataType.Password)]
        [NotMapped]
        [Compare("Password")]
        [Display(Name="Confirm Password")]
        public string Confirm {get; set;}
        
        public string Avatar {get; set;} = "/images/default_avatar.png";

        [Required]
        public int AddressID {get; set;}

        public Address Address {get; set;}

        public List<Listing> Listings {get; set;}
        public List<Booking> Bookings {get; set;}

        public List<ListingReview> CreatedReviews {get; set;}

    }

    public class NotFutureDate : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if(DateTime.Compare(DateTime.Now, (DateTime)value) < 0)
            {
                return new ValidationResult("No future dates.");
            }
            return ValidationResult.Success;
        }
    }

    public class Over18 : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime dob = (DateTime)value;
            if(DateTime.Now.Year - dob.Year < 18)
            {
                return new ValidationResult("Must be 18 or older.");
            }
            return ValidationResult.Success;
        }
    }
}