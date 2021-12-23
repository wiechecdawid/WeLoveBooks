using Moq;
using System;
using System.Collections.Generic;
using WeLoveBooks.DataAccess.Data;
using WeLoveBooks.DataAccess.Models;
using WeLoveBooks.Mvc.ViewModels;
using Xunit;

namespace WeLoveBooks.Tests.Unit.Controllers;

public class ReviewControllerUnitTests
{
    private readonly string _bookId;
    private readonly Mock<AppDbContext> _contextMock;

    public ReviewControllerUnitTests()
    {
        _bookId = Guid.NewGuid().ToString();
        _contextMock = new Mock<AppDbContext>();
        _contextMock.Setup( c => c.Books).Returns(new MockDbSet<Review>(GetTestReviews()));
    }

    [Fact]
    public void Test1()
    {

    }

    private List<Review> GetTestReviews()
    {
        Guid.TryParse(_bookId, out Guid guidId);
        return new List<Review>
                {
                    new Review
                    {
                        BookId = guidId,
                        Book = new Book
                        {
                            Id = guidId,
                            Title = "Test Book",
                            CreatedDate = DateTime.Now.AddYears(-5),
                            Description = "This is test book object"
                        },
                        Title = "Positive review",
                        Content = "Positive review content",
                        CreatedDate = DateTime.Now
                    },

                    new Review
                    {
                        BookId = guidId,
                        Book = new Book
                        {
                            Id = guidId,
                            Title = "Test Book",
                            CreatedDate = DateTime.Now.AddYears(-5),
                            Description = "This is test book object"
                        },
                        Title = "Negative review",
                        Content = "Negative review content",
                        CreatedDate = DateTime.Now
                    }
                };
    }
}
