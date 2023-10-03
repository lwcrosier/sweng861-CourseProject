namespace FlightBooking.Models
{
    /*
     * Interface to interact with the TripRepository class
     */
    public interface ITripRepository
    {
        public IEnumerable<Trip> GetAllTrips();
    }
}

