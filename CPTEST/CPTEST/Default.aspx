<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="CPTEST.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Button ID="Button1" runat="server" Text="Populate Grid" OnClick="Button1_Click" />
        <br />
        <asp:GridView ID="GridView1" runat="server" PageSize="5"></asp:GridView>

        <br />
        <asp:Button ID="Button2" runat="server" Text="    1    " OnClick="Button2_Click" />
        <br />
        <br />
        <asp:Button ID="Button3" runat="server" Text="DateTime" OnClick="Button3_Click" />
&nbsp;
        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
    </div>
    </form>
</body>
</html>
