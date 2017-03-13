using MFIntake.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace MFIntake.DAL
{
    public class IntakeContext : DbContext
    {
        public IntakeContext() : base("IntakeContext")
        {
        }

        public DbSet<Intake> Intakes { get; set; }
        public DbSet<Exam> Exams { get; set; }
        public DbSet<Custodian> Custodians { get; set; }
        public DbSet<Agent> Agents { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Status> Statuses { get; set; }
        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}