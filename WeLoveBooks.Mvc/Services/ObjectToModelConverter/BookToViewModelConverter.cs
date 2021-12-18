using WeLoveBooks.DataAccess.Models;
using WeLoveBooks.Mvc.ViewModels;

namespace WeLoveBooks.Mvc.Services.ObjectToModelConverter;

public class BookToViewModelConverter : IObjectToModelConverter<Book, BookViewModel>
{
    public BookViewModel Convert(Book book) => new BookViewModel
    {
        Title = book.Title,
        Description = book.Description,
        Author = book.Author,
        CreatedDate = book.CreatedDate,
        Id = book.Id.ToString()
    };
}
