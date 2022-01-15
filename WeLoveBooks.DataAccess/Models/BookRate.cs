namespace WeLoveBooks.DataAccess.Models;

public class BookRate
{
    public Guid Id { get; set; }
    public string AppUserId { get; set; }
    public AppUser AppUser { get; set; }
    public Guid BookId { get; set; }
    public Book Book { get; set; }
    public Verdict Verdict { get; set; }
}

public enum Verdict
{
    VeryBad = 1,
    Bad = 2,
    Neutral = 3,
    Good = 4,
    VeryGood = 5
}
