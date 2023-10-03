using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FlightBooking.Models;
using FlightBooking.Services;
using Newtonsoft.Json.Linq;
using Azure.Core;
using System.Text.Json;

namespace FlightBooking.Controllers;

public class FlightBookingController : Controller
{
    //private readonly ITripAdvisorSearchService _tripSearch;
    public FlightBookingController()//ITripAdvisorSearchService tripSearch)
    {
        //_tripSearch = tripSearch;
    }

    public IActionResult Index() {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> SaveSearchResultAsTrip(string jsonInputData)
    {
        // For some reason, there is a trailing '/' so we are trimming that off until we can find out what is causing it
        jsonInputData = jsonInputData.TrimEnd('/');
        
        JObject jsonInputObj = JObject.Parse(jsonInputData);

        // Add Check for valid data here
        /*
         let jsonData = '{ ' +
                '"departure":"' + departure + '",' +
                '"destination":"' + destination + '",' +
                '"layovers":"' + layovers + '",' +
                '"carrierName":"' + carrierName + '",' +
                '"carrierCode":"' + carrierCode + '",' +
                '"price":"' + price + '"' +
        // Need to add departure date here too
            '}'
        
        */

        Trip trip = new Trip
        {
            BookingDate = DateTime.Now,
            SourceAirportCode = jsonInputObj["departure"].ToString(),
            DestinationAirportCode = jsonInputObj["destination"].ToString(),
            Price = Convert.ToDouble(jsonInputObj["price"].ToString()),
            PrimaryCarrier = jsonInputObj["carrierName"].ToString(),
            PrimaryCarrierCode = jsonInputObj["carrierCode"].ToString(),
            TravelDate = DateTime.Parse(jsonInputObj["departureDate"].ToString()),
        };

        string requestUri = "https://localhost:7258/api/trips";
        HttpClient client = new HttpClient();

        HttpResponseMessage getResponse = await client.GetAsync(requestUri);

        var allTripsString = await getResponse.Content.ReadAsStringAsync();
        JArray allTripsObj = JArray.Parse(allTripsString);


        //Loop through array to see if travel date is already used.  If yes, do update rather than new
        foreach (var tripObj in allTripsObj )
        {
            DateTime tripObjDate = DateTime.Parse(tripObj["travelDate"].ToString());
            if (tripObjDate.Date == trip.TravelDate.Date)
            {
                trip.Id = Int32.Parse(tripObj["id"].ToString());
                // Perform HttpPut for update using id and Trip
                JObject putObj =
                    new JObject(
                        new JProperty("id", tripObj["id"]),
                        new JProperty("trip", JToken.FromObject(trip)));

                requestUri = String.Format(@"{0}/{1}", requestUri, trip.Id);
                HttpResponseMessage putResponse = await client.PutAsJsonAsync(requestUri, trip);

                // Return
                return RedirectToAction("Index");
            }
        }
        
        // Should only run if the TravelDate is not already selected, so new trip should be booked
        HttpResponseMessage postResponse = await client.PostAsJsonAsync(
            requestUri, trip);
        postResponse.EnsureSuccessStatusCode();

        return RedirectToAction("Index");
    }

    /*
     * 
     * private async Task<string> FlightSearchAsync(HttpRequestMessage request) 
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
     */

    /*
        [HttpPost]
        public async Task<string> Search(TripInformation tripInformation)
        {
            string results = await _tripSearch.SearchTripInformation(tripInformation);
            return results;
        }
    */
}