using System;

namespace TripAdvisor
{
    class Program
    {
        static void Main(string[] args)
        {
            var crawler = new Crawler();
            crawler.Delay = TimeSpan.FromSeconds(1);
            crawler.Crawl(49.310786, -126.363568, 24.056479, -68.718305, 13, 1000);
            crawler.Save("map.json");
        }
    }
}
