using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testumium.Domain.Data;

namespace Testumium.Domain.Models.Domain.Notifications
{
    public interface INotificationService
    {
        void SendEmailToUserRequestManualTest(DbTest test);
        void SendEmailToUserRequestAutomaticTest(Data.DbTest test);
        void GetHelpEmail(string name, string email, string phone, string company, string comments);
    }
}
