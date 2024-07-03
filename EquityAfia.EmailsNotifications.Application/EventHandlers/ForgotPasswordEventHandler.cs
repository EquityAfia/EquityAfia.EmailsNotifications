using EquityAfia.EmailsNotifications.Domain.Message;
using EquityAfia.EmailsNotifications.Application.interfaces;
using MassTransit;
using EquityAfia.SharedContracts;

namespace EquityAfia.EmailsNotifications.Application.EventHandlers
{
    public class ForgotPasswordEventHandler : IConsumer<UserForgotPassword>
    {
        private readonly IEmailService _emailService;

        public ForgotPasswordEventHandler(IEmailService emailService)
        {
            _emailService = emailService;
        }

        public async Task Consume(ConsumeContext<UserForgotPassword> context)
        {
            var userForgotPasswordEvent = context.Message;

            var subject = "Forgot Password Token";
            var body = $"Dear {userForgotPasswordEvent.FirstName} {userForgotPasswordEvent.LastName},\n\nYour password reset token is {userForgotPasswordEvent.Token} and expires in 5 minutes\n\nBest regards,\nThe Teleafia Team";

            var message = new Message(new[] { userForgotPasswordEvent.Email }, subject, body);

            _emailService.SendEmail(message);
        }
    }
}
