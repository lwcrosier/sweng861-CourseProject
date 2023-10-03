using Microsoft.IdentityModel.Tokens;

namespace FlightBooking.Models
{
    /*
     * Class for database business logic for TripSearchResults from RapidAPI TripAdvisor searches
     */
    public class TripSearchResultsRepository : ITripSearchResultsRepository
    {
        private readonly AppDbContext _appDbContext;
        //private readonly AppDbContext _memoryContext;

        /*
         * TripSearchResultsRepository() Constructor
         * @param appDbContext used to modify the database
         */
        public TripSearchResultsRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        /*
         * CacheSearchResultsAsync() saves a TripSearchResults object to the database for caching
         * @param searchResults TripSearchResults object to be saved
         */
        public async void CacheSearchResultsAsync(TripSearchResults searchResults)
        {
            
            _appDbContext.SearchResults.Add(searchResults);
            await _appDbContext.SaveChangesAsync();
        }


        // Function should only be called when "CachedResultsAreCurrent" returns true
        /*
         * GetSearchResultsAsync() gets cached searchresults object from database
         * @param tripInformation information of Trip to search for in database
         * @return TripSearchResults that have been cached in the database
         */
        public async Task<TripSearchResults> GetSearchResultsAsync(TripInformation tripInformation)
        {
            DateTime currentTime = DateTime.Now;
            var searchResults = _appDbContext.SearchResults
                .Where( r => r.Source == tripInformation.Source )
                .Where( r => r.Destination == tripInformation.Destination )
                .Where( r => r.DepartureDate == tripInformation.Date )
                .OrderByDescending(x => x.CachedTime).ToList();

            foreach (var result in searchResults)
            {
                TimeSpan hours = currentTime - result.CachedTime;
                if (hours.TotalHours > 8)
                {
                    return result;
                }
            }
            return searchResults.Last();
        }

        /*
         * CacheResultsAreCurrent() checks if given TripInformation is current
         * @param tripInformation to check if current
         * @return boolean representing whether the results are current or not current
         */
        public async Task<bool> CachedResultsAreCurrent(TripInformation tripInformation)
        {
            DateTime currentTime = DateTime.Now;
            

            var cachedResults = _appDbContext.SearchResults
                .Where( r => r.Source == tripInformation.Source )
                .Where( r => r.Destination == tripInformation.Destination)
                .Where( r => r.DepartureDate == tripInformation.Date)
                .OrderByDescending(x => x.CachedTime).ToList();

            if (cachedResults.IsNullOrEmpty())
            {
                return false;
            }

            foreach (var result in cachedResults)
            {
                TimeSpan hours = currentTime - result.CachedTime;

                if ( hours.TotalHours >= 9)
                {
                    await DeleteCachedResult(result);
                    return false;
                }
            }
            return true;
        }

        /*
         * DeleteCachedResults() deletes results from database cache
         * @param searchResults TripSearchResults object to delete from database
         */
        public async Task DeleteCachedResult(TripSearchResults searchResults)
        {
            _appDbContext.Remove(searchResults);
            await _appDbContext.SaveChangesAsync();
        }

    }
}
