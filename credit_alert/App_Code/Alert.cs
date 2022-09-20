using System;
using System.Collections.Generic;
using System.Web;
using System.Data.SqlClient;
using System.Data;
//using Generic;
using System.Configuration;

/// <summary>
/// Summary description for Alert
/// </summary>
public class Alert
{        
    private string _ordernumber = "";
    private string _dateposted = "";
    private string _CompanyName = "";
    private string _AcctCode = "";
    private string _alerttype = "";
    private string _alertdesc = "";
    private string _notes = "";
    private string _ad_account = "";
    private string _action = "";
    private string _action_datetime = "";

    public string Action_Datetime
    {
        get
        {
            return _action_datetime;
        }
        set
        {
            _action_datetime = value;
        }
    }
    public string Action
    {
        get
        {
            return _action;
        }
        set
        {
            _action = value;
        }
    }
    public string Ad_Account
    {
        get
        {
            return _ad_account;
        }
        set
        {
            _ad_account = value;
        }
    }
    public string Notes
    {
        get
        {
            return _notes;
        }
        set
        {
            _notes = value;
        }
    }
    public string AlertDesc
    {
        get
        {
            return _alertdesc;
        }
        set
        {
            _alertdesc = value;
        }
    }
    public string AlertType
    {
        get
        {
            return _alerttype;
        }
        set
        {
            _alerttype = value;
        }
    }
    public string AcctCode
    {
        get
        {
            return _AcctCode;
        }
        set
        {
            _AcctCode = value;
        }
    }
    public string CompanyName
    {
        get
        {
            return _CompanyName;
        }
        set
        {
            _CompanyName = value;
        }
    }
    public string DatePosted
    {
        get
        {
            return _dateposted;
        }
        set
        {
            _dateposted = value;
        }
    }    
    public string OrderNumber
    {
        get
        {
            return _ordernumber;
        }
        set
        {
            _ordernumber = value;
        }
    }

    public static void Update_ALERTORD_Record(Alert a)
    {
        string sql = "Update ALERTORD ";
        sql += "SET AlTCOMPLT='Yes', ALTACTION='" + a.Action + "', ALTCLRBY='" + a.Ad_Account + "' ";
        sql += "Where ALTORNU = '" + a.OrderNumber + "' and ALTTYPE='" + a.AlertType + "' ";

        Data.SQL_Execute_Update(sql);
    }
    public static void Insert_Alert_Action_Log_Record(Alert a)
    {
        string sqlcols = "(ordernumber, dateposted, companyname, accountcode, alert_type, alert_desc, ad_account, action, action_datetime, notes)";
        string sqlvals = "('" + a.OrderNumber + "', '" + a.DatePosted + "', '" + a.CompanyName + "', '" + a.AcctCode + "', '" + a.AlertType + "', '" + a.AlertDesc + "', '" + a.Ad_Account + "', '" + a.Action + "', '" + a.Action_Datetime + "', '" + a.Notes + "')";
        Data.sql_AlertAction_Insert(sqlcols, sqlvals);
    }
    public static void Insert_Alert_Note_Record(Alert a)
    {
        string onum = a.OrderNumber.Substring(0, 6);
        string sqlcols = "(NoteTypeID, NoteDate, NoteAccountType, NoteAccountCode, NoteAuthor, NoteText, NoteStanding, NoteOrderNumber)";
        string sqlvals = "('100', '" + DateTime.Now.ToShortDateString() + "', 'C', '" + a.AcctCode + "', '*4', 'Alert System Note: " + a.AlertDesc + " - " + a.Notes + "', 'False', '" + onum + "')";
        Data.sql_AlertNote_Insert(sqlcols, sqlvals);
    }
    public static SqlConnection ddb()
    {
        string connectionString = ConfigurationManager.ConnectionStrings["AGProd"].ConnectionString;
        SqlConnection conn = new SqlConnection(connectionString);
        return conn;
    }
    public static Alert Populate_Alert(string ordernum, string dateposted, string companyname, string acctcode, string alerttype, string alertdesc, string notes, string adaccount, string action, string actiondatetime)
    {
        Alert alert = new Alert();

        alert.OrderNumber = ordernum;
        alert.DatePosted = dateposted;
        alert.CompanyName = companyname;
        alert.AcctCode = acctcode;
        alert.AlertType = alerttype;
        alert.AlertDesc = alertdesc;
        alert.Notes = notes;
        alert.Ad_Account = adaccount;
        alert.Action = action;
        alert.Action_Datetime = actiondatetime;

        return alert;
    }
}