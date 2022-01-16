using WeLoveBooks.DataAccess.Models;

namespace WeLoveBooks.Mvc.ViewModels;

public class ReviewPageViewModel
{
    public string Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public string Verdict { get; set; }
    public DateTime CreatedDate { get; set; }
    public AppUser AppUser { get; set; }
    public string BookId { get; set; }
    public IEnumerable<Comment> Comments { get; set; }
    public string UserFullName => (AppUser.FirstName is not null && AppUser.LastName is not null) ? $"{AppUser.FirstName} {AppUser.LastName}" :
        AppUser.UserName;
}
