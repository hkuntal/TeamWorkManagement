<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default1.aspx.cs" Inherits="SampleWebApp1.Default1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
    <script type="text/javascript"> alert("Hariom")</script>
<body>
    <form id="form1" runat="server">
    <div>
    <div>
        <asp:Label ID="lblName" runat="server" Text="Label"></asp:Label>
        <asp:Button ID="Button1" runat="server" Text="Submit" OnClick="Button1_Click" />
        <asp:Button ID="Button2" runat="server" Text="End the Session" OnClick="Button2_Click" />
    </div>
    </div>
    </form>
</body>
</html>
