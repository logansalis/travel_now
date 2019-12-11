using System;
using System.ComponentModel.DataAnnotations;

namespace TravelNow.Models
{
    public class ListingReview
    {
        [Key]
        public int ReviewID {get; set;}

        [Required(ErrorMessage="Rating required")]
        [Range(1, 5)]
        public int Rating {get; set;}

        [Required(ErrorMessage="Review required")]
        [MinLength(10, ErrorMessage="Review must contain at least 10 characters")]
        public string Comment {get; set;}

        [Required]
        [DataType(DataType.Date)]
        public DateTime CreatedAt {get; set;} = DateTime.Now;

        [Required]
        public int CreatorID {get; set;}

        [Required]
        public int ListingID {get; set;}

        public User Creator {get; set;}
        public Listing Listing {get; set;}
    }
}