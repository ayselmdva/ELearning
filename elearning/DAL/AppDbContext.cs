using elearning.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace elearning.DAL
{
    public class AppDbContext:IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext>options):base(options)
        {

        }

        public DbSet<Teacher>Teachers { get; set; }
        public DbSet<Course>Courses { get; set; }
        public DbSet<CartService>CartServices { get; set; }
        public DbSet<Settings>Settings { get; set; }
    }
}
