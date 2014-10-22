namespace Testumium.Domain.Models.Domain.Notifications
{
    public interface IEmailSender
    {
        void Send(EmailTemplate template, IEmailFormatter formatter, ParameterCollection parameterCollection);
    }
}
