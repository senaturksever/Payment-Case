<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/Site.Master" CodeBehind="RefundORCancel.aspx.cs" Inherits="UPayment_Case.RefundORCancel" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <main>

            <div class="row">

            <section class="col-md-4" aria-labelledby="gettingStartedTitle">
              
      
               <button id='cancelBtn' runat='server' onserverclick='btn_CancelClick'>İptal Et</button>

                
               <button id='refundBtn' runat='server' onserverclick='btn_RefundClick'>İade Et</button>
           

            </section>

        </div>
    </main>

</asp:Content>
