using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Results;

namespace UPayment_Case.Models.LoginOperationDtos
{
    public class LoginResponseDto
    {
        public bool fail { get; set; }
        public int statusCode { get; set; }
        public Result result { get; set; }
        public int count { get; set; }
        public object errorCode { get; set; }
        public object errorDescription { get; set; }
    }
    public class Result
    {
        public long userId { get; set; }
        public string token { get; set; }
    }
}