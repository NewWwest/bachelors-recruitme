using System;
using System.Collections.Generic;
using System.Text;

namespace RecruitMe.Logic.Logging
{
    public class ConsoleLogger : ILogger
    {
        public void Log(string message)
        {
            Console.WriteLine(message);
        }
    }
}
