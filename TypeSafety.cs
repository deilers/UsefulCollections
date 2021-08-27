using System.Data.SqlTypes;
using System;

namespace EntityMockFactory
{
    public static class TypeSafety
    {
        /// <summary>
        /// Check if DateTime value is valid SqlDateTime
        /// </summary>
        public static DateTime? EnsureSQLSafe(DateTime? dateTime)
        {
            if (dateTime.HasValue && 
                (dateTime.Value < (DateTime) SqlDateTime.MinValue || 
                 dateTime.Value > (DateTime) SqlDateTime.MaxValue))
            {
                return null;
            }
            else
            {
                return dateTime;
            }
        }
    }
}