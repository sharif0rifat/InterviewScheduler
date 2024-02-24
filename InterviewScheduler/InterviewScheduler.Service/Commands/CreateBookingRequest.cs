using InterviewScheduler.Model.DtoModels;
using MediatR;

namespace InterviewScheduler.Service.Commands
{
    public class CreateBookingRequest:IRequest<BookingSlotDto>
    {
        public BookingSlotDto BookingSlotDtoRequest { get; }
        public CreateBookingRequest(BookingSlotDto bookingSlotDtoRequest)
        {
            BookingSlotDtoRequest = bookingSlotDtoRequest;
        }
    }
}
