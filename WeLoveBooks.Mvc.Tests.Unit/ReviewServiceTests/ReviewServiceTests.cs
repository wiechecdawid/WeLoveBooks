using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using WeLoveBooks.DataAccess.Data;
using WeLoveBooks.DataAccess.Models;
using WeLoveBooks.Mvc.Services.ReviewService;
using WeLoveBooks.Mvc.ViewModels;
using Xunit;

namespace WeLoveBooks.Mvc.Tests.Unit.ReviewServiceTests;

public class ReviewServiceTests
{
    private readonly MockResourceProvider _provider;
    private readonly List<Review> _mockReviews;
    private readonly List<Book> _mockBooks;
    private readonly List<AppUser> _mockUsers;

    public ReviewServiceTests()
    {
        _provider = new MockResourceProvider();
        _mockReviews = _provider.GetReviews();
        _mockBooks = _provider.GetBooks();
        _mockUsers = _provider.GetUsers();
    }

    [Fact]
    public void GetAllBookReviewsShould_ReturnListOfReviewList_WhenCorrectBookIdIsProvided()
    {
        // ARRANGE
        var reviewService = CreateReviewService();

        // ACT
        var reviewList = reviewService.GetAllBookReviews(_mockReviews[0].BookId.ToString());

        //ASSERT
        var expected = new List<ReviewListViewModel>
        {
            new ReviewListViewModel
            {
                Id = _mockReviews[0].Id.ToString(),
                Title = _mockReviews[0].Title,
                Content = LoremIpsum.Print(),
                CreatedDate = _mockReviews[0].CreatedDate,
                Verdict = "Bardzo dobra",
                UserName = "user1@test.com"
            },
            new ReviewListViewModel
            {
                Id = _mockReviews[1].Id.ToString(),
                Title = _mockReviews[1].Title,
                Content = LoremIpsum.Print(),
                CreatedDate = _mockReviews[1].CreatedDate,
                Verdict = "Bardzo słaba",
                UserName = "user2@test.com"
            }
        };

        Assert.Equal(typeof(List<ReviewListViewModel>), reviewList.GetType());
        Assert.Equal(expected[0].Id, reviewList.First().Id);
        Assert.Equal(expected[1].Id, reviewList.Last().Id);
    }

    [Fact]
    public void GetAllBookReviewsShould_ReturnArgumentException_WhenInorrectBookIdIsProvided()
    {
        // ARRANGE        
        var reviewService = CreateReviewService();

        // ACT & ASSERT
        Assert.Throws<ArgumentException>(() => reviewService.GetAllBookReviews(Guid.NewGuid().ToString()));
    }

    private ReviewService CreateReviewService()
    {
        var reviewsMock = MockDbSetFactory<Review>.Create(_mockReviews);
        var booksMock = MockDbSetFactory<Book>.Create(_mockBooks);
        var usersMock = MockDbSetFactory<AppUser>.Create(_mockUsers);

        var context = new Mock<AppDbContext>();
        context.Setup(c => c.Reviews).Returns(reviewsMock.Object);
        context.Setup(c => c.Books).Returns(booksMock.Object);
        context.Setup(c => c.Users).Returns(usersMock.Object);

        var reviewService = new ReviewService(context.Object, new Mock<IConfiguration>().Object);

        return reviewService;
    }
}
