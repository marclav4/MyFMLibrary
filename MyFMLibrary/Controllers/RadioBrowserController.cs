using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyFMLibrary.DTOs;
using MyFMLibrary.Services;

namespace MyFMLibrary.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RadioBrowserController : ControllerBase
    {
        private readonly ILogger<RadioBrowserController> _logger;
        private readonly RadioBrowserService _radioBrowserService;

        /// <summary>
        /// Endpoints involving the Radio Browser, externar API.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="radioBrowserService"></param>
        public RadioBrowserController(ILogger<RadioBrowserController> logger, RadioBrowserService radioBrowserService)
        {
            _logger = logger;
            _radioBrowserService = radioBrowserService;
            
        }

        /// <summary>
        /// Gets a list of Radio Stations from the specified country, optionally filtered by station name.
        /// </summary>
        /// <param name="country">Country where the stations are from.</param>
        /// <param name="name">Full or just part of the name of the station.</param>
        /// <param name="pageNumber">Page number of the query results to be shown.</param>
        /// <param name="pageSize">Number of records to be shown per page.</param>
        /// <returns></returns>
        [HttpGet("list")]
        public async Task<IActionResult> GetStations([FromQuery]string country, [FromQuery] string? name, [FromQuery]uint? pageNumber = 1, [FromQuery]uint? pageSize = 15)
        {
            var results = await _radioBrowserService.GetStations(country, name, pageNumber, pageSize);
            return Ok(results);
        }

        /// <summary>
        /// Adds a Radio Station to the user's favourites.
        /// </summary>
        /// <param name="uuid">Unique identifier of the Radio Station.</param>
        /// <returns></returns>
        [HttpPost("addfavourite")]
        public async Task<IActionResult> AddFavourite([FromQuery] Guid uuid)
        {
            await _radioBrowserService.AddFavourite(uuid);
            return Ok();
        }
    }
}
