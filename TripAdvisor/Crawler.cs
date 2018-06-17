using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace TripAdvisor
{
    public class Crawler
    {
        private Regex imgRegex = new Regex("https://media-cdn.tripadvisor.com/media/photo-[^']+.jpg");
        private Regex reviewsRegex = new Regex("(?<reviews>[0-9,.]+) review");
        private Regex ratingRegex = new Regex(@"src=""https://static.tacdn.com/img2/x.gif"" alt=""(?<rating>[0-9,.]+) of 5 stars""");
        private Regex categoryRegex = new Regex("<div class=\"gray-footer \">(?<category>.*?)<\\/div>", RegexOptions.Singleline);
        
        private HashSet<Attraction> _attractions = new HashSet<Attraction>();

        StreamWriter _log = new StreamWriter("log.txt", false);

        public TimeSpan Delay { get; set; }

        public IEnumerable<Attraction> Attractions
        {
            get
            {
                return _attractions;
            }
        }

        public void Crawl(double startLat, double startLng, double endLat, double endLng, double zoom, double size)
        {
            var area = (startLat - endLat) * (endLng - startLng);
            for (double lat = endLat; lat <= startLat; lat += zoom * 10 / size)
            {
                for (double lng = startLng; lng <= endLng; lng += zoom * 10 / size)
                {
                    var covered = (lat - endLat) * (endLng - startLng) + (lng - startLng) * zoom * 10 / size;
                    ReadSegment(lat, lng, zoom, size);
                    Log($"{covered / area * 100 }% complete");
                }
            }
        }

        public void Save(string filename)
        {
            var serializer = new DataContractJsonSerializer(typeof(List<Attraction>), new DataContractJsonSerializerSettings()
            {
                UseSimpleDictionaryFormat = true
            });
            using (var fw = new FileStream(filename, FileMode.Create))
            {
                serializer.WriteObject(fw, _attractions);
            }
        }

        public void Log(string message)
        {
            Console.WriteLine(message);
            _log.WriteLine(message);
            _log.Flush();
        }

        private void ReadSegment(double lat, double lng, double zoom, double size)
        {
            var webClient = new WebClient();
            Directory.CreateDirectory("download");
            var segmentFilename = $"download\\{lat.ToString("0.0000")}_{lng.ToString("0.0000")}_{zoom}_{size}.json";
            byte[] json;
            if (!File.Exists(segmentFilename))
            {
                var url = $"https://www.tripadvisor.com/GMapsLocationController?Action=update&from=Attractions&g=35805&geo=35805&mapProviderFeature=ta-maps-gmaps3&validDates=false&mc={lat},{lng}&mz={zoom}&mw={size}&mh={size}&pinSel=v2&origLocId=35805&sponsors=&finalRequest=false&includeMeta=false&trackPageView=false";
                //Thread.Sleep(Delay);
                Thread.Sleep(TimeSpan.FromSeconds(0.1));
                json = webClient.DownloadData(url);
                Log($"Read segment {lat}, {lng}. {json.Length} bytes.");
                if (json.Length > 400000)
                {
                    throw new Exception("Too much items. Increase zoom.");
                }
                File.WriteAllBytes(segmentFilename, json);
            }
            else
            {
                json = File.ReadAllBytes(segmentFilename);
            }

            using (var sr = new MemoryStream(json))
            {
                var serializer = new DataContractJsonSerializer(typeof(Map));
                var map = (Map)serializer.ReadObject(sr);

                Log($"Found {map.attractions.Count} attractions");

                foreach (var attraction in map.attractions)
                {
                    Log($"Attraction: {attraction.customHover.title}");
                    var attractionFilename = $@"download\{attraction.locId}.html";
                    string html;
                    if (!File.Exists(attractionFilename))
                    {
                        var infoUrl = $"https://www.tripadvisor.com{attraction.customHover.url.Replace("Action=info", "Action=infoCardAttr")}";
                        Log($"{attraction.locId}: {infoUrl}");
                        Thread.Sleep(Delay);
                        html = webClient.DownloadString(infoUrl);
                        File.WriteAllText(attractionFilename, html);
                    }
                    else
                    {
                        html = File.ReadAllText(attractionFilename);
                    }
                    var imgUrl = imgRegex.Match(html).Value;
                    var reviews = reviewsRegex.Match(html).Groups["reviews"].Value;
                    var rating = ratingRegex.Match(html).Groups["rating"].Value;
                    var categories = categoryRegex.Match(html).Groups["category"].Value.Trim(new char[] { '\r', '\n', ' ' });

                    Log($"reviews: {reviews}, rating: {rating}, categories: {categories}, image: {imgUrl}");

                    attraction.categories = (categories ?? string.Empty).Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(_c => _c.Trim(new char[] { ' ' })).ToList();
                    
                    attraction.imgUrl = imgUrl;

                    if (!string.IsNullOrEmpty(reviews))
                    {
                        attraction.reviews = int.Parse(reviews, System.Globalization.NumberStyles.AllowThousands);
                    }

                    if (!string.IsNullOrEmpty(rating))
                    {
                        attraction.rating = float.Parse(rating);
                    }

                    if (!_attractions.Contains(attraction))
                    {
                        _attractions.Add(attraction);
                    }
                    else
                    {
                        Log($"Attraction {attraction.customHover.title} already exists.");
                    }                    
                }                
            }
        }


    }
}
