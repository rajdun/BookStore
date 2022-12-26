using System.Security.Cryptography;
using System.Text;
using BookStore.Db;
using BookStore.Models;
using Microsoft.AspNetCore.Identity;

namespace BookStore.Services;

public class BookStoreService : IBookStoreService
{
    private BookStoreDbContext _db;

    public BookStoreService(BookStoreDbContext db)
    {
        _db = db;
    }
    
    public DbAuthor AddAuthor(DbAuthor author)
    {
        _db.Author.Add(author);
        _db.SaveChanges();

        return author;
    }

    public void DeleteAuthor(DbAuthor author)
    {
        _db.Author.Remove(author);
        _db.SaveChanges();
    }

    public DbAuthor UpdateAuthor(DbAuthor author)
    {
        _db.Author.Update(author);
        _db.SaveChanges();
        return author;
    }

    public ICollection<DbAuthor> GetAuthors()
    {
        return _db.Author.ToList();
    }

    public DbAuthor GetAuthorById(int id)
    {
        return _db.Author.Single(a => a.Id == id);
    }

    public DbBook AddBook(DbBook book)
    {
        book.CreatedDate = DateTime.UtcNow;
        _db.Book.Add(book);
        _db.SaveChanges();
        
        return book;
    }

    public void DeleteBook(DbBook book)
    {
        _db.Remove(book);
        _db.SaveChanges();
    }

    public DbBook UpdateBook(DbBook book)
    {
        _db.Book.Update(book);
        _db.SaveChanges();
        return book;
    }

    public DbBook GetBookById(int bookId)
    {
        return _db.Book.Single(b => b.Id == bookId);
    }

    public ICollection<DbBook> GetBooks()
    {
        return _db.Book.ToList();
    }

    public ICollection<DbBook> GetBooksForAuthor(DbAuthor author)
    {
        return _db.Book.Where(b => b.AuthorId == author.Id).ToList();
    }

    public void AddRating(IdentityUser user, DbBook book, RatingScore score)
    {
        var rating = this.CreateRating(user, book, score);

        if (_db.Rating.Any(r => (r.Book.Id == book.Id) && (r.User.Id == user.Id)))
        {
            _db.Rating.Update(rating);
        }
        else
        {
            _db.Rating.Add(rating);
        }
        
        _db.SaveChanges();
    }

    public void RemoveRating(IdentityUser user, DbBook book)
    {
        var rating = _db.Rating.SingleOrDefault(r => (r.Book.Id == book.Id) && (r.User.Id == user.Id));
        if (rating != null)
        {
            _db.Rating.Remove(rating);
        }
        _db.SaveChanges();
    }

    // Private
    private Db.Rating CreateRating(IdentityUser user, DbBook book, RatingScore score)
    {
        Db.Rating rating = new Db.Rating()
        {
            User = user,
            UserId = user.Id,
            Book = book,
            BookId = book.Id,
            Score = (int)score
        };

        return rating;
    }
}