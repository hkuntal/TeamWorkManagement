using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SampleWebApp1
{
    public partial class FileUpload : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Write("The file was received on the server. TextBox1 = " + TextBox1.Text);
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            Response.Write("The file was received on the server");
        }
    }
}