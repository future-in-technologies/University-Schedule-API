using Microsoft.EntityFrameworkCore;
using UniversitySchedule.Core.Entites;
using UniversitySchedule.DAL.EntityConfigurations;

namespace UniversitySchedule.DB
{
    public class UniversityScheduleContext : DbContext
    {
        public UniversityScheduleContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region -- model builder configurations --
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            #endregion -- model builder configurations --
        }
    }
}
