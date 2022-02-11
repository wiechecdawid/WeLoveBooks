namespace WeLoveBooks.DataAccess.Models;

public class Photo
{
    public string Id { get; set; }
    public string Url { get; set; }
    public PhotoType Type { get; set; }
}

public enum PhotoType
{
    Book,
    Author,
    User
}
