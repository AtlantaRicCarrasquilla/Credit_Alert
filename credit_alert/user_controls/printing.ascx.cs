using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using IRS = System.Runtime.InteropServices;

//using rsExecService = PDFBuilder.ReportExecutionService;
//using  rs2005 =  PDFBuilder.ReportingService2005;

using System.Web.Services.Protocols;
using System.IO;
using System.Drawing.Printing;
using System.Drawing;

using System.Diagnostics;





public partial class user_controls_printing : System.Web.UI.UserControl
{
    //private StreamReader streamToPrint;
    private string file = "";

    public void Output(string line, bool newline)
    {
        if (newline)
        {
            Response.Write(line + "<br />");
        }
        else
        {
            Response.Write(line);
        }
    }
    public void DiagnosticsOutput(string env, string onum, string pid, string dbs, string pre, string rn, string rf, string rp, string rpv)
    {
        Output("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~", true);
        Output("Printing Order", true);
        Output("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~", true);
        Output("<b>Environment -</b> " + env, true);
        Output("<b>Order Number -</b> " + onum, true);
        Output("<b>PrinterID -</b> " + pid, true);
        Output("<b>DBServer -</b> " + dbs, true);
        Output("<b>Prefix -</b> " + pre, true);
        Output("<b>ReportName -</b> " + rn, true);
        Output("<b>ReportFolder -</b> " + rf, true);
        Output("<b>ReportParameter -</b> " + rp, true);
        Output("<b>ReportParameterValue -</b> " + rpv, true);
        
    }

    public void Print(string ordernum)
    {
        string ENV = System.Configuration.ConfigurationManager.AppSettings["Environment"];
        string PPDB = System.Configuration.ConfigurationManager.AppSettings["Print_Process_DBServer"];
        string PPP = System.Configuration.ConfigurationManager.AppSettings["Print_Process_Prefix"];
        string PPRN = System.Configuration.ConfigurationManager.AppSettings["Print_Process_Report_Name"];
        string PPRF = System.Configuration.ConfigurationManager.AppSettings["Print_Process_ReportFolder"];
        string PPRP = System.Configuration.ConfigurationManager.AppSettings["Print_Process_ReportParameter"];


        string OrderNumber = ordernum;
     //   string PrinterID = "";
        string DBServer = PPDB;
        string Prefix = PPP;
        string ReportName = PPRN;
        string ReportFolder = PPRF;
        string ReportParameter = PPRP;
        string ReportParameterValue = OrderNumber;
    //    string DefaultPrinterName = "";

        Page.Title = ENV;
        PrinterSettings PS = new PrinterSettings();
        string CurrentLocalDefaultPrinter = PS.PrinterName;

        string printprocessingurl = System.Configuration.ConfigurationManager.AppSettings["PrintProcessingURL"];

       // string lnk = printprocessingurl + "?OrderNumber=" + OrderNumber + "&prefix=OR" + "&printerName=" + CurrentLocalDefaultPrinter + "&id=<%=NewID%>";

       // string ad_acct = HttpContext.Current.Request.ServerVariables["AUTH_USER"].ToString();

     //   string newid = Data.PP_InsertPrintLogEntry(ordernum, "NY-Orders", ad_acct);

        string lnk = printprocessingurl + "?OrderNumber=" + OrderNumber + "&prefix=OR" + "&printerName=PP&printprocess=Shipping&reportName=ApprovedOrder";
   


        Response.Write("<script>");
        Response.Write("window.open('" + lnk + "','_blank')");
        Response.Write("</script>");






       // PDFBuilder.ReportingService2005 rs2005 = new PDFBuilder.ReportingService2005();
       // PDFBuilder.ReportExecutionService rsExecService = new PDFBuilder.ReportExecutionService();
             
       // rs2005.Credentials = System.Net.CredentialCache.DefaultCredentials;
       // rsExecService.Credentials = System.Net.CredentialCache.DefaultCredentials;

       // rs2005.Url = System.Configuration.ConfigurationManager.AppSettings["rs2005"];
       // rsExecService.Url = System.Configuration.ConfigurationManager.AppSettings["rsExecService"];

       // //rs2005.Url = "http://argedsrefsql001/ReportServer/ReportService2005.asmx";
       // //rsExecService.Url = "http://argedsrefsql001/ReportServer/ReportExecution2005.asmx";
                     
       // string historyID = null;
       // string deviceInfo = null;
       // string format = "IMAGE";
       // byte[] results;
       // string encoding = string.Empty;
       // string mimType = string.Empty;
       // string extension = string.Empty;
       // PDFBuilder.Warning[] warnings = null;
       // string[] streamIDs = null;
       // string OutputFileDirectory = System.Configuration.ConfigurationManager.AppSettings["Output_File_Directory"];
       // if (!Directory.Exists(OutputFileDirectory))
       // {
       //     Directory.CreateDirectory(OutputFileDirectory);
       // }

       // string fileName = Prefix + "_" + ReportParameterValue;
       // string filePath = @"" + OutputFileDirectory  + fileName + ".jpg";
       // string _reportName = @"/" + ReportFolder + "/" + ReportName;
       // bool _forRendering = false;
       // string _historyID = null;
       // PDFBuilder.ParameterValue[] _values = null;
       // PDFBuilder.DataSourceCredentials[] _credentials = null;
       // PDFBuilder.ReportParameter[] _parameters = null;
        
       // _parameters = rs2005.GetReportParameters(_reportName, _historyID, _forRendering, _values, _credentials);

       // PDFBuilder.ExecutionInfo ei = rsExecService.LoadReport(_reportName, historyID);

       // PDFBuilder.ParameterValue[] parameters = new PDFBuilder.ParameterValue[1];

       // if (_parameters.Length > 0)
       // {
       //     parameters[0] = new PDFBuilder.ParameterValue();
       //     parameters[0].Label = ReportParameter;
       //     parameters[0].Name = ReportParameter;
       //     parameters[0].Value = ReportParameterValue;
       // }

       // rsExecService.SetExecutionParameters(parameters, "en-us");

       // results = rsExecService.Render(format, deviceInfo, out extension, out encoding, out mimType, out warnings, out streamIDs);
       //// string resultww = System.Text.Encoding.UTF8.GetString(results);
       // using (FileStream stream = File.OpenWrite(filePath))
       // {
       //     stream.Write(results, 0, results.Length);
       // }

        
       // //Get Printer to Print to
       // PrinterSettings PS = new PrinterSettings();

       // string CurrentLocalDefaultPrinter = PS.PrinterName;
       //    string ReturnDefaultPrinter = "";

       //    DataTable dtPrinters = Data.GetDefaultPrintersByProcess("Shipping");
       //    if (dtPrinters.Rows.Count > 0)
       //    {
       //        if (CurrentLocalDefaultPrinter != dtPrinters.Rows[0]["printer_name"].ToString())
       //        {
       //            DefaultPrinterName = dtPrinters.Rows[0]["printer_name"].ToString();
       //            ReturnDefaultPrinter = CurrentLocalDefaultPrinter;
       //        }
       //        else
       //        {
       //            DefaultPrinterName = CurrentLocalDefaultPrinter;
       //        }
       //    }        
       // myPrinters.SetDefaultPrinter(DefaultPrinterName);
       
       // PrintDocument pd = new PrintDocument();
       // file = filePath;
       // pd.PrintPage += PrintPage;
       // pd.Print();

       // lblPName.Text = DefaultPrinterName;
       // lblFName.Text = fileName;

       // myPrinters.SetDefaultPrinter(ReturnDefaultPrinter);  
     }

    private void PrintPage(object o, PrintPageEventArgs e)
    {
        System.Drawing.Image img = System.Drawing.Image.FromFile(file);
        Point loc = new Point(0, 0);
        e.Graphics.DrawImage(img, loc);                
    }

    protected void Page_Load(object sender, EventArgs e)
    {
    }
}
public static class myPrinters
{
    [IRS.DllImport("winspool.drv", CharSet = IRS.CharSet.Auto, SetLastError = true)]
    public static extern bool SetDefaultPrinter(string Name);
}
