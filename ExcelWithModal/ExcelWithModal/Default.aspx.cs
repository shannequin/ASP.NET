using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;

using Excel = Microsoft.Office.Interop.Excel;
using ExcelAutoFormat = Microsoft.Office.Interop.Excel.XlRangeAutoFormat;

namespace ExcelWithModal
{
    public partial class Default : System.Web.UI.Page
    {
        protected string getPostBackControlName()
        {
            // Function returns the control ID of the one that caused the postback. Needed for the modal popup dialog box.
            Control control = null;
            //first we will check the "__EVENTTARGET" because if post back made by the controls
            //which used "_doPostBack" function also available in Request.Form collection.
            string ctrlname = Page.Request.Params["__EVENTTARGET"];
            if (ctrlname != null && ctrlname != String.Empty)
            {
                control = Page.FindControl(ctrlname);
            }
            // if __EVENTTARGET is null, the control is a button type and we need to
            // iterate over the form collection to find it
            else
            {
                string ctrlStr = String.Empty;
                Control c = null;
                foreach (string ctl in Page.Request.Form)
                {
                    //handle ImageButton they have an additional "quasi-property" in their Id which identifies
                    //mouse x and y coordinates
                    if (ctl.EndsWith(".x") || ctl.EndsWith(".y"))
                    {
                        ctrlStr = ctl.Substring(0, ctl.Length - 2);
                        c = Page.FindControl(ctrlStr);
                    }
                    else
                    {
                        c = Page.FindControl(ctl);
                    }
                    if (c is System.Web.UI.WebControls.Button ||
                             c is System.Web.UI.WebControls.ImageButton)
                    {
                        control = c;
                        break;
                    }
                }
            }
            if (control != null)
                return control.ID.ToString();
            else
                return "";
        }
        protected void loadTerm(SqlConnection cAW)
        {
            string strSQL = "SELECT DISTINCT TERM FROM SCHEDULE ORDER BY TERM;";
            SqlCommand command = new SqlCommand(strSQL, cAW);
            SqlDataReader SQLdr = command.ExecuteReader();

            if (SQLdr.HasRows)
            {
                while (SQLdr.Read())
                {
                    dlTerm.Items.Insert(dlTerm.Items.Count, new ListItem(SQLdr["TERM"].ToString(), SQLdr["Term"].ToString()));
                }
                dlTerm.SelectedIndex = dlTerm.Items.Count - 1;
            }
            SQLdr.Close();
            SQLdr.Dispose();
            command.Dispose();
        }
        protected void loadProg(SqlConnection cAW)
        {
            dlProg.Items.Clear();
            string strSQL = "SELECT DISTINCT PROG FROM SCHEDULE WHERE TERM = '" + dlTerm.SelectedItem.Value + "' ORDER BY PROG; ";
            SqlCommand command = new SqlCommand(strSQL, cAW);
            SqlDataReader SQLdr = command.ExecuteReader();

            if (SQLdr.HasRows)
            {
                while (SQLdr.Read())
                {
                    dlProg.Items.Insert(dlProg.Items.Count, new ListItem(SQLdr["PROG"].ToString(), SQLdr["PROG"].ToString()));
                }
                if (dlProg.Items.Count > 0)
                    dlProg.SelectedIndex = 0;
            }
            SQLdr.Close();
            SQLdr.Dispose();
            command.Dispose();
        }
        protected void loadDetail(SqlConnection cAW)
        {
            if (dlProg.SelectedItem.Value != "")
            {
                //A Program has been selected
                string strSQL = "SELECT SCHOOL, CRN, PROG, COURSENUM, COURSESECTION, COURSE, ENROLLED "
                    + "FROM SCHEDULE WHERE TERM = '" + dlTerm.SelectedItem.Value + "' "
                    + "AND PROG = '" + dlProg.SelectedItem.Value + "' "
                    + "ORDER BY SCHOOL, PROG, COURSENUM, COURSESECTION;";
                SqlCommand command = new SqlCommand(strSQL, cAW);
                SqlDataReader SQLdr = command.ExecuteReader();

                if (SQLdr.HasRows)
                {
                    string SchoolHold = "";
                    while (SQLdr.Read())
                    {
                        TableRow rowSch = new TableRow();
                        tblSchedule.Rows.Add(rowSch);
                        //Print Modal must duplicate the code
                        TableRow rowSchPrint = new TableRow();
                        tblSchedulePrint.Rows.Add(rowSchPrint);

                        for (int ColNum = 0; ColNum < 5; ColNum++)
                        {
                            TableCell colSch = new TableCell();
                            rowSch.Cells.Add(colSch);

                            TableCell colSchPrint = new TableCell();
                            rowSchPrint.Cells.Add(colSchPrint);

                            colSch.Font.Bold = false;
                            colSch.ColumnSpan = 1;

                            colSchPrint.Font.Bold = false;
                            colSchPrint.ColumnSpan = 1;

                            if (ColNum == 0)
                            {
                                if (SchoolHold.ToString() != SQLdr["SCHOOL"].ToString()) //New school, need new row
                                {
                                    colSch.Font.Bold = true;
                                    colSch.ColumnSpan = 5;
                                    SchoolHold = SQLdr["SCHOOL"].ToString();
                                    colSch.Text = SchoolHold;

                                    colSchPrint.Font.Bold = true;
                                    colSchPrint.ColumnSpan = 5;
                                    colSchPrint.Text = SchoolHold;

                                    //Force a new row after a change in school
                                    rowSch = new TableRow();
                                    tblSchedule.Rows.Add(rowSch);

                                    rowSchPrint = new TableRow();
                                    tblSchedulePrint.Rows.Add(rowSch);

                                    colSch = new TableCell();
                                    rowSch.Cells.Add(colSch);
                                    colSch.Font.Bold = false;
                                    colSch.ColumnSpan = 1;

                                    colSchPrint = new TableCell();
                                    rowSchPrint.Cells.Add(colSchPrint);
                                    colSchPrint.Font.Bold = false;
                                    colSchPrint.ColumnSpan = 1;
                                }
                            }


                            switch (ColNum)
                            {
                                case 0: //Column 1 CRN
                                    colSch.Text = SQLdr["CRN"].ToString();
                                    colSchPrint.Text = SQLdr["CRN"].ToString();
                                    break;
                                case 1: //Column 2 Prog
                                    colSch.Text = SQLdr["PROG"].ToString();
                                    colSchPrint.Text = SQLdr["PROG"].ToString();
                                    break;
                                case 2: //Column 3 Course
                                    colSch.Text = SQLdr["COURSENUM"].ToString()
                                          + "-" + SQLdr["COURSESECTION"].ToString();
                                    colSchPrint.Text = SQLdr["COURSENUM"].ToString()
                                          + "-" + SQLdr["COURSESECTION"].ToString();
                                    break;
                                case 3: //Column 4 Course Name
                                    colSch.Text = SQLdr["COURSE"].ToString();
                                    colSchPrint.Text = SQLdr["COURSE"].ToString();
                                    break;
                                case 4: //Column 5 Enrolled
                                    colSch.Text = SQLdr["ENROLLED"].ToString();
                                    colSchPrint.Text = SQLdr["ENROLLED"].ToString();
                                    break;
                            }
                        }
                    }
                }
                SQLdr.Close();
                SQLdr.Dispose();
                command.Dispose();
            }

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            //This code will delete all files the are more
            //than one day old in the ExportFiles directory
            string path = Server.MapPath("ExportFiles\\");
            DirectoryInfo dirInfo = new DirectoryInfo(path);
            String fileNamePart = "ProgramSchedule.xlsx";
            //Must use the wild card to return all of the files
            String searchPattern = String.Format("*{0}", fileNamePart);
            // Loop through all of the files and if they are more than one day old delete them
            foreach (var fs in dirInfo.GetFiles(searchPattern))
            {
                //If the file is more than one day old
                if (fs.CreationTime < DateTime.Now.AddDays(-1))
                    fs.Delete(); //Delete the file
            }
            //End of new code

            //Open a connection to the database
            string conStr = ConfigurationManager.ConnectionStrings["conAW"].ConnectionString;
            SqlConnection conAW = new SqlConnection(conStr);
            conAW.Open();

            if (!IsPostBack)
            {
                //We are loading the page for the first time
                loadTerm(conAW);
                loadProg(conAW);
                loadDetail(conAW);
            }
            else
            {
                //We need to find out what control caused the postback
                string ctl = getPostBackControlName();
                switch (ctl)
                {
                    case "dlTerm":
                        loadProg(conAW);
                        loadDetail(conAW);
                        break;
                    case "dlProg":
                        loadDetail(conAW);
                        break;
                    case "lnkPrint":
                        loadDetail(conAW);
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript1", "initMyModal('mdlPrint');", true);
                        break;
                    case "btnExcel":
                        loadDetail(conAW);
                        ExportToExcel();
                        break;
                }
            }

            //Close the connection to the DB
            conAW.Close();
            conAW.Dispose();

        }
        protected void ExportToExcel()
        {
            try
            {
                if (tblSchedulePrint.Rows.Count > 1)
                {
                    string path = Server.MapPath("ExportFiles\\");
                    string FinalName = Path.GetRandomFileName(); //Must get a unique name to use due to multiple users
                    FinalName = Convert.ToString(FinalName).Replace(".", ""); // Must strip out the . if one exist

                    if (!Directory.Exists(path))   // CHECK IF THE FOLDER EXISTS. IF NOT, CREATE A NEW FOLDER.
                        Directory.CreateDirectory(path);

                    // ADD A WORKBOOK USING THE EXCEL APPLICATION.
                    Excel.Application xlAppToExport = new Excel.Application();
                    xlAppToExport.Workbooks.Add("");

                    // ADD A WORKSHEET.
                    Excel.Worksheet xlWorkSheetToExport = default(Excel.Worksheet);
                    xlWorkSheetToExport = (Excel.Worksheet)xlAppToExport.Sheets["Sheet1"];

                    // ROW ID FROM WHERE THE DATA STARTS SHOWING.
                    int iRowCnt = 4;

                    // Create the sheet header
                    xlWorkSheetToExport.Cells[1, 1] = "Program Schedule";
                    xlWorkSheetToExport.Cells.NumberFormat = "@";

                    Excel.Range range = xlWorkSheetToExport.Cells[1, 1] as Excel.Range;
                    range.EntireRow.Font.Name = "Calibri";
                    range.EntireRow.Font.Bold = true;
                    range.EntireRow.Font.Size = 20;

                    xlWorkSheetToExport.Range["A1:E1"].MergeCells = true; // MERGE CELLS OF THE HEADER.

                    int colID = 1;
                    foreach (TableRow trp in tblSchedulePrint.Rows) //Loop through each row of the asp table
                    {
                        foreach (TableCell tcp in trp.Cells) //Loop through each column
                        {
                            xlWorkSheetToExport.Cells[iRowCnt, colID] = tcp.Text;
                            if (tcp.ColumnSpan == 5) //Test to see if we are on a new campus name
                            {
                                xlWorkSheetToExport.Range["A" + iRowCnt + ":E" + iRowCnt].MergeCells = true;
                                //The bold is overridden below when using the auto format
                                //xlWorkSheetToExport.Range["A" + iRowCnt + ":E" + iRowCnt].Font.Bold = true;
                                continue;
                            }
                            colID++;
                        }
                        colID = 1;
                        iRowCnt++;
                    }

                    // FINALLY, FORMAT THE EXCEL SHEET USING EXCEL'S AUTOFORMAT FUNCTION.
                    Excel.Range range1 = xlAppToExport.ActiveCell.Worksheet.Cells[4, 1] as Excel.Range;
                    range1.AutoFormat(ExcelAutoFormat.xlRangeAutoFormatList3);

                    // SAVE THE FILE IN A FOLDER.
                    xlWorkSheetToExport.SaveAs(path + FinalName + "ProgramSchedule.xlsx");

                    // Clean up
                    xlAppToExport.Workbooks.Close();
                    xlAppToExport.Quit();
                    xlAppToExport = null;
                    xlWorkSheetToExport = null;

                    //Download the file
                    DownLoadFile(path, FinalName + "ProgramSchedule.xlsx");
                }
            }
            catch (Exception ex)
            {
                Response.Write(@"<script>alert('Warning export unsuccessful: " + ex.Message.ToString() + "');</script>");
            }
        }
        // DOWNLOAD THE FILE.
        protected void DownLoadFile(string fPath, string fName)
        {
            try
            {
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + fName);
                System.IO.FileInfo file = new System.IO.FileInfo(fPath + fName);
                Response.AppendHeader("Content-Length", file.Length.ToString());
                Response.ContentType = "application/octet-stream";
                Response.WriteFile(fPath + fName);
                Response.Flush();
                Response.SuppressContent = true;  // Gets or sets a value indicating whether to send HTTP content to the client.
                HttpContext.Current.ApplicationInstance.CompleteRequest(); // Causes ASP.NET to bypass all events and filtering in the HTTP pipeline chain of execution and directly execute the EndRequest event.

                //Delete the file on the server
                File.Delete(fPath + fName);

                //Have to use system.net
                //This downloads a file to the client computer in the background
                //WebClient webClient = new WebClient();
                //You have to give the path where you want the file saved to
                //webClient.DownloadFile(new Uri(sPath + "ProgramSchedule.xlsx"), @"C:\ProgramSchedule.xlsx");
            }
            catch (Exception ex)
            {
                Response.Write(@"<script>alert('Warning file download unsuccessful: " + ex.Message.ToString() + "');</script>");
            }
        }
    }
}