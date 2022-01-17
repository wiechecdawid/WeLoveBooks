using WeLoveBooks.DataAccess.Models;
using WeLoveBooks.Mvc.ViewModels;

namespace WeLoveBooks.Mvc.Services.ObjectToModelConverter;

public class BookToViewModelConverter : IObjectToModelConverter<Book, BookViewModel>
{
    public BookViewModel Convert(Book book)
    {
        List<ReviewListViewModel> reviews = new();

        if(book.Reviews is not null)
        {
            foreach(var r in book.Reviews)
            {
                reviews.Add(new ReviewListViewModel
                {
                    Id = r.Id.ToString(),
                    UserName = r.AppUser.UserName,
                    CreatedDate = r.CreatedDate,
                    Title = r.Title,
                    Content = r.Content
                });
            }
        }
        return new BookViewModel
        {
            Title = book.Title,
            Description = book.Description,
            Author = book.Author,
            CreatedDate = book.CreatedDate,
            Id = book.Id.ToString(),
            Reviews = reviews
        };
    }
}
