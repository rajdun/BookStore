using BookStore.Db;

namespace BookStore.Models;

public class Rating
{
    public int BookId { get; set; }
    public RatingScore Score { get; set; }
}