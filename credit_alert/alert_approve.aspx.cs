using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Xsl;
using System.Xml.XPath;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Drawing;


public partial class alert_approve : System.Web.UI.Page
{
    protected string ENV = "";
    //public string ad_account = HttpContext.Current.Request.ServerVariables["AUTH_USER"];
    //public string ad_acct = HttpContext.Current.Request.ServerVariables["AUTH_USER"].ToString();
    public string ad_account = "ric_carrasquilla";
    public string ad_acct = "ric_carrasquilla";
    public string name = "Ric Carrasquilla";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
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
            UserAccess UA = new UserAccess();

            UA = UserAccess.LoadUserAccess(ad_account);


            string ordernum = Request.QueryString["ordernumber"].ToString();
            string dateposted = Request.QueryString["date_posted"].ToString();
            string companyname = Request.QueryString["comp_name"].ToString();
            string acctcode = Request.QueryString["account_code"].ToString();
            string alert = Request.QueryString["alert_desc"].ToString();
            string alert_type = Request.QueryString["alert_type"].ToString();
            string linked_credit = Request.QueryString["linked_credit"].ToString();
            string linked_tech = Request.QueryString["linked_tech"].ToString();
            string linked_ris = Request.QueryString["linked_ris"].ToString();

            // string multi_alerts = Request.QueryString["multi"].ToString();
            // lblMultiAlert.Text = multi_alerts;

            lblOrdNum.Text = ordernum;
            lblDatePosted.Text = dateposted;
            lblCompName.Text = companyname;
            lblAcctCode.Text = acctcode;
            tbAlert.Text = alert;

            btnApprove.Visible = true;
            bool PreReqsMet_C = false;
            bool PreReqsMet_T = false;
            string msg_c = "";
            string msg_t = "";
            string msg_r = "";



            if (alert_type == "RIS")
            {
                //Check to see if CU Code is blank on CUSTMST
                msg_r = "Pending this Approval";

                if (linked_credit == "Y")
                {
                    DataTable dtCreditAlert = Data.Get_CreditAlertForOrder(ordernum);
                    if (dtCreditAlert.Rows.Count > 0)
                    {
                        msg_c = dtCreditAlert.Rows[0]["notes"].ToString().Trim(); ;
                        PreReqsMet_C = true;
                    }
                    else
                    {
                        msg_c = "Pending Credit Alert";
                        PreReqsMet_C = false;
                    }
                }
                else
                {
                    lblOther_C.Visible = false;
                    hfMultiDontPrint.Value = "False";
                }
                if (linked_tech == "Y")
                {
                    DataTable dtTechAlert = Data.Get_TechAlertForOrder(ordernum);
                    if (dtTechAlert.Rows.Count > 0)
                    {
                        msg_t = dtTechAlert.Rows[0]["notes"].ToString().Trim();
                        PreReqsMet_T = true;
                    }
                    else
                    {
                        msg_t = "Pending Tech Alert";
                        PreReqsMet_T = false;
                    }
                }
                else
                {
                    lblOther_T.Visible = false;
                    PreReqsMet_T = true;
                    hfMultiDontPrint.Value = "False";

                }
                
            }
            else if (alert_type == "Credit")
            {
                msg_c = "Pending This Approval";
                if (linked_tech == "Y")
                {
                    DataTable dtTechAlert = Data.Get_TechAlertForOrder(ordernum);
                    if (dtTechAlert.Rows.Count > 0)
                    {
                        msg_t = dtTechAlert.Rows[0]["notes"].ToString().Trim();
                        PreReqsMet_T = true;
                        hfMultiDontPrint.Value = "False";
                    }
                    else
                    {
                        msg_t = "Pending Tech Alert";
                        PreReqsMet_T = true;
                        hfMultiDontPrint.Value = "True";
                    }
                    
                }
                else
                {
                    if (hfMultiDontPrint.Value != "True")
                    {
                        hfMultiDontPrint.Value = "False";
                    }
                    PreReqsMet_T = true;
                    lblOther_T.Visible = false;                    
                }
                
                if (linked_ris == "Y")
                {
                    DataTable dtRISAlert = Data.Get_RISAlertForOrder(ordernum);
                    if (dtRISAlert.Rows.Count > 0)
                    {
                        msg_r = dtRISAlert.Rows[0]["notes"].ToString().Trim();
                        PreReqsMet_C = true;
                        hfMultiDontPrint.Value = "False";
                    }
                    else
                    {
                        msg_r = "Pending RIS Alert";
                        PreReqsMet_C = true;
                        hfMultiDontPrint.Value = "True";
                    }
                    
                }
                else
                {
                    PreReqsMet_C = true;
                    lblOther_R.Visible = false;
                    if (hfMultiDontPrint.Value != "True")
                    {
                        hfMultiDontPrint.Value = "False";
                    }
                }
            }
            else if (alert_type == "Tech")
            {

                msg_t = "Pending This Approval";
                PreReqsMet_T = true;
                PreReqsMet_C = true;

                if (linked_credit == "Y")
                {
                    DataTable dtCreditAlert = Data.Get_CreditAlertForOrder(ordernum);
                    if (dtCreditAlert.Rows.Count > 0)
                    {
                        msg_c = dtCreditAlert.Rows[0]["notes"].ToString().Trim();
                        hfMultiDontPrint.Value = "False";
                    }
                    else
                    {
                        msg_c = "Pending Credit Alert";
                        hfMultiDontPrint.Value = "True";

                    }
                }
                else
                {
                    PreReqsMet_C = true;
                    lblOther_C.Visible = false;
                    if (hfMultiDontPrint.Value != "True")
                    {
                        hfMultiDontPrint.Value = "False";
                    }
                }

                if (linked_ris == "Y")
                {
                    DataTable dtRISAlert = Data.Get_RISAlertForOrder(ordernum);
                    if (dtRISAlert.Rows.Count > 0)
                    {
                        msg_r = dtRISAlert.Rows[0]["notes"].ToString().Trim();
                    }
                    else
                    {
                        msg_r = "Pending RIS Alert";
                        hfMultiDontPrint.Value = "True";
                    }
                }
                else
                {
                    lblOther_R.Visible = false;
                    if (hfMultiDontPrint.Value != "True")
                    {
                        hfMultiDontPrint.Value = "False";
                    }
                }

            }
            else
            {
                PreReqsMet_C = true;
                PreReqsMet_T = true;
                lblOtherAlerts.Visible = false;
                lblOther_C.Visible = false;
                lblOtherCredit.Visible = false;
              //  btnOtherCredit.Visible = false;
                lblOther_T.Visible = false;
                lblOtherTech.Visible = false;
              //  btnOtherTech.Visible = false;
            }

            lblOtherCredit.Text = msg_c;
            lblOtherTech.Text = msg_t;
            lblOtherRIS.Text = msg_r;

            if (PreReqsMet_C == true && PreReqsMet_T == true)
            {
                btnApprove.Enabled = true;
            }
            else
            {
                btnApprove.Enabled = false;
            }

            //string[] split_ad_acct = ad_acct.Split('\\');
            //string name = split_ad_acct[1].Replace('_', ' ');
            tbNotes.Text = "Approved - By " + name + " - " + DateTime.Now.ToString().Trim();

            pnlInfo.Visible = true;
            pnlConfirm.Visible = false;
            btnConfirmHome.Visible = false;
        }
    }

    protected void btnOtherCredit_Click(object sender, EventArgs e)
    {
        //string ad_acct = HttpContext.Current.Request.ServerVariables["AUTH_USER"].ToString();
        string onum = lblOrdNum.Text;
        string dateposted = lblDatePosted.Text;
        string compname = lblCompName.Text;
        if (compname.Contains("'"))
        {
            compname = compname.Replace("'", "''");
        }
        string acctcode = lblAcctCode.Text;
        string alerttype = "Credit";
        string alertdesc = tbAlert.Text;
        string notes = tbNotes.Text;        
        string action = "approve";
        string action_datetime = DateTime.Now.ToString();
        string PrintOrder = System.Configuration.ConfigurationManager.AppSettings["Print_Orders"];
        string InsertNote = System.Configuration.ConfigurationManager.AppSettings["Add_Note"];
        Alert alert = Alert.Populate_Alert(onum, dateposted, compname, acctcode, alerttype, alertdesc, notes, ad_acct, action, action_datetime);
        Alert.Insert_Alert_Action_Log_Record(alert);

        if (InsertNote == "Yes")
        {
            Alert.Insert_Alert_Note_Record(alert);            
        }

        Response.Redirect(Page.Request.Url.ToString(), true);
      
    }
    protected void btnOtherTech_Click(object sender, EventArgs e)
    {
        string ad_acct = HttpContext.Current.Request.ServerVariables["AUTH_USER"].ToString();
        string onum = lblOrdNum.Text;
        string dateposted = lblDatePosted.Text;
        string compname = lblCompName.Text;
        if (compname.Contains("'"))
        {
            compname = compname.Replace("'", "''");
        }
        string acctcode = lblAcctCode.Text;
        string alerttype = "Tech";
        string alertdesc = tbAlert.Text;
        string notes = tbNotes.Text;
        string action = "approve";
        string action_datetime = DateTime.Now.ToString();
        string PrintOrder = System.Configuration.ConfigurationManager.AppSettings["Print_Orders"];
        string InsertNote = System.Configuration.ConfigurationManager.AppSettings["Add_Note"];
        Alert alert = Alert.Populate_Alert(onum, dateposted, compname, acctcode, alerttype, alertdesc, notes, ad_acct, action, action_datetime);
        Alert.Insert_Alert_Action_Log_Record(alert);

        if (InsertNote == "Yes")
        {
            Alert.Insert_Alert_Note_Record(alert);
        }

        Response.Redirect(Page.Request.Url.ToString(), true);
    }


    protected void btnApprove_Click(object sender, EventArgs e)
    {
       

        btnApprove.Enabled = false;
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
        //string ad_acct = HttpContext.Current.Request.ServerVariables["AUTH_USER"].ToString();
        string action = "approve";
        string action_datetime = DateTime.Now.ToString();
        string PrintOrder = System.Configuration.ConfigurationManager.AppSettings["Print_Orders"];
        string InsertNote = System.Configuration.ConfigurationManager.AppSettings["Add_Note"];
        string msg = "Order Approved, ";
        try
        {
            if (cbPrintByPass.Checked)
            {
                notes += " - Print Overide Checked, Order Not Printed";
            }
            Alert alert = Alert.Populate_Alert(onum, dateposted, compname, acctcode, alerttype, alertdesc, notes, ad_acct, action, action_datetime);

            Alert.Update_ALERTORD_Record(alert);
            
            Alert.Insert_Alert_Action_Log_Record(alert);

           

            if (InsertNote == "Yes")
            {
                
                Alert.Insert_Alert_Note_Record(alert);
                msg += "Noted, ";
            }
            if (PrintOrder == "Yes")
            {

                if (hfMultiDontPrint.Value == "False")
                {
                    if (cbPrintByPass.Checked == false)
                    {
                        //TODO:  Replace this printing call with a call to the new print piece
                        PrinterSettings PS = new PrinterSettings();
                        string CurrentLocalDefaultPrinter = PS.PrinterName;

                        string newid = Data.PP_InsertPrintLogEntry(onum, "NY-Orders", ad_acct, @"CreditAlerts\alert_approve.aspx.cs");

                        string printprocessingurl = System.Configuration.ConfigurationManager.AppSettings["PrintProcessingURL"];

                        string lnk = printprocessingurl + "?OrderNumber=" + onum + "&prefix=OR" + "&printerName=PP&printprocess=Shipping&reportName=ApprovedOrder&id=" + newid;



                        Response.Write("<script>");
                        Response.Write("window.open('" + lnk + "','_blank')");
                        Response.Write("</script>");

 
                      //  ucPringing.Print(onum);
                        msg += "Printed. ";


                    }
                    else
                    {
                        msg += "Override Checked, Did Not Print. ";
                    }
                }
                else
                {
                    msg += "Did not Print due to multiple alerts. ";
                }
            }
            //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
            //~~ Check to see if need to send afer hours email.
            //~~ Send after hours during week and on weekends
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
            //  Saturday & Sunday - Send Email
            if ( DateTime.Now.DayOfWeek.ToString() == "Saturday" || DateTime.Now.DayOfWeek.ToString() == "Sunday")
            {
                Data d = new Data();
                d.SendEmail(ToEmail, profilename, subject, body);
                pnlShipAlertEmail.Visible = true;
            }
            //  Friday After 6:00 Pm & Before 6:00 AM - Send Email
            else if (DateTime.Now.DayOfWeek.ToString() == "Friday")
            {
                if ((DateTime.Parse(action_datetime) >= DateTime.Parse(NonBuisnessHour_Start)) || (DateTime.Parse(action_datetime) <= DateTime.Parse(NonBuisnessHour_END)))
                {
                    Data d = new Data();
                    d.SendEmail(ToEmail, profilename, subject, body);
                    pnlShipAlertEmail.Visible = true;
                }                
            }
            //  Monday Before 6:00 AM & After 6:00 PM
            else if (DateTime.Now.DayOfWeek.ToString() == "Monday")
            {
                if ((DateTime.Parse(action_datetime) >= DateTime.Parse(NonBuisnessHour_Start)) || (DateTime.Parse(action_datetime) <= DateTime.Parse(NonBuisnessHour_END)))
                {
                    Data d = new Data();
                    d.SendEmail(ToEmail, profilename, subject, body);
                    pnlShipAlertEmail.Visible = true;
                }
            }
            else
            {
                pnlShipAlertEmail.Visible = false;
            }

            pnlInfo.Visible = true;
            lblConfirmString.Text = msg;
            pnlConfirm.Visible = true;            
            btnApprove.Visible = false;
            btnCancel.Visible = false;
            btnConfirmHome.Visible = true;
        }
        catch(Exception ex)
        {
            Response.Write(ex.Message);
        }        
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("home.aspx");
    }

    protected void btnConfirmHome_Click(object sender, EventArgs e)
    {       
        Response.Redirect("home.aspx");      
    }
}