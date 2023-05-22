using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UPayment_Case.Models.LoginOperationDtos
{
    public class LoginRequestDto
    {
        public string password { get; set; }
        public string lang { get; set; }
        public string email { get; set; }
    }
}