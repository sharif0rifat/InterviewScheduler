using InterviewScheduler.Model.DtoModels;
using MediatR;

namespace InterviewScheduler.Service.Commands
{
    public class CancelBookRequest : IRequest<BookingSlotDto>
    {
        public Guid bookingId { get; set; }

        public CancelBookRequest(Guid slotId)
        {
            this.bookingId = slotId;
        }
    }
}
