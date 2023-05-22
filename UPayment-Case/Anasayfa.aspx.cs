using System;
using System.Windows.Forms;
using UPayment_Case.Business;

namespace UPayment_Case
{
    public partial class Anasayfa : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btn_PaymentClick(object sender, EventArgs e)
        {
            string amount = Amount.Value;
            string installment = Installment.Value;
            string cardHolderName = CardName.Value;
            string cardNo = CardNo.Value;
            string exDate = expDate.Value;
            string cvv= Cvv.Value;

            cardNo.Replace(" ", "");
            var payment = new UPaymentApi();
            var response = payment.NonsecurePayment(amount,installment,cardHolderName,cardNo,exDate,cvv);
            if(response.statusCode == 200 )
            {
                MessageBox.Show("Ödeme İşlemi Başarılı");
                Response.Redirect("RefundORCancel.aspx", true);
            }
        }

     
    }
}