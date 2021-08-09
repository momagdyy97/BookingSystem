using BookingSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : Controller
    {
        private readonly ResourcesContext _context;
        public BookingController(ResourcesContext context)
        {
            _context = context;
        }
        [Route("GetAllBookings")]
        [HttpGet]
        public async Task<IActionResult> GetAllBookings()
        {
            var bookings = await _context.Bookings.ToListAsync();
            return Ok(bookings);
        }
        [Route("BookResource")]
        [HttpPost]
        public async Task<IActionResult> BookResource(Resource res, DateTime From, DateTime To, int quantity)
        {
            Boolean success = true;
            foreach (var booking in res.Bookings)
            {
                if (dateConflict(booking.DateFrom, booking.DateTo, From, To))
                {
                    success = false;
                    break;
                }
            }
            if (success)
            {
                Booking newBooking = new Booking();
                newBooking.DateFrom = From;
                newBooking.DateTo = To;
                newBooking.BookedQuantity = quantity;
                newBooking.Resource = res;
                await _context.Bookings.AddAsync(newBooking);
                await _context.SaveChangesAsync();
                return Ok("Success:Resource is booked successfully");
            }
            else
            {
                return Ok("Failed:Resource is already Booked");
            } 
        }
        [HttpGet]
        public bool dateConflict(DateTime From, DateTime To, DateTime ResFrom, DateTime ResTo) => From < ResTo && ResFrom < To;
    }
}