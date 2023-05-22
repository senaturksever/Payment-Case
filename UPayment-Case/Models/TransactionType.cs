using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UPayment_Case.Models
{
    public class TransactionType
    {
        public const string
            Payment = "Auth",
            Cancel = "Void",
            Refund = "Refund";
    }
}