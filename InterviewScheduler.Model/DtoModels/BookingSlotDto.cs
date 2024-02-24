using InterviewScheduler.Model.Enums;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace InterviewScheduler.Model.DtoModels
{
    public class BookingSlotDto
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? BookingName { get; set; }
        public int Status { get; set; }
        public required DateTime StartTime { get; set; }
        public required DateTime EndTime { get; set; }
        public string? CandidateName { get; set; }
    }
}
