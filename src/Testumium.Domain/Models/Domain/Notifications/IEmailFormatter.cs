
namespace Testumium.Domain.Models.Domain.Notifications
{
    public interface IEmailFormatter
    {
        EmailTemplate Format(EmailTemplate template, ParameterCollection parameterHash);
    }
}
