using Moq;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using RecruitMe.Logic.Tests.Helpers.TestAsyncMocks;
using System.Threading;

namespace RecruitMe.Logic.Tests.Helpers
{
    public static class MoqHelper
    {
        public static Expression<Func<T, bool>> AreEqual<T>(Expression<Func<T, bool>> expr)
        {
            return Match.Create<Expression<Func<T, bool>>>(t => t.ToString() == expr.ToString());
        }

        public static Mock<DbSet<T>> GetTable<T>(List<T> listToMoq) where T : class
        {
            IQueryable<T> list = listToMoq.AsQueryable();

            Mock<DbSet<T>> mockSet = new Mock<DbSet<T>>();
            mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(list.Provider);
            mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(list.Expression);
            mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(list.ElementType);
            mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(list.GetEnumerator());

            return mockSet;
        }

        public static Mock<DbSet<T>> GetTableForAsync<T>(IEnumerable<T> listToMoq) where T : class
        {
            IQueryable<T> list = listToMoq.AsQueryable();
            Mock<DbSet<T>> mockSet = new Mock<DbSet<T>>();

            mockSet.As<IAsyncEnumerable<T>>().Setup(m => m.GetEnumerator())
                .Returns(new TestAsyncEnumerator<T>(list.GetEnumerator()));
            mockSet.As<IQueryable<T>>().Setup(m => m.Provider)
                .Returns(new TestAsyncQueryProvider<T>(list.Provider));

            mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(list.Expression);
            mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(list.ElementType);
            mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(list.GetEnumerator());

            return mockSet;
        }
    }
}
