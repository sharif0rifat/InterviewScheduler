using InterviewScheduler.API.Helper;
using InterviewScheduler.API.HttpService;
using InterviewScheduler.Model.DbModels;
using InterviewScheduler.Model.DtoModels;
using InterviewScheduler.Model.Enums;
using InterviewScheduler.Service.Commands;
using InterviewScheduler.Service.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;

namespace InterviewScheduler.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookingSlotController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IWeatherService _weatherService;

        public BookingSlotController(IMediator mediator, IWeatherService weatherService)
        {
            _mediator = mediator;
            _weatherService = weatherService;
        }

        [HttpPost("/CreateBookingSlot", Name = "CreateBookingSlot")]
        public async Task<IActionResult> CreateBookingSlot(BookingSlotDto bookingSlot)
        {
            var command = new CreateBookingRequest(bookingSlot);
            var result = await _mediator.Send(command);
            if (result == null)
                return BadRequest("There are some problem happened saving the Booking slot");
            return Ok(result);
        }

        [HttpGet("/GetAllSlotWithDateTime", Name = "GetAllSlot")]
        public async Task<IEnumerable<BookingSlotDto>> GetAllSlotWithDateTime()
        {
            var weatherResponse = await _weatherService.Get("paris");
            var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(weatherResponse);
            if (apiResponse == null)
                throw new InvalidOperationException("Some data mapping problem happened.");

            var req = new GetAllBookingQueries();
            var res = await _mediator.Send(req);
            return res;
        }

        [HttpPut("/BookASlot", Name = "BookASlot")]
        public async Task<IActionResult> BookASlot(Guid slotId, string candidateName)
        {
            //---Check Weather(calling third party API)----
            var weather= await _weatherService.Get("aris");
            var response = JsonConvert.DeserializeObject<ApiResponse>(weather);
            if (response.current.TempC < 10)
                return BadRequest("Cold day not good for interview.");
                //-----------
            var req = new GetSingleBookingQuery(slotId);
            var booking = await _mediator.Send(req);
            if (booking.Status == (int)BookingStatus.Booked)
                return BadRequest("The Slot is already booked");

            var command = new BookRequest(slotId, candidateName);
            var result = await _mediator.Send(command);
            if (result == null)
                return BadRequest("Some problem Happened booking the slot");
            return Ok(result);
        }

        [HttpPut("/CancelBooking", Name = "CancelBooking")]
        public async Task<IActionResult> CancelBooking(Guid slotId)
        {
            var command = new CancelBookRequest(slotId);
            var result = await _mediator.Send(command);
            if (result == null)
                return BadRequest("Some problem Happened cancelling the slot");
            return Ok(result);
        }
        [HttpPut("/UpdateBooking", Name = "UpdateBooking")]
        public async Task<IActionResult> UpdateBooking(BookingSlotDto bookingSlot)
        {
            var command = new UpdateBookingRequest(bookingSlot, bookingSlot.Id);
            var result = await _mediator.Send(command);
            if (result == null)
                return BadRequest("Some problem Happened cancelling the slot");
            return Ok(result);
        }
        [HttpGet("/GetSlot", Name = "GetSlot")]
        public async Task<BookingSlotDto> GetSlot(Guid guid)
        {
            var req = new GetSingleBookingQuery(guid);
            return await _mediator.Send(req);
        }
    }
}
