using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using TripAdvisor;
using Where2GoNow.DataAccess.Repositories;

namespace Where2GoNow.DataAccess
{
    class Program
    {
        static void Main(string[] args)
        {
            using (FileStream stream = new FileStream("map.json", FileMode.Open, FileAccess.Read))
            {
                var jsonSerializer = new DataContractJsonSerializer(typeof(IEnumerable<Attraction>));
                var attractions = (IEnumerable<Attraction>)jsonSerializer.ReadObject(stream);

                var repo = new AttractionRepository();
                repo.InsertAttractions(attractions).Wait();
            }
        }
    }
}
