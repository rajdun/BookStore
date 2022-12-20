namespace BookStore.Db;

public class Rating
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int BookId { get; set; }
    public User User { get; set; }
    public DbBook Book { get; set; }
    public int Score { get; set; }
}

public enum RatingScore
{
    Positive = 1,
    Negative = -1
}