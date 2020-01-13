using Moq;
using NUnit.Framework;
using RecruitMe.Logic.Data;
using RecruitMe.Logic.Data.Entities;
using RecruitMe.Logic.Logging;
using RecruitMe.Logic.Operations.Abstractions;
using RecruitMe.Logic.Operations.Administration.Exam;
using RecruitMe.Logic.Operations.Payments;
using RecruitMe.Logic.Operations.Payments.Enums;
using RecruitMe.Logic.Operations.Payments.Payment;
using RecruitMe.Logic.Operations.Payments.PaymentLink;
using RecruitMe.Logic.Operations.Payments.SuccessfulMoneyTransfer;
using RecruitMe.Logic.Tests.Helpers;
using RecruitMe.Logic.Utilities.Dates;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RecruitMe.Logic.Tests.UnitTests.Operations.Payments.SuccessfulMoneyTransfer
{
    public class SuccessfulMoneyTransferTests
    {
        ILogger Logger { get; set; }
        IDateTimeProvider DateTimeProvider { get; set; }
        Mock<BaseDbContext> DbContext { get; set; }

        RemoveExistingPaymentLink RemovePaymentLinkCommand { get; set; }
        UpdateSuccessfulPaymentCommand UpdatePaymentCommand { get; set; }
        AssignCandidateToExamsCommand AssignCandidateToExamsCommand { get; set; }

        [SetUp]
        public void Setup()
        {
            DateTimeProvider = new FakeDateProvider();
            DbContext = StartupHelper.GetSetUpAsyncMethod(t => t.Users, GetUsers())
                            .Extend(t => t.Payments, GetPayments())
                            .Extend(t => t.ExamCategories, GetExamCategories())
                            .Extend(t => t.Exams, GetExams())
                            .Extend(t => t.ExamTakers, ExamTakers)
                            .Extend(t => t.PaymentLinks, PaymentLinks);
            Logger = new ConsoleLogger();

            AssignCandidateToExamsCommand = new AssignCandidateToExamsCommand(Logger, DbContext.Object);
            RemovePaymentLinkCommand = new RemoveExistingPaymentLink(Logger, DbContext.Object);
            UpdatePaymentCommand = new UpdateSuccessfulPaymentCommand(Logger, DbContext.Object, DateTimeProvider);

            // to make removeCancellationLink work properly
            DbContext.Setup(x => x.PaymentLinks.Remove(It.IsAny<PaymentLink>()))
                .Callback<PaymentLink>(link => PaymentLinks.Remove(link));
            DbContext.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);
            // to add to examTakers something
            DbContext.Setup(x => x.ExamTakers.Add(It.IsAny<ExamTaker>()))
                .Callback<ExamTaker>(et => ExamTakers.Add(et));
        }

        [Test]
        public async Task ShouldOperationSucceedWhenExistingAndValidDataProvided()
        {
            int expectedPaymentLinksCount = PaymentLinks.Count - 1;
            PaymentResponseDto responseDto = new PaymentResponseDto()
            {
                Id = 1921093123,
                Control = "1:1",
                Description = "Opłata rekrutacyjna 1/1/2001",
                Number = "M10001/1001",
                OperationAmount = 7,
                OperationCurrency = "PLN",
                OperationOriginalAmount = 7,
                OperationOriginalCurrency = "PLN",
                Status = OperationStatus.Completed,
                Type = OperationType.Payment
            };
            SuccessfulMoneyTransferParamValidator validator = new SuccessfulMoneyTransferParamValidator();
            Mock<RemovePaymentLinkInDotpayCommand> RemovePaymentLinkInDotpayCommandMock = 
                new Mock<RemovePaymentLinkInDotpayCommand>(null,null,null,null);
            RemovePaymentLinkInDotpayCommandMock.Setup(c => c.Execute(It.IsAny<PaymentLink>())).ReturnsAsync(new OperationSucceded());

            SuccessfulMoneyTransferCommand command = new SuccessfulMoneyTransferCommand(Logger, validator,
                DbContext.Object, UpdatePaymentCommand, RemovePaymentLinkCommand, AssignCandidateToExamsCommand,
                RemovePaymentLinkInDotpayCommandMock.Object);
            OperationResult result = await command.Execute(responseDto);

            Assert.IsTrue(result.Success);
            Assert.AreEqual(expectedPaymentLinksCount, PaymentLinks.Count);
            Assert.AreEqual(GetExams().Count, ExamTakers.Count);
        }

        private List<User> GetUsers()
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
                }
            };
        }
        private List<Payment> GetPayments()
        {
            return new List<Payment>()
            {
                new Payment()
                {
                    Id = 1,
                    Description = $"Opłata rekrutacyjna 1/1/{DateTimeProvider.Now.Year}",
                    DotpayOperationNumber = null,
                    IssueDate = DateTimeProvider.Now,
                    PaidDate = null,
                    UserId = 1
                }
            };
        }
        private List<Exam> GetExams()
        {
            return new List<Exam>()
            {
                new Exam()
                {
                    Id = 1,
                    StartDateTime = DateTimeProvider.Now.AddDays(-30),
                    SeatCount = 2,
                    DurationInMinutes = 30,
                    ExamTakers = GetExamTakers(),
                    ExamCategoryId = 1,
                    ExamCategory = GetExamCategories()[0]
                }
            };
        }
        private List<ExamCategory> GetExamCategories()
        {
            return new List<ExamCategory>()
            {
                new ExamCategory()
                {
                    Id = 1,
                    ExamType = ExamType.Collective,
                    Name = "asdf"
                }
            };
        }
        private List<ExamTaker> GetExamTakers()
        {
            return new List<ExamTaker>()
            {

            };
        }
        private List<ExamTaker> ExamTakers
        {
            get
            {
                if (examTakers == null)
                {
                    examTakers = GetExamTakers();
                }

                return examTakers;
            }
        }
        private List<ExamTaker> examTakers;
        private List<PaymentLink> GetPaymentLinks()
        {
            return new List<PaymentLink>()
            {
                new PaymentLink()
                {
                    Id = 1,
                    Link = "xddddd.com",
                    UserId = 1
                },
                new PaymentLink()
                {
                    Id = 2,
                    Link = "wykop.pl",
                    UserId = -1
                }
            };
        }
        private List<PaymentLink> PaymentLinks
        {
            get
            {
                if (paymentLinks == null)
                {
                    paymentLinks = GetPaymentLinks();
                }

                return paymentLinks;
            }
        }
        private List<PaymentLink> paymentLinks;
    }
}
