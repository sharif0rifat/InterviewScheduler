using InterviewScheduler.Model.DbModels;
using InterviewScheduler.Model.DtoModels;
using InterviewScheduler.Model.Enums;
using InterviewScheduler.Repository;
using InterviewScheduler.Service.Commands;
using Mapster;
using MediatR;

namespace InterviewScheduler.Service.Handlers
{
    public class CancelBookRequestHandler : IRequestHandler<CancelBookRequest, BookingSlotDto>
    {
        private readonly IRepository<BookingSlot> _repository;

        public CancelBookRequestHandler(IRepository<BookingSlot> repository)
        {
            _repository = repository;
        }
        public async Task<BookingSlotDto> Handle(CancelBookRequest request, CancellationToken cancellationToken)
        {
            var existing = await _repository.GetByIdAsync(request.bookingId);
            if (existing != null)
            {
                existing.Status = (int)BookingStatus.Canceled;
                await _repository.UpdateAsync(existing);
                await _repository.SaveAsync();
                return existing.Adapt<BookingSlotDto>();
            }
            else
                return null;
            
        }
    }
}
