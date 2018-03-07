<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ExcelWithModal.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <%-- Bootstrap and printThis are being used in this project --%>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />

    <link href="Content/bootstrap.css" rel="stylesheet" />
    <script src="Scripts/jquery-1.11.0.min.js"></script>

    <%--js for the modal popup--%>
    <script src="Scripts/bootstrap.min.js"></script>
    <script type="text/javascript">
        function initMyModal(ModalName) {
            $('#' + ModalName).one().modal('show');
        }
    </script>

    <script type="text/javascript">
        function closeMyModal(ModalName) {
            $('#' + ModalName).modal('hide');
            //$('#' + ModalName).one().modal('hide');
        }
    </script>

    <script src="Scripts/printThis.js"></script>

    <script type="text/javascript">
        $(function () {
            //This is the call to print the contents of the modal
            $("input:button").click(function () {
                $("#mdlPrint").printThis({
                    debug: false,              
                    importCSS: true,             
                    printContainer: false,       
                    loadCSS: "../Content/bootstrap.css",
                    removeInline: false,        
                    printDelay: 333,            
                    header: null,             
                    formValues: true          
                });
                //Close the modal after printing
                $("#mdlPrint").one().modal('hide');
            });

        });
    </script>


    <style>
        @media print
        {
            /* Hide the main web page from the print */
            body * { visibility: hidden; }
            #mdlPrint .modal-header * { visibility: visible; }
            #mdlPrint .modal-body * { visibility: visible; }
            footer {page-break-after: avoid; }
        }
    </style>

</head>
<body>
    <form id="form1" runat="server">
        <%-- Modal form for print --%>
        <div class="modal fade" id="mdlPrint">
          <div class="modal-dialog">
            <div class="modal-content">
              <div class="modal-header modal-header-warning">
                <h4 class="modal-title">Course Schedule</h4>
              </div>
              <div class="modal-body">
                <div class="form-group">
                    <div class="row">
                        <a class="col-lg-3"></a>
                            <asp:Table ID="tblSchedulePrint" runat="server" cssclass="table table-hover col-xs-12 col-lg-6">
                                <asp:TableRow runat="server">
                                    <asp:TableCell runat="server">CRN</asp:TableCell>
                                    <asp:TableCell runat="server">Prog</asp:TableCell>
                                    <asp:TableCell runat="server">Course</asp:TableCell>
                                    <asp:TableCell runat="server">Description</asp:TableCell>
                                    <asp:TableCell runat="server">Enrolled</asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        <a class="col-lg-3"></a>
                    </div>
                </div>
              </div>
              <div class="modal-footer hidden-print">
                  <input type="button" value="Print" class="btn btn-primary" />
                  <asp:Button Text="Export to Excel" CssClass="btn" ID="btnExcel" OnClientClick="closeMyModal('mdlPrint');" runat="server" />
              </div>
            </div>
          </div>
        </div>
        <%-- End of modal form print --%>

        <div class="container-fluid">
            <div class="row">
                <h1>Schedule Demo</h1>
            </div>
            <br />
            <div class="row">
                <div class="col-sm-1"></div>
                <div class="col-sm-2">
                    <h3>Term</h3>
                </div>
                <div class="col-sm-3">
                    <asp:DropDownList ID="dlTerm" CssClass="form-control" runat="server" AutoPostBack="True"></asp:DropDownList>
                </div>
                <div class="col-sm-2">
                    <h3>Program</h3>
                </div>
                <div class="col-sm-3">
                    <asp:DropDownList ID="dlProg" CssClass="form-control" runat="server" AutoPostBack="True"></asp:DropDownList>
                </div>
                <div class="col-sm-1"></div>
            </div>
            <br />
            <hr />
            <br />
            <%-- Program Class Listing --%>
            <section>
                <div class="row">
			        <div class="col-md-offset-3 col-md-6">
				        <div class="section-header">
					        <h3>Program Schedule</h3>
				        </div>
			        </div>
		        </div>
                <div class="row"><asp:LinkButton ID="lnkPrint" runat="server">Print/Export</asp:LinkButton></div>
                <div class="row">
                    <a class="col-lg-3"></a>
                        <asp:Table ID="tblSchedule" runat="server" cssclass="table table-hover col-xs-12 col-lg-6">
                            <asp:TableRow runat="server">
                                <asp:TableCell runat="server">CRN</asp:TableCell>
                                <asp:TableCell runat="server">Prog</asp:TableCell>
                                <asp:TableCell runat="server">Course</asp:TableCell>
                                <asp:TableCell runat="server">Description</asp:TableCell>
                                <asp:TableCell runat="server">Enrolled</asp:TableCell>
                            </asp:TableRow>
                        </asp:Table>
                    <a class="col-lg-3"></a>
                </div>
            </section>
        </div>
    </form>
</body>
</html>
