using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results
{
    public class Result : IResult
    {
        public Result(bool success, string message) : this(success) //bu class da tek parametreli olanı da çalıştır demek . Yani aşağıdaki constructoru da çalıştırmış olur aynı zamanda
        {
            Message = message;
        }
        public Result(bool success)
        {  
            Success = success;
        }
        public bool Success { get; }
        public string Message { get; }
    }
}
