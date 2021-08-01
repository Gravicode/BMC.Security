using BMC.Security.Tools;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace BMC.Security.Web.Svc
{
    /// <summary>
    /// Summary description for sendmail
    /// </summary>
    public class sendmail : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            try
            {
                string strJson = new StreamReader(context.Request.InputStream).ReadToEnd();

                //deserialize the object
                MailInfo objUsr = Deserialize<MailInfo>(strJson);
                var task1 = MailService.SendEmail(objUsr.subject, objUsr.body,objUsr.mailto,true);
                task1.Start();
                task1.Wait();
                if (task1.Result)
                {

                    context.Response.Write("{ 'result':'success'}");
                }
                else
                {
                    context.Response.Write("{ 'result':'failed'}");
                }
            }
            catch (Exception ex)
            {
                context.Response.Write("{ 'result':'{" + ex.Message + "}");
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        // we create a userinfo class to hold the JSON value
        public class MailInfo
        {
            public string mailto { get; set; }
            public string subject { get; set; }
            public string from { get; set; }
            public string body { get; set; }

        }


        // Converts the specified JSON string to an object of type T
        public T Deserialize<T>(string context)
        {
            string jsonData = context;

            //cast to specified objectType
            var obj = (T)new JavaScriptSerializer().Deserialize<T>(jsonData);
            return obj;
        }
    }
}