using InterviewScheduler.Model.DbModels;
using InterviewScheduler.Model.DtoModels;
using MediatR;

namespace InterviewScheduler.Service.Queries
{
    public class GetAllBookingQueries:IRequest<IEnumerable<BookingSlotDto>>
    {
    }
}
