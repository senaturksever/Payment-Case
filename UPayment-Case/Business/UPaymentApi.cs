using Newtonsoft.Json;
using RestSharp;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using UPayment_Case.DataAccess;
using UPayment_Case.Models;
using UPayment_Case.Models.LoginOperationDtos;
using UPayment_Case.Models.PaymentDtos;
using UPayment_Case.Models.RefundOrCancelDtos;

namespace UPayment_Case.Business
{
    public class UPaymentApi
    {
        string loginUrl = "https://ppgsecurity-test.birlesikodeme.com:55002";
        string paymentUrl = "https://ppgpayment-test.birlesikodeme.com:20000";
        readonly Database database = new Database();

        private readonly string CurrencyCode = "949";
        private static string Token { get; set; }
        private static string CustomerId { get; set; }
        private string ApiKey { get; set; }
        private string MemberId { get; set; }
        private string MerchantId { get; set; }
        private static string Amount { get; set; }
        private string UserCode { get; set; }
        private static string OrderId { get; set; }
        public UPaymentApi()
        {

        }
        private void GetAccountInfo()
        {
            var dataTable = database.AccountInfo();
            if (dataTable.Rows.Count > 0)
            {
                MemberId = dataTable.Rows[0][1].ToString();
                MerchantId = dataTable.Rows[0][2].ToString();
                ApiKey = dataTable.Rows[0][3].ToString();
                UserCode = dataTable.Rows[0][4].ToString();
            }
        }
        public LoginResponseDto Login(string email, string password, string lang)
        {
            CustomerId = email;

            var requestDto = new LoginRequestDto
            {
                email = CustomerId,
                password = password,
                lang = lang.ToString(),
            };

            var client = new RestClient(loginUrl);
            var request = new RestRequest("/api/ppg/Securities/authenticationMerchant", Method.Post);
            request.AddHeader("Content-Type", "application/json");

            var body = JsonConvert.SerializeObject(requestDto);
            request.AddStringBody(body, DataFormat.Json);
            var response = client.Execute(request);

            if (response.Content != null)
            {
                var responseBody = JsonConvert.DeserializeObject<LoginResponseDto>(response.Content);
                if (responseBody.statusCode == 200 && responseBody.fail == false)
                {
                    Token = responseBody.result.token;
                }
                return responseBody;
            }
            else
            {
                // throw new Exception($"API isteği başarısız: {response.ErrorMessage}");
                return null;
            }

        }


        /// <summary>
        /// HashÜretir
        /// </summary>
        /// <param name="hashString"></param>
        /// <returns></returns>
        private string CreateHash(string hashString)
        {
            try
            {
                var s512 = SHA512.Create();
                var ByteConverter = new UnicodeEncoding();
                byte[] bytes = s512.ComputeHash(ByteConverter.GetBytes(hashString));
                var hash = BitConverter.ToString(bytes).Replace("-", "");
                return hash;
            }
            catch (Exception)
            {
            }
            return null;
        }

        public PaymentResponseDto NonsecurePayment(string amount, string installment, string cardHolderName, string cardNo, string expDate, string cvv)
        {
            GetAccountInfo();
            try
            {
                var totalAmount = amount.ToAmountNotFraction();
                var txnType = TransactionType.Payment;
                var rnd = Guid.NewGuid().ToString();
                var orderId = Guid.NewGuid().ToString();
                OrderId = orderId;
                Amount = totalAmount;
                var hashString = $"{ApiKey}{UserCode}{rnd}{txnType}{Amount}{CustomerId}{OrderId}";

                var hash = CreateHash(hashString);

                var payment = new PaymentRequestDto
                {
                    memberId = MemberId,
                    merchantId = MerchantId,
                    customerId = CustomerId,
                    cardNumber = cardNo,
                    cardHolderName = cardHolderName,
                    expiryDateMonth = expDate.Split('/')[0],
                    expiryDateYear = expDate.Split('/')[1],
                    cvv = cvv,
                    userCode = UserCode,
                    txnType = txnType,
                    installmentCount = installment,
                    currency = CurrencyCode,
                    orderId = OrderId,
                    totalAmount = Amount,
                    rnd = rnd,
                    hash = hash
                };

                var client = new RestClient(paymentUrl);
                var request = new RestRequest("/api/ppg/Payment/NoneSecurePayment", Method.Post);
                request.AddHeader("Authorization", $"Bearer {Token}");
                request.AddHeader("Content-Type", "application/json");

                var body = JsonConvert.SerializeObject(payment);
                request.AddStringBody(body, DataFormat.Json);
                var response = client.Execute(request);

                if (response.Content != null)
                {
                    var responseBody = JsonConvert.DeserializeObject<PaymentResponseDto>(response.Content);
                    if (responseBody.statusCode == 200)
                    {
                        database.PaymentInsertData(CustomerId, orderId, MerchantId, Amount, cardNo, "Ödeme");
                        MessageBox.Show("Ödeme İşlemi Başarılı");
                        
                    }
                    else
                    {

                        database.PaymentInsertData(CustomerId, orderId, MerchantId, Amount, cardNo, "Ödeme Yapılamadı");
                        MessageBox.Show($"Ödeme İşlemi Yapılamadı {responseBody.result.responseMessage}");
                    }
                    return responseBody;
                }
                else { return null; }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"TanımsızHata Detayı:{ex.Message}");
                return null;
            }

        }

        public ROCResponseDto Cancel()
        {
            GetAccountInfo();
            var rnd = Guid.NewGuid().ToString();
            var totalAmount = Amount;
            var txnType = TransactionType.Cancel;
            var hashString = $"{ApiKey}{UserCode}{rnd}{txnType}{totalAmount}{CustomerId}{OrderId}";

            var hash = CreateHash(hashString);

            var cancel = new ROCRequestDto
            {
                memberId = MemberId,
                merchantId = MerchantId,
                customerId = CustomerId,
                userCode = UserCode,
                txnType = txnType,
                orderId = OrderId,
                totalAmount = totalAmount,
                rnd = rnd,
                hash = hash

            };

            var client = new RestClient(paymentUrl);
            var request = new RestRequest("/api/ppg/Payment/Payment", Method.Post);
            request.AddHeader("Authorization", $"Bearer {Token}");
            request.AddHeader("Content-Type", "application/json");
            var body = JsonConvert.SerializeObject(cancel);
            request.AddStringBody(body, DataFormat.Json);
            var response = client.Execute(request);
            if (response != null)
            {
                var responseBody = JsonConvert.DeserializeObject<ROCResponseDto>(response.Content);
                database.PaymentInsertData(CustomerId, OrderId, MerchantId, Amount, "", "İptal");
                return responseBody;
            }
            else
            {
                database.PaymentInsertData(CustomerId, OrderId, MerchantId, Amount, "", "İptal Edilemedi");
                return null;
            }

        }

        public ROCResponseDto Refund()
        {
            GetAccountInfo();
            var rnd = Guid.NewGuid().ToString();
            var totalAmount = Amount;
            var txnType = TransactionType.Refund;
            var hashString = $"{ApiKey}{UserCode}{rnd}{txnType}{totalAmount}{CustomerId}{OrderId}";

            var hash = CreateHash(hashString);

            var cancel = new ROCRequestDto
            {
                memberId = MemberId,
                merchantId = MerchantId,
                customerId = CustomerId,
                userCode = UserCode,
                txnType = txnType,
                orderId = OrderId,
                totalAmount = totalAmount,
                rnd = rnd,
                hash = hash

            };

            var client = new RestClient(paymentUrl);
            var request = new RestRequest("/api/ppg/Payment/Payment", Method.Post);
            request.AddHeader("Authorization", $"Bearer {Token}");
            request.AddHeader("Content-Type", "application/json");
            var body = JsonConvert.SerializeObject(cancel);
            request.AddStringBody(body, DataFormat.Json);
            var response = client.Execute(request);
            if (response != null)
            {
                var responseBody = JsonConvert.DeserializeObject<ROCResponseDto>(response.Content);
                database.PaymentInsertData(CustomerId, OrderId, MerchantId, Amount, "", "İade");
                return responseBody;
            }
            else
            {
                database.PaymentInsertData(CustomerId, OrderId, MerchantId, Amount, "", "İade Yapılamadı");
                return null;
            }

        }

    }
}