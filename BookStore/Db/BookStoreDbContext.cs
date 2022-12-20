using Microsoft.EntityFrameworkCore;

namespace BookStore.Db;

public class BookStoreDbContext : DbContext
{
    public BookStoreDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<DbAuthor> Author { get; set; }
    public DbSet<DbBook> Book { get; set; }
    public DbSet<User> User { get; set; }
    public DbSet<Rating> Rating { get; set; }
}