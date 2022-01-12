namespace WeLoveBooks.Mvc.ViewModels;

public class ReviewViewModel
{
    public string Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public string UserName { get; set; }
    public DateTime CreatedDate { get; set; }
    public string ShortenedContent => Content.Split('.').Take(2)
        .Aggregate("", (s1, s2) => s1 + s2);
}
