<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="Week4_BootStrap._default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>First Bootstrap Program</title>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />

    <link href="Content/bootstrap.css" rel="stylesheet" />
    <script src="Scripts/jquery-3.0.0.slim.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container-fluid">
            <div class="row">
                <h1>AdventureWorks</h1>
            </div>
            <br />
            <div class="row">
                <div class="col-sm-2">
                    <h3>Customer</h3>
                </div>
                <div class="col-sm-6">
                    <asp:DropDownList ID="dlCustomer" CssClass="form-control" runat="server" AutoPostBack="True"></asp:DropDownList>
                </div>
                <div class="col-sm-4"></div>
            </div>
            <br />
            <hr />
            <br />
            <div class="row">
                <div class="col-sm-1">
                    <h4>First Name</h4>
                </div>
                <div class="col-sm-2">
                    <asp:TextBox ID="txtFName" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
                <div class="col-sm-1">
                    <h4>MI</h4>
                </div>
                <div class="col-sm-2">
                    <asp:TextBox ID="txtMI" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
                <div class="col-sm-1">
                    <h4>Last Name</h4>
                </div>
                <div class="col-sm-2">
                    <asp:TextBox ID="txtLName" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
                <div class="col-sm-3"></div>
            </div>
            <div class="row">
                <div class="col-sm-1">
                    <h4>Company Name</h4>
                </div>
                <div class="col-sm-2">
                    <asp:TextBox ID="txtCompanyName" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
                <div class="col-sm-9"></div>
            </div>
        </div>
    </form>
</body>
</html>
