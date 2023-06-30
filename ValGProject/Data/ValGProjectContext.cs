using Microsoft.EntityFrameworkCore;
using ValGProject.Models;

namespace ValGProject.Data
{
    public class ValGProjectContext : DbContext
    {
        public ValGProjectContext(DbContextOptions<ValGProjectContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Topic> Topics { get; set; }
    }
}
