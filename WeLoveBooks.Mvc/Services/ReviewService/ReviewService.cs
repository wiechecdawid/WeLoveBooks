using Microsoft.EntityFrameworkCore;
using WeLoveBooks.DataAccess.Data;
using WeLoveBooks.DataAccess.Models;
using WeLoveBooks.Mvc.ViewModels;

namespace WeLoveBooks.Mvc.Services.ReviewService;

public class ReviewService : IReviewService
{
    private AppDbContext _context;
    private static Dictionary<int, string> _verdictMap;

    public ReviewService(AppDbContext context, IConfiguration config)
    {
        _context = context;
        _verdictMap = new Dictionary<int, string>
        {
            { 1, config["BookRates:PL:VeryPoor"] },
            { 2, config["BookRates:PL:Poor"] },
            { 3, config["BookRates:PL:Mediocre"] },
            { 4, config["BookRates:PL:Good"] },
            { 5, config["BookRates:PL:VeryGood"] }
        };
    }

    public IEnumerable<ReviewListViewModel> GetLatestReviews()
    {
        var reviews = _context.Reviews
            .OrderByDescending(r => r.CreatedDate)
            .Take(5)
            .Include(r => r.BookRate)
            .Include(r => r.AppUser)
            .ThenInclude(u => u.Photo)
            .Include(r => r.BookRate)
            .ToList();

        return reviews.Select(r => ToReviewListViewModel(r));
    }

    public IEnumerable<ReviewListViewModel> GetAllBookReviews(string bookId)
    {
        var guidId = ConvertIdToGuid(bookId);

        return GetAllBookReviews(guidId);
    }

    public IEnumerable<ReviewListViewModel> GetAllBookReviews(Guid bookId)
    {
        var reviews = _context.Reviews
            .Include(r => r.Book)
            .Where(r => r.Book.Id == bookId)
            .Include(r => r.AppUser)
            .Include(r => r.BookRate)
            .Select(r => ToReviewListViewModel(r))
            .ToList();

        if (!reviews.Any()) throw new ArgumentException("No reviews found");

        return reviews;
    }

    public ReviewPageViewModel GetReview(string id)
    {
        var guidId = ConvertIdToGuid(id);

        var review = _context.Reviews
            .Where(r => r.Id == guidId)
            .Include(r => r.AppUser)
            .ThenInclude(u => u.Photo)
            .Include(r => r.BookRate)
            .FirstOrDefault();

        if (review is null)
            throw new ArgumentException($"Could not find any review with {nameof(id)}: {id}");

        return new ReviewPageViewModel
        {
            Id = id,
            BookId = review.BookId.ToString(),
            Title = review.Title,
            Content = review.Content,
            AppUser = review.AppUser,
            Verdict = GetVerdict(review.BookRate.Verdict),
            Photo = GetPhoto(review.AppUser.Photo)
        };
    }

    private PhotoViewModel? GetPhoto(Photo? photo)
    {
        if (photo is null)
            return null;

        return new PhotoViewModel
        {
            Id = photo.Id,
            Url = photo.Url,
            Type = (int)photo.Type
        };
    }

    private Guid ConvertIdToGuid(string id)
    {
        if (!Guid.TryParse(id, out Guid guidId))
            throw new ArgumentException($"Parameter {nameof(id)}: {id} is invalid");

        return guidId;
    }

    private static ReviewListViewModel ToReviewListViewModel(Review r) => new ReviewListViewModel
    {
        Id = r.Id.ToString(),
        Title = r.Title,
        Content = r.Content,
        UserName = r.AppUser.UserName,
        CreatedDate = r.CreatedDate,
        BookTitle = r.Book.Title,
        PhotoUrl = r.AppUser.Photo?.Url,
        Verdict = GetVerdict(r.BookRate?.Verdict)
    };

    private static string GetVerdict(Verdict? verdict) => verdict is not null ? _verdictMap[(int)verdict] : "";
}
