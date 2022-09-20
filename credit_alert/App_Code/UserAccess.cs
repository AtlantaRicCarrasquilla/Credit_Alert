using System;
using System.Collections.Generic;
using System.Web;
using System.Data.SqlClient;
using System.Data;


/// <summary>
/// Summary description for UserAccess
/// </summary>
public class UserAccess
{
    private string _Ad_Account = "";
    private bool _Clear_Tech = false;
    private bool _Clear_Credit = false;
    private bool _Clear_Ris = false;
    private bool _Access_Alert_Log = false;
    private bool _Approved_Access = false;


    public bool Access_Alert_Log
    {
        get
        {
            return _Access_Alert_Log;
        }
        set
        {
            _Access_Alert_Log = value;
        }
    }
    public bool Clear_Ris
    {
        get
        {
            return _Clear_Ris;
        }
        set
        {
            _Clear_Ris = value;
        }
    }
    public bool Clear_Credit
    {
        get
        {
            return _Clear_Credit;
        }
        set
        {
            _Clear_Credit = value;
        }
    }
    public bool Clear_Tech
    {
        get
        {
            return _Clear_Tech;
        }
        set
        {
            _Clear_Tech = value;
        }
    }
    public string Ad_Account
    {
        get
        {
            return _Ad_Account;
        }
        set
        {
            _Ad_Account = value;
        }
    }
    public bool Approved_Access
    {
        get
        {
            return _Approved_Access;
        }
        set
        {
            _Approved_Access = value;
        }
    }


    static public UserAccess LoadUserAccess(string ad_account)
    {
        DataTable dtAlertUserAccess = Data.GetAlerUserAcces_ByAdAccount(ad_account);
        UserAccess ua = new UserAccess();

        if (dtAlertUserAccess.Rows.Count == 0)
        {
            ua.Ad_Account = "";
            ua.Clear_Tech = false;
            ua.Clear_Credit = false;
            ua.Access_Alert_Log = false;
            ua.Clear_Ris = false;
            ua.Approved_Access = false;
        }
        else if (dtAlertUserAccess.Rows.Count == 1)
        {
            ua.Ad_Account = dtAlertUserAccess.Rows[0]["ad_account"].ToString();
            ua.Clear_Tech = bool.Parse(dtAlertUserAccess.Rows[0]["clear_tech"].ToString());
            ua.Clear_Credit = bool.Parse(dtAlertUserAccess.Rows[0]["clear_credit"].ToString());
            ua.Access_Alert_Log = bool.Parse(dtAlertUserAccess.Rows[0]["access_alert_log"].ToString());
            ua.Clear_Ris = bool.Parse(dtAlertUserAccess.Rows[0]["clear_ris"].ToString());
            ua.Approved_Access = true;
        }

        return ua;
    }
	
}