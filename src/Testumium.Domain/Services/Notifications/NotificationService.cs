using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Testumium.Domain.Models.Domain.Notifications;

namespace Testumium.Domain.Services
{
    public class NotificationService : INotificationService
    {
        private IEmailSender emailSender;
        private string emailTemplatesFolder;
        public NotificationService(IEmailSender emailSender)
        {
            this.emailSender = emailSender;
            this.emailTemplatesFolder = HttpContext.Current.Server.MapPath("~/Resources/EmailTemplates");
        }

        public void SendEmailToUserRequestManualTest(Data.DbTest test)
        {
            string customerSupportEmail = GetSystemEmail();
            string emailTemplatePath = Path.Combine(this.emailTemplatesFolder, string.Format("{0}.xml", "ManualTest"));

            ParameterCollection parameters = new ParameterCollection();
            parameters.Add("FromMail", customerSupportEmail);
            parameters.Add("ToMail", test.Email);
            parameters.Add("Name", test.ContactName);
            parameters.Add("Url", test.Domain);

            EmailTemplate emailTemplate = EmailUtility.ComposeTemplate(emailTemplatePath, parameters);
            emailTemplate.Cc = new List<string>() { customerSupportEmail };
            Dispatch(new EmailMessage(emailTemplate));
        }


        public void SendEmailToUserRequestAutomaticTest(Data.DbTest test)
        {
            string customerSupportEmail = GetSystemEmail();
            string emailTemplatePath = Path.Combine(this.emailTemplatesFolder, string.Format("{0}.xml", "AutomaticTest"));

            ParameterCollection parameters = new ParameterCollection();
            parameters.Add("FromMail", customerSupportEmail);
            parameters.Add("ToMail", test.Email);
            parameters.Add("Url", test.Domain);

            EmailTemplate emailTemplate = EmailUtility.ComposeTemplate(emailTemplatePath, parameters);
            emailTemplate.Cc = new List<string>() { customerSupportEmail };
            Dispatch(new EmailMessage(emailTemplate));
        }

        public void GetHelpEmail(string name, string email, string phone, string company, string comments)
        {
            string customerSupportEmail = GetSystemEmail();
            string emailTemplatePath = Path.Combine(this.emailTemplatesFolder, string.Format("{0}.xml", "GetHelp"));

            ParameterCollection parameters = new ParameterCollection();
            parameters.Add("FromMail", customerSupportEmail);
            parameters.Add("ToMail", email);
            parameters.Add("Name", name);

            EmailTemplate emailTemplate = EmailUtility.ComposeTemplate(emailTemplatePath, parameters);
            emailTemplate.Cc = new List<string>() { customerSupportEmail };
            Dispatch(new EmailMessage(emailTemplate));
        }

        private string GetSystemEmail()
        {
            return "info@testumium.com";
        }

        private void Dispatch(EmailMessage message)
        {
            EmailTemplate emailTemplate = new EmailTemplate(
                message.Subject,
                message.From,
                message.To,
                message.Body,
                message.ContentType,
                System.Text.Encoding.UTF8);
            emailTemplate.Attachments = message.Attachments;
            emailSender.Send(emailTemplate, new BypassEmailFormatter(), null);
        }

        private class BypassEmailFormatter : IEmailFormatter
        {
            public EmailTemplate Format(EmailTemplate template, ParameterCollection parameters)
            {
                return template;
            }
        }
    }
}
