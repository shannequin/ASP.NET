<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="AdventureWorks2._default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div>
                <h1>AdventureWorks</h1>
            </div>
            <div>
                <h4>Customers</h4>
            </div>
            <div>
                <asp:DropDownList ID="dlCustomer" runat="server"></asp:DropDownList>
            </div>
            <hr />
            <div>
                <h5>First Name</h5>
            </div>
            <div>
                <asp:TextBox ID="TextBoxFirstName" runat="server"></asp:TextBox>
            </div>
            <div>
                <h5>MI</h5>
            </div>
            <div>
                <asp:TextBox ID="TextBoxMI" runat="server"></asp:TextBox>
            </div>
            <div>
                <h5>Last Name</h5>
            </div>
            <div>
                <asp:TextBox ID="TextBoxLastName" runat="server"></asp:TextBox>
            </div>
            <div>
                <h5>Company Name</h5>
            </div>
            <div>
                <asp:TextBox ID="TextBoxCompanyName" runat="server"></asp:TextBox>
            </div>
        </div>
    </form>
</body>
</html>
