<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="XMLExport.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

        <%-- Bootstrap Links --%>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />

    <link href="Content/bootstrap.css" rel="stylesheet" />
    <script src="scripts/jquery-3.0.0.slim.min.js"></script>

    <%-- Style Sheets --%>
    <link href="CSS/Main.css" rel="stylesheet" />

</head>
<body>
    <form id="form1" runat="server">
	    <div class="container  pad-top50">
		    <div class="row mar-bot40">
				<div class="section-header">
					<h3>Course Schedule</h3>
				</div>
		    </div>
            <div id="displayListing" class="row" runat="server">


<%--                <table id="tblSchedule" runat="server" style="width: 100%;">
                    <tr>
                        <th>CRN</th>
                        <th>Program</th>
                        <th>Course</th>
                        <th>Course Name</th>
                        <th>Status</th>
                        <th>Enrolled</th>
                        <th>Seats Avail.</th>
                        <th>Days</th>
                        <th>Time</th>
                        <th>Room</th>
                    </tr>
                    <tr>
                        <td colspan="10">
                            <hr class="hr-bold" />
                        </td>
                    </tr>
                    <tr>
                        <td>13399</td>
                        <td>CISP</td>
                        <td>1010-N01</td>
                        <td>Computer Science I</td>
                        <td>A</td>
                        <td>15</td>
                        <td>0</td>
                        <td>MW</td>
                        <td>1220 PM - 0210 PM</td>
                        <td>C-226</td>
                    </tr>
                    <tr>
                        <td>16924</td>
                        <td>CISP</td>
                        <td>1010-N40</td>
                        <td>Computer Science I</td>
                        <td>A</td>
                        <td>19</td>
                        <td>1</td>
                        <td>T</td>
                        <td>0600 PM - 0950 PM</td>
                        <td>C-229</td>
                    </tr>
                    <tr>
                        <td>14014</td>
                        <td>CISP</td>
                        <td>1010-W01</td>
                        <td>Computer Science I</td>
                        <td>A</td>
                        <td>24</td>
                        <td>1</td>
                        <td></td>
                        <td></td>
                        <td>WEB</td>
                    </tr>
                </table>--%>
            </div>
            <br />
            <div>
                <asp:Button ID="btnExport" CssClass="btn btn-info btn-xs" runat="server" Text="Export" />
            </div>
        </div>
    </form>
</body>
</html>
