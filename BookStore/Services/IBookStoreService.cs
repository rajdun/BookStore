using BookStore.Db;
using BookStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;

namespace BookStore.Services;

public interface IBookStoreService
{
    public DbAuthor AddAuthor(DbAuthor author);
    public  void DeleteAuthor(DbAuthor author);
    public DbAuthor UpdateAuthor(DbAuthor author);
    public ICollection<DbAuthor> GetAuthors();
    public DbAuthor GetAuthorById(int id);
    
    public DbBook AddBook(DbBook book);
    public void DeleteBook(DbBook book);
    public DbBook UpdateBook(DbBook book);
    public DbBook GetBookById(int bookId);
    public ICollection<DbBook> GetBooks();
    public ICollection<DbBook> GetBooksForAuthor(DbAuthor author);

    public void AddRating(IdentityUser user, DbBook book, RatingScore score);
    public void RemoveRating(IdentityUser user, DbBook book);
}