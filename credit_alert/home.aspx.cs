using System;
//using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;
using System.Configuration;
using System.Timers;

public partial class home : System.Web.UI.Page
{
    protected string webserver = "";

    public string ApproveLink(string onum, string alerttype, string alertdesc, string compname, string acctcode, string dateposted, string linked_credit, string linked_tech, string linked_ris)
    {
        string approvelink = "alert_approve.aspx?ordernumber=" + onum + "&alert_type=" + alerttype;
        approvelink += "&alert_desc=" + alertdesc;

        if (compname.Contains("'"))
        {
            compname = compname.Replace("'", "%27");
        }
        if (compname.Contains("#"))
        {
            compname = compname.Replace("#", "%23;");
        }

        approvelink += "&comp_name=" + compname;
        approvelink += "&account_code=" + acctcode;

        approvelink += "&linked_credit=" + linked_credit;
        approvelink += "&linked_ris=" + linked_ris;
        approvelink += "&linked_tech=" + linked_tech;

        approvelink += "&date_posted=" + dateposted;
        return approvelink;
    }


  

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (System.Configuration.ConfigurationManager.AppSettings["TEST_ENVIRONMENT"] != null & System.Configuration.ConfigurationManager.AppSettings["TEST_WEBSERVER"] != null & System.Configuration.ConfigurationManager.AppSettings["WEBSERVER"] != null)
            {
                string ENV = System.Configuration.ConfigurationManager.AppSettings["TEST_ENVIRONMENT"];
                if (ENV == "YES")
                {
                    pnlEnvironment.Visible = true;
                    webserver = System.Configuration.ConfigurationManager.AppSettings["TEST_WEBSERVER"].ToString();
                }
                else
                {
                    pnlEnvironment.Visible = false;
                    webserver = System.Configuration.ConfigurationManager.AppSettings["WEBSERVER"].ToString();
                }
            }

            pnlNoResults.Visible = false;
            pnlSearchResults.Visible = false;
            pnlAlerts.Visible = true;
            cblAlertType.Items.Clear();
            bool CriteriaSearch = false;

            string ad_account = HttpContext.Current.Request.ServerVariables["AUTH_USER"];
            ad_account = "ric.carrasquilla";
            UserAccess UA = new UserAccess();

            UA = UserAccess.LoadUserAccess(ad_account);
            UA.Approved_Access = true;
            if (UA.Approved_Access != true)
            {
                Response.Redirect("no_access.aspx");
            }

            if (UA.Clear_Credit)
            {
                cblAlertType.Items.Add(new ListItem("Credit", "Credit"));
                if (Request.QueryString["credit"] != null)
                {
                    string credit = Request.QueryString["credit"].ToString();
                    cblAlertType.Items[cblAlertType.Items.IndexOf(new ListItem("Credit", "Credit"))].Selected = true;
                    CriteriaSearch = true;
                }
                else
                {
                    cblAlertType.Items[cblAlertType.Items.IndexOf(new ListItem("Credit", "Credit"))].Selected = true;
                }
            }
            if (UA.Clear_Tech)
            {
                cblAlertType.Items.Add(new ListItem("Tech", "Tech"));
                if (Request.QueryString["tech"] != null)
                {
                    cblAlertType.Items[cblAlertType.Items.IndexOf(new ListItem("Tech", "Tech"))].Selected = true;
                    CriteriaSearch = true;
                }
                else
                {
                    cblAlertType.Items[cblAlertType.Items.IndexOf(new ListItem("Tech", "Tech"))].Selected = true;
                }

            }
            if (UA.Clear_Ris)
            {
                cblAlertType.Items.Add(new ListItem("Missing Information", "RIS"));
                if (Request.QueryString["ris"] != null)
                {
                    cblAlertType.Items[cblAlertType.Items.IndexOf(new ListItem("Missing Information", "RIS"))].Selected = true;
                    CriteriaSearch = true;
                }
                else
                {
                    cblAlertType.Items[cblAlertType.Items.IndexOf(new ListItem("Missing Information", "RIS"))].Selected = true;
                }

            }

            if (UA.Access_Alert_Log == false)
            {
                btnLog.Visible = false;
            }
            // IF Query srings present populate
            if (Request.QueryString["st"] != null)
            {
                string searchtext = Request.QueryString["st"].ToString();
                tbSearchText.Text = searchtext;
                CriteriaSearch = true;
            }
            else
            {
                tbSearchText.Text = "";
            }

            if (Request.QueryString["fd"] != null)
            {
                string qsFromDate = Request.QueryString["fd"].ToString();
                tbFromDate.Text = qsFromDate;
                CriteriaSearch = true;
            }
            else
            {
                tbFromDate.Text = DateTime.Now.ToShortDateString();
            }

            if (Request.QueryString["td"] != null)
            {
                string qsToDate = Request.QueryString["td"].ToString();
                tbToDate.Text = qsToDate;
                CriteriaSearch = true;
            }
            else
            {
                tbToDate.Text = DateTime.Now.ToShortDateString();
            }
            //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

            SearchCriteria sc = new SearchCriteria();


            sc.ResponseCode = "";
            sc.CardType = "";
            sc.FromDate = tbFromDate.Text.ToString().Trim();
            sc.ToDate = tbToDate.Text.ToString().Trim();
            sc.SearchText = tbSearchText.Text.ToString().Trim();

            if (rblSearchType.Items[1].Selected)
            {
                sc.StrBeginsWith = "%";
                sc.StrEndsWith = "%";
            }
            else if (rblSearchType.Items[2].Selected)
            {
                sc.StrEndsWith = "%";
            }
            else
            {
                sc.StrBeginsWith = "%";
            }

            if (cbCheckNumber.Checked)
                sc.CheckNumber = true;
            else
                sc.CheckNumber = false;
            if (cbCheckCompanyName.Checked)
                sc.CheckCompanyName = true;
            else
                sc.CheckCompanyName = false;
            if (cbCheckAccountCode.Checked)
                sc.CheckAccountCode = true;
            else
                sc.CheckAccountCode = false;

            int icheckalerts = 0;
            foreach (ListItem li in cblAlertType.Items)
            {
                if (li.Selected)
                {
                    if (icheckalerts > 0)
                        sc.AlertTypes += ", ";
                    sc.AlertTypes += "'" + li.Value.ToString() + "'";
                    icheckalerts += 1;
                }
            }
            DataTable dtSearchResults = SearchForAlerts();

            if (dtSearchResults.Rows.Count > 0)
            {
                int cntRows = dtSearchResults.Rows.Count;
                string earliestdate = dtSearchResults.Rows[cntRows - 1]["OHODT8"].ToString();
                earliestdate = earliestdate.Substring(4, 2).ToString() + "/" + earliestdate.Substring(6, 2).ToString() + "/" + earliestdate.Substring(0, 4).ToString();

                if (CriteriaSearch != true)
                {
                    tbFromDate.Text = earliestdate;
                }

                string rectype = "-";

                dtSearchResults.Columns.Add("Action");
                dtSearchResults.Columns.Add("OHODT8_2");
                dtSearchResults.Columns.Add("SHOWROW");

                foreach (DataRow dr in dtSearchResults.Rows)
                {
                    dr["SHOWROW"] = "1";
                    string Alert_Type = dr["Alert Type"].ToString();
                    rectype = dr["Alert Type"].ToString();
                    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
                    //~~  Can user view?
                    switch (rectype)
                    {
                        case "Credit":
                            {
                                if (!UA.Clear_Credit)
                                {
                                    dr["SHOWROW"] = "0";
                                }
                                break;
                            }
                        case "Tech":
                            {
                                if (!UA.Clear_Tech)
                                {
                                    dr["SHOWROW"] = "0";
                                }
                                break;
                            }
                        case "RIS":
                            {
                                if (!UA.Clear_Ris)
                                {
                                    dr["SHOWROW"] = "0";
                                }
                                break;
                            }
                    }

                    string onumlink = "http://" + webserver + "/apps/order_invoice_lookup/order.asp?OrderID=" + dr["OrderNumber"].ToString().Trim();
                    string clink = "http://" + webserver + "/apps/company_info/customer_info.asp?CustomerCode=" + dr["OHCSCD"].ToString().Trim();
                    string compname = dr["CMCSNM"].ToString().Trim();
                    string compcode = dr["OHCSCD"].ToString().Trim();
                    if (compcode.Contains("#"))
                    {
                        compcode = compcode.Replace("#", "%23;");
                    }


                    if (compname.Contains("'"))
                    {
                        compname = compname.Replace("'", "%27");
                    }
                    if (compname.Contains("#"))
                    {
                        compname = compname.Replace("#", "%23;");
                    }

                    string declinelink = "alert_decline.aspx?ordernumber=" + dr["OHORNU"].ToString().Trim() + "&alert_type=" + dr["Alert Type"].ToString().Trim();
                    declinelink += "&alert_desc=" + dr["Alert Description"].ToString().Trim();
                    declinelink += "&comp_name=" + compname;
                    declinelink += "&account_code=" + compcode;

                    string d = dr["OHODT8"].ToString().Trim();

                    dr["OrderNumber"] = "<a href='" + onumlink + "' target='_blank'><span style='color:blue'>" + dr["OrderNumber"].ToString().Trim() + "</span></a>";
                    d = d.Substring(4, 2).ToString() + "/" + d.Substring(6, 2).ToString() + "/" + d.Substring(0, 4).ToString();
                    dr["OHODT8_2"] = d;


                    dr["OHCSCD"] = "<a href='" + clink + "' target='_blank'><span style='color:blue'>" + dr["OHCSCD"].ToString().Trim() + "</span></a>";
                    declinelink += "&date_posted=" + dr["OHODT8_2"].ToString().Trim().ToLower();

                    string CR = "N";
                    string T = "N";
                    string RIS = "N";

                    switch (rectype)
                    {
                        case "Credit":
                            {
                                CR = "Y";

                                DataRow[] drTCheck = dtSearchResults.Select("OHORNU = '" + dr["OHORNU"].ToString() + "' AND [Alert Type] = 'Tech'");
                                if (drTCheck.Length == 1)
                                { T = "Y"; }

                                DataRow[] drRISCheck = dtSearchResults.Select("OHORNU = '" + dr["OHORNU"].ToString() + "' AND [Alert Type] = 'RIS'");
                                if (drRISCheck.Length == 1)
                                {
                                    RIS = "Y";
                                }


                                break;
                            }
                        case "Tech":
                            {
                                T = "Y";

                                DataRow[] drTCheck = dtSearchResults.Select("OHORNU = '" + dr["OHORNU"].ToString() + "' AND [Alert Type] = 'Credit'");
                                if (drTCheck.Length == 1)
                                {
                                    CR = "Y";
                                }

                                DataRow[] drRISCheck = dtSearchResults.Select("OHORNU = '" + dr["OHORNU"].ToString() + "' AND [Alert Type] = 'RIS'");
                                if (drRISCheck.Length == 1)
                                {
                                    RIS = "Y";
                                }
                                break;
                            }
                        case "RIS":
                            {
                                RIS = "Y";
                                CR = "Y";

                                DataRow[] drTCheck = dtSearchResults.Select("OHORNU = '" + dr["OHORNU"].ToString() + "' AND [Alert Type] = 'Tech'");
                                if (drTCheck.Length == 1)
                                {
                                    T = "Y";
                                }

                                break;
                            }
                    }


                    string approvallink = ApproveLink(dr["OHORNU"].ToString().Trim(), dr["Alert Type"].ToString().Trim(), dr["Alert Description"].ToString().Trim(), dr["CMCSNM"].ToString().Trim(), compcode, dr["OHODT8_2"].ToString().Trim().ToLower(), CR, T, RIS);
                    dr["Action"] = "<a href='" + approvallink + "' ><span style='color:blue'>Approve</span></a> | <a href='" + declinelink + "' ><span style='color:blue'>Decline</span></a>";

                    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

                    //~~  Final Processing and show datatable
                    DataView dview = new DataView(dtSearchResults);
                    dview.RowFilter = "SHOWROW = '1'";

                    lblCount.Text = "Alert Count: " + dview.Count.ToString();
                    lblLastUpdate.Text = "List Last Updated: " + DateTime.Now.ToString();

                    dgCreditTechAlert.DataSource = dview;
                    dgCreditTechAlert.DataBind();
       
                    refreshTag.Content = "180; url=home.aspx?" + CreateMetaTagRefreshString();

                }
            }
            else
            {
                pnlNoResults.Visible = true;
                pnlSearchResults.Visible = false;
                pnlAlerts.Visible = false;
            }
        }

    }

    protected void btnLog_Click(object sender, EventArgs e)
    {
        Response.Redirect("alert_log.aspx");
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (System.Configuration.ConfigurationManager.AppSettings["TEST_ENVIRONMENT"] != null & System.Configuration.ConfigurationManager.AppSettings["TEST_WEBSERVER"] != null & System.Configuration.ConfigurationManager.AppSettings["WEBSERVER"] != null)
        {
            string ENV = System.Configuration.ConfigurationManager.AppSettings["TEST_ENVIRONMENT"];
            if (ENV == "YES")
            {
                pnlEnvironment.Visible = true;
                webserver = System.Configuration.ConfigurationManager.AppSettings["TEST_WEBSERVER"].ToString();
            }
            else
            {
                pnlEnvironment.Visible = false;
                webserver = System.Configuration.ConfigurationManager.AppSettings["WEBSERVER"].ToString();
            }
        }

        string ad_account = HttpContext.Current.Request.ServerVariables["AUTH_USER"];

        UserAccess UA = new UserAccess();
        UA = UserAccess.LoadUserAccess(ad_account);
        UA.Approved_Access = true;
        if (UA.Approved_Access != true)
        {
            Response.Redirect("no_access.aspx");
        }

        SearchCriteria sc = new SearchCriteria();
        sc.ResponseCode = "";
        sc.CardType = "";
        sc.FromDate = tbFromDate.Text.ToString().Trim();
        sc.ToDate = tbToDate.Text.ToString().Trim();
        sc.SearchText = tbSearchText.Text.ToString().Trim();

        if (rblSearchType.Items[1].Selected)
        {
            sc.StrBeginsWith = "%";
            sc.StrEndsWith = "%";
        }
        else if (rblSearchType.Items[2].Selected)
        {
            sc.StrEndsWith = "%";
        }
        else
        {
            sc.StrBeginsWith = "%";
        }

        if (cbCheckNumber.Checked)
            sc.CheckNumber = true;
        else
            sc.CheckNumber = false;
        if (cbCheckCompanyName.Checked)
            sc.CheckCompanyName = true;
        else
            sc.CheckCompanyName = false;
        if (cbCheckAccountCode.Checked)
            sc.CheckAccountCode = true;
        else
            sc.CheckAccountCode = false;

        int icheckalerts = 0;
        foreach (ListItem li in cblAlertType.Items)
        {
            if (li.Selected)
            {
                if (icheckalerts > 0)
                    sc.AlertTypes += ", ";
                sc.AlertTypes += "'" + li.Value.ToString() + "'";
                icheckalerts += 1;
            }
        }

        DataTable dtSearchResults = SearchForAlerts(sc);

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        if (dtSearchResults.Rows.Count > 0)
        {
            int cntRows = dtSearchResults.Rows.Count;
            string earliestdate = dtSearchResults.Rows[cntRows - 1]["OHODT8"].ToString();
            earliestdate = earliestdate.Substring(4, 2).ToString() + "/" + earliestdate.Substring(6, 2).ToString() + "/" + earliestdate.Substring(0, 4).ToString();

           
                tbFromDate.Text = earliestdate;
            

            string rectype = "-";

            dtSearchResults.Columns.Add("Action");
            dtSearchResults.Columns.Add("OHODT8_2");
            dtSearchResults.Columns.Add("SHOWROW");

            foreach (DataRow dr in dtSearchResults.Rows)
            {
                dr["SHOWROW"] = "1";
                string Alert_Type = dr["Alert Type"].ToString();
                rectype = dr["Alert Type"].ToString();
                //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
                //~~  Can user view?
                switch (rectype)
                {
                    case "Credit":
                        {
                            if (!UA.Clear_Credit)
                            {
                                dr["SHOWROW"] = "0";
                            }
                            break;
                        }
                    case "Tech":
                        {
                            if (!UA.Clear_Tech)
                            {
                                dr["SHOWROW"] = "0";
                            }
                            break;
                        }
                    case "RIS":
                        {
                            if (!UA.Clear_Ris)
                            {
                                dr["SHOWROW"] = "0";
                            }
                            break;
                        }
                }

                //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
                //~~  Is order Warehouse - Exclude
                if (dr["SHOWROW"].ToString() == "1")
                {
                    DataTable drTransCount = Data.IsThisOrderTransfer(dr["OHORNU"].ToString());
                    if (int.Parse(drTransCount.Rows[0]["TransCount"].ToString()) > 0)
                    {
                        dr["SHOWROW"] = "0";
                    }
                }

                //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
                //~~  Do order contain any neg lines - Exclude
                if (dr["SHOWROW"].ToString() == "1")
                {
                    bool AllPositiveLines = true;
                    DataTable drOrderLineQty = Data.OrderDetails_Quantity(dr["OHORNU"].ToString());
                    foreach (DataRow drLineQty in drOrderLineQty.Rows)
                    {
                        int qty = int.Parse(drLineQty["ODQTOR"].ToString());
                        if (qty < 0)
                        {
                            AllPositiveLines = false;
                        }
                    }
                    if (AllPositiveLines == false)
                    {
                        dr["SHOWROW"] = "0";
                    }
                }

                string onumlink = "http://" + webserver + "/apps/order_invoice_lookup/order.asp?OrderID=" + dr["OrderNumber"].ToString().Trim();
                string clink = "http://" + webserver + "/apps/company_info/customer_info.asp?CustomerCode=" + dr["OHCSCD"].ToString().Trim();
                string compname = dr["CMCSNM"].ToString().Trim();
                string compcode = dr["OHCSCD"].ToString().Trim();

                if (compcode.Contains("#"))
                {
                    compcode = compcode.Replace("#", "%23;");
                }

                if (compname.Contains("'"))
                {
                    compname = compname.Replace("'", "%27");
                }
                if (compname.Contains("#"))
                {
                    compname = compname.Replace("#", "%23;");
                }

                string declinelink = "alert_decline.aspx?ordernumber=" + dr["OHORNU"].ToString().Trim() + "&alert_type=" + dr["Alert Type"].ToString().Trim();
                declinelink += "&alert_desc=" + dr["Alert Description"].ToString().Trim();
                declinelink += "&comp_name=" + compname;
                declinelink += "&account_code=" + dr["OHCSCD"].ToString().Trim();

                string d = dr["OHODT8"].ToString().Trim();

                dr["OrderNumber"] = "<a href='" + onumlink + "' target='_blank'><span style='color:blue'>" + dr["OrderNumber"].ToString().Trim() + "</span></a>";
                d = d.Substring(4, 2).ToString() + "/" + d.Substring(6, 2).ToString() + "/" + d.Substring(0, 4).ToString();
                dr["OHODT8_2"] = d;


                dr["OHCSCD"] = "<a href='" + clink + "' target='_blank'><span style='color:blue'>" + dr["OHCSCD"].ToString().Trim() + "</span></a>";
                declinelink += "&date_posted=" + dr["OHODT8_2"].ToString().Trim().ToLower();

                string CR = "N";
                string T = "N";
                string RIS = "N";

                switch (rectype)
                {
                    case "Credit":
                        {
                            CR = "Y";

                            DataRow[] drTCheck = dtSearchResults.Select("OHORNU = '" + dr["OHORNU"].ToString() + "' AND [Alert Type] = 'Tech'");
                            if (drTCheck.Length == 1)
                            { T = "Y"; }

                            DataRow[] drRISCheck = dtSearchResults.Select("OHORNU = '" + dr["OHORNU"].ToString() + "' AND [Alert Type] = 'RIS'");
                            if (drRISCheck.Length == 1)
                            {
                                RIS = "Y";
                            }


                            break;
                        }
                    case "Tech":
                        {
                            T = "Y";

                            DataRow[] drTCheck = dtSearchResults.Select("OHORNU = '" + dr["OHORNU"].ToString() + "' AND [Alert Type] = 'Credit'");
                            if (drTCheck.Length == 1)
                            {
                                CR = "Y";
                            }

                            DataRow[] drRISCheck = dtSearchResults.Select("OHORNU = '" + dr["OHORNU"].ToString() + "' AND [Alert Type] = 'RIS'");
                            if (drRISCheck.Length == 1)
                            {
                                RIS = "Y";
                            }
                            break;
                        }
                    case "RIS":
                        {
                            RIS = "Y";
                            CR = "Y";

                            DataRow[] drTCheck = dtSearchResults.Select("OHORNU = '" + dr["OHORNU"].ToString() + "' AND [Alert Type] = 'Tech'");
                            if (drTCheck.Length == 1)
                            {
                                T = "Y";
                            }

                            break;
                        }
                }


                string approvallink = ApproveLink(dr["OHORNU"].ToString().Trim(), dr["Alert Type"].ToString().Trim(), dr["Alert Description"].ToString().Trim(), dr["CMCSNM"].ToString().Trim(), compcode, dr["OHODT8_2"].ToString().Trim().ToLower(), CR, T, RIS);
                dr["Action"] = "<a href='" + approvallink + "' ><span style='color:blue'>Approve</span></a> | <a href='" + declinelink + "' ><span style='color:blue'>Decline</span></a>";

                //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

                //~~  Final Processing and show datatable
                DataView dview = new DataView(dtSearchResults);
                dview.RowFilter = "SHOWROW = '1'";

                lblCount.Text = "Alert Count: " + dview.Count.ToString();
                lblLastUpdate.Text = "List Last Updated: " + DateTime.Now.ToString();

                dgCreditTechAlert.DataSource = dview;
                dgCreditTechAlert.DataBind();
          
                refreshTag.Content = "180; url=home.aspx?" + CreateMetaTagRefreshString();

            }
        }
        else
        {
            pnlNoResults.Visible = true;
            pnlSearchResults.Visible = false;
            pnlAlerts.Visible = false;
        }
    }    
    public string CreateMetaTagRefreshString()
    {
        string QueryString = "";

        // Search Text
        if (tbSearchText.Text.ToString().Length > 0)
        {
            QueryString += "&st=" + tbSearchText.Text.ToString();
        }

        //Date Range
        QueryString += "&fd=" + tbFromDate.Text.ToString().Trim();
        QueryString += "&td=" + tbToDate.Text.ToString().Trim();

        //Search Type
        foreach (ListItem li in rblSearchType.Items)
        {
            if (li.Selected)
            {
                QueryString += "&" + li.Value.ToString().ToLower() + "=1";
            }
        }
        // Alert Type
        foreach (ListItem li in cblAlertType.Items)
        {
            if (li.Selected)
            {
                QueryString += "&" + li.Value.ToString().ToLower() + "=1";
            }
        }

        if (QueryString.StartsWith("&"))
        {
            QueryString = QueryString.Remove(0, 1);
        }

        return QueryString;
    }
    static public string GetCreditAlertTextByCode(string cgcode)
    {
        SqlConnection conn4 = openDB.ddb();
        conn4.Open();
        string sql4 = "SELECT CGTEXT FROM CREDTMST WHERE CGCODE = '" + cgcode + "'";
        SqlCommand cmd4 = new SqlCommand();
        cmd4.Connection = conn4;
        cmd4.CommandType = CommandType.Text;
        cmd4.CommandText = sql4;
        string CGCODE = Convert.ToString(cmd4.ExecuteScalar());
        cmd4.Dispose();
        conn4.Close();
        conn4.Dispose();
        return CGCODE;
    }
    static public string ConvertDate(string date)
    {
        string ohodt8 = "";
        string[] sdate = date.Split('/');
        if (sdate[0].Length == 1)
            sdate[0] = "0" + sdate[0];
        if (sdate[1].Length == 1)
            sdate[1] = "0" + sdate[1];
        ohodt8 = sdate[2].ToString().Trim() + sdate[0].ToString().Trim() + sdate[1].ToString().Trim();
        return ohodt8;
    }
    static public DataTable SearchForAlerts(SearchCriteria sc)
    {
        string to = ConvertDate(sc.ToDate);
        string from = ConvertDate(sc.FromDate);
        string sql = "SELECT * ";
        string whereclause = "";

        sql += "FROM vw_CreditAndTechAlert as vw ";
        sql += "where (OHODT8 >= '" + from + "') and (OHODT8 <= '" + to + "')  ";

        sql += "and ([Alert Type] IN (" + sc.AlertTypes.ToString() + ")) ";

        if (sc.SearchText != "")
        {
            whereclause = "";
            if (sc.CheckNumber)
                whereclause += "OR OHORNU LIKE '" + sc.StrEndsWith + sc.SearchText.ToString().Trim() + sc.StrBeginsWith + "' ";
            if (sc.CheckCompanyName)
                whereclause += "OR LTRIM(RTRIM(CMCSNM)) LIKE '" + sc.StrEndsWith + sc.SearchText.ToString().Trim() + sc.StrBeginsWith + "' ";
            if (sc.CheckAccountCode)
                whereclause += "OR LTRIM(RTRIM(OHCSCD)) LIKE '" + sc.StrEndsWith + sc.SearchText.ToString().Trim() + sc.StrBeginsWith + "' ";

            if (whereclause != "")
                sql += "AND ( " + whereclause.Remove(0, 3) + ") ";
        }

        sql += "ORDER BY OHoDT8 DESC, OHORNU DESC ";
        DataTable dt = SQL_Execute_DataTable(sql);
        return dt;
    }
    static public DataTable SearchForAlerts()
    {
        string sql = "SELECT * FROM vw_CreditAndTechAlert ORDER BY OHoDT8 DESC, OHORNU DESC";
        DataTable dt = SQL_Execute_DataTable(sql);
        return dt;
    }
    static public DataTable SQL_Execute_DataTable(string sql)
    {
        SqlConnection conn2 = openDB.ddb();
        SqlDataAdapter da = new SqlDataAdapter();
        SqlCommand cmd2 = new SqlCommand();
        string sql2 = sql;
        cmd2.CommandText = sql2;
        da.SelectCommand = cmd2;
        DataTable ds = new DataTable();
        cmd2.Connection = conn2;
        cmd2.CommandType = CommandType.Text;
        conn2.Open();
        da.Fill(ds);
        cmd2.Dispose();
        conn2.Close();
        conn2.Dispose();
        return ds;
    }

}

public class openDB
{
    public static SqlConnection ddb()
    {
        string connectionString = ConfigurationManager.ConnectionStrings["AGProd"].ConnectionString;
        SqlConnection conn = new SqlConnection(connectionString);
        return conn;
    }
}


