using System.ComponentModel.DataAnnotations;

namespace BookStore.Db;

public class User
{
    public int Id { get; set; }
    [MaxLength(128)]
    public string Email { get; set; }

    [StringLength(32)]
    public string Salt { get; set; }
    
    [StringLength(512)]
    public string Hash { get; set; }
}
