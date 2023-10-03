using FlightBooking.Models;

namespace FlightBooking.ViewModels
{
    /*
     * ViewModel Class to contain a list of Trips for displaying in Views
     */
    public class TripListViewModel
    {
        public IEnumerable<Trip> TripList { get; set; }
    }
}
