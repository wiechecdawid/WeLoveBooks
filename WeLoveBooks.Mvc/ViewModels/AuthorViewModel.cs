namespace WeLoveBooks.Mvc.ViewModels;

public class AuthorViewModel
{
    public string Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Bio { get; set; }
    public DateTime DateOfBirth { get; set; }
    public PhotoViewModel? Photo { get; set; }
    public IEnumerable<BookViewModel> Books { get; set; }
    public int BooksCount => Books is not null ? Books.Count() : 0;
    public string FullName => $"{FirstName} {LastName}";
    public string ShortBio => Bio.Split('.').Take(2)
            .Aggregate("", (s1, s2) => s1 + s2 + ".");
}
