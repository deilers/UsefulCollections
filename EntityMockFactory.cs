using System.Linq;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace EntityMockFactory
{
    /// <summary>
    /// Static utility methods for generating mocked Entity Framework objects and data structures with the Moq library
    /// </summary>
    public static class EntityMockFactory
    {
        /// <summary>
        /// Sets up a mocked DbSet object with test data of a generic type
        /// </summary>
        public static Mock<DbSet<T>> SetupDbSetMock<T>(IQueryable<T> modelList) where T : class
        {
            var mockSet = new Mock<DbSet<T>>();
            mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(modelList.Provider);
            mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(modelList.Expression);
            mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(modelList.ElementType);
            mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(modelList.GetEnumerator());

            return mockSet;
        }
    }
}
