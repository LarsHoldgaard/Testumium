using System;
using System.Net.Mail;
using log4net;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Net.Mime;
using Testumium.Domain.Models.Domain.Notifications;

namespace Testumium.Domain.Services
{
    public class SystemNetMailSender : IEmailSender
    {
        private SmtpClient smtpClient;
        private static readonly ILog logger = LogManager.GetLogger(typeof(SystemNetMailSender));

        public SystemNetMailSender(string host, int port)
        {
            smtpClient = new SmtpClient(host, port);
        }

        public SystemNetMailSender(string host, int port, string username, string password)
            : this(host, port)
        {
            smtpClient.Credentials = new System.Net.NetworkCredential(username, password);
        }

        public SystemNetMailSender(string host, int port, string username, string password, bool enableSsl)
            : this(host, port)
        {
            logger.DebugFormat("{0}/{1}", username, password);
            smtpClient.Credentials = new System.Net.NetworkCredential(username, password);
            smtpClient.EnableSsl = enableSsl;
        }

        public SystemNetMailSender(string host, int port, string username, string password, string domain)
            : this(host, port)
        {
            smtpClient.Credentials = new System.Net.NetworkCredential(username, password, domain);
        }

        public SystemNetMailSender(string host, int port, string username, string password, string domain, bool enableSsl)
            : this(host, port)
        {
            smtpClient.Credentials = new System.Net.NetworkCredential(username, password, domain);
            smtpClient.EnableSsl = enableSsl;
        }

        #region IEmailSender Members

        public virtual void Send(EmailTemplate template, IEmailFormatter formatter, ParameterCollection parameterHash)
        {
            EmailTemplate formattedEmailTemplate = formatter.Format(template, parameterHash);
            MailMessage mailMessage = new MailMessage(formattedEmailTemplate.From, formattedEmailTemplate.To,
                formattedEmailTemplate.Subject, formattedEmailTemplate.Body);

            mailMessage.IsBodyHtml = !String.IsNullOrEmpty(formattedEmailTemplate.BodyContentType) && formattedEmailTemplate.BodyContentType.Equals("text/html");
            if (template.Cc != null && template.Cc.Count > 0)
            {
                foreach (var address in template.Cc)
                {
                    mailMessage.CC.Add(new MailAddress(address));
                }
            }

            if (template.Attachments != null && template.Attachments.Count > 0)
            {
                foreach (var item in template.Attachments)
                {
                    var att = new Attachment(item.FileName);
                    att.Name = item.Name;
                    mailMessage.Attachments.Add(att);
                }
            }
            
            try
            {
                smtpClient.Send(mailMessage);
            }
            catch (Exception ex)
            {
                logger.Error("Error on sending mail: " + ex);
                throw new EmailSenderException("System cannot send email to " + formattedEmailTemplate.To);
            }
        }

        #endregion
    }
}
