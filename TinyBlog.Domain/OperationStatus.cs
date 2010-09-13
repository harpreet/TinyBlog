using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TinyBlog.Objects
{
    public class OperationStatus
    {
        public string Message { get; private set; }
        public bool Success { get; private set; }

        public OperationStatus(bool success, string message)
        {
            Success = success;
            Message = message ?? string.Empty;
        }
    }

    public class Success: OperationStatus
    {
        public Success() : base(true, string.Empty)
        {
        }
    }

    public class Failure: OperationStatus
    {
        public Failure(string message) : base(false, message)
        {
        }
    }


}
