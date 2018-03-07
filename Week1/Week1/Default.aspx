<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Week1.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div>
            <asp:TextBox ID="txtHello" runat="server"></asp:TextBox>
        </div>
        <div>
            <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
        </div>
        <div>
            <asp:DropDownList ID="dlstProg" runat="server" AutoPostBack="True" OnSelectedIndexChanged="dlstProg_SelectedIndexChanged"></asp:DropDownList>
        </div>    
    </div>
    </form>
</body>
</html>
