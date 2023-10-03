using FlightBooking.Services;
using FlightBooking.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace FlightBooking.Controllers;

/*
 * API Controller Class that is used to call the TripAdvisor API via RapidApi.
 * It will cache results for 8 hours before making a new call
 */
[ApiController]
[Route("api/[controller]")]
public class TripAdvisorSearchController : ControllerBase
{
    public readonly ITripAdvisorSearchService _search;
    private readonly ITripSearchResultsRepository _searchRepository;
    
    /*
     * TripAdvisorSearchController() constructor
     * @param search is a TripAdvisorSearchService interface via dependency injection to call the API Search Service
     * @param searchRepository is a TripSearchResultsRepository interface via dependency injection to handle TripSearchResults objects in the database
     */
    public TripAdvisorSearchController(ITripAdvisorSearchService search, ITripSearchResultsRepository searchRepository) 
    {
        _search = search;
        _searchRepository = searchRepository;
    }

    /*
     * SearchTripAdvisorAsync() calls TripAdvisor via RapidApi to search for flights with given parameters.  HttpGet only
     * @param src is the source airport code
     * @param dst is the destination airport code
     * @param date is the date of travel
     */
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

    }
}
