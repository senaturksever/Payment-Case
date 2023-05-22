using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using UPayment_Case.Business;
using UPayment_Case.Models.LoginOperationDtos;

namespace UPayment_Case
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_Click(object sender, EventArgs e)
        {
            long kimlikNo = long.Parse(tcNo.Value);
            string name = nameTc.Value;
            string surname =  surnameTc.Value;
            int birthDate = int.Parse(bDate.Value);

            string status = "";
            try
            {
                using(TcDogrula.KPSPublicSoapClient services= new TcDogrula.KPSPublicSoapClient { })
                {
                    status = (services.TCKimlikNoDogrula(kimlikNo, name, surname, birthDate)).ToString();
                }

                if(status == "True")
                {
                    
                    var reg = new CustomerRegister().Register(name, surname, birthDate.ToString(), kimlikNo.ToString(), status.ToString());
                    MessageBox.Show("Kayıt Başarılı");
                }
                else
                {
                    MessageBox.Show("Bilgiler Hatalı");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        protected void btn_LoginClick(object sender, EventArgs e)
        {
            
                var emailAdress = email.Value;
                var pass = password.Value;
                var lang = languageDll.Value;


                var login = new UPaymentApi();

                var responseDto = login.Login(emailAdress, pass, lang);

                if (responseDto != null)
                {
                    Response.Redirect("Anasayfa.aspx", true);
               
                    MessageBox.Show("Giriş Başarılı");
                }
                else
                {
                    MessageBox.Show("Bilgiler Yanlış");
                }
           
           
        

        }
    }
}