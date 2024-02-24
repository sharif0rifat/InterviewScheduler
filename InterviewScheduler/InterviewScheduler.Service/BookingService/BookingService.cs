using InterviewScheduler.Model.DbModels;
using InterviewScheduler.Model.DtoModels;
using InterviewScheduler.Repository;
using Mapster;

namespace InterviewScheduler.Service.BookingService
{
    public class BookingService : IBookingService
    {
        private readonly IRepository<BookingSlot> _repository;

        public BookingService(IRepository<BookingSlot> repository)
        {
            _repository = repository;
        }

        public async Task<BookingSlotDto> CreateBookingSlot(BookingSlotDto bookingSlot)
        {
            try
            {
                BookingSlot slot = bookingSlot.Adapt<BookingSlot>();
                slot = await _repository.AddAsync(slot);
                await _repository.SaveAsync();
                return slot.Adapt<BookingSlotDto>(); ;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<BookingSlotDto> GetBookingSlot(Guid guid)
        {
            try
            {
                var slot = await _repository.GetByIdAsync(guid);
                return slot.Adapt<BookingSlotDto>(); ;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
