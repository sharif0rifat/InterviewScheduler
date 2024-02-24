using InterviewScheduler.Model.DbModels;
using InterviewScheduler.Model.DtoModels;
using InterviewScheduler.Repository;
using InterviewScheduler.Service.Commands;
using Mapster;
using MediatR;

namespace InterviewScheduler.Service.Handlers
{
    public class BookingSlotCreationHandler : IRequestHandler<CreateBookingRequest, BookingSlotDto>
    {
        private readonly IRepository<BookingSlot> _repository;

        public BookingSlotCreationHandler(IRepository<BookingSlot> repository)
        {
            _repository = repository;
        }
        public async Task<BookingSlotDto> Handle(CreateBookingRequest request, CancellationToken cancellationToken)
        {
            var result = request.BookingSlotDtoRequest.Adapt<BookingSlot>();
            var exist = await _repository.GetAsync(i =>
            (i.StartTime >= result.StartTime && i.StartTime <= result.EndTime)
            || (i.EndTime >= result.StartTime && i.EndTime <= result.EndTime)
            );
            if (exist.Any())
                return null;
            await _repository.AddAsync(result);
            await _repository.SaveAsync();
            return result.Adapt<BookingSlotDto>();
        }
    }
}
