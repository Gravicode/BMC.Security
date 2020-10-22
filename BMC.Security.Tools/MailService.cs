using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace BMC.Security.Tools
{
    public class EmailManager
    {
        private string m_HostName; // your email SMTP server  
        public int Port { get; set; }
        public EmailManager(string hostName, int Port = 25)
        {
            this.Port = Port;
            m_HostName = hostName;
        }

        public void SendMail(EmailSendConfigure emailConfig, EmailContent content)
        {

            MailMessage msg = ConstructEmailMessage(emailConfig, content);
            Send(msg, emailConfig);
        }

        // Put the properties of the email including "to", "cc", "from", "subject" and "email body"  
        private MailMessage ConstructEmailMessage(EmailSendConfigure emailConfig, EmailContent content)
        {
            MailMessage msg = new System.Net.Mail.MailMessage();
            foreach (string to in emailConfig.TOs)
            {
                if (!string.IsNullOrEmpty(to))
                {
                    msg.To.Add(to);
                }
            }

            foreach (string cc in emailConfig.CCs)
            {
                if (!string.IsNullOrEmpty(cc))
                {
                    msg.CC.Add(cc);
                }
            }

            msg.From = new MailAddress(emailConfig.From,
                                       emailConfig.FromDisplayName,
                                       System.Text.Encoding.UTF8);
            msg.IsBodyHtml = content.IsHtml;
            msg.Body = content.Content;
            msg.Priority = emailConfig.Priority;
            msg.Subject = emailConfig.Subject;
            msg.BodyEncoding = System.Text.Encoding.UTF8;
            msg.SubjectEncoding = System.Text.Encoding.UTF8;

            if (content.AttachFileName != null)
            {
                Attachment data = new Attachment(content.AttachFileName,
                                                 MediaTypeNames.Application.Zip);
                msg.Attachments.Add(data);
            }

            return msg;
        }

        //Send the email using the SMTP server  
        private void Send(MailMessage message, EmailSendConfigure emailConfig)
        {
            SmtpClient client = new SmtpClient();


            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential(
                                  emailConfig.ClientCredentialUserName,
                                  emailConfig.ClientCredentialPassword);
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.Host = m_HostName;
            client.Port = Port;  // this is critical
            client.EnableSsl = true;  // this is critical

            try
            {
                client.Send(message);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error in Send email: {0}", e.Message);
                throw;
            }
            message.Dispose();
        }

    }

    public class EmailSendConfigure
    {
        public string[] TOs { get; set; }
        public string[] CCs { get; set; }
        public string From { get; set; }
        public string FromDisplayName { get; set; }
        public string Subject { get; set; }
        public MailPriority Priority { get; set; }
        public string ClientCredentialUserName { get; set; }
        public string ClientCredentialPassword { get; set; }
        public EmailSendConfigure()
        {
        }
    }

    public class EmailContent
    {
        public bool IsHtml { get; set; }
        public string Content { get; set; }
        public string AttachFileName { get; set; }
    }
    public class MailService
    {
        public static string MailUser { get; set; }
        public static string MailPassword { get; set; }
        public static int MailPort { get; set; }
        public static string MailServer { get; set; }
        public static async Task<bool> SendEmail(string subject, string message, string toemail, bool IsHTML = true)
        {
            var NewTask = new Task<bool>(() =>
            {
                //string smtpServer = "smtp-mail.outlook.com";

                var UserMail = MailUser;//"silpo@outlook.co.id";
                var UserPassword = MailPassword;//"Balittanah123";

                try
                {
                    //Send teh High priority Email  
                    EmailManager mailMan = new EmailManager(MailServer, MailPort);

                    EmailSendConfigure myConfig = new EmailSendConfigure();
                    // replace with your email userName  
                    myConfig.ClientCredentialUserName = UserMail;
                    // replace with your email account password
                    myConfig.ClientCredentialPassword = UserPassword;
                    myConfig.TOs = new string[] { toemail };
                    myConfig.CCs = new string[] { };
                    myConfig.From = UserMail;
                    myConfig.FromDisplayName = "[Toko Online] - Jamaah";
                    myConfig.Priority = System.Net.Mail.MailPriority.Normal;
                    myConfig.Subject = subject;

                    EmailContent myContent = new EmailContent();
                    myContent.Content = message;
                    myContent.IsHtml = IsHTML;
                    mailMan.SendMail(myConfig, myContent);
                    Console.WriteLine("email was sent successfully!");
                    return true;
                }
                catch (Exception ex)
                {
                    //Console.WriteLine("failed to send email with the following error:");
                    //Console.WriteLine(ep.Message);
                    //LogHelpers.source = typeof(EmailService).ToString();
                    //LogHelpers.message = "failed to send email with the following error:" + ex.Message;
                    //LogHelpers.user = CommonWeb.GetCurrentUser();
                    //LogHelpers.WriteLog();
                    return false;
                }
            });
            NewTask.Start();
            return await NewTask;
        }
    }
}
