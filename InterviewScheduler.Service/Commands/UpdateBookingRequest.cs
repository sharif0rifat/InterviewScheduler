using InterviewScheduler.Model.DtoModels;
using MediatR;

namespace InterviewScheduler.Service.Commands
{
    public class UpdateBookingRequest : IRequest<BookingSlotDto>
    {
        public BookingSlotDto BookingSlotDtoRequest { get; }
        public Guid bookingId  { get; }
        public UpdateBookingRequest(BookingSlotDto bookingSlotDtoRequest, Guid bookingId)
        {
            BookingSlotDtoRequest = bookingSlotDtoRequest;
            this.bookingId = bookingId;
        }
    }
}
