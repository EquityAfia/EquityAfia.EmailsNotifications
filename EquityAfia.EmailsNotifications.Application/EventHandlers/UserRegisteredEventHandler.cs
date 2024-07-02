using EquityAfia.EmailsNotifications.Domain.Message;
using EquityAfia.EmailsNotifications.Application.interfaces;
using MassTransit;
using EquityAfia.SharedContracts;

namespace EquityAfia.EmailsNotifications.Application.EventHandlers
{
    public class UserRegisteredEventHandler : IConsumer<UserRegistered>
    {
        private readonly IEmailService _emailService;

        public UserRegisteredEventHandler(IEmailService emailService)
        {
            _emailService = emailService;
        }

        public async Task Consume(ConsumeContext<UserRegistered> context)
        {
            var userRegisteredEvent = context.Message;

            var subject = "Welcome to Teleafia!";
            var body = $"Dear {userRegisteredEvent.FirstName} {userRegisteredEvent.LastName},\n\nWelcome to Teleafia. We are excited to have you on board!\n\nBest regards,\nThe Teleafia Team";

            var message = new Message(new[] { userRegisteredEvent.Email }, subject, body);

             _emailService.SendEmail(message);
        }
    }
}
