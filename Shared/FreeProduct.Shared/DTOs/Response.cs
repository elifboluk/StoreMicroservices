using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FreeProduct.Shared.DTOs
{
    public class Response<T>
    {
        public T Data { get; private set; } // OK - Get Data

        [JsonIgnore]
        public int StatusCode { get; private set; }

        [JsonIgnore]
        public bool IsSuccessfull { get; private set; }

        public List<string> Errors { get; set; } // Get Errors

        // Static Factory Method
        public static Response<T> Success(T data, int statusCode)
        {
            return new Response<T> { Data = data, StatusCode = statusCode, IsSuccessfull = true };
        }

        public static Response<T> Success(int statusCode)
        {
            return new Response<T> { Data = default(T), StatusCode = statusCode, IsSuccessfull = true };
        }

        public static Response<T> Fail(List<string> errors, int statusCode)
        {
            return new Response<T> { Errors = errors, StatusCode = statusCode, IsSuccessfull = false };
        }

        public static Response<T> Fail(string error, int statusCode)
        {
            return new Response<T> { Errors = new List<string>() { error }, StatusCode = statusCode, IsSuccessfull = false };
        }
    }
}
