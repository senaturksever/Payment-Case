using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UPayment_Case
{
    public static class Amount
    {
        public static string ToAmountNotFraction(this string amount)
        {
            return amount.Replace(".", "").Replace(",", "");
        }
    }
}