using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Models;

public class Book
{
    [MaxLength(64)]
    public string Name { get; set; }
    
    [MaxLength(1024)]
    public string Description { get; set; }
    
    public DateTime ReleaseDate { get; set; }
    
    [NotMapped]
    public float Rating { get; set; }
}