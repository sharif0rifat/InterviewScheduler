using InterviewScheduler.Model.DbModels;
using Microsoft.EntityFrameworkCore;

namespace InterviewScheduler.Repository.Context
{
    public class InterviewSchedulerDbContext : DbContext
    {
        public InterviewSchedulerDbContext(DbContextOptions<InterviewSchedulerDbContext> options) : base(options)
        {
        }
        public DbSet<BookingSlot> BookingSlots { get; set; }
    }
}
