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
        IEnumerable<Review> Reviews { get; set; }
        public int ReviewCount => Reviews is not null ? Reviews.Count() : 0;
        public string AuthorName => $"{Author.FirstName} {Author.LastName}";
    }
}
