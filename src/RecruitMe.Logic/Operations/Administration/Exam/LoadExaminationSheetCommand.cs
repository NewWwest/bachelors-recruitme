using Microsoft.EntityFrameworkCore;
using RecruitMe.Logic.Data;
using RecruitMe.Logic.Logging;
using RecruitMe.Logic.Operations.Abstractions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RecruitMe.Logic.Operations.Administration.Exam
{
    public class LoadExaminationSheetCommand : BaseOperation<OperationResult, (int id, Stream fileStream)>
    {
        private static object lockTempFile = new object();
        public LoadExaminationSheetCommand(ILogger logger, BaseDbContext dbContext) : base(logger, dbContext)
        {
        }

        public override OperationResult Execute((int id, Stream fileStream) request)
        {
            var exam = _dbContext.Exams.Include(e => e.ExamTakers).First(e => e.Id == request.id);
            GetPoints(request.fileStream);
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

                    List<int> grades = outputString.Trim()
                        .Replace("[", "")
                        .Replace("]", "")
                        .Split(".", StringSplitOptions.RemoveEmptyEntries)
                        .Select(s => s.Trim())
                        .Select(s => int.Parse(s))
                        .ToList();

                    return grades;
                }
                finally
                {
                    File.Delete("Scripts/temp");
                }
            }
        }
    }
}
