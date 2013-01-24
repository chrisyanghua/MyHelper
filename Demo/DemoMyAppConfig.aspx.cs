using System;
using MyHelper4Web;

namespace Demo
{
    public partial class DemoMyAppConfig : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           Response.Write(Run());
        }

        private string Run()
        {
            const string format = "Before: {0} After: {1}";

            var config = new MyAppConfigHelper("Web.config");
            string s1= config.AppConfigGet("IsSmtp");
            config.AppConfigSet("IsSmtp", DateTime.Now.ToString("hh:mm:ss"));
            string s2 = config.AppConfigGet("IsSmtp");

            return string.Format(format, s1, s2);
        }
    }
}