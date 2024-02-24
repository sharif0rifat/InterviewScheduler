using InterviewScheduler.Model.DbModels;
using InterviewScheduler.Model.DtoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewScheduler.Service.BookingService
{
    public interface IBookingService
    {
        Task<BookingSlotDto> CreateBookingSlot(BookingSlotDto bookingSlot);
        Task<BookingSlotDto> GetBookingSlot(Guid guid);
    }
}
