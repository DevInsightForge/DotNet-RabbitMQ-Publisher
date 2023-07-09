using BookingApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookingApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly IBookingPublisherService _bookingPublisherService;

        public BookingController( IBookingPublisherService bookingPublisherService)
        {
            _bookingPublisherService = bookingPublisherService;
        }

        [Authorize]
        [HttpPost]
        public IActionResult SendMessage(string message)
        {
            _bookingPublisherService.PublishBooking(message);

            return Ok(message);
        }
    }
}