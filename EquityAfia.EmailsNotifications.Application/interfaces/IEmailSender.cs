
using EquityAfia.EmailsNotifications.Domain.Message;

namespace EquityAfia.EmailsNotifications.Application.interfaces
{
    public interface IEmailService
    {
        void SendEmail(Message message);
    }
}
