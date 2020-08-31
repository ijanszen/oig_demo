using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using dashboard.Models;

namespace dashboard.DAL
{
    public class SurveyContext : DbContext
    {
        public SurveyContext() : base("SurveyContext")
        {
        }

        public DbSet<Owner> Owners { get; set; }
        public DbSet<Survey> Surveys { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}