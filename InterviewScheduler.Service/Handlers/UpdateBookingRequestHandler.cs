using InterviewScheduler.Model.DbModels;
using InterviewScheduler.Model.DtoModels;
using InterviewScheduler.Repository;
using InterviewScheduler.Service.Commands;
using Mapster;
using MediatR;

namespace InterviewScheduler.Service.Handlers
{
    public class UpdateBookingRequestHandler : IRequestHandler<UpdateBookingRequest, BookingSlotDto>
    {
        private readonly IRepository<BookingSlot> _repository;

        public UpdateBookingRequestHandler(IRepository<BookingSlot> repository)
        {
            _repository = repository;
        }
        public async Task<BookingSlotDto> Handle(UpdateBookingRequest request, CancellationToken cancellationToken)
        {
            
            var result = request.BookingSlotDtoRequest.Adapt<BookingSlot>();
            var duplicate =await  _repository.GetAsync(i => 
            (i.StartTime >= result.StartTime && i.StartTime <= result.EndTime)
            || (i.EndTime >= result.StartTime && i.EndTime <= result.EndTime));
            if (duplicate.Any(i => i.Id != request.bookingId))
                return null;
            var existing = await _repository.GetByIdAsync(request.bookingId);
            existing.Name=result.Name;
            existing.StartTime=result.StartTime;
            existing.EndTime=result.EndTime;
            existing.EndTime=result.EndTime;
            existing.CandidateName=result.CandidateName;
            await _repository.UpdateAsync(existing);
            _repository.Save();
            return result.Adapt<BookingSlotDto>();
        }
    }
}
