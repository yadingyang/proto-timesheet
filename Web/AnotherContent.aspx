<%@ Page Title="" Language="C#" MasterPageFile="~/Workday.Master" AutoEventWireup="true" CodeBehind="AnotherContent.aspx.cs" Inherits="Workday.Web.AnotherContent" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <div>
        <asp:Label ID="WelcomeMsg" runat="Server"></asp:Label>
    </div>
       <div>
            <asp:Button id="Button1"
           Text="Sort Ascending"
           CommandName="Sort"
           CommandArgument="Ascending"
           runat="server"/>
          <asp:Label ID="ShowDebugInfo" runat="server">default text</asp:Label>

      </div>
</asp:Content>
