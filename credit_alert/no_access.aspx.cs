using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class no_access : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (HttpContext.Current.Request.ServerVariables["TEST_ENVIRONMENT"] != null)
        {
            string ENV = HttpContext.Current.Request.ServerVariables["TEST_ENVIRONMENT"];
            if (ENV == "YES")
            {
                pnlEnvironment.Visible = true;
            }
            else
            {
                pnlEnvironment.Visible = false;
            }
        }
    }
}