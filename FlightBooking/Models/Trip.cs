namespace FlightBooking.Models;

/*
 * Class to represent Trips that have been 'booked' or 'saved'
 */
public class Trip
{
    public int Id { get; set; }
    public DateTime BookingDate { get; set; }
    public string? SourceAirportCode { get; set; }
    public string? DestinationAirportCode { get; set; }
    public string? PrimaryCarrier { get; set; }
    public string? PrimaryCarrierCode { get; set; }
    public double Price { get; set; }
    public DateTime TravelDate { get; set; }
}
