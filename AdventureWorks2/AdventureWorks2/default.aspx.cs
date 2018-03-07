using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace AdventureWorks2
{
    public partial class _default : System.Web.UI.Page
    {
        protected string getPostBackControlName()
        {
            //Function returns the control ID of the one that caused the postback.
            //Need for the modal popup dialog box.
            Control control = null;

            //Fist check the "__EVENTTARGET" because if postback made by the controls
            //which use "_doPostBack" function also available in Request.Form collection.
            string controlName = Page.Request.Params["__EVENTTARGET"];

            if(controlName != null && controlName != String.Empty)
            {
                control = Page.FindControl(controlName);
            }

            //If __EVENTTARET is null, the control is a button type, and we need to 
            //iterate over the form collection to find it.
            else
            {
                string controlString = String.Empty;
                Control c = null;

                foreach(string ctrl in Page.Request.Form)
                {
                    //Handle ImageButton they have an additional "quasi-property" in their ID
                    //which identifies mouse x/y coordinates.
                    if(ctrl.EndsWith(".x") || ctrl.EndsWith(".y"))
                    {
                        controlString = ctrl.Substring(0, ctrl.Length - 2);

                        c = Page.FindControl(controlString);
                    }
                    else
                    {
                        c = Page.FindControl(ctrl);
                    }

                    if(control is System.Web.UI.WebControls.Button || control is System.Web.UI.WebControls.ImageButton)
                    {
                        control = c;
                        break;
                    }

                }
            }

            if (control != null)
            {
                return control.ID.ToString();
            }
            else
            {
                return "";
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            //Open a connection to the database
            string conString = ConfigurationManager.ConnectionStrings["conAW"].ConnectionString;
            SqlConnection conAW = new SqlConnection(conString);
            conAW.Open();

            if (!IsPostBack)
            {
                //Load the page for the first time
                loadCustomer(conAW);
            }
            else
            {
                //Find out which control caused postback
                string ctl = getPostBackControlName();
                switch (ctl)
                {
                    case "dlCustomer":
                        loadDetail(conAW);
                        break;
                }
            }

            //Close connection to the database
            conAW.Close();
            conAW.Dispose();
        }

        protected void loadCustomer(SqlConnection cAW)
        {
            //Creates string to send to database
            string stringSQL = "SELECT DISTINCT CustomerID, FirstName, LastName, CompanyName FROM Customer ORDER BY LastName, FirstName;";

            SqlCommand command = new SqlCommand(stringSQL, cAW);
            SqlDataReader SQLdr = command.ExecuteReader();

            //Find out if it returned empty
            if (SQLdr.HasRows)
            {
                //Populate dropdown list
                while (SQLdr.Read())
                {
                    dlCustomer.Items.Insert(dlCustomer.Items.Count, new ListItem(SQLdr[2].ToString() + ", " + SQLdr[1].ToString(), SQLdr[0].ToString()));
                }

                dlCustomer.Items.Insert(0, new ListItem("Select a Customer", ""));
            }

            //Close and dispose reader and dispose command
            SQLdr.Close();
            SQLdr.Dispose();
            command.Dispose();
        }

        //loadDetail populates information in textboxes
        protected void loadDetail(SqlConnection cAW)
        {
            if(dlCustomer.SelectedItem.Value != "")
            {
                //A customer has been selected
                string stringSQL = "SELECT DISTINCT CustomerID, FirstName, MiddleName, LastName, CompanyName FROM Customer WHERE CustomerID = " + dlCustomer.SelectedItem.Value + ";";
                SqlCommand command = new SqlCommand(stringSQL, cAW);
                SqlDataReader SQLdr = command.ExecuteReader();

                if (SQLdr.HasRows)
                {
                    while (SQLdr.Read())
                    {
                        TextBoxFirstName.Text = SQLdr["FirstName"].ToString();
                        TextBoxMI.Text = SQLdr["MiddleName"].ToString();
                        TextBoxLastName.Text = SQLdr["LastName"].ToString();
                        TextBoxCompanyName.Text = SQLdr["CompanyName"].ToString();
                    }
                }

                SQLdr.Close();
                SQLdr.Dispose();
                command.Dispose();
            }
        }
    }
}