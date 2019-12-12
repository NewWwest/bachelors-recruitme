using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace RecruitMe.Logic.Data
{
    public interface IFileRepository
    {
        Stream Get(string url);

        void Delete(string fileName);
    }
}
