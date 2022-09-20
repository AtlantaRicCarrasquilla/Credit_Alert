using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Text;

public partial class alert_log : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (System.Configuration.ConfigurationManager.AppSettings["TEST_ENVIRONMENT"] != null)
            {
                string ENV = System.Configuration.ConfigurationManager.AppSettings["TEST_ENVIRONMENT"];
                if (ENV == "YES")
                {
                    pnlEnvironment.Visible = true;
                }
                else
                {
                    pnlEnvironment.Visible = false;
                }
            }

            string ad_account = HttpContext.Current.Request.ServerVariables["AUTH_USER"];
            UserAccess UA = new UserAccess();

            UA = UserAccess.LoadUserAccess(ad_account);
            if (UA.Approved_Access != true)
            {
                Response.Redirect("no_access.aspx");
            }
            
            tbFromDate.Text = DateTime.Now.ToShortDateString();
            tbToDate.Text = DateTime.Now.ToShortDateString();

            SearchCriteria sc = new SearchCriteria();
            sc.FromDate = tbFromDate.Text.ToString().Trim();
            sc.ToDate = tbToDate.Text.ToString().Trim();

            //DataTable dtSearchResults = Data.GetActionLog_ByDateRange(sc);
            DataTable dtSearchResults = Data.GetActionLog();
            int rowcnt = dtSearchResults.Rows.Count - 1;

            tbFromDate.Text = DateTime.Parse( dtSearchResults.Rows[rowcnt]["dateposted"].ToString()).ToShortDateString();

            

            lblCount.Text = "Alert Count: " + dtSearchResults.Rows.Count.ToString();
            lblLastUpdate.Text = "List Last Updated: " + DateTime.Now.ToString();

            dgCreditTechAlert.DataSource = dtSearchResults;
            dgCreditTechAlert.DataBind();
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {

        
        SearchCriteria sc = new SearchCriteria();
        sc.FromDate = tbFromDate.Text.ToString().Trim();
        sc.ToDate = tbToDate.Text.ToString().Trim();

        DataTable dtSearchResults = Data.GetActionLog_ByDateRange(sc);

        lblCount.Text = "Alert Count: " + dtSearchResults.Rows.Count.ToString();
        lblLastUpdate.Text = "List Last Updated: " + DateTime.Now.ToString();

        dgCreditTechAlert.DataSource = dtSearchResults;
        dgCreditTechAlert.DataBind();
    }
    protected void btnReturnToAlerts_Click(object sender, EventArgs e)
    {
        Response.Redirect("home.aspx");
    }
    protected void ibtnExcel_Click(object sender, EventArgs e)
    {
        SearchCriteria sc = new SearchCriteria();
        sc.FromDate = tbFromDate.Text.ToString().Trim();
        sc.ToDate = tbToDate.Text.ToString().Trim();

        DataTable dtSearchResults = Data.GetActionLog_ByDateRange(sc);

        lblCount.Text = "Alert Count: " + dtSearchResults.Rows.Count.ToString();
        lblLastUpdate.Text = "List Last Updated: " + DateTime.Now.ToString();

        DataGrid dgExcelExport = new DataGrid();
        dgExcelExport.AutoGenerateColumns = true;

        

        dgExcelExport.DataSource = dtSearchResults;
        dgExcelExport.DataBind();
        
        string OutputFileDirectory = System.Configuration.ConfigurationManager.AppSettings["Output_File_Directory"];
        if (!Directory.Exists(OutputFileDirectory))
        {
            Directory.CreateDirectory(OutputFileDirectory);
        }        

        string filedate = DateTime.Now.ToShortDateString();
        filedate = filedate.Replace("/", "");

        using (StreamWriter sw = new StreamWriter(@"" + OutputFileDirectory + "Alert_Log_" + filedate + ".xls"))
        {
            using (HtmlTextWriter hw = new HtmlTextWriter(sw))
            {
                dgExcelExport.RenderControl(hw);
            }
        }
        System.Diagnostics.Process.Start(@"" + OutputFileDirectory + "Alert_Log_" + filedate + ".xls");


    }    
}