using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace RecruitMe.Logic.Data
{
    public interface IFileSaver
    {
        Task<string> SaveAsync(Stream stream, string name);
    }
}
