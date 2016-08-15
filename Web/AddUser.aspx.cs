using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Workday.Business;

namespace Workday.Web
{
    public partial class Register : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {

            string from=null;

            if (Request.QueryString["from"] != null)
            {
                from = Request.QueryString["from"];
            }
            if (this.IsPostBack)
            {
                //提交登录
                string email = Request.Form["email"];
                string password = Request.Form["password"];
                string username = Request.Form["username"];
                
                if(email!="" & email!=null & password!="" & password!=null & username!="" & username!=null)
                {
                    if(password== "The default password:111111")
                    {
                        password = "111111";
                    }
                    Common.User newUser = new Common.User();
                    newUser.Email = email;
                    newUser.Password = password;
                    newUser.UserName = username;
                    newUser.Status = Common.UserStatus.Normal;
                    newUser.IsAdmin = Common.UserIsAdmin.NotAdmin;

                    newUser = UserBusiness.AddUser(newUser);

                    if (newUser != null && newUser.UserId > 0)
                    {
                        //注册成功
                        if (from != null) { 
                        Response.Redirect(from);
                        }
                        else
                        {
                            Response.Redirect("Admin.aspx");
                        }
                    }
                    else if (newUser.UserId == -1)
                    {
                        Label1.Text = "user has existed!!";
                        
                    }
                }

                
            }
        }
    }
}