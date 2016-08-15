<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Timesheet1.aspx.cs" Inherits="Workday.Web.Timesheet1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" > 
<head runat="server"> 
    <title>Untitled Page</title> 
    <script type="text/javascript"> 
        function btnClick(){ 
            // 调用页面后台方法，前面跟方法所需的参数，接着是方法回调成功时要执行的js函数，最后一个是方法回调失败时要执行的js函数 
            PageMethods.Hello("you", funReady, funError);
            
        }         
        // result 就是后台方法返回的数据 
        function funReady(result){ 
            alert(result); 
        } 
        // err 就是后台方法返回的错误信息 
        function funError(err){ 
            alert("Error:" + err._message ); 
        } 
    </script> 
</head> 
<body> 
    <form id="form1" runat="server"> 
    <div> 
        下面要加上EnablePageMethods="true"属性，才能使用后台方法         
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"> 
        </asp:ScriptManager> 
        <input type="button" onclick="btnClick();" value="test" /> 
        <asp:Image ID="Image1" runat="server" visible="false"/>
    </div> 
    </form> 
</body> 
</html>