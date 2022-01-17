using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeLoveBooks.DataAccess.Models;

namespace WeLoveBooks.Mvc.Tests.Unit
{
    public class MockResourceProvider
    {
        private readonly IdBroker _idBroker;
        public MockResourceProvider()
        {
            _idBroker = new IdBroker();
        }


        public List<AppUser> GetUsers() => new List<AppUser>
        {
            new AppUser
            {
                FirstName = "User",
                LastName = "Test",
                Email = "user1@test.com",
                Id = _idBroker.IssueId().ToString()
            },
            new AppUser
            {
                FirstName = "Test",
                LastName = "User",
                Email = "user2@test.com",
                Id= _idBroker.IssueId().ToString()
            }
        };

        public List<Book> GetBooks() => new List<Book>
        {
            new Book
            {
                Title = "Test Book 1",
                Id = _idBroker.IssueId()
            },
            new Book
            {
                Title = "Test Book 2",
                Id= _idBroker.IssueId()
            }
        };

        public List<Review> GetReviews()
        {
            var users = GetUsers();
            var books = GetBooks();

            return new List<Review>
            {
                new Review
                {
                    Title = "Positive Review 1",
                    Id = _idBroker.IssueId(),
                    Book = books[0],
                    BookId = books[0].Id,
                    BookRate = new BookRate{ Verdict = Verdict.VeryGood },
                    AppUser = users[0],
                    Content = LoremIpsum.Print(),
                    CreatedDate = new DateTime(2022, 1, 16)
                },
                new Review
                {
                    Title = "Negative Review 1",
                    Id = _idBroker.IssueId(),
                    Book = books[0],
                    BookId = books[0].Id,
                    BookRate = new BookRate{ Verdict = Verdict.VeryBad },
                    AppUser = users[1],
                    Content = LoremIpsum.Print(),
                    CreatedDate = new DateTime(2022, 1, 16)
                },
                new Review
                {
                    Title = "Positive Review 2",
                    Id = _idBroker.IssueId(),
                    Book = books[1],
                    BookId = books[1].Id,
                    BookRate = new BookRate{ Verdict = Verdict.Good },
                    AppUser = users[1],
                    Content = LoremIpsum.Print(),
                    CreatedDate = new DateTime(2022, 1, 16)
                },
                new Review
                {
                    Title = "Negative Review 2",
                    Id = _idBroker.IssueId(),
                    Book = books[1],
                    BookId = books[1].Id,
                    BookRate = new BookRate{ Verdict = Verdict.Bad },
                    AppUser = users[0],
                    Content = LoremIpsum.Print(),
                    CreatedDate = new DateTime(2022, 1, 16)
                }
            };
        }
    }

    class IdBroker
    {
        private List<Guid> _ids;

        public IdBroker()
        {
            _ids = new List<Guid>();

            for(int i = 0; i < 10; i++)
                _ids.Add(Guid.NewGuid());
        }

        public Guid IssueId()
        {
            Guid id = _ids.First();

            _ids.Remove(id);
            _ids.Add(Guid.NewGuid());

            return id;
        }
    }
}