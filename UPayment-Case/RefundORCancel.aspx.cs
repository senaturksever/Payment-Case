using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UPayment_Case.Business;

namespace UPayment_Case
{
    public partial class RefundORCancel : System.Web.UI.Page
    {
        UPaymentApi api = new UPaymentApi();
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btn_CancelClick(object sender, EventArgs e)
        {
            api.Cancel();
        
        }
        protected void btn_RefundClick(object sender, EventArgs e)
        {
            api.Refund();
        }
    }
}