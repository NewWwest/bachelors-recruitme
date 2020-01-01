using Microsoft.EntityFrameworkCore;
using PdfSharpCore.Drawing;
using PdfSharpCore.Drawing.Layout;
using PdfSharpCore.Pdf;
using QRCoder;
using RecruitMe.Logic.Data;
using RecruitMe.Logic.Data.Entities;
using RecruitMe.Logic.Logging;
using RecruitMe.Logic.Operations.Abstractions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecruitMe.Logic.Operations.Administration.Exam
{
    public class GetExaminationSheetQuery : BaseAsyncOperation<Stream, int>
    {
        public GetExaminationSheetQuery(ILogger logger, BaseDbContext dbContext) : base(logger, dbContext)
        {
        }

        public override async Task<Stream> Execute(int request)
        {
            Data.Entities.Exam exam = await _dbContext.Exams
                .Include(e => e.ExamCategory)
                .Include(e => e.ExamTakers)
                .ThenInclude(et => et.User)
                .FirstOrDefaultAsync(e => e.Id == 1);
            if (exam == null)
            {
                throw new KeyNotFoundException();
            }
            if (exam.ExamTakers.Count() > 15)
            {
                throw new ValidationFailedException()
                {
                    ValidationResult = new ValidationResult("Karty egzaminacyjne nie są dostępne gdy egzaminy ma więcej niż 15 uczestników")
                };
            }

            using (PdfDocument outputDocument = new PdfDocument())
            {
                outputDocument.Info.Title = $"IdCard_{request}";
                outputDocument.Info.Author = "RecruitMe";

                PdfPage page = outputDocument.AddPage();
                XGraphics gfx = XGraphics.FromPdfPage(page);

                gfx.DrawRectangle(new XPen(XColor.FromKnownColor(XKnownColor.Black), 1.5), 10, 10, page.Width - 20, page.Height - 20);

                PrintHeader(gfx, exam);

                var examTakers = exam.ExamTakers.OrderBy(et => et.UserId).ToList();
                for (int i = 0; i < examTakers.Count; i++)
                {
                    AddOmrRow(i, examTakers[i], gfx);
                }

                DrawQrCode(examTakers, gfx, (int)page.Width - 130, (int)page.Height - 130, 100, 100);

                var stream = new MemoryStream();
                outputDocument.Save(stream, false);
                stream.Seek(0, SeekOrigin.Begin);
                return stream;
            }
        }

        private void PrintHeader(XGraphics gfx, Data.Entities.Exam exam)
        {
            var text = $"Recruit.Me Exam Sheet \nExam: {exam.ExamCategory.Name} \nStart: {exam.StartDateTime.ToString("HH:mm dd-MM-YYYY")}";
            XFont font = new XFont("Arial", 12, XFontStyle.Regular);
            XTextFormatter tf = new XTextFormatter(gfx);
            tf.DrawString(text, font, XBrushes.Gray, new XRect(15, 15, 300, 100));
        }

        private void AddOmrRow(int i, ExamTaker examTaker, XGraphics gfx)
        {
            int y = 70 + 50 * i;
            var text = $"{i + 1}. {examTaker.User.Name} {examTaker.User.Surname}";
            XFont font = new XFont("Arial", 12, XFontStyle.Regular);
            gfx.DrawString(text, font, XBrushes.Gray, new XPoint(15, y));

            XFont smallFont = new XFont("Arial", 5, XFontStyle.BoldItalic);
            for (int points = 0; points < 10; points++)
            {
                string pointsPlusOne = (points + 1).ToString();
                var pointX = 15 + points * 20;
                var pointY = y + 15;
                gfx.DrawEllipse(new XPen(XColor.FromKnownColor(XKnownColor.Black), 2), pointX, pointY, 10, 10);
                gfx.DrawString(pointsPlusOne, smallFont, XBrushes.LightGray, new XRect(pointX, pointY, 10, 10), XStringFormats.Center);
            }
        }

        private void DrawQrCode(List<ExamTaker> examTakers, XGraphics gfx, int x, int y, int sizeX, int sizeY)
        {
            var formattedUserIds = string.Join(",", examTakers.Select(et => et.UserId.ToString()));
            using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
            {
                QRCodeData qrCodeData = qrGenerator.CreateQrCode("formattedUserIds", QRCodeGenerator.ECCLevel.Q);
                using (QRCode qrCode = new QRCode(qrCodeData))
                using (var tempStream = new MemoryStream())
                {
                    Bitmap qrCodeImage = qrCode.GetGraphic(20);

                    qrCodeImage.Save(tempStream, ImageFormat.Png);
                    tempStream.Seek(0, SeekOrigin.Begin);
                    XImage image = XImage.FromStream(() => tempStream);

                    gfx.DrawImage(image, x, y, sizeX, sizeY);
                }
            }
        }
    }
}
