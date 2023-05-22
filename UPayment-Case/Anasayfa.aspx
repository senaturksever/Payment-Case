<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Anasayfa.aspx.cs" Inherits="UPayment_Case.Anasayfa" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <main>

            <div class="row">

            <section class="col-md-4" aria-labelledby="gettingStartedTitle">
                <h2 id="gettingStartedTitle">Ödeme İşlemi</h2>

                <input type="text" id="Amount" placeholder="Tutar"  maxlength="6" size="6" required="required" runat="server" /><br /><br />
                <input type="text" id="Installment" placeholder="Taksit Tutarı" required="required" runat="server" /><br /><br /><br />


                <input type="text" id="CardName" placeholder="Kredi Kartı Sahibi" required="required" maxlength="25" size="25" runat="server" /><br />
                <br />
                <input type="text" id="CardNo" placeholder="Kredi Kartı No" required="required" maxlength="25" size="25" runat="server" /><br />
                <br />
                <input type="text" id="expDate" placeholder="MM/yyyy"  maxlength="7" size="7"  required="required" runat="server" /><br />
                <br />
                <input type="text" id="Cvv" placeholder="CVV"  maxlength="3" size="3"  required="required" runat="server" /><br />
                <br />
               <button id='btn' runat='server' onserverclick='btn_PaymentClick'>Kaydet</button>
            </section>

        </div>
    </main>

</asp:Content>
