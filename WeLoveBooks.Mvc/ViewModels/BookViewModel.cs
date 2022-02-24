using WeLoveBooks.DataAccess.Models;

namespace WeLoveBooks.Mvc.ViewModels
{
    public class BookViewModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public Author Author { get; set; }
        public PhotoViewModel? Photo { get; set; }
        public IEnumerable<ReviewListViewModel> Reviews { get; set; }
        public int ReviewCount => Reviews is not null ? Reviews.Count() : 0;
        public string AuthorName => $"{Author.FirstName} {Author.LastName}";
        public string ShortDescription => Description.Split('.').Take(2)
            .Aggregate("", (s1, s2) => s1 + s2 + ".");
    }
}
