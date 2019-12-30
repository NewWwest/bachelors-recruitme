﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using RecruitMe.Logic.Data;
using System;
using System.Collections.Generic;
using System.Text;
using RecruitMe.Logic.Tests.Helpers;
using RecruitMe.Logic.Data.Entities;
using RecruitMe.Logic.Logging;
using RecruitMe.Logic.Operations.Account.Login;
using RecruitMe.Logic.Operations.Account.Helpers;
using RecruitMe.Logic.Tests.Helpers.TestAsyncMocks;
using System.Threading.Tasks;
using RecruitMe.Logic.Configuration;

namespace RecruitMe.Logic.Tests.Operations.Account.Login
{
    public class LoginTests
    {
        Mock<BaseDbContext> DbContext { get; set; }

        [SetUp]
        public void Setup()
        {
            // dbContext in-memory setup
            var serviceProvider = new ServiceCollection()
                                    .AddEntityFrameworkInMemoryDatabase()
                                    .BuildServiceProvider();

            var builder = new DbContextOptionsBuilder<BaseDbContext>()
                            .UseInMemoryDatabase("InMemoryDb")
                            .UseInternalServiceProvider(serviceProvider);

            DbContext = new Mock<BaseDbContext>(builder.Options, new BusinessConfiguration());

            Mock<DbSet<User>> userTable = MoqHelper.GetTableForAsync(new TestAsyncEnumerable<User>(GetUserCollection()));
            DbContext.Setup(t => t.Users).Returns(userTable.Object);
        }

        [Test]
        public async Task ShouldReturnExistingUserOnValidCandidateIdAndPassword()
        {
            ILogger logger = new ConsoleLogger();
            LoginRequestValidator validator = new LoginRequestValidator();
            PasswordHasher passwordHasher = new PasswordHasher();
            LoginDto loginDto = new LoginDto() { CandidateId = "aaabbb000", Password = "alaMaKota" };

            var query = new LoginUserQuery(logger, validator, DbContext.Object, passwordHasher);
            var result = await query.Execute(loginDto);
            Assert.AreEqual(result, GetUserCollection()[0]);
        }

        [Test]
        public void ShouldThrowUnauthorizedAccessExceptionWhenWrongDataPassed()
        {
            ILogger logger = new ConsoleLogger();
            LoginRequestValidator validator = new LoginRequestValidator();
            PasswordHasher passwordHasher = new PasswordHasher();
            LoginDto loginDto = new LoginDto() { CandidateId = "misxyz000", Password = "CzTeRyRoZoWeSlOnIe" };

            Assert.ThrowsAsync<UnauthorizedAccessException>(async () => await new LoginUserQuery(logger, validator, DbContext.Object, passwordHasher).Execute(loginDto));
        }

        private List<User> GetUserCollection()
        {
            return new List<User>()
            {
                new User()  // password: alaMaKota
                {
                    Id = 1,
                    Name = "Aaa",
                    Surname = "Bbb",
                    CandidateId = "aaabbb000",
                    PasswordHash = "wCsly8oz1n+pUyXCnWXWHk26+Kqb7N4BDys7BUK3Tmybnt0k",
                },
                new User() // password: czteryRozoweSlonie
                {
                    Id = 2,
                    Name = "Mister",
                    Surname = "XYZ",
                    CandidateId = "misxyz000",
                    PasswordHash = "lie7sXZlOcWGS9+EkEeom+1GD6E4tyygWCtLGQRa8M7NTr9t",
                }
            };
        }
    }
}
