using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Module2
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void LoadControls()
        {
            TextBox lblHold = new TextBox();
            lblHold.Text = "First dynamic textbox.";
            lblHold.ID = "txtdy1";
            rowOne.Controls.Add(lblHold);

            LinkButton lnkHold1 = new LinkButton();
            lnkHold1.Text = "Microsoft MSDN";
            lnkHold1.ID = "lnkMSDN";
            lnkHold1.CommandName = "lnk1";
            lnkHold1.Command += new CommandEventHandler(this.lnkHold_Click);
            rowTwo.Controls.Add(lnkHold1);

            LinkButton lnkHold2 = new LinkButton();
            lnkHold2.Text = "Microsoft.com";
            lnkHold2.ID = "lnkMicrosoft";
            lnkHold2.CommandName = "lnk2";
            lnkHold2.Command += new CommandEventHandler(this.lnkHold_Click);
            rowThree.Controls.Add(lnkHold2);

            LinkButton lnkHold3 = new LinkButton();
            lnkHold3.Text = "NSCC.edu";
            lnkHold3.ID = "lnkNSCC";
            lnkHold3.CommandName = "lnk3";
            lnkHold3.Command += new CommandEventHandler(this.lnkHold_Click);
            rowFour.Controls.Add(lnkHold3);

            if (!IsPostBack)
            {
                dlstProg.Items.Insert(dlstProg.Items.Count, new ListItem("CITC", "P1"));
                dlstProg.Items.Insert(dlstProg.Items.Count, new ListItem("BUSN", "P2"));
                dlstProg.Items.Insert(dlstProg.Items.Count, new ListItem("ECON", "P3"));
                dlstProg.Items.Insert(0, new ListItem("", ""));
            }
        }

        protected void lnkHold_Click(object sender, CommandEventArgs e)
        {
            switch(e.CommandName)
            {
                case "lnk1":
                    Response.Redirect("https://msdn.microsoft.com/en-us/library/kx37x362.aspx");
                    break;
                case "lnk2":
                    Response.Redirect("http://www.microsoft.com");
                    break;
                case "lnk3":
                    Response.Write(@"<script>alert('You are about to leave the current web page. You will be redirected to NSCC.edu'); window.location=""http://www.nscc.edu""</script>");
                    break;
            }
        }

        protected void Page_init(object sender, EventArgs e)
        {
            //if(!IsPostBack) LoadControls();
            //Msg.InnerHtml = "Message: " + dlstProg.SelectedValue.ToString();
        }
    
        protected void Page_InitComplete(object sender, EventArgs e)
        {
            //Msg.InnerHtml = "Message: " + dlstProg.SelectedValue.ToString();
        }

        protected void Page_PreLoad(object sender, EventArgs e)
        {
            //Msg.InnerHtml = "Message: " + dlstProg.SelectedValue.ToString();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            LoadControls();
            Msg.InnerHtml = "Message: " + dlstProg.SelectedValue.ToString();
        }

        protected void Page_LoadComlete(object sender, EventArgs e)
        {

        }
    }
}