<%@ Page Title="" Language="C#" MasterPageFile="~/Workday.Master" AutoEventWireup="true" CodeBehind="Admin.aspx.cs" Inherits="Workday.Web.Admin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function addfrom(){
            currenturl = window.location.pathname
            currenturl = currenturl.slice(1)
            tourl = "AddUser.aspx?from=" + currenturl
            window.location = tourl;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <div id="allusershow">
        <asp:GridView ID="AllUserGridView" AllowPaging="True" PageSize="10" AutoGenerateColumns="False" runat="server" OnRawCommand="AllUserGridView_RowCommand" OnSelectedIndexChanged="AllUserGridView_SelectedIndexChanged" OnPageIndexChanging="PageIndexChanging">
            <Columns>
                <asp:BoundField HeaderText="User_ID" DataField="UserID" ReadOnly="true" />
                <asp:BoundField HeaderText="Email" DataField="Email" />
                <asp:BoundField HeaderText="User_Name" DataField="UserName" />
                <asp:BoundField HeaderText="User_Status" DataField="UserStatus" />
                <asp:BoundField HeaderText="Create_Date" DataField="CreateDate" />
                <asp:TemplateField HeaderText="Status_String">
                    <ItemTemplate>
                        <asp:Label ID="Status_String" runat="server" Text='<%# GetStatus(Convert.ToInt32(Eval("UserStatus"))) %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Disable/Enable">
                    <ItemTemplate>
                        <asp:Button ID="ChangeUserStatusButton"  Runat="Server" CausesValidation="false"
                        Text='<%# ButtonText(Convert.ToInt32(Eval("UserStatus"))) %>' 
                        Visible='<%# Convert.ToString(Eval("UserName")) != Session["CurrentUser"].ToString() ? true : false%>'
                        CommandName="ChangeStatus"
                        CommandArgument='<%#((GridViewRow) Container).RowIndex %>'/>
                    </ItemTemplate>
                </asp:TemplateField>
         </Columns>
        </asp:GridView>

    </div>
    <div style="padding-left:600px">
        <br/>
        <%--<input id="Button1" type="button" value="AddUser" onclick="document.location.href ='AddUser.aspx';"/>--%>
        <input id="Button1" type="button" value="AddUser" onclick="addfrom()"/>
    </div>
 <%--     <div>
            <asp:Button id="Button1"
           Text="Sort Ascending"
           CommandName="Sort"
           CommandArgument="Ascending"
           OnClientClick="return confirm('are you sure to sort?')"
           runat="server"/> --%>
          <asp:Label ID="ShowDebugInfo" runat="server"></asp:Label>
          <br/>
          <%-- <asp:Button ID="Button2" runat="server" Text="aspButton" PostBackUrl="~/Admin2.aspx"/>
          <input id="Button13" type="button" value="html-button"/>
          <input id="Submit1" type="submit" value="html-submit" />
      </div>--%>
</asp:Content>
