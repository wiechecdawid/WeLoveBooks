namespace WeLoveBooks.DataAccess.Models;

public class BookRate
{
    public Guid Id { get; set; }
    public Guid ReviewId { get; set; }
    public virtual Review Review { get; set; }
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
