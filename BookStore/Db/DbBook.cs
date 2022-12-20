using BookStore.Models;

namespace BookStore.Db;

public class DbBook : Book
{
    public int Id { get; set; }
    public DateTime CreatedDate { get; set; }

    public int AuthorId { get; set; }
    public DbAuthor Author { get; set; }

    public static DbBook FromBook(Book book, DbAuthor author)
    {
        
        DbBook dbBook = new DbBook()
        {
            Name = book.Name,
            ReleaseDate = book.ReleaseDate,
            Description = book.Description,
            AuthorId = author.Id,
            Author = author
        };

        return dbBook;
    }

    public Book ToBook()
    {
        Book book = new Book()
        {
            Description = this.Description,
            Name = this.Name,
            ReleaseDate = this.ReleaseDate
        };

        return book;
    }
}