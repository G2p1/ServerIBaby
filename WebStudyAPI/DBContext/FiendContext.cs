using Microsoft.EntityFrameworkCore;
using WebStudyAPI.Model;

namespace WebStudyAPI.DBContext
{
    public class FiendContext : DbContext
    {
        public FiendContext(DbContextOptions<FiendContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<YearGradation> YearGradations { get; set; }
        public DbSet<UserLessonDone> UserLessonDones { get; set; }
}
}
