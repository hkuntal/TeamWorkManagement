<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FileUpload.aspx.cs" Inherits="SampleWebApp1.FileUpload" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" language="javascript">
        function validate() {
            document.getElementById("form1").submit();
            return false;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div>
            Welcome to file upload tool..
        </div>
        <div>
            
        </div>
    </div>
        <asp:FileUpload ID="FileUpload1" runat="server" />
        <br />
            <asp:Label ID="Label1" runat="server" Text="File Purpose"></asp:Label>
            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        <br />
        <%--<asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" OnClientClick="return validate()"  Text="Button" />--%>
        <input id="Button1" type="button" value="button" onclick="validate()"/>
     </form>
</body>
</html>
