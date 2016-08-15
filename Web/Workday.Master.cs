using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Workday.Web
{
    public partial class Workday : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //任何页面加载的时候，判断是否登录，如果登录，显示用户名和注销按钮
            if (Session["CurrentUser"] == null)
            {
                Logoutlink.Visible = false;
                WelcomeMsg.Visible = false;
                //if not login, could not access any page, redirect to login
                if (Request.FilePath.IndexOf("Login.aspx", StringComparison.OrdinalIgnoreCase) == -1)
                        Response.Redirect("Login.aspx");
            }
            else if ( Session["CurrentUser"] != null)
            {
                Logoutlink.Visible = true;
                WelcomeMsg.Visible = true;
                WelcomeMsg.Text = "Welcome: " + Session["CurrentUser"].ToString();

            }


            //任何页面加载的时候，判断登录用户是否管理员，如果不是管理员，去掉menu中的管理员页面入库
            if (Session["IsAdmin"]==null)
            {
                MenuItemCollection mymenu = Menu1.Items;
                MenuItem adminitem = new MenuItem();
                MenuItem adminitem2 = new MenuItem();
                MenuItem adminitem3 = new MenuItem();
                foreach (MenuItem menuItem in mymenu)
                {
                    if (menuItem.Text == "Admin")
                        adminitem = menuItem;
                    if (menuItem.Text == "Admin2")
                        adminitem2 = menuItem;
                    if (menuItem.Text == "AdminDepts")
                        adminitem3 = menuItem;
                }
                mymenu.Remove(adminitem);
                mymenu.Remove(adminitem2);
                mymenu.Remove(adminitem3);

                // 如果用户访问admin页面并且不是管理员（通过浏览器地址直接访问），则跳转到default页面，这样防止非管理员用户访问管理员页面
                if (Request.FilePath.IndexOf("Admin.aspx" ,StringComparison.OrdinalIgnoreCase)!=-1)
                    Response.Redirect("login.aspx");
                if (Request.FilePath.IndexOf("Admin2.aspx", StringComparison.OrdinalIgnoreCase) != -1)
                    Response.Redirect("login.aspx");
                if (Request.FilePath.IndexOf("AddUser.aspx", StringComparison.OrdinalIgnoreCase) != -1)
                    Response.Redirect("login.aspx");

                //如果用户初次登陆切还没有设置密码，则不允许访问其他页面
                if (Session["isFirst"] == null)
                {
                    if (Request.FilePath.IndexOf("Default.aspx", StringComparison.OrdinalIgnoreCase) != -1)
                        Response.Redirect("login.aspx");
                    if (Request.FilePath.IndexOf("AnotherContent.aspx", StringComparison.OrdinalIgnoreCase) != -1)
                        Response.Redirect("login.aspx");
                } 
                else if((string)Session["isFirst"] =="no") //如果用户已经登陆成功，且不是初次访问，则菜单去掉login，增加timesheet
                {
                    MenuItem menuitem1 = new MenuItem();
                    MenuItem menuitem2 = new MenuItem();
                    menuitem2.NavigateUrl = "Timesheet.aspx";
                    menuitem2.Text = "TimeSheet";
                    foreach (MenuItem menuItem in mymenu)
                    {
                        if (menuItem.Text == "Login")
                            menuitem1 = menuItem;
                    }
                    mymenu.Remove(menuitem1);
                    mymenu.Add(menuitem2);
                }
            }
            else //if admin, remove login from menu
            {
                MenuItemCollection mymenu = Menu1.Items;
                MenuItem menuitem1 = new MenuItem();
                foreach (MenuItem menuItem in mymenu)
                {
                    if (menuItem.Text == "Login")
                        menuitem1 = menuItem;
                }
                mymenu.Remove(menuitem1);
            }

            if (Request.FilePath.IndexOf("AddUser.aspx", StringComparison.OrdinalIgnoreCase) != -1)
                Menu1.Visible = false;


        }

        protected void Page_Init(object sender, EventArgs e)
        {

        }

        protected void Logout_Click(object sender, EventArgs e)
        {
            if (Session["CurrentUser"] != null)
            {
                Session["CurrentUser"] = null;
                Session["IsAdmin"] = null;
                Session["isFirst"] = null;
                Response.Redirect("login.aspx");
            }
        }
    }
}