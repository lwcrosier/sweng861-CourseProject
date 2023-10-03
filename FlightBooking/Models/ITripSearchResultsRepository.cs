namespace FlightBooking.Models
{
    /*
     * Interface to interact with the TripSearchResultsRepositoryClass
     */
    public interface ITripSearchResultsRepository
    {
        public void CacheSearchResultsAsync (TripSearchResults searchResults);
        public Task<bool> CachedResultsAreCurrent(TripInformation tripInformation);
        public Task<TripSearchResults> GetSearchResultsAsync(TripInformation tripInformation);
    }
}
