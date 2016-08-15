using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Workday.Business;
using Workday.Common;

namespace Workday.Web
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["CurrentUser"] == null)
                {
                    LoginPanel.Visible = true;
                    ChangepassPanel.Visible = false;
                }
                else
                {
                    //LBLoginMsg.Text = "Welcome " + Session["CurrentUser"].ToString() + " ,you have logged in.";
                    //LoginPanel.Visible = false;
                    //ChangepassPanel.Visible = false;
                    if (Session["IsAdmin"] == null)    //如果登录成功，切不是管理员，则需要判断是否初次登陆，如果初次，显示设置密码页面，如果不是初次，重定向到timesheet页面
                    {
                        if ((string)(Session["isFirst"]) == "no")
                        {
                            Response.Redirect("TimeSheet.aspx");
                        }
                        else if (Session["isFirst"] == null)
                        {
                            secretlist.DataSource = UserBusiness.GetSecretQuestion();
                            secretlist.DataTextField = "Value";
                            secretlist.DataValueField = "Key";
                            secretlist.DataBind();
                            secretlist.Items.Insert(0, new ListItem(string.Empty, string.Empty));
                            secretlist.SelectedIndex = 0;
                            //secretlist.ClearSelection();
                            //secretlist.SelectedIndex = -1;
                            LoginPanel.Visible = false;
                            ChangepassPanel.Visible = true;
                        }
                    }
                    else if (Session["IsAdmin"] != null)  //如果登录成功，并且是管理员，则不判断是否初次登陆，直接重定向到admin页面
                    {
                        Response.Redirect("Admin.aspx");
                    }
                   
                }
            }

        }

        protected void setnewpassbutton_Click(object sender, EventArgs e)
        {
            string newp1 = sp.Value;
            string newp2 = cp.Value;
            string newp;
            int questionid = 0;
            if(secretlist.SelectedIndex!=-1 & secretlist.SelectedIndex != 0)
            {
                questionid = Convert.ToInt32(secretlist.SelectedValue);
            }
            string answer = secret.Value;
            string uname = Session["CurrentUser"].ToString();
            Common.User cuser = new Common.User();
            cuser = UserBusiness.GetUserByName(uname);
            newp= Common.User.GenerateHash(newp1);

            if (newp1 != newp2)
            {
                LBLoginMsg.Text = "new passwords are not identical";
            }
            else if (newp1 == null || newp2 == null|| newp1 == "" || newp2 == "")
            {
                LBLoginMsg.Text = "Please enter new password";
            }
            else if (newp == cuser.Password)
            {
                LBLoginMsg.Text = "new password could not be same with the default password";
            }
            else if (questionid == 0)
            {
                LBLoginMsg.Text = "Please select a secret question";
            }
            else if (answer == null || answer == "")
            {
                LBLoginMsg.Text = "Please input your secret answer";
            }
            else if (answer.Length < 3)
            {
                LBLoginMsg.Text = "the shortest length of secret answer is 3";
            }
            else
            {
                cuser.Password = newp1;
                cuser.SecretQuestion = questionid;
                cuser.SecretAnswer = answer;
                bool ifupdate = UserBusiness.ChangeDefaultPassword(cuser);
                if (ifupdate == true)
                {
                    Session["isFirst"] = "no";
                    LBLoginMsg.Text = "you have set your own password!";
                    LoginPanel.Visible = false;
                    ChangepassPanel.Visible = false;
                    Response.AddHeader("REFRESH", "3;URL=TimeSheet.aspx");
                }
                else
                {
                    LBLoginMsg.Text = "set password failed. Please try again!";
                }
            }
        }

        protected void cancel_Click(object sender, EventArgs e)
        {
            if (Session["CurrentUser"] != null)
            {
                Session["CurrentUser"] = null;
                Session["IsAdmin"] = null;
                Response.Redirect("default.aspx");
            }
        }

        protected void submit_Click(object sender, EventArgs e)
        {
            //提交登录
            string username = Request.Form["username"];
            string password = Request.Form["password"];

            string errMsg;
            string isAdmin;
            bool isFlogin;

            Common.User user = UserBusiness.Verify(username, password, out errMsg, out isAdmin,out isFlogin);

            if (user == null)
            {
                this.LBLoginMsg.Text = errMsg;
            }
            else
            {

                Session["UserID"] = user.UserId;
                Session["CurrentUser"] = user.UserName;//记录登录信息，用来验证用户是否登录

                if (isAdmin == null)
                {
                    if(isFlogin==false)
                    {

                        Session["isFirst"] = "no";
                        HttpCookie myCookie = new HttpCookie("UserName");
                        myCookie.Value = user.UserName;
                        myCookie.Expires = DateTime.Now.AddDays(1);
                        Response.Cookies.Add(myCookie);
                        Response.Redirect("TimeSheet.aspx");
                    }
                    else
                    {
                        secretlist.DataSource = UserBusiness.GetSecretQuestion();
                        secretlist.DataTextField = "Value";
                        secretlist.DataValueField = "Key";
                        secretlist.DataBind();
                        secretlist.Items.Insert(0, new ListItem(string.Empty, string.Empty));
                        secretlist.SelectedIndex = 0;
                        LoginPanel.Visible = false;
                        ChangepassPanel.Visible = true;
                    }

                }
                else if (isAdmin != null)
                {

                    HttpCookie myCookie = new HttpCookie("UserName");
                    myCookie.Value = user.UserName;
                    myCookie.Expires = DateTime.Now.AddDays(1);
                    Response.Cookies.Add(myCookie);
                    Session["IsAdmin"] = isAdmin;
                    Response.Redirect("Admin.aspx");
                }
                

                // Response.Redirect("Default.aspx");
            }

            //for debug
            //if (Session["IsAdmin"] != null)
            //{
            //    this.ShowSession.Text = Session["IsAdmin"].ToString();
            //}
            //else
            //{
            //    this.ShowSession.Text = "you are not admin and session is null";
            //}

        }

        protected void secretlist_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void secretlist_a(object sender, EventArgs e)
        {

        }
    }
}