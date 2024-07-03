using EquityAfia.EmailsNotifications.Application.interfaces;
using EquityAfia.EmailsNotifications.Application.SendEmail.Command;
using EquityAfia.EmailsNotifications.Domain.EmailRequestAndResponse;
using EquityAfia.EmailsNotifications.Infrastructure.Consumers;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using TeleAfiaPersonal.Contracts.Email;
using TeleAfiaPersonal.Infrastructure.EmailSender;
using static Org.BouncyCastle.Bcpg.Attr.ImageAttrib;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

// Email Configuration
var emailConfig = configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>();

// Add MediatR for handling commands and queries
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

// Register command and query handlers
builder.Services.AddTransient<IRequestHandler<SendEmailCommand, SendEmailResponse>, SendEmailCommandHandler>();


//Services
builder.Services.AddSingleton(emailConfig);
builder.Services.AddScoped<IEmailService, EmailService>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("localhost", "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });

        //Message to be displayed as queue name
        cfg.ReceiveEndpoint("user-registered-queue", e =>
        {
            e.ConfigureConsumer<UserRegisteredConsumer>(context);
        });

        cfg.ReceiveEndpoint("user-forgot-password-queue", e =>
        {
            e.ConfigureConsumer<UserForgotPasswordConsumer>(context);
        });

    });

    x.AddConsumer<UserRegisteredConsumer>();
    x.AddConsumer<UserForgotPasswordConsumer>();
});

builder.Services.AddMassTransitHostedService();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
