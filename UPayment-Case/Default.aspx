<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="UPayment_Case._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <main>


        <div class="row">

            <section class="col-md-4" aria-labelledby="gettingStartedTitle">
                <h2 id="gettingStartedTitle">Tc Kimlik Kontrolü</h2>
                <input type="text" id="tcNo" placeholder="Tc Kimlik No" required="required" runat="server" /><br />
                <br />
                <input type="text" id="nameTc" placeholder="Ad" required="required" runat="server" /><br />
                <br />
                <input type="text" id="surnameTc" placeholder="Soyad" required="required" runat="server" /><br />
                <br />
                <input type="text" id="bDate" placeholder="Dogum Yılı" required="required" runat="server" /><br />
                <br />
                <button id='btnN' runat='server' onserverclick='btn_Click'>Kaydet</button>
            </section>


            <section class="col-md-4" aria-labelledby="gettingStartedTitle">
                <h2 id="login">Login İşlemi</h2>
                <input type="text" id="email" placeholder="Email" required="required" runat="server" /><br />
                <br />
                <input type="text" id="password" placeholder="Şifre" required="required" runat="server" /><br />
                <br />
                <select name="language" id="languageDll" runat="server">
                    <option value="">Dil</option>
                    <option value="TR">TR</option>
                    <option value="EN">EN</option>
                </select>

                <button id='btnLogin' runat='server' onserverclick='btn_LoginClick'>Giriş Yap</button>
            </section>

        </div>
    </main>

</asp:Content>
