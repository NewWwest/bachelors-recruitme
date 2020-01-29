using System;
using System.Collections.Generic;
using System.Text;

namespace RecruitMe.Logic.Operations.Abstractions
{
    public abstract class OperationResult
    {
        public abstract bool Success { get; }
    }

    public class OperationSucceded : OperationResult
    {
        public override bool Success => true;
    }

    public class OperationFailed : OperationResult
    {
        public override bool Success => false;
    }
}
