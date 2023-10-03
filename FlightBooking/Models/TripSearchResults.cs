namespace FlightBooking.Models;

/*
 * Class to represent results from Searching TripAdvisory through RapidApi
 */
public class TripSearchResults
{
    public int TripSearchResultsId { get; set; }
    public string Source { get; set; }
    public string Destination { get; set; }
    public DateTime DepartureDate { get; set; }
    public string Results { get; set; }
    public DateTime CachedTime { get; set; }

}
