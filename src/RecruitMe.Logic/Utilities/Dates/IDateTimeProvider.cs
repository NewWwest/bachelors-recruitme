using System;
using System.Collections.Generic;
using System.Text;

namespace RecruitMe.Logic.Utilities.Dates
{
    public interface IDateTimeProvider
    {
        DateTime Now { get; }
    }
}
