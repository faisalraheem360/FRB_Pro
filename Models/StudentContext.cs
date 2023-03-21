using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FRB_Projects.Models
{
    public class StudentContext : DbContext
    {

        public StudentContext(DbContextOptions options) : base(options) { }
       public DbSet<Student> Students { get; set; }
    }
}
