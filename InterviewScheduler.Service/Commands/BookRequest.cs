using InterviewScheduler.Model.DtoModels;
using MediatR;

namespace InterviewScheduler.Service.Commands
{
    public class BookRequest : IRequest<BookingSlotDto>
    {
        public Guid BookingId { get; }
        public string CandidateName { get; }
        public BookRequest(Guid bookingId, string candidateName)
        {
            BookingId = bookingId;
            CandidateName = candidateName;
        }
    }
}
