using BookStore.Db;
using BookStore.Models;
using BookStore.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class BookController : ControllerBase
{
    private readonly ILogger<BookController> _logger;
    private readonly IBookStoreService _service;
    
    public BookController(ILogger<BookController> logger, IBookStoreService service)
    {
        _logger = logger;
        _service = service;
    }

    [HttpGet]
    public ActionResult<ICollection<DbBook>> GetAllBooks()
    {
        try
        {
            var books = _service.GetBooks();
            return Ok(books);
        }
        catch (Exception ex)
        {
            _logger.Log(LogLevel.Error, ex.ToString());
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("id/{bookId}")]
    public ActionResult<DbBook> GetBookById([FromRoute] int bookId)
    {
        try
        {
            return _service.GetBookById(bookId);
        }
        catch(Exception ex)
        {
            _logger.Log(LogLevel.Error, ex.ToString());
            return BadRequest(ex.Message);
        }
    }
    
    [HttpPut]
    public ActionResult<DbAuthor> PutBook(Book book, [FromQuery]int authorId)
    {
        try
        {
            var author = _service.GetAuthorById(authorId);
            var dbBook = DbBook.FromBook(book, author);
            var newBook = _service.AddBook(dbBook);
            return Ok(newBook);
        }
        catch (Exception ex)
        {
            _logger.Log(LogLevel.Error, ex.ToString());
            return BadRequest(ex.Message);
        }
    }
    
    [HttpDelete]
    public ActionResult DeleteBook([FromQuery] int bookId)
    {
        try
        {
            var book = _service.GetBookById(bookId);
            _service.DeleteBook(book);
            return Ok();
        }
        catch (Exception ex)
        {
            _logger.Log(LogLevel.Error, ex.ToString());
            return BadRequest(ex.Message);
        }
    }
    
    [HttpPatch]
    public ActionResult<DbBook> UpdateBook(Book book, [FromQuery] int bookId)
    {
        try
        {
            var dbBook = _service.GetBookById(bookId);

            dbBook.Description = String.IsNullOrEmpty(book.Description) ? dbBook.Description : book.Description;
            dbBook.Name = String.IsNullOrEmpty(book.Name) ? dbBook.Name : book.Name;
            dbBook.ReleaseDate = book.ReleaseDate;

            _service.UpdateBook(dbBook);
            return Ok(dbBook);

        }
        catch (Exception ex)
        {
            _logger.Log(LogLevel.Error, ex.ToString());
            return BadRequest(ex.Message); 
        }
    }

    [HttpGet("author/{authorId}")]
    public ActionResult<DbAuthor> GetBooksForAuthor([FromRoute]int authorId)
    {
        try
        {
            var author = _service.GetAuthorById(authorId);
            var authors = _service.GetBooksForAuthor(author);
            return Ok(authors);
        }
        catch (Exception ex)
        {
            _logger.Log(LogLevel.Error, ex.ToString());
            return BadRequest(ex.Message);
        }
    }
    
    [HttpPost("rating")]
    public ActionResult AddRating(Models.Rating rating)
    {
        try
        {
            return Ok(); // TODO: Dodac obsluge ratingu po zaimplementowania kont uzytkownikow
        }
        catch (Exception ex)
        {
            _logger.Log(LogLevel.Error, ex.ToString());
            return BadRequest(ex.Message);
        }
    }
    
    [HttpDelete("rating")]
    public ActionResult DeleteRating([FromQuery] int bookId)
    {
        try
        {
            return Ok(); // TODO: Dodac obsluge ratingu po zaimplementowania kont uzytkownikow
        }
        catch (Exception ex)
        {
            _logger.Log(LogLevel.Error, ex.ToString());
            return BadRequest(ex.Message);
        }
    }
}