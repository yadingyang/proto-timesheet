using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Workday.Web
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["CurrentUser"] == null)
            {
                this.WelcomeMsg.Text = "You do not login, web page will redirect to login page";
                Response.Redirect("Login.aspx");
            }
            else
            {
                this.WelcomeMsg.Text = Session["CurrentUser"].ToString();
            }
        }

        protected void Logout_Click(object sender, EventArgs e)
        {
            if (Session["CurrentUser"] != null)
            {
                Session["CurrentUser"] = null;
                Session["IsAdmin"] = null;
                Session["isFirst"] =null;
                Response.Redirect("default.aspx");
            }
        }
    }
}