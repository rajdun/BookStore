using Microsoft.AspNetCore.Identity;

namespace BookStore.Db;

public class Rating
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public int BookId { get; set; }
    public IdentityUser User { get; set; }
    public DbBook Book { get; set; }
    public int Score { get; set; }
}

public enum RatingScore
{
    Positive = 1,
    Negative = -1
}