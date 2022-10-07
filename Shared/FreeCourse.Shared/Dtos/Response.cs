using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FreeCourse.Shared.Dtos
{
    public class Response<T>
    {
        public T Data { get;private set; }
        
        [JsonIgnore]//response içinde olmasına gerek yok
        public int StatusCode { get;private set; }
        [JsonIgnore]
        public bool IsSuccessfull { get;private set; }

        public List<string> Errors { get; set; }

        //Static factory method
        public static Response<T> Success(T data, int statusCode)
        {
            return new Response<T>
            {
                Data = data,
                StatusCode = statusCode,
                IsSuccessfull = true
            };
        }

        public static Response<T> Success(int statusCode)
        {
            return new Response<T>
            {
                Data = default(T),
                StatusCode = statusCode,
                IsSuccessfull = true
            };
        }

        public static Response<T> Fail(List<string> errors, int statusCode)
        {
            return new Response<T>
            {
                Errors = errors,
                StatusCode = statusCode,
                IsSuccessfull = false
            };
        }
        //tek bir hata olabilir (string error)
        public static Response<T> Fail(string error,int statusCode)
        {

            return new Response<T>
            {
                Errors = new List<string>(){error},
                StatusCode = statusCode,
                IsSuccessfull = false
            };
        }
    }
}
