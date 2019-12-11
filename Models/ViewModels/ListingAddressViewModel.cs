using System.Collections.Generic;

namespace TravelNow.Models
{
    public class ListingAddressViewModel
    {
        public Listing NewListing {get; set;}
        public Address Address {get; set;}
        public Dictionary<string, bool> AmenitiesDict {get; set;} = new Dictionary<string, bool>() 
            {
                {"Garage", false},
                {"Covered Parking", false},
                {"Parking", false},
                {"On Site Laundry", false},
                {"Washer/Dryer", false},
                {"Kitchen", false},
                {"Kitchenette", false},
                {"Central Heating/Air", false},
                {"Air Conditioning", false},
                {"Heat", false},
                {"Fireplace", false},
                {"Patio/Balcony", false},
                {"Cable/Satelite TV", false},
                {"Wifi", false},
                {"Linens Provided", false},
                {"Toiletries Provided", false}
            };
    }
}