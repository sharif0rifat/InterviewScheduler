using InterviewScheduler.Model.DbModels;
using InterviewScheduler.Model.DtoModels;
using InterviewScheduler.Model.Enums;
using InterviewScheduler.Repository;
using InterviewScheduler.Service.Commands;
using Mapster;
using MediatR;

namespace InterviewScheduler.Service.Handlers
{
    public class BookRequestHandler : IRequestHandler<BookRequest, BookingSlotDto>
    {
        private readonly IRepository<BookingSlot> _repository;

        public BookRequestHandler(IRepository<BookingSlot> repository)
        {
            _repository = repository;
        }
        public async Task<BookingSlotDto> Handle(BookRequest request, CancellationToken cancellationToken)
        {
            var existing = await _repository.GetByIdAsync(request.BookingId);
            if (existing != null)
            {
                existing.Status = (int)BookingStatus.Booked;
                existing.CandidateName = request.CandidateName;
                await _repository.UpdateAsync(existing);
                await _repository.SaveAsync();
                return existing.Adapt<BookingSlotDto>();
            }
            else
                return null;
            
        }
    }
}
