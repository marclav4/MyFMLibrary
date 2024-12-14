using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyFMLibrary.Data;
using MyFMLibrary.DTOs;
using MyFMLibrary.Models;
using RadioBrowser;
using RadioBrowser.Models;
using System.Diagnostics.Metrics;
using System.Security.Claims;

namespace MyFMLibrary.Services
{
    public class RadioBrowserService
    {
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContext;
        private readonly MyFMLibraryContext _dbContext;

        public RadioBrowserService(IMapper mapper, IHttpContextAccessor httpContext, MyFMLibraryContext context) 
        {
            _mapper = mapper;
            _httpContext = httpContext;
            _dbContext = context;
            
        }

        /// <summary>
        /// Get stations from Radio Browser API.
        /// </summary>
        /// <returns></returns>
        public async Task<List<RadioStationDTO>> GetStations([FromQuery] string country, [FromQuery] string? name, 
            [FromQuery] uint? pageNumber = 1, [FromQuery] uint? pageSize = 15)
        {
            //Add filtering options
            var options = new AdvancedSearchOptions();
            options.Country = country;
            options.Name = name;

            //Implement paging
            if (pageNumber > 1)
                options.Offset = pageSize * pageNumber - 1;

            options.Limit = pageSize <= 50 ? pageSize : 50;

            //Get results
            var client = new RadioBrowserClient();
            var stations = await client.Search.AdvancedAsync(options);
            var mappedResults = _mapper.Map<List<RadioStationDTO>>(stations);

            //Get favs and match
            string? userId = _httpContext.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            var favourites = await _dbContext.Favourites
                .Where(f => f.UserId == userId)
                .Take(50)
                .AsNoTracking()
                .ToListAsync();

            foreach (var station in mappedResults)
            {
                station.IsFavorite = favourites.Any(f => f.StationUuid == station.StationUuid);
            }

            return mappedResults;
        }

        /// <summary>
        /// Creates a Favourite record that associates the station Uuid and the UserId.
        /// </summary>
        /// <param name="uuid"></param>
        /// <returns></returns>
        public async Task AddFavourite(Guid uuid)
        {
            Favourite newFav = new Favourite();
            newFav.StationUuid = uuid;
            string? userId = _httpContext.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            newFav.UserId = userId;

            //Check if this favourite station exists for this user
            if (await _dbContext.Favourites.AnyAsync(f => f.UserId == newFav.UserId && f.StationUuid == newFav.StationUuid))
                throw new Exception("Favourite already exists.");

            //Save
            await _dbContext.AddAsync(newFav);
            await _dbContext.SaveChangesAsync();
        }
    }
}
