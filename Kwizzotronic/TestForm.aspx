<%--<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestForm.aspx.cs" Inherits="Kwizzotronic.TestForm" %>--%>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <label>Ime</label>
            <asp:TextBox runat="server" ID="txtIme"></asp:TextBox>
            <br />

            <label>Prezime</label>
            <asp:TextBox runat="server" ID="txtPrezime"></asp:TextBox>
            <br />

            <label>Email</label>
            <asp:TextBox runat="server" ID="txtEmail"></asp:TextBox>
            <br />

            <label>Password</label>
            <asp:TextBox runat="server" ID="txtLozinka"></asp:TextBox>
            <br />

            <asp:Button runat="server" Text="Dodaj" ID="btnDodaj" OnClick="btnDodaj_Click" />

            <br />
            <asp:TextBox runat="server" ID="txtNewId"></asp:TextBox>
        </div>
    </form>
</body>
</html>
