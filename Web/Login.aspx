<%@ Page Title="" Language="C#" MasterPageFile="~/Workday.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Workday.Web.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        #debuginfo{
            padding:50px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <div style="padding-left: 100px">
        <div>
            <asp:Label ID="LBLoginMsg" runat="server" Font-Size="Large" ForeColor="Red"></asp:Label>
        </div>
    <asp:Panel ID="LoginPanel" runat="server">
        <div>Username：<input type="text" name="username" align="right"/></div>
        <div>Password：<input type="password" name="password" align="right"/></div>
        <div>
            <asp:Button ID="submit" runat="server" Text="Login" OnClick="submit_Click" />
            <%--    <input type="submit" name="submit" value="Login" runat="server" onclick=""/>--%>
        </div>
    </asp:Panel>
    <asp:Panel ID="ChangepassPanel" runat="server">
        You login for the first time, lease set your own password<br />
        new password:<input type="password" id="sp" name="setnewpassword" runat="server" /><br />
        confirm password:<input type="password" id="cp" name="connewpassword" runat="server" /><br />
        Please select secret questions:<asp:DropDownList ID="secretlist" runat="server" OnSelectedIndexChanged="secretlist_SelectedIndexChanged" OnTextChanged="secretlist_a"></asp:DropDownList><br />
        Please input your secret answer:<input type="text" id="secret" name="secretanswer" runat="server" /><br />
        <asp:Button ID="setnewpassbutton" runat="server" Text="Set New Password" OnClick="setnewpassbutton_Click" />
        <asp:Button ID="cancel" runat="server" Text="Logout" OnClick="cancel_Click" />
    </asp:Panel>
    </div>
 <%--   <div id="debuginfo">
        <p id="showcookie">default text</p>
    </div>
    <script type="text/javascript">
        allcookie = document.cookie;
        document.getElementById("showcookie").innerHTML = allcookie;
        //document.getElementById("passerr").innerHTML
    </script>--%>
<%--    <div>
        <asp:Label ID="ShowSession" runat="server">default text of session</asp:Label>
    </div>--%>
</asp:Content>
