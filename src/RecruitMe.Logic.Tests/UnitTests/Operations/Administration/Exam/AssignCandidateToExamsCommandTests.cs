using Moq;
using NUnit.Framework;
using RecruitMe.Logic.Data;
using RecruitMe.Logic.Data.Entities;
using RecruitMe.Logic.Operations.Administration.Exam;
using RecruitMe.Logic.Tests.Helpers;
using RecruitMe.Logic.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace RecruitMe.Logic.Tests.UnitTests.Operations.Administration.Exam
{
    public class AssignCandidateToExamsCommandTests
    {
        Mock<BaseDbContext> DbContext { get; set; }
        List<ExamTaker> ExamTakers = new List<ExamTaker>();

        [SetUp]
        public void Setup()
        {
            //TEST DATA
            List<User> users = new List<User>()
            {
                new User()
                    {
                    Id = 1,
                    Name = "testName1",
                    Surname = "testName1",
                    CandidateId = "aaabbb000",
                    PasswordHash = "testPasswordHash1",
                },
                new User()
                    {
                    Id = 2,
                    Name = "testName2",
                    Surname = "testName2",
                    CandidateId = "aaabbb111",
                    PasswordHash = "testPasswordHash2",
                }
            };
            List<RecruitMe.Logic.Data.Entities.Exam> exams = new List<Data.Entities.Exam>()
            {
                new Data.Entities.Exam()
                {
                    DurationInMinutes=1,
                    ExamCategoryId=1,
                    Id=1,
                    SeatCount=-1,
                    StartDateTime=DateTime.Now.AddDays(-10),
                },
                new Data.Entities.Exam()
                {
                    DurationInMinutes=1,
                    ExamCategoryId=1,
                    Id=2,
                    SeatCount=10,
                    StartDateTime=DateTime.Now.AddDays(5),
                },
                new Data.Entities.Exam()
                {
                    DurationInMinutes=1,
                    ExamCategoryId=1,
                    Id=3,
                    SeatCount=10,
                    StartDateTime=DateTime.Now.AddDays(10),
                },
                new Data.Entities.Exam()
                {
                    DurationInMinutes=1,
                    ExamCategoryId=2,
                    Id=4,
                    SeatCount=10,
                    StartDateTime=DateTime.Now.AddDays(-10),
                }
            };
            List<ExamCategory> categories = new List<ExamCategory>()
            {
                new ExamCategory()
                {
                    ExamType=ExamType.Collective,
                    Id=1,
                    Name="CategoryName1",
                    Exams=exams.Where(e=>e.ExamCategoryId==1)
                },
                new ExamCategory()
                {
                    ExamType=ExamType.Collective,
                    Id=2,
                    Name="CategoryName2",
                    Exams=exams.Where(e=>e.ExamCategoryId==2)
                },
            };
            foreach (var exam in exams)
            {
                exam.ExamTakers = new List<ExamTaker>();
                exam.ExamCategory = categories.Single(ec => ec.Id == exam.ExamCategoryId);
            }

            DbContext = SetupHelper.GetSetUpAsyncMethod(t => t.Users, users);
            DbContext.Extend(t => t.Users, users);
            DbContext.Extend(t => t.ExamCategories, categories);
            DbContext.Extend(t => t.Exams, exams);
            DbContext.Extend(t => t.ExamTakers, ExamTakers);
        }

        [Test]
        public async Task ShouldAssignCandidateToAllTestsWhenConditionsAreMet()
        {
            BackdoorLogger logger = new BackdoorLogger();
            var cmd = new AssignCandidateToExamsCommand(logger, DbContext.Object);
            var result = await cmd.Execute(1);

            Assert.IsTrue(result.Success);
            Assert.AreEqual("Kandydat o Id:1 został zapisany na 2 egzaminów (2,4)", logger.Logs.ToString());
        }

        private class BackdoorLogger : ILogger
        {
            public StringBuilder Logs = new StringBuilder();
            public void Log(object obj) => Logs.Append(obj.ToString());

            public void Log(string message) => Logs.Append(message);
        }
    }
}
