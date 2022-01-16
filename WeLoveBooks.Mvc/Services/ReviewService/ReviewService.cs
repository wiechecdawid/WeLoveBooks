using Microsoft.EntityFrameworkCore;
using WeLoveBooks.DataAccess.Data;
using WeLoveBooks.DataAccess.Models;
using WeLoveBooks.Mvc.ViewModels;

namespace WeLoveBooks.Mvc.Services.ReviewService;

public class ReviewService : IReviewService
{
    private AppDbContext _context;
    private Dictionary<int, string> _verdictMap;

    public ReviewService(AppDbContext context)
    {
        _context = context;
        _verdictMap = new Dictionary<int, string>
        {
            { 1, "Bardzo słaba" },
            { 2, "Słaba" },
            { 3, "Średnia" },
            { 4, "Dobraa" },
            { 5, "Bardzo dobra" }
        };
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
            .Include(r => r.AppUser)
            .Where(r => r.Book.Id == bookId)
            .Include(r => r.AppUser)
            .Include(r => r.BookRate)
            .Select(r => new ReviewListViewModel
            {
                Id = r.Id.ToString(),
                Title = r.Title,
                Content = r.Content,
                UserName = r.AppUser.UserName,
                CreatedDate = r.CreatedDate,
                Verdict = GetVerdict(r.BookRate.Verdict)
            })
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
            .Include(r => r.BookRate)
            .FirstOrDefault();

        if (review is null)
            throw new InvalidOperationException($"Could not find any review with {nameof(id)}: {id}");

        return new ReviewPageViewModel
        {
            Id = id,
            BookId = review.BookId.ToString(),
            Title = review.Title,
            Content = review.Content,
            AppUser = review.AppUser,
            Verdict = GetVerdict(review.BookRate.Verdict)
        };
    }

    private Guid ConvertIdToGuid(string id)
    {
        if (!Guid.TryParse(id, out Guid guidId))
            throw new ArgumentException($"Parameter {nameof(id)}: {id} is invalid");

        return guidId;
    }

    private string GetVerdict(Verdict verdict) => _verdictMap[(int)verdict];
}
