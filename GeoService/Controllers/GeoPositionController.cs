using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeoService.DataServices.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GeoService.Controllers
{
    public class GeoPositionController : Controller
    {
        private readonly IGeoPositionDataService _geoPositionDataService;

        public GeoPositionController(IGeoPositionDataService geoPositionDataService)
        {
            _geoPositionDataService = geoPositionDataService;
        }

        [HttpGet("/coordinates")]
        public IActionResult GetGeoPosition(string country, string city, string street)
        {
            try
            {
                var coordinates = _geoPositionDataService.GetCoordinates(country, city, street);
                return Ok(coordinates);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("/adress")]
        public IActionResult Adress(string lon, string lat)
        {
            try
            {
                var result = _geoPositionDataService.GetAdress(lon, lat);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}

