using BookEntityDemo.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace BookEntityDemo.Data.Context
{
    public class BookContext : DbContext
    {
        public BookContext(DbContextOptions options): base(options) { }

        DbSet<Books> Books { get; set; }

    }
}
