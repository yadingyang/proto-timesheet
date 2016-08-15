<%@ Page Title="" Language="C#" MasterPageFile="~/Workday.Master" AutoEventWireup="true" CodeBehind="Admin2.aspx.cs" Inherits="Workday.Web.Admin2" %>
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
    <div id="allusershow" style="padding-left:50px">
        <asp:GridView ID="AllUserGridView"  AllowPaging="True" PageSize="5" AutoGenerateColumns="False" runat="server" Font-Name="Verdana" 
             Font-Size="10pt" Cellpadding="20"
             HeaderStyle-BackColor="Gray"
             HeaderStyle-ForeColor="White"
             AlternatingRowStyle-BackColor="#dddddd"
             GridLines="none" OnPageIndexChanged="PageIndexChanged" OnPageIndexChanging="PageIndexChanging"

             >
            <Columns>
                <asp:BoundField HeaderText="User_ID" DataField="UserID" ReadOnly="true"  />
                <asp:BoundField HeaderText="User_Email" DataField="Email" />
                <asp:BoundField HeaderText="User_Name" DataField="UserName" />
                <asp:BoundField HeaderText="User_Status" DataField="UserStatus" />
                <asp:BoundField HeaderText="Create_Date" DataField="CreateDate" />
                <asp:TemplateField HeaderText="Status_String" >
                    <ItemTemplate >
                        <asp:Label ID="Status_String" runat="server" Text='<%# GetStatus(Convert.ToInt32(Eval("UserStatus"))) %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Disable/Enable" >
                    <ItemTemplate>
                        <asp:Button ID="ChangeUserStatusButton"  Runat="Server" CausesValidation="false"
                        Text='<%# ButtonText(Convert.ToInt32(Eval("UserStatus"))) %>' 
                        Visible='<%# Convert.ToString(Eval("UserName")) != Session["CurrentUser"].ToString() ? true : false%>'
                        CommandName="ChangeStatus"
                        CommandArgument='<%#((GridViewRow) Container).RowIndex %>'/>
                    </ItemTemplate>
                    <ItemTemplate>
                        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# "admin2.aspx?userid=" + Eval("UserID")+"&currentstatus="+Eval("UserStatus") %>'
                            Text='<%# LinkText(Convert.ToInt32(Eval("UserStatus"))) %>'
                            Visible='<%# Convert.ToString(Eval("UserName")) != Session["CurrentUser"].ToString() ? true : false%>'>HyperLink</asp:HyperLink>
                    </ItemTemplate>
                </asp:TemplateField>
         </Columns>
        </asp:GridView>
        <br/>
    </div>
        <div style="padding-left:600px">
        <br/>
        <input id="Button12" type="button" value="AddUser" onclick="addfrom()"/>
    </div>
  <%--  <div>
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        <br />
        <asp:Button ID="Button1" runat="server" Text="md5-encode" OnClick="Button1_Click" />
        <asp:Button ID="Button2" runat="server" Text="md5-decode" OnClick="Button2_Click" />
        <br/>
        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
    </div>--%>
</asp:Content>
