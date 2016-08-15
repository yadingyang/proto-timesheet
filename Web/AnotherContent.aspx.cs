using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Workday.Web
{
    public partial class AnotherContent : System.Web.UI.Page
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
                Button1.Click += new EventHandler(this.CommandBtn_Click);

            }
        }

        void CommandBtn_Click(Object sender, EventArgs e)
        {

        //    switch (e.CommandName)
        //    {

        //        case "Sort":

        //            // Call the method to sort the list.
        //            Sort_List((String)e.CommandArgument);
        //            break;
        //    }
        //}

        //void Sort_List(string commandArgument)
        //{

        //    switch (commandArgument)
        //    {

        //        case "Ascending":

        //            // Insert code to sort the list in ascending order here.
                    ShowDebugInfo.Text = "You clicked the <b>Sort Ascending</b> button.";
                    //break;
            }
        }
    }
