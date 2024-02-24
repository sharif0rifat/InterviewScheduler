using InterviewScheduler.Model.Enums;
using System.ComponentModel.DataAnnotations;

namespace InterviewScheduler.Model.DbModels
{
    public class BookingSlot
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime CreatedDate { get; set; }
        public string Name { get; set; }
        public int Status { get; set; }= (int)BookingStatus.Open;
        public string? CandidateName { get; set; }
        public DateTime StartTime { get; set; } 
        public DateTime EndTime { get; set; }

        [Timestamp]
        public uint Version { get; set; }
    }
}
