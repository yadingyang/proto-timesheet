<%@ Page Title="" Language="C#"  MasterPageFile="~/Workday.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Workday.Web.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <div>
        <asp:Label ID="WelcomeMsg" runat="server"></asp:Label>
        </br>
        </br>
        <asp:Button ID="Logout" Text="Logout" runat="server" onclick="Logout_Click"/>
    </div>
</asp:Content>
