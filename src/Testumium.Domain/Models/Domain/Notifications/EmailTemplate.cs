using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Testumium.Domain.Models.Domain.Notifications
{
    public class EmailTemplate
    {
        public IList<EmailAttachment> Attachments { get; set; }
        #region Fields
        protected string subject;
        protected string from;
        protected string to;
        protected string body;
        protected Encoding bodyEncoding;
        protected string bodyContentType;
        #endregion Fields

        #region Constructors
        public EmailTemplate()
        {
            this.Attachments = new List<EmailAttachment>();
        }

        public EmailTemplate(string subject, string from, string to, string body, string bodyContentType, Encoding bodyEncoding)
            : this()
        {
            this.subject = subject;
            this.from = from;
            this.to = to;
            this.body = body;
            this.bodyEncoding = bodyEncoding;
            this.bodyContentType = bodyContentType;
        }

        public EmailTemplate(string subject, string from, string to, List<string> cc, string body, string bodyContentType, Encoding bodyEncoding)
            : this(subject, from, to, body, bodyContentType, bodyEncoding)
        {
            this.Cc = cc;
        }

        #endregion Constructors

        #region Properties
        public string Subject
        {
            get { return subject; }
        }
        public string From
        {
            get { return from; }
        }
        public string To
        {
            get { return to; }
        }

        public List<string> Cc { get; set; }

        public string Body
        {
            get { return body; }
        }
        public string BodyContentType
        {
            get { return bodyContentType; }
        }
        public Encoding BodyEncoding
        {
            get { return bodyEncoding; }
        }
        #endregion Properties

        public override string ToString()
        {
            return String.Format(@"
                From: {0}
                To: {1}
                Subject: {2}
                Body Content Type: {3}
                Body Encoding : {4}    
                Body: {5}", From, To, Subject, BodyContentType, BodyEncoding, Body);
        }
    }

    public class EmailAttachment
    {
        public string Name { get; set; }
        public string FileName { get; set; }
        protected Stream inputStream;

        public virtual Stream Stream
        {
            get
            {
                if (this.inputStream != null) return this.inputStream;

                if (!string.IsNullOrEmpty(this.FileName))
                {
                    try
                    {
                        this.inputStream = new BufferedStream(new FileStream(this.FileName, FileMode.Open, FileAccess.Read, FileShare.Read), 4096);
                    }
                    catch (Exception)
                    {
                        throw new Exception("Cannot load resource from: " + this.FileName);
                    }
                }

                return inputStream;
            }
            set
            {
                this.inputStream = value;
            }
        }
    }
}
