using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UPayment_Case.Models.RefundOrCancelDtos
{
    public class ROCResponseDto
    {
        public string url { get; set; }
        public string responseCode { get; set; }
        public string responseMessage { get; set; }
        public string orderId { get; set; }
        public string txnType { get; set; }
        public string txnStatus { get; set; }
        public int vposId { get; set; }
        public string vposName { get; set; }
    }
}