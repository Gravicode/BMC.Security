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
    /// Summary description for sms
    /// </summary>
    public class sms : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            try
            {
                string strJson = new StreamReader(context.Request.InputStream).ReadToEnd();

                //deserialize the object
                SmsInfo objUsr = Deserialize<SmsInfo>(strJson);
                var task1 = SmsService.SendSms(objUsr.message, objUsr.phoneto);
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
                context.Response.Write("{ 'result':'{"+ex.Message+"}");
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
        public class SmsInfo
        {
            public string phoneto { get; set; }
            public string message { get; set; }
          
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