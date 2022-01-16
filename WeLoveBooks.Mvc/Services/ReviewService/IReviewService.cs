using WeLoveBooks.DataAccess.Models;
using WeLoveBooks.Mvc.ViewModels;

namespace WeLoveBooks.Mvc.Services.ReviewService;

public interface IReviewService
{
    ReviewPageViewModel GetReview(string id);
    IEnumerable<ReviewListViewModel> GetAllBookReviews(string bookId);
    IEnumerable<ReviewListViewModel> GetAllBookReviews(Guid bookId);
}
