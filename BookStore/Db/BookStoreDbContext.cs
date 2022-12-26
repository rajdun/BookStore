using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Db;

public class BookStoreDbContext : IdentityDbContext
{
    public BookStoreDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<DbAuthor> Author { get; set; }
    public DbSet<DbBook> Book { get; set; }
    public DbSet<Rating> Rating { get; set; }
}