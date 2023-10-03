using Microsoft.EntityFrameworkCore;

namespace FlightBooking.Models
{
    /*
     * Class for database business logic of the TripRepository
     */
    public class TripRepository : ITripRepository
    {
        private readonly AppDbContext _appDbContext;

        /*
         * TripRepository() constructor
         * @param appDbContext is used to modify the database 
         */
        public TripRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        /*
         * GetAllTrips() returns all trip objects saved in the Database
         * @return a List of all Trip objects
         */
        public IEnumerable<Trip> GetAllTrips()
        {
            return _appDbContext.Trips.ToList();
        }

    }
}
