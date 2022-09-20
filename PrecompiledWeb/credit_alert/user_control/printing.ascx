<%@ control language="C#" autoeventwireup="true" inherits="user_controls_printing, App_Web_abxgzf0o" %>
<div><br />
    <table>
        <tr>
            <td colspan="2"><strong>Order Print Infromation</strong></td>            
        </tr>
        <tr>
            <td align="right" width="100px"><b>Printer Name:</b></td>
            <td><asp:Label runat="server" ID="lblPName" Text="[pname]" /></td>
        </tr>
        <tr>
            <td align="right" width="100px"><b>File Name:</b></td>
            <td><asp:Label runat="server" ID="lblFName" Text="[fname]" /></td>            
        </tr>       
    </table>
</div>