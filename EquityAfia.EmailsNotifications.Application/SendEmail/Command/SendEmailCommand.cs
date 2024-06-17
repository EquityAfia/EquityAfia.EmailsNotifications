using EquityAfia.EmailsNotifications.Domain.EmailRequestAndResponse;
using MediatR;

namespace EquityAfia.EmailsNotifications.Application.SendEmail.Command
{
    public class SendEmailCommand : IRequest<SendEmailResponse>
    {
        public SendEmailRequest SendEmailRequest { get; set; }

        public SendEmailCommand(SendEmailRequest request)
        {
            SendEmailRequest = request;
        }
    }
}
