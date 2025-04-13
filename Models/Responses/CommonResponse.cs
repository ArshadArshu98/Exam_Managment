using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Responses
{
    public class CommonResponse<T>
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }

        public static CommonResponse<T> Ok(T data, string? message = "Success") =>
            new CommonResponse<T> { Success = true, Message = message, Data = data };

        public static CommonResponse<T> Fail(string message) =>
            new CommonResponse<T> { Success = false, Message = message };
    }
}
