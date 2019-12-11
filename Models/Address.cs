using System;
using System.ComponentModel.DataAnnotations;

namespace TravelNow.Models
{
    public class Address
    {
        [Key]
        public int AddressID {get; set;}

        [Required(ErrorMessage="Street address required")]
        [MinLength(2, ErrorMessage="Must contain at least 2 characters")]
        [MaxLength(255)]
        [Display(Name="Street Address")]
        public string StreetAddress1 {get; set;}

        [MinLength(2, ErrorMessage="Must contain at least 2 characters")]
        [MaxLength(255)]
        [Display(Name="Apartment, suite, ect.")]
        public string StreetAddress2 {get; set;}

        [Required(ErrorMessage="City Required")]
        [MinLength(2, ErrorMessage="Must contain at least 2 characters")]
        [MaxLength(255)]
        public string City {get; set;}

        [Required(ErrorMessage="State required")]
        [MinLength(2, ErrorMessage="Must contain at least 2 characters")]
        [MaxLength(2, ErrorMessage="Please provide the state abbreviation.")]
        public string State {get; set;}

        [Required(ErrorMessage="Zip code required")]
        // [MinLength(5, ErrorMessage="Zip code must contain 5 numbers")]
        // [MaxLength(5, ErrorMessage="Zip code must contain 5 numbers")]
        [RegularExpression(@"^\d{5}$", ErrorMessage="Zip code must be a 5 digit code.")]
        public string Zip {get; set;}
    }
}