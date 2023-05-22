using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UPayment_Case.Models
{
    public class CustomerCardDto
    {
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDate { get; set; }
        public string IdentityNo { get; set; }
        public string Status { get; set; }
    }
}