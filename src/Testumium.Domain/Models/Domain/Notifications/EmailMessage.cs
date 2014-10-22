using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testumium.Domain.Models.Domain.Notifications
{
    public class EmailMessage
    {
        public IList<EmailAttachment> Attachments { get; set; }
        public EmailMessage()
        {
            this.Attachments = new List<EmailAttachment>();
        }

        public EmailMessage(EmailTemplate template)
            : this()
        {
            this.Subject = template.Subject;
            this.From = template.From;
            this.To = template.To;
            this.Cc = template.Cc;
            this.Body = template.Body;
            this.ContentType = "text/html";
        }

        public EmailMessage(EmailTemplate template, IList<EmailAttachment> attachments)
            : this(template)
        {
            this.Attachments = attachments;
        }

        public virtual int Id { get; set; }
        public virtual string From { get; set; }
        public virtual string To { get; set; }
        public virtual List<string> Cc { get; set; }
        public virtual string Subject { get; set; }
        public virtual string ContentType { get; set; }
        public virtual string Body { get; set; }

        public virtual string Type
        {
            get { return "Email"; }
        }

        public virtual bool Dispatched { get; set; }
    }
}
