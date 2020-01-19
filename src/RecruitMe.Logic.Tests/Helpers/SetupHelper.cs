using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using RecruitMe.Logic.Configuration;
using RecruitMe.Logic.Data;
using RecruitMe.Logic.Tests.Helpers.TestAsyncMocks;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace RecruitMe.Logic.Tests.Helpers
{
    public static class SetupHelper
    {
        public static Mock<BaseDbContext> GetSetUpAsyncMethod<T>(
            Expression<Func<BaseDbContext, DbSet<T>>> tableExpression,
            List<T> collection) where T : class
        {
            // dbContext in-memory setup
            var serviceProvider = new ServiceCollection()
                                    .AddEntityFrameworkInMemoryDatabase()
                                    .BuildServiceProvider();

            var builder = new DbContextOptionsBuilder<BaseDbContext>()
                            .UseInMemoryDatabase("InMemoryDb")
                            .UseInternalServiceProvider(serviceProvider);

            Mock<BaseDbContext> dbContext = new Mock<BaseDbContext>(builder.Options, new BusinessConfiguration());
            dbContext.Extend(tableExpression, collection);

            return dbContext;
        }

        public static Mock<BaseDbContext> Extend<T>(this Mock<BaseDbContext> dbContext,
            Expression<Func<BaseDbContext, DbSet<T>>> tableExpression,
            List<T> collection) where T : class
        {
            Mock<DbSet<T>> table = MoqHelper.GetTableForAsync(new TestAsyncEnumerable<T>(collection));
            dbContext.Setup(tableExpression).Returns(table.Object);

            return dbContext;
        }
    }
}
