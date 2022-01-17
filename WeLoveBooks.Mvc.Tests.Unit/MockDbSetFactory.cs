using Microsoft.EntityFrameworkCore;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace WeLoveBooks.Mvc.Tests.Unit
{
    public static class MockDbSetFactory<T> where T: class
    {
        public static Mock<DbSet<T>> Create(IEnumerable<T> elements)
        {
            var elementAsQueryable = elements.AsQueryable();
            var dbSetMock = new Mock<DbSet<T>>();
            dbSetMock.As<IQueryable<T>>().Setup(x => x.Provider).Returns(elementAsQueryable.Provider);
            dbSetMock.As<IQueryable<T>>().Setup(x => x.Expression).Returns(elementAsQueryable.Expression);
            dbSetMock.As<IQueryable<T>>().Setup(x => x.ElementType).Returns(elementAsQueryable.ElementType);
            dbSetMock.As<IQueryable<T>>().Setup(x => x.GetEnumerator()).Returns(elementAsQueryable.GetEnumerator());

            return dbSetMock;
        }
    }
}
