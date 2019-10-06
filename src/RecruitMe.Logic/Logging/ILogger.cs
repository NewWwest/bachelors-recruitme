using System;
using System.Collections.Generic;
using System.Text;

namespace RecruitMe.Logic.Logging
{
    public interface ILogger
    {
        void Log(object obj);
        void Log(string message);
    }
}
