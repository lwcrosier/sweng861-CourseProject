using FlightBooking.Services;
using FlightBooking.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace FlightBooking.Controllers;


[ApiController]
[Route("api/[controller]")]
public class TripAdvisorSearchController : ControllerBase
{
    public readonly ITripAdvisorSearchService _search;
    private readonly ITripSearchResultsRepository _searchRepository;
    

    public TripAdvisorSearchController(ITripAdvisorSearchService search, ITripSearchResultsRepository searchRepository) 
    {
        _search = search;
        _searchRepository = searchRepository;
    }

    [HttpGet]
    public async Task<IActionResult> SearchTripAdvisorAsync(string src, string dst, string date)
    {
        
        TripInformation tripInformation = new TripInformation 
        {
            Source = src,
            Destination = dst,
            Date = DateTime.Parse(date)
        };

        bool cachedResultsAreCurrent = await _searchRepository.CachedResultsAreCurrent(tripInformation);
        // Check if there are any currently cached results;  If there are no current results (within 8 hours), add these results    
        if (!cachedResultsAreCurrent)
        {
            string results = await _search.SearchTripInformation(tripInformation);
            
            TripSearchResults tripSearcResults = new TripSearchResults
            {
                Source = tripInformation.Source,
                Destination = tripInformation.Destination,
                DepartureDate = tripInformation.Date,
                Results = results,
                CachedTime = DateTime.Now
            };
            _searchRepository.CacheSearchResultsAsync(tripSearcResults);
            
            return Ok(results);
        } 
        else
        {
            var results = await _searchRepository.GetSearchResultsAsync(tripInformation);
            return Ok(results.Results);
        }

        //return Ok();


    }

    private async Task CacheSearchResults(TripInformation tripInformation, string results)
    {
        DateTime cachedtime = DateTime.Now;
    }

    /*
    [HttpPost]
    public IActionResult Test(string jsonInputData)
    {
        string deser = JsonConvert.SerializeObject(jsonInputData); 
        Console.WriteLine(deser);
        return Json(deser);
    }
    */

    

    
}
