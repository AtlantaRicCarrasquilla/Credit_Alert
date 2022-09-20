<%@ page language="C#" autoeventwireup="true" inherits="alert_log, App_Web_gqpsboyi" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="intranet.css" rel="stylesheet" type="text/css" />
    <link href="intranet_print.css" rel="stylesheet" type="text/css" /> 
</head>
<body>
    <form id="form1" runat="server">
    <asp:Panel runat="server" ID="pnlEnvironment" CssClass="pnlEnvironment" >Using Test Enivronment & Test Data</asp:Panel>
    <div>
    <table>
        <tr>
            <td width="100px">Date Range:</td>
            <td width="300px" colspan="2" valign="middle">
                <asp:TextBox runat="server" ID="tbFromDate" Width="150px" /> to <asp:TextBox runat="server" ID="tbToDate" Width="150px" /><asp:Button runat="server" ID="btnSearch" Text="Search Log" OnClick="btnSearch_Click" ToolTip="Search Alert Log" /><br />
                <asp:CompareValidator Display="Dynamic" 
                    id="CompareValidator2" runat="server"
                    type="Date" Operator="DataTypeCheck" ControlToValidate="tbFromDate" ErrorMessage="Enter a valid from date. " /><br /><asp:CompareValidator 
                    id="CompareValidator3" runat="server" Display="Dynamic"
                    type="Date" Operator="DataTypeCheck" ControlToValidate="tbToDate" ErrorMessage="Enter a valid to date. " />
            </td>                    
        </tr>

    </table> <br />   
    <asp:Panel runat="server" ID="pnlAlertsLog">
        <table  Width="85%" cellpadding="0" cellspacing="0" >
            <tr>
                <td align="left" class="clearCell"><asp:Label runat="server" ID="lblCount" Text="[Count]" /></td>
                <td align="right" class="clearCell">
                    <asp:ImageButton runat="server" ID="ibtnExcel" ImageUrl="~/images/excel.bmp" Width="25px" Height="25px" ImageAlign="Middle" OnClick="ibtnExcel_Click" ToolTip="Save list to excel file" />
                    <asp:Button runat="server" ID="btnReturnToAlerts" Text="Back to Alert List" OnClick="btnReturnToAlerts_Click" ToolTip="Back to Alerts" />
                </td>                   
            </tr>
            <tr>
                <td colspan="2">
                    <asp:DataGrid runat="server" ID="dgCreditTechAlert" Width="95%" AutoGenerateColumns="false" AllowSorting="true"   >
                           <ItemStyle Font-Size="12px" />
                           <Columns>
                                <asp:BoundColumn DataField="ordernumber" HeaderText="Order Number" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Left"  />
                                <asp:BoundColumn DataField="dateposted" HeaderText="Date Posted" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left"  />
                                <asp:BoundColumn DataField="companyname" HeaderText="Company Name" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left"   />
                                <asp:BoundColumn DataField="accountcode" HeaderText="Account Code" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left"   />            
                                <asp:BoundColumn DataField="alert_type" HeaderText="Alert Type" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left"  />
                                <asp:BoundColumn DataField="alert_desc" HeaderText="Alert Desc" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left"  />                            
                                <asp:BoundColumn DataField="ad_account" HeaderText="User" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"  />            
                                <asp:BoundColumn DataField="action" HeaderText="Action" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />            
                                <asp:BoundColumn DataField="action_datetime" HeaderText="Action Timestamp" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />            
                                <asp:BoundColumn DataField="notes" HeaderText="Notes" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />            
                            </Columns>                            
                    </asp:DataGrid>
                </td>
            </tr>
            <tr>
                <td align="left" class="clearCell"><asp:Label runat="server" ID="lblLastUpdate" Text="[Run Time]" /></td>
                <td class="clearCell"></td>
            </tr>

        </table>
        
    </asp:Panel>
    </div>
    </form>
</body>
</html>
