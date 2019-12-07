using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace RecruitMe.Logic.Data
{
    public interface IPictureSaver
    {
        Task<string> SaveAsync(Stream stream, string name);
    }
}
