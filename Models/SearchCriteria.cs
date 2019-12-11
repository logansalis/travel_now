using System;
using System.ComponentModel.DataAnnotations;

namespace TravelNow.Models
{
    public class SearchCriteria
    {
        public string Location {get; set;}

        [DataType(DataType.Date)]
        public DateTime CheckIn {get; set;}

        [DataType(DataType.Date)]
        public DateTime CheckOut {get; set;}

        [Range(1, Int32.MaxValue, ErrorMessage="Must be a positive number")]
        public int GuestNum {get; set;}

        public bool PetFriendly {get; set;}
    }
}