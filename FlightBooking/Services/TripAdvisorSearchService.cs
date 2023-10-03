using Newtonsoft.Json;
using System.Text.RegularExpressions;
using FlightBooking.Configurations;
using FlightBooking.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.Options;
namespace FlightBooking.Services;

/*
 * Class for the backend service to make RapidAPI TripAdvisor API calls
 */
public class TripAdvisorSearchService : ITripAdvisorSearchService
{
    private readonly TripAdvisorSearchConfiguration _rapidApiConfig;

    /*
     * TripAdvisorSearchService() Constructor
     * @param rapidApiConfig configuration information for using RapidApi
     */
    public TripAdvisorSearchService(TripAdvisorSearchConfiguration rapidApiConfig)
    {
        _rapidApiConfig = rapidApiConfig;
    }

    /*
     * SearchTripInformation() public function that can be used to search TripAdvisor
     * @param tripInformation basic tripinformation required to search TripAdvisor
     * @return string results in JSON format from TripAdvisor
     */
    public async Task<string> SearchTripInformation(TripInformation tripInformation)
    {
        HttpRequestMessage flightSearchRequestMessage = CreateFlightSearchRequestMessage(tripInformation);

        string searchResults = await FlightSearchAsync(flightSearchRequestMessage);

        return searchResults;
        
    }

    /*
     * CreateFlightSearchRequestMessage() used to create the HttpRequestMessage required to send to TripAdvisor
     * @param tripInformation basic TripInformation required to search TripAdvisor
     * @return HttpRequestMessage object representing an Http message for TripAdvisor
     */
    private HttpRequestMessage CreateFlightSearchRequestMessage(TripInformation tripInformation)
    {
        //_rapidApiConfig.ApiUri
        //string departureDateString = tripInformation.Date.ToString("yyyy-MM-dd");
        string requestString = string.Format(@"{0}
            sourceAirportCode={1}
            &destinationAirportCode={2}
            &date={3}
            &itineraryType=ONE_WAY
            &sortOrder=PRICE
            &numAdults=1
            &numSeniors=0
            &classOfService=ECONOMY
            &pageNumber=1
            &currencyCode=USD",
            _rapidApiConfig.ApiUri, tripInformation.Source, tripInformation.Destination, tripInformation.Date.ToString("yyyy-MM-dd")
        );
        Regex whitespace = new Regex(@"\s+");
        requestString = whitespace.Replace(requestString, "");

        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri(requestString),
            Headers = 
            {
                { "X-RapidAPI-Key", _rapidApiConfig.rapidApiKey },
                { "X-RapidAPI-Host", _rapidApiConfig.rapidApiHost },
            },
        };
        //Console.WriteLine(request.ToString());
        return request;

    }

    /*
     * FlightSearchAsync() sends HttpRequestMessages to TripAdvisor 
     * @param request HttpRequestMessage object representing the request data for TripAdvisor
     * @return string results from TripAdvisor in JSON format
     */
    private async Task<string> FlightSearchAsync(HttpRequestMessage request) 
    {
        HttpClient client = new HttpClient();
        client.Timeout = TimeSpan.FromMinutes(10);

        string requestResults;
        try 
        {
            using (var response = await client.SendAsync(request)) {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                requestResults = body.ToString();
                return requestResults;
            }
        }
        catch (Exception e) 
        {
            Console.WriteLine("Request failed!");
            Console.WriteLine(e);
            return("An exception was raised while making the request");
        }
    }
}
