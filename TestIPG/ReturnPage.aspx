<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReturnPage.aspx.cs" Inherits="ReturnPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">

        <table style="font-family: tahoma, serif; font-size: 12px;">
            <tbody>
                <tr>
                    <td>شماره فاکتور</td>
                    <td>
                        <asp:TextBox ID="txtRequestIdentifier" runat="server" ReadOnly="True" Width="231px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>شماره پیگیری</td>
                    <td>
                        <asp:TextBox ID="txtReferenceCode" runat="server" ReadOnly="True" Width="231px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>پیام</td>
                    <td>
                        <asp:TextBox ID="txtMessage" runat="server" ReadOnly="True" Width="231px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>کد وضعیت</td>
                    <td>
                        <asp:TextBox ID="txtResultCode" runat="server" ReadOnly="True" Width="231px"></asp:TextBox>
                    </td>
                </tr>

                <tr>                    
                    <td>
                        <asp:Label ID="lblMessage" Text="" runat="server"> </asp:Label>
                    </td>
                </tr>
            </tbody>
        </table>
       




    </form>
</body>
</html>
