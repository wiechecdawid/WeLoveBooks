using WeLoveBooks.DataAccess.Models;
using WeLoveBooks.Mvc.ViewModels;

namespace WeLoveBooks.Mvc.Services.ReviewService;

public interface IReviewService
{
    IEnumerable<ReviewListViewModel> GetLatestReviews();
    ReviewPageViewModel GetReview(string id);
    IEnumerable<ReviewListViewModel> GetAllBookReviews(string bookId);
    IEnumerable<ReviewListViewModel> GetAllBookReviews(Guid bookId);
}
