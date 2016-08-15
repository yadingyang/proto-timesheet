using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Workday.Business;
using Workday.Common;
using System.Security.Cryptography;

namespace Workday.Web
{
    public partial class Admin2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
            if (!this.IsPostBack)
            {

                string uid = Request.QueryString["userid"];
                string ustatus = Request.QueryString["currentstatus"];
                if (uid != null & uid != "" & ustatus != "" & ustatus != "")
                {
                    int id = Convert.ToInt32(uid);
                    int status = Convert.ToInt32(ustatus);
                    bool ifupdate = UserBusiness.ChangeUserStatus(id, status);
                    Response.Redirect("admin2.aspx");
                }
                    Common.AllUser Allusers = new Common.AllUser();
                    Allusers = UserBusiness.GetAllUsers(Allusers);
                    AllUserGridView.DataSource = Allusers.AllUserSet;
                    AllUserGridView.DataBind();

            }
        }

        public static string LinkText(int value)
        {
            if (value == 0)
            {
                return "Disable";
            }
            else
            {
                return "Enable";

            }
        }

        public static string ButtonText(int value)
        {
            if (value == 0)
            {
                return "Disable";
            }
            else
            {
                return "Enable";

            }
        }

        public static string GetStatus(int value)
        {
            if (value == 0)
            {
                return "Normal";
            }
            else
            {
                return "Disabled";

            }
        }

        //protected void Button1_Click(object sender, EventArgs e)
        //{
        //    if(TextBox1.Text!=null & TextBox1.Text != "")
        //    {
        //        var salt = System.Text.Encoding.UTF8.GetBytes("qiuxt");
        //        var password = System.Text.Encoding.UTF8.GetBytes(TextBox1.Text);
        //        var hmacSHA1 = new HMACSHA1(salt);
        //        var saltedHash = hmacSHA1.ComputeHash(password);
        //        string result = Convert.ToBase64String(saltedHash);
        //        Label1.Text = result;
               
        //    }
        //}

        protected void Button2_Click(object sender, EventArgs e)
        {

        }

        protected void PageIndexChanged(object sender, EventArgs e)
        {

        }

        protected void PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            AllUserGridView.PageIndex = e.NewPageIndex;

            Common.AllUser Allusers = new Common.AllUser();
            Allusers = UserBusiness.GetAllUsers(Allusers);
            AllUserGridView.DataSource = Allusers.AllUserSet;
            AllUserGridView.DataBind();
        }
    }
}