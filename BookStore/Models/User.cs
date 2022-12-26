using System.ComponentModel.DataAnnotations;

namespace BookStore.Models;

public class User
{
    public string Username { get; set; }
    public string Password { get; set; }
}
