using BookingManagement.Models;
using BookingManagement.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingManagement.Controllers
{
    [ApiController]
    [ApiVersion("1")]
    [Route("api/v{v:apiVersion}/[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly IFlightBookRepository _bookingRepository;
        public BookingController(IFlightBookRepository obj)
        {
            _bookingRepository = obj;
        }
        [HttpPost]
        [Route("bookTicket")]
        public IActionResult BookTicket([FromBody] TblFlightBook tblFlightBook)
        {
            string response = string.Empty;
            try
            {

                var booking = _bookingRepository.BookTicket(tblFlightBook);
                if (booking != null)
                {
                    //int pnrNumber = booking.PnrNumber;
                    response = "Your ticket has been successfully booked and Your PNR Number is: " + booking.PnrNumber;
                    return new OkObjectResult(response);
                }
            }
            catch (Exception)
            {

                throw;
            }
            return new NotFoundObjectResult(response);
        }

        [Route("cancelTicket")]
        [HttpGet]
        public IActionResult CancelTicket(string pNRNumber)
        {
            string response = string.Empty;
            try
            {
                var booking = _bookingRepository.CancelTicket(pNRNumber);
                if (booking != null)
                {
                    return new OkObjectResult("Ticket Cancel Successfully. PNR Number is: " + pNRNumber);
                }
            }
            catch (Exception ex)
            {
                response = ex.Message;
            }
            return new NotFoundObjectResult(response);
        }
        [Route("getHistoryTicketsByUser")]
        [HttpPost]
        public IActionResult HistoryTickets(int UserId)
        {
            string response = string.Empty;
            try
            {
                var booking = _bookingRepository.HistoryTickets(UserId);
                if (booking != null)
                {
                    return new OkObjectResult(booking);
                }
            }
            catch (Exception ex)
            {
                response = ex.Message;
            }
            return new NotFoundObjectResult(response);
        }

        

    }
}
