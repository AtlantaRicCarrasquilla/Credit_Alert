<%@ page language="C#" autoeventwireup="true" inherits="alert_approve, App_Web_gqpsboyi" %>
<%@ Register TagPrefix="uc" TagName="print" Src="~/user_controls/printing.ascx"  %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Credit Alerts Approve Order</title>
    <link href="intranet.css" rel="stylesheet" type="text/css" />
    <link href="intranet_print.css" rel="stylesheet" type="text/css" />  
</head>
<body>
    <form id="form1" runat="server"><div>
    <asp:Panel runat="server" ID="pnlEnvironment" CssClass="pnlEnvironment" >Using Test Enivronment & Test Data</asp:Panel>
    <asp:Panel runat="server" ID="pnlConfirm" CssClass="pnlConfirmPrint"><strong><asp:label runat="server" ID="lblConfirmString" /></strong></asp:Panel>
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
            <td align="right" valign="top"><asp:label runat="server" ID="lblOtherAlerts" Text="Required Approvals" /></td>
            <td>
                <table class="table_OtherAlerts">
                    <tr>
                        <td class="table_OtherAlerts_c1"><asp:label runat="server" ID="lblOther_C" Text="Credit Alert - " /></td>
                        <td><asp:Label runat="server" ID="lblOtherCredit" /></td>                        
                    </tr>
                    <tr>
                        <td class="table_OtherAlerts_c1"><asp:label runat="server" ID="lblOther_T" Text="Tech Alert - " /></td>
                        <td><asp:Label runat="server" ID="lblOtherTech" /></td>                           
                    </tr>
                    <tr>
                        <td class="table_OtherAlerts_c1"><asp:label runat="server" ID="lblOther_R" Text="RIS Alert - " /></td>
                        <td><asp:Label runat="server" ID="lblOtherRIS" /></td>                           
                    </tr>
                </table>

          </td>
        </tr>
         
             <tr>
                 <td align="right">
                     <b>Alert</b></td>
                 <td>
                     <asp:TextBox ID="tbAlert" runat="server" Width="350px" />
                 </td>
             </tr>
             <tr>
                 <td align="right" valign="top">
                     <b>Notes</b></td>
                 <td>
                     <asp:TextBox ID="tbNotes" runat="server" Height="50px" TextMode="MultiLine" 
                         Width="350px" />
                     <br />
                     <asp:RequiredFieldValidator ID="rfvNotes" runat="server" 
                         ControlToValidate="tbNotes" Display="Dynamic" ErrorMessage="Note Required" />
                 </td>
             </tr>
             <tr>
                 <td colspan="2">
                     <uc:print ID="ucPringing" runat="server" Visible="false" />
                 </td>
             </tr>
             <tr>
                 <td align="right">
                     <b>Print Override</b></td>
                 <td>
                     <asp:CheckBox ID="cbPrintByPass" runat="server" AutoPostBack="true" 
                         Text="Do Not Print" />
                 </td>
             </tr>
             <tr>
                 <td colspan="2">
                     <asp:Button ID="btnApprove" runat="server" OnClick="btnApprove_Click" 
                         Text="Approve" />
                     <asp:Button ID="btnCancel" runat="server" CausesValidation="false" 
                         OnClick="btnCancel_Click" Text="Cancel" />
                     <asp:Button ID="btnConfirmHome" runat="server" OnClick="btnConfirmHome_Click" 
                         Text="Back to Alert List" />
                     <asp:HiddenField ID="hfMultiDontPrint" runat="server" Value="0" />
                 </td>
             </tr>
        
        </table>
        </asp:panel>
    </div>
    </form>
</body>
</html>
