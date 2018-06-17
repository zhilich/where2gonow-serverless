using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TripAdvisor;

namespace Where2GoNow.Utils
{
    public class GeoSearch
    {
        public static readonly IDictionary<string, string[]> Categories = new Dictionary<string, string[]>() {
            { "Sights & Landmarks", new [] { "Historic Sites", "Points of Interest & Landmarks", "Monuments & Statues", "Historic Walking Areas", "Architectural Buildings", "Scenic Drives", "Arenas & Stadiums", "Sacred & Religious Sites", "Lookouts", "Neighborhoods", "Observation Decks & Towers", "Churches & Cathedrals", "Educational sites", "Government Buildings", "Cemeteries", "Military Bases & Facilities", "Bridges", "Missions", "Scenic Walking Areas", "Ancient Ruins", "Battlefields", "Lighthouses", "Piers & Boardwalks", "Ships", "Auto Race Tracks", "Fountains", "Mysterious Sites", "Ranches" } },
            { "Nature & Parks", new [] { "Nature & Wildlife Areas", "Parks", "Hiking Trails", "National Parks", "Gardens", "Geologic Formations", "State Parks", "Beaches", "Bodies of Water", "Mountains", "Canyons", "Zoos", "Waterfalls", "Aquariums", "Caverns & Caves", "Volcanos", "Islands", "Forests", "Playgrounds", "Valleys", "Biking Trails", "Dams", "Deserts", "Hot Springs & Geysers", "Off-Road & ATV Trails", "Other Nature & Parks", "Ski & Snowboard Areas" } },
            { "Outdoor Activities", new [] { "Boat Tours", "Nature & Wildlife Tours", "Dolphin & Whale Watching", "Scuba & Snorkeling", "Zipline & Aerial Adventure Parks", "Kayaking & Canoeing", "River Rafting & Tubing", "Hiking Trails", "Bike Tours", "4WD, ATV & Off-Road Tours", "Eco Tours", "Fishing Charters & Tours", "Scenic Drives", "Beaches", "Adrenaline & Extreme Tours", "Gear Rentals", "Hiking & Camping Tours", "Stand-Up Paddleboarding", "Surfing, Windsurfing & Kitesurfing", "Water Sports", "Air Tours", "Other Outdoor Activities", "Horseback Riding Tours", "Swim with Dolphins", "Zoos", "Balloon Rides", "Waterskiing & Jetskiing", "Climbing Tours", "Parasailing & Paragliding", "Speed Boats Tours", "Sports Camps & Clinics", "Boat Rentals", "Ski & Snow Tours", "Running Tours", "Safaris", "Biking Trails", "Canyoning & Rappelling Tours", "Duck Tours", "Horse-Drawn Carriage Tours", "Jogging Paths & Tracks", "Off-Road & ATV Trails", "Shark Diving", "Ski & Snowboard Areas" } },
            { "Tours", new [] { "Boat Tours", "Private Tours", "Nature & Wildlife Tours", "Walking Tours", "City Tours", "Historical & Heritage Tours", "Dolphin & Whale Watching", "Food Tours", "Scuba & Snorkeling", "Zipline & Aerial Adventure Parks", "Kayaking & Canoeing", "Sightseeing Tours", "River Rafting & Tubing", "Segway Tours", "Bike Tours", "4WD, ATV & Off-Road Tours", "Cultural Tours", "Eco Tours", "Wine Tours & Tastings", "Fishing Charters & Tours", "Adrenaline & Extreme Tours", "Helicopter Tours", "Hiking & Camping Tours", "Stand-Up Paddleboarding", "Surfing, Windsurfing & Kitesurfing", "Water Sports", "Bus Tours", "Air Tours", "Brewery Tours", "Night Tours", "Swim with Dolphins", "Balloon Rides", "Day Trips from United States", "Ghost & Vampire Tours", "Waterskiing & Jetskiing", "Climbing Tours", "Parasailing & Paragliding", "Photography Tours", "Speed Boats Tours", "Bar, Club & Pub Tours", "Literary, Art & Music Tours", "Vespa, Scooter & Moped Tours", "Boat Rentals", "Scenic Railroads", "Movie & TV Tours", "Running Tours", "Self-Guided Tours & Rentals", "Shopping Tours", "Canyoning & Rappelling Tours", "Distillery Tours", "Duck Tours", "Factory Tours", "Hop-On Hop-Off Tours", "Horse-Drawn Carriage Tours", "Motorcycle Tours", "Rail Tours", "Shark Diving" } },
            { "Museums", new [] { "Specialty Museums", "Art Museums", "History Museums", "Military Museums", "Natural History Museums", "Observatories & Planetariums", "Science Museums", "Art Galleries", "Children's Museums" } },
            { "Water & Amusement Parks", new [] { "Theme Parks", "Disney Parks & Activities", "Water Parks" } },
            { "Concerts & Shows", new [] { "Theater & Performances", "Theaters", "Dinner Theaters", "Cirque du Soleil Shows", "Concerts", "Operas", "Comedy Clubs", "Luaus", "Symphonies" } },
            { "Boat Tours & Water Sports", new [] { "Boat Tours", "Dolphin & Whale Watching", "Scuba & Snorkeling", "Kayaking & Canoeing", "River Rafting & Tubing", "Fishing Charters & Tours", "Stand-Up Paddleboarding", "Surfing, Windsurfing & Kitesurfing", "Water Sports", "Swim with Dolphins", "Waterskiing & Jetskiing", "Parasailing & Paragliding", "Speed Boats Tours", "Boat Rentals", "Duck Tours", "Shark Diving" } },
            { "Zoos & Aquariums", new [] { "Zoos", "Aquariums" } },
            { "Food & Drink", new [] { "Food Tours", "Wine Tours & Tastings", "Brewery Tours", "Cooking Classes", "Distilleries", "Breweries", "Wineries & Vineyards", "Distillery Tours", "Other Food & Drink" } },
            { "Fun & Games", new [] { "Room Escape Games", "Game & Entertainment Centers", "Shooting Ranges", "Other Fun & Games", "Comedy Clubs", "Playgrounds", "Sports Complexes", "Casinos", "Horse Tracks", "Scavenger Hunts", "Wedding Chapels" } },
            { "Shopping", new [] { "Gift & Specialty Shops", "Flea & Street Markets", "Art Galleries", "Shopping Tours" } },
            { "Transportation", new [] { "Taxis & Shuttles", "Ferries", "Tramways", "Bus Transportation", "Mass Transportation Systems" } },
            { "Traveler Resources", new [] { "Libraries", "Visitor Centers" } },
            { "Classes & Workshops", new [] { "Lessons & Workshops", "Cooking Classes", "Sports Camps & Clinics" } },
            { "Casinos & Gambling", new [] { "Casinos", "Horse Tracks" } },
            { "Nightlife", new [] { "Bar, Club & Pub Tours", "Comedy Clubs" } }
        };

        static GeoSearch()
        {
        }

        public static bool HasCategory(Attraction attraction, string categories)
        {
            if (string.IsNullOrWhiteSpace(categories)) return true;

            var categoryList = categories.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            return attraction.categories.Intersect(categoryList).Any();
        }
    }
}