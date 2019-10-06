using System;
using System.Collections.Generic;
using System.Text;

namespace RecruitMe.Logic.Logging
{
    public class ConsoleLogger : ILogger
    {
        public void Log(object obj) => Log(obj.ToString());

        public void Log(string message)
        {
            Console.WriteLine(message);
        }
    }
}
