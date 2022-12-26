namespace BookStore.Models;

public class Login
{
    public string Token { get; set; }
    public DateTime Expires { get; set; }
}