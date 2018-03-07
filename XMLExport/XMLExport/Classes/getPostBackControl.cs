using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace XMLExport
{
    public class getPostBackControl : System.Web.UI.Page
    {
        //All ASP.NET Web Form controls (and forms, which derive from Control) provide a FindControl() method. 
        //This method will search all the child controls of a given control to find the one that matches a given ID.
        //This can be a handy feature but it often comes up short because it is not recursive. That is, it searches a 
        //control's children, but not its children's children. For example, if you try and find a control on a form, 
        //but that control exists inside a panel, then FindControl() will not be able to find it unless you specifically 
        //search the panel control. Since a web form page normally consists of a rich tree structure of controls and 
        //subcontrols, any general control search on a web form is likely to fail if the search is not recursive.
        private Control FindCtrlRecursive(Control root, string id)
        {
            if (root.ID == id) return root;
            foreach (Control ctrl in root.Controls)
            {
                Control hCtrl = FindCtrlRecursive(ctrl, id);
                if (hCtrl != null) return hCtrl;
            }
            return null;
        }
        public string getPostBackControlName(Page pagePointer)
        {
            // Function returns the control ID of the one that caused the postback. Needed for the modal popup dialog box.
            Control control = null;
            //first we will check the "__EVENTTARGET" because if post back made by the controls
            //which used "_doPostBack" function also available in Request.Form collection.
            string ctrlname = pagePointer.Request.Params["__EVENTTARGET"];
            if (ctrlname != null && ctrlname != String.Empty)
            {
                control = pagePointer.FindControl(ctrlname);
                if (control == null)
                    //See if the control is hiding as a child
                    control = FindCtrlRecursive(pagePointer, ctrlname);
            }
            // if __EVENTTARGET is null, the control is a button type and we need to
            // iterate over the form collection to find it
            else
            {
                string ctrlStr = String.Empty;
                Control c = null;
                foreach (string ctl in pagePointer.Request.Form)
                {
                    //handle ImageButton they have an additional "quasi-property" in their Id which identifies
                    //mouse x and y coordinates
                    if (ctl.EndsWith(".x") || ctl.EndsWith(".y"))
                    {
                        ctrlStr = ctl.Substring(0, ctl.Length - 2);
                        c = pagePointer.FindControl(ctrlStr);
                    }
                    else
                    {
                        c = pagePointer.FindControl(ctl);
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

        public Control FindControlDeepSearch(Page pagePointer, string controlID)
        {
            // Function returns a handle to the control object
            Control control = null;
            string ctrlname = controlID;

            control = pagePointer.FindControl(ctrlname);
            if (control == null)
                //See if the control is hiding as a child
                control = FindCtrlRecursive(pagePointer, ctrlname);

            if (control != null)
                return control;
            else
                return null;

        }
    }
}