using FlightBooking.Models;
using FlightBooking.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace FlightBooking.Controllers
{
    /*
     * Controller class for the 'SaveTrips' page set
     */
    public class SavedTripsController : Controller
    {
        private readonly ITripRepository _tripRepository;

        /*
         * SavedTripsController() constructor 
         * @param tripRepository is a TripRepository interface for dependency injection to allow use of Trip objects in the database
         */
        public SavedTripsController(ITripRepository tripRepository)
        {
            _tripRepository = tripRepository;
        }

        /*
         * Index() controller method for calls to /SavedTrips/Index.cshtml
         * @param None
         * @return Views/SavedTrips/Index.cshtml with a ViewModel object containing a list of all Trip Objects
         */
        public IActionResult Index()
        {
            TripListViewModel tlvm = new TripListViewModel
            {
                TripList = _tripRepository.GetAllTrips()
            };

            return View(tlvm);
        }

        /*
         * DeleteTrip(int id) calls the localhost api to delete the Trip corresponding to id
         * @param id of the trip to delete
         * @return a call the Index() to return Views/SavedTrips/Index.cshtml
         */
        public async Task<IActionResult> DeleteTrip(int id)
        {

            string requestUri = String.Format(@"https://localhost:7258/api/trips/{0}", id);
            HttpClient client = new HttpClient();

            HttpResponseMessage response = await client.DeleteAsync(requestUri);
                
                //.DeleteFromJsonAsync(
                //requestUri, id);
            response.EnsureSuccessStatusCode();

            return RedirectToAction("Index");
        }
    }
}
