using System;

namespace Testumium.Domain.Models.Domain.Notifications
{
	/// <summary>
	/// Description of EmailSenderException.
	/// </summary>
	public class EmailSenderException : Exception
	{
		public EmailSenderException()
		{
		}
		
		public EmailSenderException(string message) : base(message)
		{
		}
	}
}
