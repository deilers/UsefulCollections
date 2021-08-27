using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace UsefulCollections
{
    /// <summary>
    /// Static utility methods for transforming generic collections into specialized data types.
    /// </summary>
    public static class UsefulCollections
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

        /// <summary>
        /// Converts enumerable list to in-memory DataTable
        /// </summary>
        public static DataTable ToDataTable<T>(IEnumerable<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (PropertyInfo prop in Props)
            {
                dataTable.Columns.Add(prop.Name);
            }

            foreach (T item in items)
            {
                var values = new object[Props.Length];

                for (int i = 0; i < Props.Length; i++)
                {
                        values[i] = Props[i].GetValue(item, null);
                }

                dataTable.Rows.Add(values);
            }

            return dataTable;
        }
    }
}
