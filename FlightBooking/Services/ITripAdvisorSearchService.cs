using FlightBooking.Models;
namespace FlightBooking.Services;

/*
 * Interface to interact with the TripAdvisorSearchService
 */
public interface ITripAdvisorSearchService
{
    Task<string> SearchTripInformation(TripInformation tripInformation);
}
