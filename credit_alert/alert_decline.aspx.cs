using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class alert_decline : System.Web.UI.Page
{
    protected string ENV = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (System.Configuration.ConfigurationManager.AppSettings["TEST_ENVIRONMENT"] != null)
        {
            ENV = System.Configuration.ConfigurationManager.AppSettings["TEST_ENVIRONMENT"];
            if (ENV == "YES")
            {
                pnlEnvironment.Visible = true;
            }
            else
            {
                pnlEnvironment.Visible = false;
            }
        }

        string ordernum = Request.QueryString["ordernumber"].ToString();
        string dateposted = Request.QueryString["date_posted"].ToString();
        string companyname = Request.QueryString["comp_name"].ToString();
        string acctcode = Request.QueryString["account_code"].ToString();
        string alert = Request.QueryString["alert_desc"].ToString();

        lblOrdNum.Text = ordernum;
        lblDatePosted.Text = dateposted;
        lblCompName.Text = companyname;
        lblAcctCode.Text = acctcode;

        tbAlert.Text = alert;

        string ad_acct = HttpContext.Current.Request.ServerVariables["AUTH_USER"].ToString();
        string[] split_ad_acct = ad_acct.Split('\\');
        string name = split_ad_acct[1].Replace('_', ' ');
        tbNotes.Text = "Declined - By " + name + " - " + DateTime.Now.ToString().Trim();

        pnlInfo.Visible = true;
        pnlConfirm.Visible = false;

        btnConfirmHome.Visible = false;
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("home.aspx");
    }
    protected void btnDecline_Click(object sender, EventArgs e)
    {
        string NonBuisnessHour_Start = System.Configuration.ConfigurationManager.AppSettings["NON-BUSINESS-STARTTIME"];
        string NonBuisnessHour_END = System.Configuration.ConfigurationManager.AppSettings["NON-BUSINESS-ENDTIME"];
        string EMAILGROUPNAME = System.Configuration.ConfigurationManager.AppSettings["EMAIL_GROUP"];
        string EMAILGROUPNAME_TEST = System.Configuration.ConfigurationManager.AppSettings["EMAIL_GROUP_TEST"];

        string onum = lblOrdNum.Text;
        string dateposted = lblDatePosted.Text;
        string compname = lblCompName.Text;
        if (compname.Contains("'"))
        {
            compname = compname.Replace("'", "''");
        }
        string acctcode = lblAcctCode.Text;
        string alerttype = Request.QueryString["alert_type"].ToString();
        string alertdesc = tbAlert.Text;
        string notes = tbNotes.Text;
        string ad_acct = HttpContext.Current.Request.ServerVariables["AUTH_USER"].ToString();
        string action = "Decline";
        string action_datetime = DateTime.Now.ToString();
        string UpdateOrderToVoid = System.Configuration.ConfigurationManager.AppSettings["UpdateOrderToVoid"];

        try
        {
            Alert alert = Alert.Populate_Alert(onum, dateposted, compname, acctcode, alerttype, alertdesc, notes, ad_acct, action, action_datetime);
                 

            if (UpdateOrderToVoid == "Yes")
            {
                Data.Update_ORDRHDR_OrderToVoid(alert.OrderNumber);
            }

            Alert.Update_ALERTORD_Record(alert);
                     
            Alert.Insert_Alert_Action_Log_Record(alert);

            //Email Non-Buisness Hours Shipment Clear Alert
            if ((DateTime.Parse(action_datetime) >= DateTime.Parse(NonBuisnessHour_Start)) && (DateTime.Parse(action_datetime) <= DateTime.Parse(NonBuisnessHour_END)))
            {               
                ENV = System.Configuration.ConfigurationManager.AppSettings["TEST_ENVIRONMENT"];
                string ToEmail = "";
                string profilename = "";
                string body = "Alert was cleared during non-business hours";
                string subject = "Clear RIS Shipping Alert";
                if (ENV == "YES")
                {
                    ToEmail = EMAILGROUPNAME_TEST;
                    profilename = "generalEmail";
                }
                else
                {                    
                    ToEmail = EMAILGROUPNAME;
                    profilename = "RefronSQLMailer";
                }

                Data d = new Data();
                d.SendEmail(ToEmail, profilename, subject, body);

                pnlShipAlertEmail.Visible = true;
            }
            else
            {
                pnlShipAlertEmail.Visible = false;
            }
            
            
            //lblONum.Text = onum;
            pnlInfo.Visible = true;
            pnlConfirm.Visible = true;
            btnCancel.Visible = false;
            btnDecline.Visible = false;
            btnConfirmHome.Visible = true;
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }        
    }

    protected void btnConfirmHome_Click(object sender, EventArgs e)
    {
        Response.Redirect("home.aspx");
    }
}