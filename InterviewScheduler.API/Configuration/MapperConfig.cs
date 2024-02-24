using InterviewScheduler.Model.DbModels;
using InterviewScheduler.Model.DtoModels;
using Mapster;

namespace InterviewScheduler.API.Configuration
{
    public class MapperConfig
    {
        public static void ConfigMapster()
        {
            TypeAdapterConfig<BookingSlot, BookingSlotDto>.NewConfig()
                .Map(dest=>dest.BookingName,src=>src.Name);
            TypeAdapterConfig<BookingSlotDto, BookingSlot>.NewConfig()
                .Map(dest => dest.Name, src => src.BookingName)
                .Ignore(dest=>dest.Id)
                .Ignore(dest=>dest.Status);
            
        }
    }
}
