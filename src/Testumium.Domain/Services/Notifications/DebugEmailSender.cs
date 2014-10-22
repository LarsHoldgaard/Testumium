using System;
using log4net;
using System.Linq;
using Testumium.Domain.Models.Domain.Notifications;

namespace Testumium.Domain.Services
{
	/// <summary>
	/// Description of DebugEmailSender.
	/// </summary>
	public class DebugEmailSender : IEmailSender
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(DebugEmailSender));

        public void Send(EmailTemplate template, IEmailFormatter formatter, ParameterCollection parameterHash)
        {
            EmailTemplate formattedTemplate = formatter.Format(template, parameterHash);
            logger.DebugFormat(@"
                Send email: {0}
                To: {1},
                Cc: {5}
                From: {2},
                
                {3},
                
                {4}", formattedTemplate.Subject, 
                    formattedTemplate.To, formattedTemplate.From, formattedTemplate.Body,
                    string.Join(", ", template.Attachments.Select(a => a.FileName).ToArray()),
                    template.Cc != null ? string.Join(", ", template.Cc.Select(a => a).ToArray()) : "");
        }
	}
}
