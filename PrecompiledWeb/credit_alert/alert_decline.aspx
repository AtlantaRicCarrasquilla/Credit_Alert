<%@ page language="C#" autoeventwireup="true" inherits="alert_decline, App_Web_gqpsboyi" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Credit Alerts Decline</title>
    <link href="intranet.css" rel="stylesheet" type="text/css" />
    <link href="intranet_print.css" rel="stylesheet" type="text/css" />     
</head>
<body>
    <form id="form1" runat="server">
    <asp:Panel runat="server" ID="pnlEnvironment" CssClass="pnlEnvironment" >Using Test Enivronment & Test Data</asp:Panel>
    <div>
    <asp:Panel runat="server" ID="pnlConfirm" CssClass="pnlDeclinePrint"><strong>Order Voided</strong></asp:Panel>     
    <asp:Panel runat="server" ID="pnlShipAlertEmail" CssClass="pnlShipAlertEmail" Visible="false" >Non-Business Hour Shipment Alert Emailed</asp:Panel>
    <asp:panel runat="server" ID="pnlInfo" CssClass="pnlInfo">

     <table>
        <tr>
            <td colspan="2"><strong>Order Information</strong></td>
        </tr>
        <tr>
            <td align="right">Order Number:</td>
            <td><asp:Label runat="server" ID="lblOrdNum" Text="[OrdNum]" /></td>
        </tr>
        <tr>
            <td align="right">Date Posted:</td>
            <td><asp:Label runat="server" ID="lblDatePosted" Text="[Date Posted]" /></td>
        </tr>
        <tr>
            <td align="right">Company Name:</td>
            <td><asp:Label runat="server" ID="lblCompName" Text="[Company Name]" /></td>
        </tr>
        <tr>
            <td align="right">Account Code:</td>
            <td><asp:Label runat="server" ID="lblAcctCode" Text="[Account Code]" /></td>
        </tr>
        <tr>
            <td colspan="2"><hr /></td>
        </tr>
        <tr>
            <td colspan="2"><strong>Alert Information</strong></td>
        </tr>
        <tr>
            <td align="right">Alert</td>
            <td><asp:TextBox runat="server" ID="tbAlert" Width="350px" /></td>
        </tr>
        <tr>
            <td align="right">Notes</td>
            <td>
                <asp:TextBox runat="server" ID="tbNotes" Width="350px" TextMode="MultiLine" Height="50px" /><br />
                <asp:RequiredFieldValidator runat="server" ID="rfvNotes" ControlToValidate="tbNotes" ErrorMessage="Note Required" Display="Dynamic" />
            </td>
        </tr>
        <tr>
            <td colspan="2"><br /></td>
        </tr>
            <tr>
                <td colspan="2">
                    <asp:Button runat="server" ID="btnDecline" OnClick="btnDecline_Click" Text="Decline" />
                    <asp:Button runat="server" ID="btnCancel" OnClick="btnCancel_Click" Text="Cancel" CausesValidation="false" />
                    <asp:Button runat="server" ID="btnConfirmHome" Text="Back to Alert List" OnClick="btnConfirmHome_Click" />
               </td>
            </tr>
        </table>
        </asp:Panel> 
    </div>
    </form>
</body>
</html>
