using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TripAdvisor
{
    [DataContract]
    public class CustomHover
    {
        [DataMember(Name = "title")]
        public string title { get; set; }
        [DataMember(Name = "url")]
        public string url { get; set; }
        //[DataMember(Name = "titleUrl")]
        //public string titleUrl { get; set; }
    }

    [DataContract]
    public class Hotel
    {
        [DataMember(Name = "lat")]
        public double lat { get; set; }
        [DataMember(Name = "lng")]
        public double lng { get; set; }
        [DataMember(Name = "locId")]
        public int locId { get; set; }
        [DataMember(Name = "url")]
        public string url { get; set; }
        [DataMember(Name = "overviewWeight")]
        public double overviewWeight { get; set; }
        [DataMember(Name = "accommodationCategory")]
        public int accommodationCategory { get; set; }
        [DataMember(Name = "customHover")]
        public CustomHover customHover { get; set; }
        [DataMember(Name = "pinProminent")]
        public bool pinProminent { get; set; }
    }

    [DataContract]
    public class Restaurant
    {
        [DataMember(Name = "lat")]
        public double lat { get; set; }
        [DataMember(Name = "lng")]
        public double lng { get; set; }
        [DataMember(Name = "locId")]
        public int locId { get; set; }
        [DataMember(Name = "url")]
        public string url { get; set; }
        [DataMember(Name = "overviewWeight")]
        public double overviewWeight { get; set; }
        [DataMember(Name = "accommodationCategory")]
        public int accommodationCategory { get; set; }
        [DataMember(Name = "customHover")]
        public CustomHover customHover { get; set; }
        [DataMember(Name = "pinProminent")]
        public bool pinProminent { get; set; }
    }

    [DataContract]
    public class Attraction
    {
        [DataMember(Name = "lat")]
        public double lat { get; set; }

        [DataMember(Name = "lng")]
        public double lng { get; set; }

        [DataMember(Name = "locId")]
        public int locId { get; set; }

        [DataMember(Name = "url")]
        public string url { get; set; }
        
        //[DataMember(Name = "overviewWeight")]
        //public double overviewWeight { get; set; }
        
        //[DataMember(Name = "accommodationCategory")]
        //public int accommodationCategory { get; set; }

        [DataMember(Name = "customHover")]
        public CustomHover customHover { get; set; }

        //[DataMember(Name = "pinProminent")]
        //public bool pinProminent { get; set; }

        [DataMember(Name = "imgUrl")]
        public string imgUrl { get; set; }

        [DataMember(Name = "rating")]
        public float rating { get; set; }

        [DataMember(Name = "reviews")]
        public int reviews { get; set; }

        [DataMember(Name = "categories")]
        public List<string> categories { get; set; }

        public override bool Equals(object obj)
        {
            var attraction = obj as Attraction;
            if (attraction == null) return false;
            return attraction.url == url;
        }

        public override int GetHashCode()
        {
            return url.GetHashCode();
        }
    }

    [DataContract]
    public class AddressInfo
    {
        [DataMember(Name = "name")]
        public string name { get; set; }
        [DataMember(Name = "address")]
        public string address { get; set; }
    }

    [DataContract]
    public class FilterState
    {
        [DataMember(Name = "cat")]
        public int cat { get; set; }
    }

    [DataContract]
    public class Map
    {
        [DataMember(Name = "hotels")]
        public IList<Hotel> hotels { get; set; }
        [DataMember(Name = "restaurants")]
        public IList<Restaurant> restaurants { get; set; }
        [DataMember(Name = "attractions")]
        public IList<Attraction> attractions { get; set; }
        [DataMember(Name = "vacationrentals")]
        public IList<object> vacationrentals { get; set; }
        [DataMember(Name = "hotelsVisible")]
        public bool hotelsVisible { get; set; }
        [DataMember(Name = "disneyVisible")]
        public bool disneyVisible { get; set; }
        [DataMember(Name = "sponsorVisible")]
        public bool sponsorVisible { get; set; }
        [DataMember(Name = "addressInfo")]
        public AddressInfo addressInfo { get; set; }
        [DataMember(Name = "homeSponsored")]
        public bool homeSponsored { get; set; }
        [DataMember(Name = "filterState")]
        public FilterState filterState { get; set; }
    }


}