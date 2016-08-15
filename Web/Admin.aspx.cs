using System;
using System.Web.UI.WebControls;
using Workday.Business;

namespace Workday.Web
{
    public partial class Admin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!this.IsPostBack)
            { 
                Common.AllUser Allusers = new Common.AllUser();
                Allusers = UserBusiness.GetAllUsers(Allusers);
                AllUserGridView.DataSource = Allusers.AllUserSet;
                AllUserGridView.DataBind();
            }

            if (this.IsPostBack)
            {
                //Button1.Command += new CommandEventHandler(this.CommandBtn_Click);
                AllUserGridView.RowCommand += AllUserGridView_RowCommand;
                //Control button = this.Master.FindControl("body").FindControl("AllUserGridView");
                //Control button=null;
                //foreach (GridViewRow row in AllUserGridView.Rows) { 
                //       button = row.FindControl("ChangeUserStatusButton");
                //}
                ////Button button2 = AllUserGridView.FindControl("Change_Status") as Button;
                //if (button != null) { 
                //    Control myControl2 = button.Parent;
                //    Response.Write("Parent of the text box is : " + myControl2.ID);
                    //button.Command += new CommandEventHandler(this.ChangeUserStatus);
                //}
                //button2.Command += new CommandEventHandler(this.ChangeUserStatus);
                //Change_Status.Command += new CommandEventHandler(this.ChangeUserStatus);
            }
        }

        private void AllUserGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //throw new NotImplementedException();
            int userid, currentstatus;
            if (e.CommandName == "ChangeStatus")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = AllUserGridView.Rows[index];
                userid = Convert.ToInt32(row.Cells[0].Text);
                currentstatus = Convert.ToInt32(row.Cells[3].Text);
                if (UserBusiness.ChangeUserStatus(userid, currentstatus) == true)
                {
                    //this.ShowDebugInfo.Text = userid.ToString() + ": status changed";
                    Common.AllUser Allusers = new Common.AllUser();
                    Allusers = UserBusiness.GetAllUsers(Allusers);
                    AllUserGridView.DataSource = Allusers.AllUserSet;
                    AllUserGridView.DataBind();

                }
            }
        }

        protected void ChangeUserStatus(object sender, GridViewCommandEventArgs e)
        {
            int userid, currentstatus;
            if (e.CommandName == "ChangeStatus")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = AllUserGridView.Rows[index];
                userid = Convert.ToInt32(row.Cells[0].Text);
                currentstatus = Convert.ToInt32(row.Cells[3].Text);
                //string[] commandargs = e.CommandArgument.ToString().Split(new char[] { ',' });
                //userid = Convert.ToInt32(commandargs[0]);
                //currentstatus = Convert.ToInt32(commandargs[1]);
                //string[] commandargs = btn.CommandArgument.ToString().Split(new char[] { ',' });
                //if (e.CommandName == "ChangeStatus") {
                //    string[] commandargs = e.CommandArgument.ToString().Split(new char[] { ',' });
                //    userid = Convert.ToInt32(commandargs[0]);
                //    currentstatus = Convert.ToInt32(commandargs[1]);
                if (UserBusiness.ChangeUserStatus(userid, currentstatus) == true)
                {
                    this.ShowDebugInfo.Text = userid.ToString() + ": status changed";

                }
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

        //for debug only
        //void CommandBtn_Click(Object sender, CommandEventArgs e)
        //{

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
        //            ShowDebugInfo.Text = "You clicked the <b>Sort Ascending</b> button.";
        //            break;
        //    }
        //}


        protected void AllUserGridView_SelectedIndexChanged(object sender, EventArgs e)
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