using BookStore.Models;

namespace BookStore.Db;

public class DbAuthor : Author
{
    public int Id { get; set; }

    public static DbAuthor FromAuthor(Author author)
    {
        DbAuthor dbAuthor = new DbAuthor()
        {
            FirstName = author.FirstName,
            LastName = author.LastName
        };

        return dbAuthor;
    }

    public Author ToAuthor()
    {
        Author author = new Author()
        {
            FirstName = this.FirstName,
            LastName = this.LastName
        };

        return author;
    }
}