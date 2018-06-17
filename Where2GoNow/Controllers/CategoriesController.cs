using System.Collections.Generic;
using Where2GoNow.Utils;
using Microsoft.AspNetCore.Mvc;

namespace Where2GoNow.Controllers
{
    [Route("api/[controller]")]
    public class CategoriesController : Controller
    {
        // GET: api/categories
        [HttpGet]
        public IDictionary<string, string[]> Get()
        {
            return GeoSearch.Categories;
        }
    }
}
