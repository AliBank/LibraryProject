using LibraryAppWeb.Models.Entities;
using Microsoft.EntityFrameworkCore;


namespace LibraryAppWeb.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {

        }
        public DbSet<Book> Books { get; set; }

    }
}
