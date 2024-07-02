using EquityAfia.SharedContracts;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquityAfia.EmailsNotifications.Infrastructure.Consumers
{
    public class UserRegisteredConsumer : IConsumer<UserRegistered>
    {
        public async Task Consume(ConsumeContext<UserRegistered> context)
        {
            var message = context.Message;

            // Implement logic to send email notification
            // Example:
            Console.WriteLine($"Sending email to {message.Email}...");

            // Dummy logic, replace with actual email sending code
            await Task.Delay(1000); // Simulate sending email

            Console.WriteLine("Email sent successfully.");
        }
    }
}
