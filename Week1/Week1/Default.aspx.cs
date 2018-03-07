using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Week1
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            //Raised after all controls have been initialized and any skin settings have been applied. 
            //Use this event to read or initialize control properties
            Response.Write("Page Init.<br />");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //Use the OnLoad event method to set properties in controls and to establish database connections.
            Response.Write("Page Load.<br />");

            txtHello.Text = "Hello World";

            if (!IsPostBack)
            {
                dlstProg.Items.Add("CITC");
                dlstProg.Items.Add("BUSN");
                dlstProg.Items.Add("ECON");
            }
        }

        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            //Raised at the end of the event-handling stage.
            //Use this event for tasks that require that all other controls on the page be loaded.
            Response.Write("Page LoadComplete.<br />");
        }

        protected void Page_PreRenderComplete(object sender, EventArgs e)
        {
            //Raised after each data bound control whose DataSourceID property is set calls its DataBind method. 
            //For more information, see Data Binding Events for Data-Bound Controls later in this topic.
            Response.Write("Page PreRenderComplete.<br />");
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            Response.Write("btnSubmit_Click.<br />");

            txtHello.Text = "Hello from btnSubmit";

        }

        protected void dlstProg_SelectedIndexChanged(object sender, EventArgs e)
        {
            Response.Write("lstProg_SelectedIndexChanged. The select value is: " + dlstProg.SelectedValue.ToString() + "<br />");
        }
    }
}