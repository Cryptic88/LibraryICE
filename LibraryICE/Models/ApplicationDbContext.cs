using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace LibraryICE.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }
            public DbSet<BookType> BookType { get; set; }
            public DbSet<Book> Book { get; set; }
            public DbSet<Loan> Loan { get; set; }


    }
    
}
