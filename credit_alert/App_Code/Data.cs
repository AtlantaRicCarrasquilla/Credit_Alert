using System;
using System.Collections.Generic;
using System.Web;
using System.Data.SqlClient;
using System.Data;
//using Generic;
using System.Configuration;

/// <summary>
/// Summary description for Data
/// </summary>
public class Data
{

    static public string PP_InsertPrintLogEntry(string ordernumber, string printername, string username, string source)
    {
        SqlConnection conn1 = openDB.ddb();
        conn1.Open();
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn1;
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "PP_InsertPrintLogEntry";

        // cmd.Parameters.Add("@ChildBC", SqlDbType.VarChar).Value = inFromSerial;
        // cmd.Parameters.Add("@ParentBC", SqlDbType.VarChar).Value = inFromBarcode;


        //Output Parameters
        SqlParameter inParm = new SqlParameter("@rptFolder", SqlDbType.VarChar);
        inParm.Value = "Refron Reports";
        inParm.Direction = ParameterDirection.Input;
        cmd.Parameters.Add(inParm);

        SqlParameter inParm1 = new SqlParameter("@rptName", SqlDbType.VarChar);
        inParm1.Value = "ApprovedOrder";
        inParm1.Direction = ParameterDirection.Input;
        cmd.Parameters.Add(inParm1);

        SqlParameter inParm9 = new SqlParameter("@rptParams", SqlDbType.VarChar);
        inParm9.Value = ordernumber;
        inParm9.Direction = ParameterDirection.Input;
        cmd.Parameters.Add(inParm9);

        SqlParameter inParm8 = new SqlParameter("@printer", SqlDbType.VarChar);
        inParm8.Value = printername;
        inParm8.Direction = ParameterDirection.Input;
        cmd.Parameters.Add(inParm8);

        SqlParameter inParm7 = new SqlParameter("@fileName", SqlDbType.VarChar);
        inParm7.Value = "~";
        inParm7.Direction = ParameterDirection.Input;
        cmd.Parameters.Add(inParm7);

        SqlParameter inParm77 = new SqlParameter("@username", SqlDbType.VarChar);
        inParm77.Value = username;
        inParm77.Direction = ParameterDirection.Input;
        cmd.Parameters.Add(inParm77);

        SqlParameter inParm90 = new SqlParameter("@datetimestamp", SqlDbType.VarChar);
        inParm90.Value = "~";
        inParm90.Direction = ParameterDirection.Input;
        cmd.Parameters.Add(inParm90);

        SqlParameter inParm901 = new SqlParameter("@source", SqlDbType.VarChar);
        inParm901.Value = source;
        inParm901.Direction = ParameterDirection.Input;
        cmd.Parameters.Add(inParm901);


        SqlParameter outParm2 = new SqlParameter("@NewID", SqlDbType.VarChar);
        outParm2.Direction = ParameterDirection.Output;
        outParm2.Size = 50;
        cmd.Parameters.Add(outParm2);

        cmd.ExecuteNonQuery();
        cmd.Dispose();
        conn1.Close();
        conn1.Dispose();

        string newid = cmd.Parameters["@NewID"].Value.ToString();

        return newid;
    }


    static public DataTable AlertLogCountByAlertType(string ordernumber, string alert_type)
    {
        string sql = "SELECT COUNT(id) AS Count FROM Alert_Action_Log WHERE (ordernumber = '" + ordernumber + "') AND (alert_type = '" + alert_type + "') ";
        return SQL_Execute_DataTable(sql);
    }
    static public DataTable OrderDetailCertificationAndCustomerCertification(string ordernumber)
    {
        string sql = "SELECT * FROM vw_CreditAlert_TechCert where ODORNU = '" + ordernumber + " '";
        return SQL_Execute_DataTable(sql);
    }

    static public DataTable OrderDetails_Quantity(string ordernumber)
    {
        string sql = "SELECT ODQTOR from ordrdtl where ODORNU = '" + ordernumber + " '";
        return SQL_Execute_DataTable(sql);
    }
    static public DataTable IsThisOrderTransfer(string ordernumber)
    {
        string sql = "select count(wmwhs) as TransCount from whsemst w inner join ordrhdr o on o.ohcscd = w.wmwhs where o.ohornu = '" + ordernumber + " '";
        return SQL_Execute_DataTable(sql);
    }
    static public DataTable Get_RISAlertForOrder(string ordernumber)
    {
        string sql = "SELECT * FROM Alert_Action_Log WHERE (ordernumber = '" + ordernumber + "' and alert_type='RIS') ";
        return SQL_Execute_DataTable(sql);
    }
    static public DataTable Get_CreditAlertForOrder(string ordernumber)
    {
        string sql = "SELECT * FROM Alert_Action_Log WHERE (ordernumber = '" + ordernumber + "' and alert_type='Credit') ";
        return SQL_Execute_DataTable(sql);
    }
    static public DataTable Get_TechAlertForOrder(string ordernumber)
    {
        string sql = "SELECT * FROM Alert_Action_Log WHERE (ordernumber = '" + ordernumber + "' and alert_type='Tech') ";
        return SQL_Execute_DataTable(sql);
    }

    static public DataTable Get_CMCUCD(string cmcscd)
    {
        string sql = "SELECT CMCSCD, CMCUCD FROM custmst WHERE (CMCSCD = '" + cmcscd + "') ";
        return SQL_Execute_DataTable(sql);
    }
    public void SendEmail(string toEmails, string profilename, string subject, string body)
    {
        SqlConnection conn = openDB.ddb();
        SqlDataAdapter da = new SqlDataAdapter();
        SqlCommand cmd = new SqlCommand();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "sp_Alert_NonBuisnessEmailSend";
        SqlParameterCollection sqlParams = cmd.Parameters;
        sqlParams.AddWithValue("@userEmail", toEmails);
        sqlParams.AddWithValue("@profileName", profilename);
        sqlParams.AddWithValue("@subject", subject);
        sqlParams.AddWithValue("@body", body);
       // DataTable dt = new DataTable();
        conn.Open();
        //SqlDataReader rs; 
        cmd.Connection = conn;
        cmd.ExecuteReader();
        //rs = cmd.ExecuteReader();
      //  da.Fill(dt);
        cmd.Dispose();

        //rs.Close();
        //rs.Dispose();
        conn.Close();
        conn.Dispose();
        //return dt;
    }
    static public DataTable GetActionLog()
    {
        string sql = "SELECT Top 100 * FROM Alert_Action_Log ORDER BY action_datetime DESC ";
        return SQL_Execute_DataTable(sql);
    }
    static public DataTable GetActionLog_ByDateRange(SearchCriteria sc)
    {
        string sql = "SELECT * FROM Alert_Action_Log where (dateposted >= '" + sc.FromDate.ToString() + "') and (dateposted <= '" + sc.ToDate.ToString() + "') ORDER BY action_datetime DESC ";
        return SQL_Execute_DataTable(sql);
    }
    static public DataTable GetAlerUserAcces_ByAdAccount(string ad_acct)
    {
        string sql = "SELECT * FROM Alert_Users where ad_account = '" + ad_acct + "' ";
        return SQL_Execute_DataTable(sql);
    }
    static public DataTable GetAllPrinters()
    {
        string sql = "SELECT * FROM vw_printers ";
        return SQL_Execute_DataTable(sql);
    }
    static public DataTable GetPrintersByProcess(string printprocess)
    {
        string sql = "SELECT * FROM vw_printers where process_name = '" + printprocess + "' ";
        return SQL_Execute_DataTable(sql);
    }
    static public DataTable GetDefaultPrintersByProcess(string printprocess)
    {
        string sql = "SELECT * FROM vw_printers where process_name = '" + printprocess + "' and [default] = 'true' ";
        return SQL_Execute_DataTable(sql);
    }
    static public void sql_AlertAction_Insert(string sqlcols, string sqlvals)
    {
        string sql = "INSERT INTO ALERT_ACTION_LOG " + sqlcols + " VALUES " + sqlvals;
        SQL_Execute_Insert(sql);
    }
    static public void sql_AlertNote_Insert(string sqlcols, string sqlvals)
    {
        string sql = "INSERT INTO Notes " + sqlcols + " VALUES " + sqlvals;
        SQL_Execute_Insert(sql);
    }
    static public void Update_ORDRHDR_OrderToVoid(string ohornu)
    {
        string sql = "update ORDRHDR set OHSTAT = 'V' where OHORNU  ='" + ohornu + "'";
        SQL_Execute_Update(sql);
    }
    //static public void Update_ORDRHDR_ApproveColumns(string OHAPPR, string OHAPDT8, string OHAPUSER, string ohornu)
    //{
    //    string sql = "update ORDRHDR set OHAPPR = '" + OHAPPR + "', OHAPDT8 = '" + OHAPDT8 + "', OHAPUSER = '" + OHAPUSER + "' where OHORNU  ='" + ohornu + "'";
    //    SQL_Execute_Update(sql);
    //}
    static public void SQL_Execute_Update(string sql)
    {
        SqlConnection conn1 = openDB.ddb();
        conn1.Open();
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn1;
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = sql;
        cmd.ExecuteNonQuery();
        cmd.Dispose();
        conn1.Close();
        conn1.Dispose();
    }
    static private void SQL_Execute_Insert(string sql)
    {
        SqlConnection conn1 = openDB.ddb();
        conn1.Open();
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn1;
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = sql;
        cmd.ExecuteNonQuery();
        cmd.Dispose();
        conn1.Close();
        conn1.Dispose();
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