using FluentValidation;
using Microsoft.EntityFrameworkCore;
using RecruitMe.Logic.Data;
using RecruitMe.Logic.Logging;
using RecruitMe.Logic.Operations.Abstractions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using ZXing;
using ZXing.Common;
using ZXing.Windows.Compatibility;

namespace RecruitMe.Logic.Operations.Administration.Exam
{
    public class LoadExaminationSheetValidator : BaseValidator<LoadExaminationSheetRequest>
    {
        public LoadExaminationSheetValidator()
        {
            RuleFor(a => a.TeacherId).NotEmpty().WithMessage("Wskazanie nauczyciela oceniającego jest wymagane.");
            RuleFor(a => a.ExamId).NotEmpty().WithMessage("Wskazanie egzaminu jest wymagane.");
        }
    }
    public class LoadExaminationSheetCommand : BaseOperation<OperationResult, LoadExaminationSheetRequest, LoadExaminationSheetValidator>
    {
        private static object lockTempFile = new object();
        public LoadExaminationSheetCommand(ILogger logger, LoadExaminationSheetValidator validator, BaseDbContext dbContext) : base(logger, validator, dbContext)
        {
        }

        protected override OperationResult DoExecute(LoadExaminationSheetRequest request)
        {
            Data.Entities.Exam exam = _dbContext.Exams.Include(e => e.ExamTakers).FirstOrDefault(e => e.Id == request.ExamId);
            if (exam == null)
            {
                throw new ValidationFailedException()
                {
                    ValidationResult = new ValidationResult($"Nieprawidłowe Id egzaminu: {request.ExamId}")
                };
            }

            Data.Entities.Teacher teacher = _dbContext.Teachers.FirstOrDefault(t => t.Id == request.TeacherId);
            if (teacher == null)
            {
                throw new ValidationFailedException()
                {
                    ValidationResult = new ValidationResult($"Nieprawidłowe Id nauczyciela: {request.TeacherId}")
                };
            }

            List<int> points = null;
            try
            {
                points = GetPoints(request.FileStream);

                if (!points.Any())
                {
                    throw new Exception("Brak sczytanych punktów. - upewnij się że dokument jest poprawny");
                }
            }
            catch (Exception e)
            {
                throw new ValidationFailedException()
                {
                    ValidationResult = new ValidationResult($"Sczytywanie punktów z karty nie powiodło się: {e.Message}")
                };
            }
            request.FileStream.Seek(0, SeekOrigin.Begin);
            List<int> userIds = null;
            try
            {
                userIds = Decode(request.FileStream);
            }
            catch (Exception e)
            {
                throw new ValidationFailedException()
                {
                    ValidationResult = new ValidationResult($"Sczytywanie kodu QR z karty nie powiodło się: {e.Message}")
                };
            }

            if (userIds.GroupBy(x => x).Any(g => g.Count() > 1))
            {
                throw new ValidationFailedException()
                {
                    ValidationResult = new ValidationResult("One user cannot take the same exam twice at the same moment")
                };
            }

            if (points.Count != userIds.Count)
            {
                throw new ValidationFailedException()
                {
                    ValidationResult = new ValidationResult("User count and points count dont match up")
                };
            }

            var examTakers = exam.ExamTakers.ToDictionary(et => et.UserId);
            foreach (var item in points.Zip(userIds, (p, i) => (userId: i, points: p)))
            {
                var examTaker = examTakers[item.userId];
                examTaker.Score = item.points;
                examTaker.TeacherId = request.TeacherId;
            }

            _dbContext.SaveChanges();
            return new OperationSucceded();
        }

        private List<int> GetPoints(Stream fileStream)
        {
            lock (lockTempFile)
            {
                using (var file = new FileStream("Scripts/temp", FileMode.Create))
                {
                    fileStream.CopyTo(file);
                }

                try
                {
                    var processStartInfo = new ProcessStartInfo()
                    {
                        UseShellExecute = false,
                        RedirectStandardInput = true,
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        FileName = "python",
                        Arguments = "-i \"sheets_reader.py\"",
                        WindowStyle = ProcessWindowStyle.Hidden,
                        CreateNoWindow = true,
                        WorkingDirectory = "Scripts/"
                    };
                    string outputString = "";

                    using (var process = new Process())
                    {
                        process.StartInfo = processStartInfo;
                        process.Start();
                        process.WaitForExit(4000);
                        process.Kill();
                        outputString = process.StandardOutput.ReadToEnd();
                    }

                    List<int> grades = outputString
                        .Trim()
                        .Replace("[", "")
                        .Replace("]", "")
                        .Split(".", StringSplitOptions.RemoveEmptyEntries)
                        .Select(s => s.Trim())
                        .Select(s => int.Parse(s) + 1)
                        .ToList();

                    return grades;
                }
                finally
                {
                    File.Delete("Scripts/temp");
                }
            }
        }

        public List<int> Decode(Stream stream)
        {
            using (var image = (Bitmap)Bitmap.FromStream(stream))
            {
                var source = new BitmapLuminanceSource(image);
                var binarizer = new HybridBinarizer(source);
                var bitmapButFunnier = new BinaryBitmap(binarizer);
                var qrCodeResult = new MultiFormatReader().decode(bitmapButFunnier);

                return qrCodeResult.Text
                            .Trim()
                            .Split(",", StringSplitOptions.RemoveEmptyEntries)
                            .Select(s => s.Trim())
                            .Select(s => int.Parse(s))
                            .ToList();
            }
        }
    }
}
