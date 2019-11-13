using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Test.Api.Models
{
    public class CustomResult
    {
        public object Result { get; set; }
        public bool Success { get; private set; }
        public string Message { get; set; }

        public CustomResult(bool success, object result, string message = "")
        {
            Result = result;
            Success = success;
            Message = message;
        }
    }
}
