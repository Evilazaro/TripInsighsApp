using Microsoft.AspNetCore.Mvc;
using poi.Data;
using poi.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace poi.Controllers
{
    [Produces("application/json")]
    [Route("api/poi")]
    public class POIController : ControllerBase
    {
        private readonly POIContext _context;

        public POIController(POIContext context)
        {
            _context = context;
        }

        [HttpGet(Name = "GetAllPOIs")]
        [Produces("application/json", Type = typeof(POI))]
        public List<POI> GetAll()
        {
            return _context.POIs.ToList();
        }

        [HttpGet("healthcheck", Name = "Healthcheck")]
        [Produces("application/json", Type = typeof(Healthcheck))]
        public IActionResult Get()
        {
            return Ok(new Healthcheck());
        }

        [HttpGet("{ID}", Name = "GetPOIById")]
        [Produces("application/json", Type = typeof(POI))]
        public IActionResult GetById(string ID)
        {
            var item = _context.POIs.Find(ID);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }

        [HttpGet("trip/{tripID}", Name = "GetPOIsByTripId")]
        [Produces("application/json", Type = typeof(POI))]
        public IActionResult GetByTripId(string tripID)
        {
            var items = _context.POIs.Where(poi => poi.TripId == tripID).ToList<POI>();

            if (items == null)
            {
                return NotFound();
            }
            return Ok(items);
        }

        [HttpPost(Name = "CreatePOI")]
        public IActionResult CreatePoi([FromBody] POI poi)
        {
            poi.Id = Guid.NewGuid().ToString();
            _context.POIs.Add(poi);
            _context.SaveChanges();

            return Ok(poi);

        }
    }
}