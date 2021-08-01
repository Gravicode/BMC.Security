using BMC.Security.Tools;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace BMC.Security.Web
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            MailService.MailUser = ConfigurationManager.AppSettings["MailUser"];
            MailService.MailPassword = ConfigurationManager.AppSettings["MailPassword"];
            MailService.MailServer = ConfigurationManager.AppSettings["MailServer"];
            MailService.MailPort = int.Parse(ConfigurationManager.AppSettings["MailPort"]);
            MailService.SetTemplate(ConfigurationManager.AppSettings["TemplatePath"]);
            MailService.SendGridKey = ConfigurationManager.AppSettings["SendGridKey"];
            MailService.UseSendGrid = true;


            SmsService.UserKey = ConfigurationManager.AppSettings["ZenzivaUserKey"];
            SmsService.PassKey = ConfigurationManager.AppSettings["ZenzivaPassKey"];

            // Code that runs on application startup
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}