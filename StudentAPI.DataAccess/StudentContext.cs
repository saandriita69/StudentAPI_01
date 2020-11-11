using Microsoft.EntityFrameworkCore;
using StudentAPI.DataAccess.Models;

namespace StudentAPI.DataAccess
{
    public class StudentContext: DbContext
    {
        public StudentContext(DbContextOptions<StudentContext> options)
           : base(options)
        {
        }

        #region Required
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>(r => r.HasNoKey());
       

           
        }
        #endregion


        public DbSet<Student> Students { get; set; }

    }
}
