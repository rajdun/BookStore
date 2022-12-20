using BookStore.Db;
using BookStore.Models;
using BookStore.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthorController : ControllerBase
{
    private readonly ILogger<BookController> _logger;
    private readonly IBookStoreService _service;

    public AuthorController(ILogger<BookController> logger, IBookStoreService service)
    {
        _logger = logger;
        _service = service;
    }

    [HttpGet]
    public ActionResult<ICollection<DbAuthor>> GetAllAuthors()
    {
        try
        {
            return Ok(_service.GetAuthors());
        }
        catch (Exception ex)
        {
            _logger.Log(LogLevel.Error, ex.ToString());
            return BadRequest(ex.Message); 
        }
    }

    [HttpGet("id/{authorId}")]
    public ActionResult<DbAuthor> GetAuthorById([FromRoute] int authorId)
    {
        try
        {
            return Ok(_service.GetAuthors());
        }
        catch (Exception ex)
        {
            _logger.Log(LogLevel.Error, ex.ToString());
            return BadRequest(ex.Message); 
        }
    }

    [HttpPut]
    public ActionResult<DbAuthor> PutAuthor(Author author)
    {
        try
        {
            var dbAuthor = _service.AddAuthor(DbAuthor.FromAuthor(author));
            return Ok(dbAuthor);
        }
        catch (Exception ex)
        {
            _logger.Log(LogLevel.Error, ex.ToString());
            return BadRequest(ex.Message); 
        }
    }

    [HttpPatch]
    public ActionResult<DbAuthor> UpdateAuthor(Author author, [FromQuery] int authorId)
    {
        try
        {
            var dbAuthor = _service.GetAuthorById(authorId);
            dbAuthor.FirstName = String.IsNullOrEmpty(author.FirstName) ? dbAuthor.FirstName : author.FirstName;
            dbAuthor.LastName = String.IsNullOrEmpty(author.LastName) ? dbAuthor.LastName : author.LastName;

            return Ok(_service.UpdateAuthor(dbAuthor));
        }
        catch (Exception ex)
        {
            _logger.Log(LogLevel.Error, ex.ToString());
            return BadRequest(ex.Message); 
        }
    }

    [HttpDelete]
    public ActionResult DeleteAuthor([FromQuery] int authorId)
    {
        try
        {
            var dbAuthor = _service.GetAuthorById(authorId);
            _service.DeleteAuthor(dbAuthor);

            return Ok();
        }
        catch (Exception ex)
        {
            _logger.Log(LogLevel.Error, ex.ToString());
            return BadRequest(ex.Message); 
        }
    }
}