namespace FlightBooking.Models;

/*
 * Class object to represent basic trip information to conduct a search
 */
public class TripInformation
{
    public string Source { get; set; }
    public string Destination { get; set; }
    public DateTime Date { get; set; }

}