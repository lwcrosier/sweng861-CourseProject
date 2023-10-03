namespace FlightBooking.Models;

/*
 * BoilerPlate ViewModel to display Errors
 */
public class ErrorViewModel
{
    public string? RequestId { get; set; }

    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
}
