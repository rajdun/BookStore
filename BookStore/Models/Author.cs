using System.ComponentModel.DataAnnotations;

namespace BookStore.Models;

public class Author
{
    [MaxLength(64)]
    public string FirstName { get; set; }
    
    [MaxLength(64)]
    public string LastName { get; set; }
}