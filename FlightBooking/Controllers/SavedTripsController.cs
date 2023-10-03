using FlightBooking.Models;
using FlightBooking.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace FlightBooking.Controllers
{
    public class SavedTripsController : Controller
    {
        private readonly ITripRepository _tripRepository;

        public SavedTripsController(ITripRepository tripRepository)
        {
            _tripRepository = tripRepository;
        }

        public IActionResult Index()
        {
            TripListViewModel tlvm = new TripListViewModel
            {
                TripList = _tripRepository.GetAllTrips()
            };

            return View(tlvm);
        }

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
