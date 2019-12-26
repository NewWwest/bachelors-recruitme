using QRCoder;
using RecruitMe.Logic.Data;
using RecruitMe.Logic.Logging;
using RecruitMe.Logic.Operations.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using System.Drawing.Imaging;
using RecruitMe.Logic.Configuration;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Drawing;
using RecruitMe.Logic.Data.Entities;

namespace RecruitMe.Logic.Operations.Administration.Candidate
{
    public class GetCandidateIdCardQuery : BaseAsyncOperation<Stream, int>
    {
        private readonly IFileRepository _fileRepository;
        private readonly EndpointConfig _endpointConfig;
        private readonly BusinessConfiguration _businessConfiguration;

        public GetCandidateIdCardQuery(ILogger logger, BaseDbContext dbContext,
            EndpointConfig endpointConfig,
            IFileRepository fileRepository,
            BusinessConfiguration businessConfiguration) : base(logger, dbContext)
        {
            _fileRepository = fileRepository;
            _endpointConfig = endpointConfig;
            _businessConfiguration = businessConfiguration;
        }

        public override async Task<Stream> Execute(int request)
        {
            var user = await _dbContext.Users
                .Include(u => u.PersonalData)
                .ThenInclude(pd => pd.ProfilePictureFile)
                .FirstOrDefaultAsync(u => u.Id == request);
            if (user == null)
            {
                throw new KeyNotFoundException();
            }

            using (PdfDocument outputDocument = new PdfDocument())
            {
                outputDocument.Info.Title = $"IdCard_{request}";
                outputDocument.Info.Author = "RecruitMe";

                PdfPage page = outputDocument.AddPage();
                XGraphics gfx = XGraphics.FromPdfPage(page);

                //Draw outline
                gfx.DrawRectangle(new XPen(XColor.FromKnownColor(XKnownColor.Gray), 0.7), 0, 0, 300, 400);

                DrawProfilePicture(user, gfx);

                //Draw name surname
                var displayName = user.Name + " " + user.Surname;
                XFont font = new XFont("Arial", GetFontSize(displayName.Length), XFontStyle.BoldItalic);
                gfx.DrawString(displayName, font, XBrushes.Black, new XRect(0, 220, 300, 20), XStringFormats.Center);

                DrawQrCode(user, gfx);

                var stream = new MemoryStream();
                outputDocument.Save(stream, false);
                stream.Seek(0, SeekOrigin.Begin);
                return stream;
            }
        }

        private double GetFontSize(int length)
        {
            if (length < 25)
                return 20;
            else if (length < 45)
                return 12;
            else
                return 8;
            //else
            //  go home greedy tester, you're drunk
        }

        private void DrawQrCode(User user, XGraphics gfx)
        {
            using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
            {
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(
                    _endpointConfig.AdminPanelDetailsCandidatate(user.Id.ToString()),
                    QRCodeGenerator.ECCLevel.Q);
                using (QRCode qrCode = new QRCode(qrCodeData))
                using (var tempStream = new MemoryStream())
                {
                    Bitmap qrCodeImage = qrCode.GetGraphic(20);

                    qrCodeImage.Save(tempStream, ImageFormat.Png);
                    tempStream.Seek(0, SeekOrigin.Begin);
                    XImage image = XImage.FromStream(tempStream);

                    gfx.DrawImage(image, 100, 250, 100, 100);
                }
            }
        }

        private void DrawProfilePicture(User user, XGraphics gfx)
        {
            var imageUrl = user.PersonalData?.ProfilePictureFile?.FileUrl ?? _businessConfiguration.DefaultProfileImagePath;
            using (Stream profilePic = _fileRepository.Get(imageUrl))
            {
                XImage image = XImage.FromStream(profilePic);

                gfx.DrawImage(image, 50, 10, 200, 200);
            }
        }
    }
}
