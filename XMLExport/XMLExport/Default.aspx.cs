using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Spreadsheet;

namespace XMLExport
{
    public partial class Default : System.Web.UI.Page
    {
        protected void loadTable()
        {
            //iOS can not handle an xlsx download so we need to hide the download button
            //for all mobile devices
            if (!Request.Browser.IsMobileDevice) btnExport.Visible = true;

            System.Web.UI.WebControls.Table tblSchedule = new System.Web.UI.WebControls.Table();
            tblSchedule.CssClass = "table table-hover col-xs-12 col-lg-6";
            tblSchedule.ID = "tblSchedule";
            tblSchedule.EnableViewState = false;
            displayListing.Controls.Add(tblSchedule);

            //Row 1: Headings
            TableRow rowHeadings = new TableRow();
            tblSchedule.Rows.Add(rowHeadings);
            for (int ColNum = 1; ColNum <= 10; ColNum++)
            {
                TableCell colHeading = new TableCell();
                colHeading.Font.Bold = true;
                switch (ColNum)
                {
                    case 1:
                        colHeading.Text = "CRN";
                        break;
                    case 2:
                        colHeading.Text = "Program";
                        break;
                    case 3:
                        colHeading.Text = "Course";
                        break;
                    case 4:
                        colHeading.Text = "Course Name";
                        break;
                    case 5:
                        colHeading.Text = "Status";
                        break;
                    case 6:
                        colHeading.Text = "Enrolled";
                        break;
                    case 7:
                        colHeading.Text = "Seats Avail.";
                        break;
                    case 8:
                        colHeading.Text = "Days";
                        break;
                    case 9:
                        colHeading.Text = "Time";
                        break;
                    case 10:
                        colHeading.Text = "Room";
                        break;
                }
                rowHeadings.Cells.Add(colHeading);
                colHeading.Dispose();
            }
            rowHeadings.Dispose();

            //Row 2: Detail record 1
            TableRow rowDetail = new TableRow();
            tblSchedule.Rows.Add(rowDetail);

            TableCell colDetail = new TableCell();
            colDetail.Text = "13399";
            rowDetail.Cells.Add(colDetail);

            colDetail = new TableCell();
            colDetail.Text = "CISP";
            rowDetail.Cells.Add(colDetail);

            colDetail = new TableCell();
            colDetail.Text = "1010-N01";
            rowDetail.Cells.Add(colDetail);

            colDetail = new TableCell();
            colDetail.Text = "Computer Science I";
            rowDetail.Cells.Add(colDetail);

            colDetail = new TableCell();
            colDetail.Text = "A";
            rowDetail.Cells.Add(colDetail);

            colDetail = new TableCell();
            colDetail.Text = "15";
            rowDetail.Cells.Add(colDetail);

            colDetail = new TableCell();
            colDetail.Text = "0";
            rowDetail.Cells.Add(colDetail);

            colDetail = new TableCell();
            colDetail.Text = "MW";
            rowDetail.Cells.Add(colDetail);

            colDetail = new TableCell();
            colDetail.Text = "1220 PM - 0210 PM";
            rowDetail.Cells.Add(colDetail);

            colDetail = new TableCell();
            colDetail.Text = "C-226";
            rowDetail.Cells.Add(colDetail);

            //row 3: Detail record 2
            rowDetail = new TableRow();
            tblSchedule.Rows.Add(rowDetail);

            colDetail = new TableCell();
            colDetail.Text = "16924";
            rowDetail.Cells.Add(colDetail);

            colDetail = new TableCell();
            colDetail.Text = "CISP";
            rowDetail.Cells.Add(colDetail);

            colDetail = new TableCell();
            colDetail.Text = "1010-N40";
            rowDetail.Cells.Add(colDetail);

            colDetail = new TableCell();
            colDetail.Text = "Computer Science I";
            rowDetail.Cells.Add(colDetail);

            colDetail = new TableCell();
            colDetail.Text = "A";
            rowDetail.Cells.Add(colDetail);

            colDetail = new TableCell();
            colDetail.Text = "19";
            rowDetail.Cells.Add(colDetail);

            colDetail = new TableCell();
            colDetail.Text = "1";
            rowDetail.Cells.Add(colDetail);

            colDetail = new TableCell();
            colDetail.Text = "T";
            rowDetail.Cells.Add(colDetail);

            colDetail = new TableCell();
            colDetail.Text = "0600 PM - 0950 PM";
            rowDetail.Cells.Add(colDetail);

            colDetail = new TableCell();
            colDetail.Text = "C-229";
            rowDetail.Cells.Add(colDetail);
            
            colDetail.Dispose();
            rowDetail.Dispose();

            tblSchedule.Dispose();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            loadTable();

            if (IsPostBack)
            {
                //We need to find out what control caused the postback
                getPostBackControl clsCtl = new getPostBackControl();
                string ctl = clsCtl.getPostBackControlName(Page);
                switch (ctl)
                {
                    case "btnExport":
                        expExcel();
                        break;
                }
            }
        }

        protected void expExcel()
        {
            //Need to install OpenXMLSDKV25.msi or OpenXMLSDKToolV25.msi located at 
            //https://www.microsoft.com/en-us/download/details.aspx?id=30425
            //Need to add reference to WindowsBase under Assemblies and Framework
            //Need to add reference to DocumentFormat.OpenXml under Assemblies and Extensions

            // Example https://msdn.microsoft.com/en-us/library/office/gg278309.aspx

            getPostBackControl findCtrl = new getPostBackControl();
            System.Web.UI.WebControls.Table tblSchedule = (System.Web.UI.WebControls.Table)findCtrl.FindControlDeepSearch(Page, "tblSchedule");

            if (tblSchedule != null)
            {
                string path = Server.MapPath("ExportFiles\\");
                string FinalName = Path.GetRandomFileName(); //Must get a unique name to use due to multiple users
                FinalName = Convert.ToString(FinalName).Replace(".", ""); // Must strip out the . if one exist

                // CHECK IF THE FOLDER EXISTS. IF NOT, CREATE A NEW FOLDER.
                //Issue: If you create a directory it will not enough rights on the server for the user to use it
                if (!Directory.Exists(path))   
                    Directory.CreateDirectory(path);

                // Create a spreadsheet document by supplying the filepath.
                // By default, AutoSave = true, Editable = true, and Type = xlsx.
                SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.Create(path + FinalName + "ProgramSchedule.xlsx", SpreadsheetDocumentType.Workbook);

                // Add a WorkbookPart to the document.
                WorkbookPart workbookpart = spreadsheetDocument.AddWorkbookPart();
                workbookpart.Workbook = new Workbook();

                // Add a WorksheetPart to the WorkbookPart.
                WorksheetPart worksheetPart = workbookpart.AddNewPart<WorksheetPart>();
                worksheetPart.Worksheet = new Worksheet(new SheetData());

                // Add Sheets to the Workbook.
                Sheets sheets = spreadsheetDocument.WorkbookPart.Workbook.AppendChild<Sheets>(new Sheets());

                // Append a new worksheet and associate it with the workbook.
                Sheet sheet = new Sheet() { Id = spreadsheetDocument.WorkbookPart.GetIdOfPart(worksheetPart), SheetId = 1, Name = "Class Schedule" };
                sheets.Append(sheet);

                // Get the sheetData cell table.
                SheetData sheetData = worksheetPart.Worksheet.GetFirstChild<SheetData>();

                // Add a row to the cell table.
                Row row;
                row = new Row() { RowIndex = 1 };
                sheetData.Append(row);

                Cell refCell = null;
                // Add the cell to the cell table at A1.
                Cell newCell = new Cell() { CellReference = "A1" };
                row.InsertBefore(newCell, refCell);

                newCell.CellValue = new CellValue("Course Listing");
                newCell.DataType = new EnumValue<CellValues>(CellValues.String);

                int numCols = tblSchedule.Rows[0].Cells.Count;
                char rangeLetter = 'A';
                switch (numCols)
                {
                    //You need to know at design what the max cells would be 
                    case 4: rangeLetter = 'D'; break; //Minimum
                    case 5: rangeLetter = 'E'; break;
                    case 6: rangeLetter = 'F'; break;
                    case 7: rangeLetter = 'G'; break;
                    case 8: rangeLetter = 'H'; break;
                    case 9: rangeLetter = 'I'; break;
                    case 10: rangeLetter = 'J'; break;
                    case 11: rangeLetter = 'K'; break;
                    case 12: rangeLetter = 'L'; break;
                    case 13: rangeLetter = 'M'; break;
                    case 14: rangeLetter = 'N'; break;
                    case 15: rangeLetter = 'O'; break;
                    case 16: rangeLetter = 'P'; break;
                    case 17: rangeLetter = 'Q'; break;
                    case 18: rangeLetter = 'R'; break; //Maximum
                }

                // Create the merged cell and append it to the MergeCells collection.
                MergeCells mergeCells;
                mergeCells = new MergeCells();

                //Merge the cells in row 1 for the title
                MergeCell mergeCell = new MergeCell() { Reference = new StringValue("A1:" + rangeLetter + "1") };
                mergeCells.Append(mergeCell);
                worksheetPart.Worksheet.InsertAfter(mergeCells, worksheetPart.Worksheet.Elements<SheetData>().First());

                // ROW ID FROM WHERE THE DATA STARTS SHOWING.
                int iRowCnt = 4;
                row = new Row() { RowIndex = 2 }; //Blank Row
                sheetData.Append(row);

                row = new Row() { RowIndex = 3 };//Blank Row
                sheetData.Append(row);

                int eSeats = 0, aSeats = 0, cRemarks = 0;
                bool bESeats = false, bASeats = false, bRemarks = false;
                bool bHeader = true;
                int colID = 1;

                foreach (TableRow trp in tblSchedule.Rows) //Loop through each row of the asp table
                {
                    char colLetter = 'A';

                    // Add a row to the cell table.
                    row = new Row() { RowIndex = (UInt32)iRowCnt };
                    sheetData.Append(row);

                    string CRNHold = "";
                    foreach (TableCell tcp in trp.Cells) //Loop through each column
                    {
                        // In the new row, find the column location to insert a cell in A1.
                        refCell = null;

                        // Add the cell to the cell table at A1.
                        newCell = new Cell() { CellReference = colLetter + iRowCnt.ToString() };
                        row.InsertBefore(newCell, refCell);

                        if (bHeader)
                        {
                            switch (tcp.Text)
                            {
                                case "Seats Avail.":
                                    aSeats = colID;
                                    bASeats = true;
                                    break;
                                case "Enrolled":
                                    eSeats = colID;
                                    bESeats = true;
                                    break;
                                case "Remarks":
                                    cRemarks = colID;
                                    bRemarks = true;
                                    break;
                            }
                            newCell.DataType = new EnumValue<CellValues>(CellValues.String);
                        }
                        else
                        {
                            if (colID == 1)
                                CRNHold = tcp.Text;
                            if (bASeats && colID == aSeats)
                                //xlWorkSheetToExport.Cells[iRowCnt, colID].NumberFormat = "0";
                                newCell.DataType = new EnumValue<CellValues>(CellValues.Number);
                            else if (bESeats && colID == eSeats)
                                //xlWorkSheetToExport.Cells[iRowCnt, colID].NumberFormat = "0";
                                newCell.DataType = new EnumValue<CellValues>(CellValues.Number);
                            else
                                //xlWorkSheetToExport.Cells[iRowCnt, colID].NumberFormat = "@";
                                newCell.DataType = new EnumValue<CellValues>(CellValues.String);
                        }

                        if (bRemarks && colID == cRemarks)
                        {
                            //LinkButton lbRemHold = new LinkButton();
                            getPostBackControl fc = new getPostBackControl();
                            LinkButton lbRemHold = (LinkButton)fc.FindControlDeepSearch(Page, "lbRemarks_" + CRNHold);
                            if (lbRemHold != null)
                                //xlWorkSheetToExport.Cells[iRowCnt, colID] = lbRemHold.Attributes["title"].ToString();
                                newCell.CellValue = new CellValue(lbRemHold.Attributes["title"].ToString());
                            else
                                //xlWorkSheetToExport.Cells[iRowCnt, colID] = tcp.Text;
                                newCell.CellValue = new CellValue(tcp.Text);
                        }
                        else
                            //xlWorkSheetToExport.Cells[iRowCnt, colID] = tcp.Text;
                            newCell.CellValue = new CellValue(tcp.Text);

                        if (tcp.ColumnSpan == numCols) //Test to see if we are on a new campus name or subtotals
                        {
                            //xlWorkSheetToExport.Range["A" + iRowCnt + ":E" + iRowCnt].MergeCells = true;
                            //The bold is overridden below when using the auto format
                            //xlWorkSheetToExport.Range["A" + iRowCnt + ":E" + iRowCnt].Font.Bold = true;
                            // Create the merged cell and append it to the MergeCells collection.
                            //MergeCells mergeCells;
                            //mergeCells = new MergeCells();

                            //mergeCell = new MergeCell() { Reference = new StringValue("A" + iRowCnt + ":E" + iRowCnt) };
                            //mergeCells.Append(mergeCell);
                            //worksheetPart.Worksheet.InsertAfter(mergeCells, worksheetPart.Worksheet.Elements<SheetData>().First());

                            continue;
                        }
                        colID++;
                        colLetter++;
                    }
                    colID = 1;
                    iRowCnt++;
                    bHeader = false;
                    colLetter = 'A';
                }


                //*********************************************
                // Close the document.
                spreadsheetDocument.Close();


                //Download the file
                DownLoadFile(path, FinalName + "ProgramSchedule.xlsx");


            }

        }

        protected void DownLoadFile(string fPath, string fName)
        {
            try
            {
                Response.AppendHeader("Content-Disposition", "attachment; filename=ProgramSchedule.xlsx"); //Changes the file name like a Save As for the download
                System.IO.FileInfo file = new System.IO.FileInfo(fPath + fName);
                Response.AppendHeader("Content-Length", file.Length.ToString());
                //Response.ContentType = "application/octet-stream";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.WriteFile(fPath + fName);
                Response.Flush();
                Response.SuppressContent = true;  // Gets or sets a value indicating whether to send HTTP content to the client.
                HttpContext.Current.ApplicationInstance.CompleteRequest(); // Causes ASP.NET to bypass all events and filtering in the HTTP pipeline chain of execution and directly execute the EndRequest event.

                //Delete the file on the server
                File.Delete(fPath + fName);
            }
            catch (Exception ex)
            {
                Response.Write(@"<script>alert('Warning file download unsuccessful: " + ex.Message.ToString() + "');</script>");
            }
        }
    }
}