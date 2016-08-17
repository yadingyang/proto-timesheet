<%@ Page Title="" Language="C#" MasterPageFile="~/Workday.Master" AutoEventWireup="true" CodeBehind="Admindept.aspx.cs" Inherits="Workday.Web.Admindept" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
     <div id="alldeptsshow" style="padding-left:50px">
        <asp:Panel ID="AddDeptPanel" runat="server" Visible="false">
            <asp:Label Text="DeptName: " runat="server"></asp:Label><asp:TextBox ID="NewDeptName" runat="server"></asp:TextBox>     &nbsp; Parent Dept: <asp:DropDownList ID="ParentDropForAdd" runat="server"></asp:DropDownList>     &nbsp; Manager: <asp:DropDownList ID="ManagerDropForAdd" runat="server"></asp:DropDownList>    &nbsp;&nbsp;&nbsp;    <asp:Button ID="AddDeptButton" runat="server" Text="Add Dept" OnClick="AddDeptButton_Click"/>&nbsp;&nbsp; <asp:Button ID="AddDeptCancel" runat="server" Text="Cancel" OnClick="AddDeptCancel_Click"/><br />
    </asp:Panel>
         <div style="padding-left:800px">  <asp:Button ID="TrigerAdd" runat="server" Text="Add Dept"  OnClick="TrigerAdd_Click" />  <br/><br />   </div>
         <asp:GridView ID="AllDeptsGridView" AutoGenerateColumns="False" runat="server" Font-Name="Verdana" 
             AllowPaging="True" PageSize="15" AllowSorting="true" OnPageIndexChanging="AllDeptsGridView_PageIndexChanging"
             Font-Size="10pt" Cellpadding="10"
             HeaderStyle-BackColor="Gray"
             HeaderStyle-ForeColor="White"
             AlternatingRowStyle-BackColor="#dddddd"
             GridLines="none" OnRowCancelingEdit="AllDeptsGridView_RowCancelingEdit" OnRowEditing="AllDeptsGridView_RowEditing" OnRowUpdating="AllDeptsGridView_RowUpdating" OnRowDeleting="AllDeptsGridView_RowDeleting" OnRowDataBound="AllDeptsGridView_RowDataBound" OnSorting="AllDeptsGridView_Sorting"
             >
             <Columns>
                <asp:BoundField HeaderText="Dept_ID" DataField="DeptID" ReadOnly="true" SortExpression="DeptId" />
                <asp:BoundField HeaderText="Dept_Name" DataField="DeptName" SortExpression="DeptName"/>
                <asp:TemplateField HeaderText="Parent_Name" SortExpression="ParentName">
                     <ItemTemplate>
                         <asp:Label ID="Parent_Name_Label" runat="server" Text='<%#Eval("ParentName")%>'></asp:Label>
                     </ItemTemplate>
                     <EditItemTemplate>
                         <asp:DropDownList ID="Parent_Name_Drop" runat="server"></asp:DropDownList>
                     </EditItemTemplate>
                 </asp:TemplateField>
                <%--<asp:BoundField HeaderText="Manager_Name" DataField="ManagerName" />--%>
                <asp:TemplateField HeaderText="Manager_Name" SortExpression="ManagerName">
                     <ItemTemplate>
                         <asp:Label ID="Manager_Name_Label" runat="server" Text='<%#Eval("ManagerName")%>'></asp:Label>
                     </ItemTemplate>
                     <EditItemTemplate>
                         <asp:DropDownList ID="Manager_Name_Drop" runat="server"></asp:DropDownList>
                     </EditItemTemplate>
                 </asp:TemplateField>
                <asp:BoundField HeaderText="Create_Date" DataField="CreateDate" ReadOnly="true" />
                <asp:CommandField HeaderText="Edit" ShowEditButton="true" />
                <asp:CommandField HeaderText="Delete" ShowDeleteButton="true" />
         </Columns>

        </asp:GridView>
         </div>
    <%--<asp:DropDownList ID="ParentDrop" runat="server"></asp:DropDownList>--%>
</asp:Content>
