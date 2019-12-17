<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RegisterPage.aspx.cs" Inherits="InternetShoppingSite.RegisterPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Table ID="Table1" runat="server">
                <asp:TableRow>
                    <asp:TableCell>Nazwa użytkownika:</asp:TableCell>
                    <asp:TableCell>
                        <asp:TextBox ID="NameTextBox" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="NameValidator" runat="server" ErrorMessage="Wymagane pole" ControlToValidate="NameTextBox" ForeColor="Red" />
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell>Adres e-mail:</asp:TableCell>
                    <asp:TableCell>
                        <asp:TextBox ID="EmailTextBox" TextMode="Email" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="EmailValidator" runat="server" ErrorMessage="Wymagane pole" ControlToValidate="EmailTextBox" ForeColor="Red" />
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell>Hasło:</asp:TableCell>
                    <asp:TableCell>
                        <asp:TextBox ID="PasswdTextBox" TextMode="Password" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="PwdValidator" runat="server" ErrorMessage="Wymagane pole" ControlToValidate="PasswdTextBox" ForeColor="Red" />
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell>
                        <asp:Button ID="RegisterBtn" runat="server" Text="Zarejestruj" OnClick="RegisterBtn_Click" />
                    </asp:TableCell>
                    <asp:TableCell>
                        <asp:Label ID="ErrorLbl" ForeColor="Red" runat="server" Text=""></asp:Label>
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
        </div>
    </form>
</body>
</html>
