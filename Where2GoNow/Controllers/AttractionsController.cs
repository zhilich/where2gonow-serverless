using System.Collections.Generic;
using TripAdvisor;
using Where2GoNow.Utils;
using Microsoft.AspNetCore.Mvc;
using Where2GoNow.DataAccess.Repositories;
using System;
using System.Threading.Tasks;

namespace Where2GoNow.Controllers
{
    [Route("api/[controller]")]
    public class AttractionsController : Controller
    {
        private AttractionRepository _attractionRepository;

        public AttractionsController(AttractionRepository attractionRepository)
        {
            _attractionRepository = attractionRepository;
        }

        // GET: api/Attractions
        [HttpGet]
        public async Task<IActionResult> Get(double lat, double lng, double radius = 10, double popularity = 0, string categories = "")
        {
            try
            {
                var attractions = await _attractionRepository.GetAttractions(lat, lng, radius, Convert.ToInt32(popularity));

                var result = new List<Attraction>();
                foreach (var attraction in attractions)
                {
                    if (GeoCodeCalc.CalcDistance(lat, lng, attraction.lat, attraction.lng) > radius) continue;
                    if (attraction.reviews < popularity) continue;
                    if (!GeoSearch.HasCategory(attraction, categories)) continue;
                    result.Add(attraction);
                }

                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.ToString());
            }
        }
    }
}
