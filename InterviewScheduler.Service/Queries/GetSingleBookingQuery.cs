using InterviewScheduler.Model.DbModels;
using InterviewScheduler.Model.DtoModels;
using MediatR;

namespace InterviewScheduler.Service.Queries
{
    public class GetSingleBookingQuery : IRequest<BookingSlotDto>
    {
        public GetSingleBookingQuery(Guid bookingId)
        {
            BookingId = bookingId;
        }

        public Guid BookingId { get;  }
    }
}
