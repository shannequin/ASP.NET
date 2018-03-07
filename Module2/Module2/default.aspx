<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="Module2.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label Text="Welcome to ASP.Net" runat="server"></asp:Label>
        </div>
        <div id="rowOne" runat="server"></div>
        <div id="rowTwo" runat="server"></div>
        <div id="rowThree" runat="server"></div>
        <div id="rowFour" runat="server"></div>
        <div id="rowFive" runat="server">
            <asp:DropDownList ID="dlstProg" runat="server"></asp:DropDownList>
        </div>
        <div></div>
        <div>
            <asp:Button ID="Button1" runat="server" Text="Button" />
        </div>
        <div id="Msg" runat="server"></div>
    </form>
</body>
</html>
