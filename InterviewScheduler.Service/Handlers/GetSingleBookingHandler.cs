using InterviewScheduler.Model.DbModels;
using InterviewScheduler.Model.DtoModels;
using InterviewScheduler.Repository;
using InterviewScheduler.Service.Queries;
using Mapster;
using MediatR;

namespace InterviewScheduler.Service.Handlers
{
    public class GetSingleBookingHandler : IRequestHandler<GetSingleBookingQuery, BookingSlotDto>
    {
        private readonly IRepository<BookingSlot> _repository;

        public GetSingleBookingHandler(IRepository<BookingSlot> repository)
        {
            _repository = repository;
        }

        public async Task<BookingSlotDto> Handle(GetSingleBookingQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var slot = await _repository.GetByIdAsync(request.BookingId);
                return slot.Adapt<BookingSlotDto>(); ;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
