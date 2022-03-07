using WeLoveBooks.DataAccess.Models;
using WeLoveBooks.Mvc.ViewModels;

namespace WeLoveBooks.Mvc.Services.ObjectToModelConverter;

public class AuthorToViewModel : IObjectToModelConverter<Author, AuthorViewModel>
{
    BookToViewModelConverter _bookConverter;

    public AuthorToViewModel()
    {
        _bookConverter = new BookToViewModelConverter();
    }

    public AuthorViewModel Convert(Author author)
    {
        var photo = GetPhotoVeiwModel(author.Photo);

        var books = ConvertAllBooks(author.Books);

        return new AuthorViewModel
        {
            Id = author.Id.ToString(),
            Photo = photo,
            FirstName = author.FirstName,
            LastName = author.LastName,
            Bio = author.Bio,
            DateOfBirth = author.DateOfBirth,
            Books = books
        };
    }

    private PhotoViewModel? GetPhotoVeiwModel(Photo? photo)
    {
        if (photo is null)
            return null;

        return new PhotoViewModel
        {
            Id = photo.Id,
            Type = (int)photo.Type,
            Url = photo.Url
        };
    }

    private IEnumerable<BookViewModel> ConvertAllBooks(IEnumerable<Book> books)
    {
        var models = new List<BookViewModel>();

        foreach (var book in books)
        {
            models.Add(BookConvert(book));
        }

        return models;
    }

    private BookViewModel BookConvert(Book book) => _bookConverter.Convert(book);
}
