<%@ page language="C#" autoeventwireup="true" inherits="home, App_Web_gqpsboyi" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Credit Alerts</title>
    <meta http-equiv="Refresh" content="" runat="server" id="refreshTag" />
    <link href="intranet.css" rel="stylesheet" type="text/css" />
    <link href="intranet_print.css" rel="stylesheet" type="text/css" /> 
</head>
<body>
    <form id="form1" runat="server">
    <asp:Panel runat="server" ID="pnlEnvironment" CssClass="pnlEnvironment" >Using Test Enivronment & Test Data</asp:Panel>
    <div>
    <table>
        <tr>
            <th colspan="3" align="left">Selection Criteria:</th>
        </tr>
        <tr>
            <td>Search Text: <asp:TextBox runat="server" ID="tbSearchText" /><asp:CheckBox runat="server" ID="cbCheckNumber" text="Order Number" ValidationGroup="SearchTxt" Checked="true" />
                <%--<asp:CheckBox runat="server" ID="cbCheckNote" text="Transaction Note" Checked="true" />
                <asp:CheckBox runat="server" ID="cbCheckReceiptDestination" text="Receipt Fax #" /><br />--%>
                <asp:CheckBox runat="server" ID="cbCheckCompanyName" text="Company Name" Checked="true" />
                <asp:CheckBox runat="server" ID="cbCheckAccountCode" text="Account Code" Checked="true" />
                <%--<asp:CheckBox runat="server" ID="cbCheckPostToCode" text="PostTo Code" />
                <asp:CheckBox runat="server" ID="cbCheckReferenceCode" text="Reference Code" />--%></td>
            <td></td>
            <td></td>
        </tr>
        <tr>
            <td>Date Range: <asp:TextBox runat="server" ID="tbFromDate" Width="150px" /> to <asp:TextBox runat="server" ID="tbToDate" Width="150px" /></td>
            <td colspan="2">
                <br />
            </td>
        </tr>
        <tr>
            <td></td>
            <td colspan="2">
                <asp:CompareValidator Display="Dynamic" 
                    id="cvDateValidator" runat="server"
                    type="Date" Operator="DataTypeCheck" ControlToValidate="tbFromDate" ErrorMessage="Enter a valid from date. " /><br /><asp:CompareValidator 
                    id="CompareValidator1" runat="server" Display="Dynamic"
                    type="Date" Operator="DataTypeCheck" ControlToValidate="tbToDate" ErrorMessage="Enter a valid to date. " />
            </td>
        </tr>
    <%--<tr>
            <td>Order Status:</td>
            <td colspan="2">
                <asp:CheckBoxList runat="server" ID="cblResponseCode" RepeatDirection="Horizontal" AutoPostBack="true" />                
            </td>
        </tr>--%> 
        <tr>
            <td>Search Type: <asp:RadioButtonList runat="server" ID="rblSearchType" RepeatDirection="Horizontal" AutoPostBack="true" BorderStyle="None" Width="350px" >                
                    <asp:ListItem  Text="Begins With" Value="BeginsWith" Selected="True"/>
                    <asp:ListItem  Text="Contains" Value="Contains"/>
                    <asp:ListItem  Text="EndsWith" Value="EndsWith"/>
                </asp:RadioButtonList></td>
            <td colspan="2" align="left">                
            </td>
        </tr>   
        <%--<tr>
            <td>Card Type:</td>
            <td colspan="2">
                <asp:CheckBoxList runat="server" ID="cblCardType" RepeatDirection="Horizontal" AutoPostBack="true" />                
            </td>
        </tr>--%>  
        <tr>
            <td>Alert Type: <asp:CheckBoxList runat="server" ID="cblAlertType" RepeatDirection="Horizontal" AutoPostBack="true" BorderStyle="None" Width="350px"  /></td>
            <td colspan="2" align="left">
                           
            </td>
        </tr>  
        <tr>
            <td><asp:Button runat="server" ID="btnSearch" Text="Search" OnClick="btnSearch_Click" /></td>
            <td colspan="2" align="right"><asp:Button runat="server" ID="btnLog" OnClick="btnLog_Click" Text="Alert Log Report" /></td>
        </tr>     
    </table>
    <br />    
    <asp:Panel runat="server" ID="pnlNoResults" CssClass="pnlNoTechAlerts">
        No Alerts Returned
    </asp:Panel>
    <asp:Panel runat="server" ID="pnlAlerts">
        <table  Width="85%" >
            <tr>
                <td align="left" class="clearCell"><b><asp:Label runat="server" ID="lblCount" Text="[Count]" /></b></td>                
                <td align="right" class="clearCell"></td>                
            </tr>
            <tr>
                <td colspan="2">
                    <asp:DataGrid runat="server" ID="dgCreditTechAlert" Width="95%" AutoGenerateColumns="false" AllowSorting="true"  >
                            <ItemStyle Font-Size="12px" BackColor="White" Height="21px" Font-Names="Tahoma" />
                            <AlternatingItemStyle BackColor="WhiteSmoke" Height="21px" Font-Names="Tahoma"  />
                            <HeaderStyle Font-Bold="true" BorderColor="Silver" Height="23px" />
                            <Columns>
                                <asp:BoundColumn DataField="OrderNumber" HeaderText="Order Number" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Left" SortExpression="OrderNumber" />
                                <asp:BoundColumn DataField="OHODT8_2" HeaderText="Date Posted" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" SortExpression="OHODT8_2" />
                                <asp:BoundColumn DataField="CMCSNM" HeaderText="Company Name" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" SortExpression="CMCSNM" />
                                <asp:BoundColumn DataField="OHCSCD" HeaderText="Account Code" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" SortExpression="OHCSCD" />            
                                <asp:BoundColumn DataField="Alert Type" HeaderText="Alert Type" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" SortExpression="Alert Type" />
                                <asp:BoundColumn DataField="Alert Description" HeaderText="Alert Desc" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" SortExpression="Alert Description" />                            
                                <asp:BoundColumn DataField="Action" HeaderText="Action" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />                                                    
                            </Columns>
                    </asp:DataGrid>
                </td>
            </tr>
            <tr>
                <td colspan="2" class="clearCell">
                    <b><asp:Label runat="server" ID="lblLastUpdate" Text="[Run Time]" /></b>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel runat="server" ID="pnlSearchResults">
        <asp:DataGrid runat="server" ID="dgCreditSearch" Width="85%" AutoGenerateColumns="false"  >
            <ItemStyle Font-Size="12px" />
            <Columns>
                <asp:BoundColumn DataField="OrderNumberSort2" HeaderText="Order #" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"  />
                <asp:BoundColumn DataField="CompanyName" HeaderText="Company Name" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <asp:BoundColumn DataField="AccountCode" HeaderText="Account Code" HeaderStyle-HorizontalAlign="Left"  ItemStyle-HorizontalAlign="Left"/>
                <asp:BoundColumn DataField="CreditCardTransactionDatePosted" HeaderText="Date Posted" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <asp:BoundColumn DataField="CreditCardTransactionAmount2" HeaderText="Amount" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" />
                <asp:BoundColumn DataField="Type" HeaderText="Type" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />            
                <asp:BoundColumn DataField="CreditCardResponseCodeName" HeaderText="Status" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundColumn DataField="Alert" HeaderText="Alert" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />                            
                <asp:BoundColumn DataField="Action" HeaderText="Action" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />                      
            </Columns>
        </asp:DataGrid>
    </asp:Panel>
    </div>
    <%--<hr />
     <asp:DataGrid runat="server" ID="dgtest" Width="85%" AutoGenerateColumns="true"  />--%>
    <%--<asp:Label runat="server" ID="lblTest" Text="[]" />--%>
    </form>
</body>
</html>
