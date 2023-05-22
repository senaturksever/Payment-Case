using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UPayment_Case.Models.PaymentDtos;

namespace UPayment_Case.Models.RefundOrCancelDtos
{
    public class ROCRequestDto
    {
        public string memberId { get; set; }
        public string merchantId { get; set; }
        public string customerId { get; set; }
        public string userCode { get; set; }
        public string txnType { get; set; }
        public string okUrl { get; set; }
        public string failUrl { get; set; }
        public string orderId { get; set; }
        public string totalAmount { get; set; }
        public string rnd { get; set; }
        public string hash { get; set; }
        public string description { get; set; }
        public string requestIp { get; set; }
        public string extraData { get; set; }
        public List<OrderProductList> orderProductList { get; set; }

    }
    public class OrderProductList
    {
        public int merchantId { get; set; }
        public string productId { get; set; }
        public string amount { get; set; }
        public string productName { get; set; }
        public string commissionRate { get; set; }
    }
}