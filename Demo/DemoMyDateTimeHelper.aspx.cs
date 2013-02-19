using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyHelper4Web;

namespace Demo
{
    public partial class DemoMyDateTimeHelper : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Write(MyDateTimeHelper.GetWeekofyear(Convert.ToDateTime("2013-02-16")));
        }
    }
}