using InterviewScheduler.Model.DbModels;
using InterviewScheduler.Model.DtoModels;
using InterviewScheduler.Repository;
using InterviewScheduler.Service.Queries;
using Mapster;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewScheduler.Service.Handlers
{
    public class GetAllBookingHandlers : IRequestHandler<GetAllBookingQueries, IEnumerable<BookingSlotDto>>
    {
        private readonly IRepository<BookingSlot> _repository;

        public GetAllBookingHandlers(IRepository<BookingSlot> repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<BookingSlotDto>> Handle(GetAllBookingQueries request, CancellationToken cancellationToken)
        {
            var bookings =await _repository.GetAllAsync();
            var z = bookings.Adapt<IEnumerable<BookingSlotDto>>();
            return z;
        }
    }
}
