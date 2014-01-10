using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SampleWebApp1
{
    public partial class Default1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //put some data in session
                Session["name"] = "Hariom";
                lblName.Text = "This is <b>not a post back</b>. The session Id for the same is: " + Session.SessionID;
            }
            else
            {

                lblName.Text = "This <b>is a post back <b> and the session Id for the same is: " + Session.SessionID;
                lblName.Text += "The name in the session is: " + Session["name"];
            }
        }
        protected void Page_Render(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            ClientScript.RegisterStartupScript(Page.GetType(), "key", "<script type=text/javascript> alert('The session Id is " + Session.SessionID + "')</script>");
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            //End the Session
            Session.Abandon();
        }
    }
}