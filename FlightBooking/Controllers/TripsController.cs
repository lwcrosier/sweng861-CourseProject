using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FlightBooking.Models;

namespace FlightBooking.Controllers
{

    /*
     * API Controller for Trips.  This is the primary controller implementing a full CRUD implementation
     */


    [Route("api/[controller]")]
    [ApiController]
    public class TripsController : ControllerBase
    {
        private readonly AppDbContext _context;

        /*
         * TripsController() Constructor
         * @param context is the AppDbContext that is used to modify the database with CRUD operations
         */
        public TripsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Trips
        /*
         * GetTrips() gets all Trip objects from the database.  Uses the HttpGet method
         * @return a List of all Trip objects
         */
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Trip>>> GetTrips()
        {
          if (_context.Trips == null)
          {
              return NotFound();
          }
            return await _context.Trips.ToListAsync();
        }

        // GET: api/Trips/5
        /*
         * GetTrip() gets the Trip object from the database specified by id.  Uses the HttpGet method
         * @param id database Id of the Trip object
         * @return Trip object with the id provdied by the id param
         */
        [HttpGet("{id}")]
        public async Task<ActionResult<Trip>> GetTrip(int id)
        {
          if (_context.Trips == null)
          {
              return NotFound();
          }
            var trip = await _context.Trips.FindAsync(id);

            if (trip == null)
            {
                return NotFound();
            }

            return trip;
        }

        // PUT: api/Trips/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /*
         * PutTrip() updates the Trip object identified by id with the settings from the new Trip object provided.  Uses HttpPut method
         * @param id database Id of the Trip objects
         * @param trip new trip object settings
         * @return Http204 - No Content
         */
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTrip(int id, Trip trip)
        {
            if (id != trip.Id)
            {
                return BadRequest();
            }

            _context.Entry(trip).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TripExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Trips
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /*
         * PostTrip() creates a new trip object in the database.  Uses the HttpPost method
         * @param trip Object representing a new Trip class.  Id should be null to allow a new id to be assigned
         * @return Http201 - created at with location header information
         */
        [HttpPost]
        public async Task<ActionResult<Trip>> PostTrip(Trip trip)
        {
          if (_context.Trips == null)
          {
              return Problem("Entity set 'AppDbContext.Trips'  is null.");
          }
            _context.Trips.Add(trip);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTrip), new { id = trip.Id }, trip);
        }

        // DELETE: api/Trips/5
        /*
         * DeleteTrip() deletes the trip identified by the provided id.  Uses the HttpDelete method
         * @param id Database id of the object to delete
         * @return Http204 - No Content
         */
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrip(int id)
        {
            if (_context.Trips == null)
            {
                return NotFound();
            }
            var trip = await _context.Trips.FindAsync(id);
            if (trip == null)
            {
                return NotFound();
            }

            _context.Trips.Remove(trip);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        /*
         * TripExists() checkes whether a trip exists with the given id
         * @param id Database id of the object to search for
         * @return boolean representing whether or not a trip with the provided id exists in the database
         */
        private bool TripExists(int id)
        {
            return (_context.Trips?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
